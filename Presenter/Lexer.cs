using CompilerV2.Model;
using CompilerV2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerV2.Presenter
{
    public class Lexer
    {
        private string text;
        private int position;
        private List<Lexem> lexemsList;
        private ResourceManager _res;

        public Lexer(string textToScan, ResourceManager res)
        {
            _res = res ?? throw new ArgumentNullException("Не найден файл локализации для выбранного языка!");
            text = textToScan;
            position = 0;
            lexemsList = new List<Lexem>();
        }

        public List<Lexem> Scan()
        {
            while (position < text.Length)
            { 
                char currentChar = text[position];

                if ((currentChar >= 'a') && (currentChar <= 'z') || ((currentChar >= 'A') && (currentChar <= 'Z') || ((currentChar >= '0') && (currentChar <= '9')) || currentChar == '-' || currentChar == '+' || currentChar == '_') || ((currentChar >= 'а') && (currentChar <= 'я')) || ((currentChar >= 'А') && (currentChar <= 'Я')))
                {
                    ProcessIdentifierOrNumber();
                }
                else
                {
                    if (ProcessSymbol()) break;
                }
            }
            return lexemsList;
        }

        private void ProcessIdentifierOrNumber()
        {
            int start = position;
            bool isDouble = false; 
            bool isScientific = false;
            bool startsWithDigit = false;
            if (text[start] == '+' || text[start] == '-')
            {
                startsWithDigit = start + 1 < text.Length && char.IsDigit(text[start + 1]);
            }
            else
            {
                startsWithDigit = char.IsDigit(text[start]);
            }
            bool hasLetter = false;
            bool hasDot = false;
            bool isAcceptable = true;
            bool isInt = true;
            bool isError = false;

            while (position < text.Length && (char.IsLetterOrDigit(text[position]) ||
                text[position] == '.' || text[position] == '+' || text[position] == '-' || text[position] == '_'))
            {
                if (text[position] == '.')
                {
                    if (!hasDot && !hasLetter)
                    {
                        hasDot = true;
                        isDouble = true;
                        isInt = false;
                    }
                    else if (!hasLetter)
                    {
                        isAcceptable = false;
                        isDouble = false;
                        isInt = false;
                    }
                    else
                    {
                        string lexeme_break_end = text.Substring(start, (position) - start);
                        lexemsList.Add(new Lexem(1,lexeme_break_end,start,position, _res));
                        return;
                    }
                }
                else if (text[position] == '+' || text[position] == '-')
                {
                    position++;
                    continue;
                }
                else if (((text[position] >= 'a') && (text[position] <= 'z') || ((text[position] >= 'A') && (text[position] <= 'Z')) && !(((text[position] >= 'а') && (text[position] <= 'я')) || ((text[position] >= 'А') && (text[position] <= 'Я')))))
                {
                    bool previousIsDigit = position > start && char.IsDigit(text[position - 1]);
                    if ((text[position] == 'e' || text[position] == 'E') && previousIsDigit)
                    {
                        if (!isScientific)
                        {
                            isScientific = true;
                            isInt = false;
                        }
                        else
                        {
                            isScientific = false;
                            isDouble = false;
                            hasLetter = true;
                            isInt = false;
                        }
                    }
                    else
                    {
                        isDouble = false;
                        isScientific = false;
                        hasLetter = true;
                        isInt = false;
                    }
                }
                else if (text[position] == '_')
                {
                    hasLetter = true;
                    isDouble = false;
                    isScientific = false;
                    isInt = false;
                }
                else if (char.IsDigit(text[position]))
                {
                    position++;
                    continue;
                }
                else if (char.IsWhiteSpace(text[position]))
                {
                    break;
                }
                else
                {
                    isAcceptable = false;
                }
                position++;
            }

            // Если внутри "словесной" лексемы встретился недопустимый символ (например for#mat),
            // поглощаем хвост до границы токена и считаем всю лексему ошибочной.
            if (position < text.Length && !IsTokenBoundary(text[position]))
            {
                isAcceptable = false;
                hasLetter = true;
                isInt = false;
                isDouble = false;
                isScientific = false;

                while (position < text.Length && !IsTokenBoundary(text[position]))
                {
                    position++;
                }
            }
            
            string lexeme = text.Substring(start, position - start);
            int code;
            if (isError)
            {
                lexemsList.Add(new Lexem(19, lexeme, start, position - start, _res));
                return;
            }
            if (isScientific && isAcceptable && startsWithDigit)
            {
                if (lexeme.Contains('-') | lexeme.Contains('+'))
                {
                    if (isDouble)
                    {
                        code = lexeme.Contains('-') ? 14 : 15;
                        lexemsList.Add(new Lexem(code, lexeme, start, position, _res));
                        return;
                    }
                    else
                    {
                        code = lexeme.Contains('-') ? 10 : 11;
                        lexemsList.Add(new Lexem(code, lexeme, start, position, _res));
                        return;
                    }
                }
                else
                {
                    lexemsList.Add(new Lexem(19, lexeme, start, position, _res));
                    return;
                }
            }
            else if (isInt && isAcceptable && startsWithDigit)
            {
                lexemsList.Add(new Lexem(12, lexeme,start, position, _res));
            }
            else if (isDouble && isAcceptable && startsWithDigit)
            {
                lexemsList.Add(new Lexem(13, lexeme, start, position, _res));
                return;
            }
            else if (startsWithDigit && !isAcceptable)
            {
                lexemsList.Add(new Lexem(19, lexeme, start, position, _res));
                return;
            }
            else if (hasLetter && !hasDot && isAcceptable && !startsWithDigit)
            {
                if (lexeme == "format")
                {
                    lexemsList.Add(new Lexem(2,lexeme,start, position, _res));
                    return;
                }
                else
                {
                    lexemsList.Add(new Lexem(1, lexeme,start, position, _res));
                    return;
                }
            }
            else
            {
                lexemsList.Add(new Lexem(19, lexeme, start, position, _res));
            }
        }

        private bool ProcessSymbol()
        {
            int start = position;
            char currentChar = text[position++];
            int code;
            switch (currentChar)
            {
                case '=': code = 4; break;
                case '"': ProcessString(); return false;
                case '-': code = 7; break;
                case '+': code = 8; break;
                case '.': code = 9; break;
                case '(': code = 16; break;
                case ')': code = 17; break;
                case ';': code = 18; 
                    if (position >= text.Length)
                    {
                        lexemsList.Add(new Lexem(code, currentChar.ToString(), start, position, _res));
                        return true;
                    }
                    else
                    {
                        break;
                    }
                default:
                    if (char.IsWhiteSpace(currentChar))
                    {
                        return false;
                    }
                    code = 19;
                    break;
            }
            lexemsList.Add(new Lexem(code, currentChar.ToString(), start, position, _res));
            return false;
        }

        private void ProcessString()
        {
            int start = position - 1;
            bool hasClosingQuote = false;

            while (position < text.Length)
            {
                char current = text[position];
                if (current == '"')
                {
                    position++;
                    hasClosingQuote = true;
                    break;
                }

                if (current == '\n' || current == '\r')
                {
                    break;
                }

                position++;
            }

            if (!hasClosingQuote)
            {
                int recoveryPosition = FindUnclosedStringRecoveryPosition(start, position);
                string brokenLexeme = text.Substring(start, recoveryPosition - start);
                lexemsList.Add(new Lexem(19, brokenLexeme, start, recoveryPosition, _res));
                position = recoveryPosition;
                return;
            }

            int end = position;
            string lexeme = text.Substring(start, end - start);

            if (string.Equals(lexeme, "\"{:f}\"", StringComparison.Ordinal))
            {
                lexemsList.Add(new Lexem(6, lexeme, start, end, _res));
                return;
            }

            // Любая другая строка в кавычках в данной грамматике считается ошибочной форматной строкой.
            lexemsList.Add(new Lexem(19, lexeme, start, end, _res));
        }

        private int FindUnclosedStringRecoveryPosition(int start, int end)
        {
            for (int i = start + 1; i < end; i++)
            {
                if (text[i] == '}')
                {
                    return i + 1;
                }
            }

            for (int i = start + 1; i < end; i++)
            {
                if (text[i] == ';' || text[i] == '\n' || text[i] == '\r')
                {
                    return i;
                }
            }

            return end;
        }

        private bool IsTokenBoundary(char c)
        {
            if (char.IsWhiteSpace(c))
            {
                return true;
            }

            switch (c)
            {
                case '=':
                case '"':
                case '(':
                case ')':
                case ';':
                    return true;
                default:
                    return false;
            }
        }
    }
}
