using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerV2.Presenter
{
    public enum PolizTokenType
    {
        Number,
        Identifier,
        Plus,
        Minus,
        Mul,
        Div,
        IntDiv,
        Mod,
        Pow,
        LParen,
        RParen,
        End
    }

    public sealed class PolizToken
    {
        public PolizTokenType Type { get; }
        public string Lexeme { get; }
        public int Start { get; }
        public int End { get; }

        public PolizToken(PolizTokenType type, string lexeme, int start, int end)
        {
            Type = type;
            Lexeme = lexeme ?? "";
            Start = start;
            End = end;
        }

        public override string ToString() => $"{Type}: {Lexeme} [{Start},{End})";
    }

    public sealed class PolizLexScanResult
    {
        public List<PolizToken> Tokens { get; } = new List<PolizToken>();
        public List<string> Errors { get; } = new List<string>();
    }

    /// <summary>Лексический анализ выражения: числа, идентификаторы, операторы, скобки.</summary>
    public static class PolizLexer
    {
        public static PolizLexScanResult Scan(string text)
        {
            var result = new PolizLexScanResult();
            if (text == null)
            {
                text = "";
            }

            int i = 0;
            while (i < text.Length)
            {
                char c = text[i];
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                int start = i;

                if (char.IsDigit(c))
                {
                    result.Tokens.Add(ScanNumber(text, ref i));
                    continue;
                }

                if (IsLetter(c))
                {
                    result.Tokens.Add(ScanIdentifier(text, ref i));
                    continue;
                }

                if (c == '(')
                {
                    result.Tokens.Add(new PolizToken(PolizTokenType.LParen, "(", start, ++i));
                    continue;
                }

                if (c == ')')
                {
                    result.Tokens.Add(new PolizToken(PolizTokenType.RParen, ")", start, ++i));
                    continue;
                }

                if (c == '+')
                {
                    result.Tokens.Add(new PolizToken(PolizTokenType.Plus, "+", start, ++i));
                    continue;
                }

                if (c == '-')
                {
                    result.Tokens.Add(new PolizToken(PolizTokenType.Minus, "-", start, ++i));
                    continue;
                }

                if (c == '%')
                {
                    result.Tokens.Add(new PolizToken(PolizTokenType.Mod, "%", start, ++i));
                    continue;
                }

                if (c == '*')
                {
                    if (i + 1 < text.Length && text[i + 1] == '*')
                    {
                        result.Tokens.Add(new PolizToken(PolizTokenType.Pow, "**", start, i + 2));
                        i += 2;
                    }
                    else
                    {
                        result.Tokens.Add(new PolizToken(PolizTokenType.Mul, "*", start, ++i));
                    }
                    continue;
                }

                if (c == '/')
                {
                    if (i + 1 < text.Length && text[i + 1] == '/')
                    {
                        result.Tokens.Add(new PolizToken(PolizTokenType.IntDiv, "//", start, i + 2));
                        i += 2;
                    }
                    else
                    {
                        result.Tokens.Add(new PolizToken(PolizTokenType.Div, "/", start, ++i));
                    }
                    continue;
                }

                result.Errors.Add($"Лексическая ошибка: недопустимый символ '{c}' на позиции {start + 1}.");
                i++;
            }

            result.Tokens.Add(new PolizToken(PolizTokenType.End, "", text.Length, text.Length));
            return result;
        }

        private static bool IsLetter(char c) => char.IsLetter(c);

        private static PolizToken ScanNumber(string text, ref int i)
        {
            int start = i;
            while (i < text.Length && char.IsDigit(text[i]))
            {
                i++;
            }

            return new PolizToken(PolizTokenType.Number, text.Substring(start, i - start), start, i);
        }

        /// <summary>id → letter {letter | digit | _}</summary>
        private static PolizToken ScanIdentifier(string text, ref int i)
        {
            int start = i;
            i++;
            while (i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'))
            {
                i++;
            }

            return new PolizToken(PolizTokenType.Identifier, text.Substring(start, i - start), start, i);
        }

        public static string TokenKindRu(PolizTokenType t)
        {
            switch (t)
            {
                case PolizTokenType.Number: return "число";
                case PolizTokenType.Identifier: return "идентификатор";
                case PolizTokenType.Plus: return "+";
                case PolizTokenType.Minus: return "-";
                case PolizTokenType.Mul: return "*";
                case PolizTokenType.Div: return "/";
                case PolizTokenType.IntDiv: return "//";
                case PolizTokenType.Mod: return "%";
                case PolizTokenType.Pow: return "**";
                case PolizTokenType.LParen: return "(";
                case PolizTokenType.RParen: return ")";
                case PolizTokenType.End: return "конец";
                default: return t.ToString();
            }
        }
    }
}
