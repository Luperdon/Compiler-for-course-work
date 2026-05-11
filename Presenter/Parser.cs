using System;
using System.Collections.Generic;
using System.Linq;
using CompilerV2.Model;

namespace CompilerV2.Presenter
{
    public class Parser
    {
        private enum ExpectedToken
        {
            Identifier,
            Assignment,
            FString,
            Dot,
            Format,
            OpenParen,
            ScientificNumber,
            CloseParen,
            Semicolon
        }

        private readonly List<Lexem> lexems;
        private readonly HashSet<int> erroredLexemStarts;
        private readonly string sourceText;
        public List<ErrorPair> stateLog;

        public Parser(List<Lexem> lexems, string sourceText)
        {
            this.lexems = lexems;
            this.erroredLexemStarts = new HashSet<int>();
            this.sourceText = sourceText ?? string.Empty;
            this.stateLog = new List<ErrorPair>();
        }

        public void Parse()
        {
            int position = 0;

            while (position < lexems.Count)
            {
                bool statementFailed = false;
                bool skipFormatExpectationOnce = false;
                ExpectedToken[] sequence = new[]
                {
                    ExpectedToken.Identifier,
                    ExpectedToken.Assignment,
                    ExpectedToken.FString,
                    ExpectedToken.Dot,
                    ExpectedToken.Format,
                    ExpectedToken.OpenParen,
                    ExpectedToken.ScientificNumber,
                    ExpectedToken.CloseParen,
                    ExpectedToken.Semicolon
                };

                for (int expectedIndex = 0; expectedIndex < sequence.Length; expectedIndex++)
                {
                    ExpectedToken expected = sequence[expectedIndex];
                    if (skipFormatExpectationOnce && expected == ExpectedToken.Format)
                    {
                        skipFormatExpectationOnce = false;
                        continue;
                    }

                    if (position >= lexems.Count)
                    {
                        // До «=» включительно — одна ошибка (короткий ввод вроде одного идентификатора).
                        // После начала правой части — фиксируем все недостающие хвостовые лексемы (')', ';' и т.д.).
                        if (expectedIndex <= (int)ExpectedToken.Assignment)
                        {
                            AddMissingTokenError(sequence[expectedIndex]);
                        }
                        else
                        {
                            for (int ei = expectedIndex; ei < sequence.Length; ei++)
                            {
                                AddMissingTokenError(sequence[ei]);
                            }
                        }

                        statementFailed = true;
                        break;
                    }

                    Lexem currentLexem = lexems[position];

                    if (currentLexem.lexemCode == 19)
                    {
                        ConsumeInvalidLexemeSequence(ref position, expected, ref skipFormatExpectationOnce);
                        statementFailed = true;

                        // После пропуска мусорной последовательности пробуем снова проверить
                        // ту же ожидаемую лексему, чтобы не сдвигать всю цепочку ожиданий.
                        if (position < lexems.Count && IsExpected(lexems[position], expected))
                        {
                            expectedIndex--;
                            continue;
                        }

                        RecoverToStatementBoundary(ref position);
                        break;
                    }

                    if (!IsExpected(currentLexem, expected))
                    {
                        if (MatchesAnyFollowingExpected(currentLexem, sequence, expectedIndex + 1, expectedIndex))
                        {
                            AddMissingTokenError(expected, currentLexem.lexemStartPosition);
                            statementFailed = true;
                            continue;
                        }

                        if (expected == ExpectedToken.Format && IsFormatLikeIdentifier(currentLexem))
                        {
                            position++;
                            continue;
                        }
                        AddErrorOnce(currentLexem, $"Ожидалась лексема: {GetExpectedText(expected)}, но получено '{currentLexem.lexemContaintment}'.");
                        position++;
                        statementFailed = true;
                        continue;
                    }

                    ValidateSpelling(currentLexem, expected);
                    position++;
                }

                if (!statementFailed)
                {
                    continue;
                }

                if (position < lexems.Count && lexems[position].lexemCode == 18)
                {
                    position++;
                }
            }
        }

        private void ConsumeInvalidLexemeSequence(ref int position, ExpectedToken expected, ref bool skipFormatExpectationOnce)
        {
            if (position >= lexems.Count || lexems[position].lexemCode != 19)
            {
                return;
            }

            int first = position;
            int last = position;

            while (position < lexems.Count && lexems[position].lexemCode == 19)
            {
                last = position;
                position++;
            }

            Lexem firstLexem = lexems[first];
            Lexem lastLexem = lexems[last];

            if (expected == ExpectedToken.Dot && IsFormatLikeToken(firstLexem))
            {
                AddErrorOnce(firstLexem, $"Ожидалась лексема: {GetExpectedText(ExpectedToken.Format)}, но получено '{firstLexem.lexemContaintment}'.");
                skipFormatExpectationOnce = true;
                return;
            }

            if (expected == ExpectedToken.FString && IsMalformedFormatSpecifierToken(firstLexem))
            {
                int line = GetLineByAbsolutePosition(firstLexem.lexemStartPosition);
                ErrorState(null, firstLexem.lexemStartPosition, lastLexem.lexemEndPosition, AppendLineInfo("Форматный спецификатор должен быть ровно \"{:f}\".", line));
                return;
            }

            string invalidSequence = string.Concat(lexems
                .Skip(first)
                .Take(last - first + 1)
                .Select(l => l.lexemContaintment));

            int invalidLine = GetLineByAbsolutePosition(firstLexem.lexemStartPosition);
            ErrorState(null, firstLexem.lexemStartPosition, lastLexem.lexemEndPosition, AppendLineInfo($"Некорректная лексема: '{invalidSequence}'.", invalidLine));
        }

        private bool MatchesAnyFollowingExpected(Lexem lexem, ExpectedToken[] sequence, int startIndex, int mismatchAtExpectedIndex)
        {
            for (int i = startIndex; i < sequence.Length; i++)
            {
                // Завершающая ';' не считается «якорем» в середине шаблона (например float_format; = ...).
                if (sequence[i] == ExpectedToken.Semicolon)
                {
                    continue;
                }

                // Точка перед format не может означать «пропущены '=' и строка», пока мы ещё ждём левую часть
                // присваивания (идентификатор или '=') — иначе «.» внутри идентификатора даёт каскад «не найдено».
                if (sequence[i] == ExpectedToken.Dot
                    && mismatchAtExpectedIndex < (int)ExpectedToken.FString)
                {
                    continue;
                }

                if (IsExpected(lexem, sequence[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool RecoverToStatementBoundary(ref int position)
        {
            while (position < lexems.Count && lexems[position].lexemCode != 18)
            {
                position++;
            }

            if (position < lexems.Count && lexems[position].lexemCode == 18)
            {
                position++;
                return true;
            }

            return false;
        }

        private bool IsExpected(Lexem lexem, ExpectedToken expected)
        {
            switch (expected)
            {
                case ExpectedToken.Identifier:
                    return lexem.lexemCode == 1;
                case ExpectedToken.Assignment:
                    return lexem.lexemCode == 4;
                case ExpectedToken.FString:
                    return lexem.lexemCode == 6;
                case ExpectedToken.Dot:
                    return lexem.lexemCode == 9;
                case ExpectedToken.Format:
                    return lexem.lexemCode == 2;
                case ExpectedToken.OpenParen:
                    return lexem.lexemCode == 16;
                case ExpectedToken.ScientificNumber:
                    return lexem.lexemCode == 10 || lexem.lexemCode == 11 || lexem.lexemCode == 14 || lexem.lexemCode == 15;
                case ExpectedToken.CloseParen:
                    return lexem.lexemCode == 17;
                case ExpectedToken.Semicolon:
                    return lexem.lexemCode == 18;
                default:
                    return false;
            }
        }

        private bool IsFormatLikeIdentifier(Lexem lexem)
        {
            if (lexem == null || lexem.lexemCode != 1 || string.IsNullOrEmpty(lexem.lexemContaintment))
            {
                return false;
            }

            return lexem.lexemContaintment.StartsWith("format", StringComparison.Ordinal);
        }

        private bool IsFormatLikeToken(Lexem lexem)
        {
            if (lexem == null || string.IsNullOrEmpty(lexem.lexemContaintment))
            {
                return false;
            }

            return lexem.lexemContaintment.StartsWith("form", StringComparison.Ordinal)
                || lexem.lexemContaintment.Contains("format");
        }

        private bool IsMalformedFormatSpecifierToken(Lexem lexem)
        {
            if (lexem == null || string.IsNullOrEmpty(lexem.lexemContaintment))
            {
                return false;
            }

            string value = lexem.lexemContaintment;
            return value.StartsWith("\"", StringComparison.Ordinal)
                || value.Contains("{")
                || value.Contains("}")
                || value.Contains(":");
        }

        private void ValidateSpelling(Lexem lexem, ExpectedToken expected)
        {
            switch (expected)
            {
                case ExpectedToken.Identifier:
                    ValidateIdentifier(lexem);
                    break;
                case ExpectedToken.FString:
                    if (!lexem.lexemContaintment.Contains("{:f}"))
                    {
                        AddErrorOnce(lexem, "Форматная строка должна содержать спецификатор '{:f}'.");
                    }
                    break;
                case ExpectedToken.Format:
                    if (!string.Equals(lexem.lexemContaintment, "format", StringComparison.Ordinal))
                    {
                        AddErrorOnce(lexem, "Ожидалось ключевое слово 'format'.");
                    }
                    break;
                case ExpectedToken.ScientificNumber:
                    if (!IsScientificNumber(lexem.lexemContaintment))
                    {
                        AddErrorOnce(lexem, "Число должно быть в экспоненциальной форме, например 3.14e+4.");
                    }
                    break;
            }
        }

        private void ValidateIdentifier(Lexem lexem)
        {
            string value = lexem.lexemContaintment;
            if (string.IsNullOrWhiteSpace(value))
            {
                AddErrorOnce(lexem, "Идентификатор не может быть пустым.");
                return;
            }

            if (char.IsDigit(value[0]))
            {
                AddErrorOnce(lexem, "Идентификатор не может начинаться с цифры.");
                return;
            }

            foreach (char c in value)
            {
                bool isCyrillic = (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я');
                if (isCyrillic)
                {
                    AddErrorOnce(lexem, "Идентификатор не может содержать кириллицу.");
                    return;
                }

                bool isValid = char.IsLetterOrDigit(c) || c == '_';
                if (!isValid)
                {
                    AddErrorOnce(lexem, $"Недопустимый символ в идентификаторе: '{c}'.");
                    return;
                }
            }
        }

        private bool IsScientificNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            int expIndex = value.IndexOf('e');
            if (expIndex < 0)
            {
                expIndex = value.IndexOf('E');
            }

            if (expIndex <= 0 || expIndex >= value.Length - 2)
            {
                return false;
            }

            string mantissa = value.Substring(0, expIndex);
            string exponent = value.Substring(expIndex + 1);
            if (exponent[0] != '+' && exponent[0] != '-')
            {
                return false;
            }

            string expDigits = exponent.Substring(1);
            if (expDigits.Length == 0 || !expDigits.All(char.IsDigit))
            {
                return false;
            }

            int dotCount = mantissa.Count(c => c == '.');
            if (dotCount > 1)
            {
                return false;
            }

            string mantissaDigits = mantissa.Replace(".", string.Empty);
            if (mantissaDigits.Length == 0 || !mantissaDigits.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }

        private string GetExpectedText(ExpectedToken expected)
        {
            switch (expected)
            {
                case ExpectedToken.Identifier: return "идентификатор";
                case ExpectedToken.Assignment: return "'='";
                case ExpectedToken.FString: return "форматная строка '\"{:f}\"'";
                case ExpectedToken.Dot: return "'.'";
                case ExpectedToken.Format: return "ключевое слово 'format'";
                case ExpectedToken.OpenParen: return "'('";
                case ExpectedToken.ScientificNumber: return "число в экспоненциальной форме";
                case ExpectedToken.CloseParen: return "')'";
                case ExpectedToken.Semicolon: return "';'";
                default: return "лексема";
            }
        }

        private void AddErrorOnce(Lexem lexem, string message)
        {
            if (lexem == null)
            {
                ErrorState(null, 0, 0, AppendLineInfo(message, GetLineByAbsolutePosition(sourceText.Length)));
                return;
            }

            if (erroredLexemStarts.Contains(lexem.lexemStartPosition))
            {
                return;
            }

            erroredLexemStarts.Add(lexem.lexemStartPosition);
            int line = GetLineByAbsolutePosition(lexem.lexemStartPosition);
            ErrorState(lexem, lexem.lexemStartPosition, lexem.lexemEndPosition, AppendLineInfo(message, line));
        }

        private void AddMissingTokenError(ExpectedToken expected, int? absolutePosition = null)
        {
            int pos = absolutePosition ?? sourceText.Length;
            int line = GetLineByAbsolutePosition(pos);
            string message = $"Ожидалась лексема: {GetExpectedText(expected)}, но лексемы не найдено.";
            ErrorState(null, pos, pos, AppendLineInfo(message, line));
        }

        private string AppendLineInfo(string message, int line)
        {
            return $"{message} (строка {line})";
        }

        private int GetLineByAbsolutePosition(int absolutePosition)
        {
            if (string.IsNullOrEmpty(sourceText))
            {
                return 1;
            }

            int boundedPosition = Math.Max(0, Math.Min(absolutePosition, sourceText.Length));
            int line = 1;
            for (int i = 0; i < boundedPosition; i++)
            {
                if (sourceText[i] == '\n')
                {
                    line++;
                }
            }

            return line;
        }

        private bool ErrorState(Lexem lexem, string message)
        {
            stateLog.Add(new ErrorPair(message, lexem));
            return false;
        }
        private void ErrorState(Lexem lexem, int posStart, int posEnd, string message)
        {
            stateLog.Add(new ErrorPair(message, lexem, posStart, posEnd));
        }

        public void PrintLog()
        {
            foreach (var entry in stateLog)
            {
                Console.WriteLine(entry);
            }
        }
    }

    public class ErrorPair
    {
        public Lexem errorLexem;
        public string errorMessage;
        public int posStart;
        public int posEnd;
        public ErrorPair(string message, Lexem lexem)
        {
            if (lexem != null)
            {
                errorLexem = lexem;
            }
            errorMessage = message;
        }
        public ErrorPair(string message, Lexem lexem, int start, int end)
        {
            if (lexem != null)
            {
                errorLexem = lexem;
            }
            errorLexem = lexem;
            errorMessage = message;
            posStart = start;
            posEnd = end;
        }
    }
}