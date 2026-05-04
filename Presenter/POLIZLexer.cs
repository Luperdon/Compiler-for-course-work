using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerV2.Presenter
{
    public class POLIZLexer
    {
        public enum TokenType
        {
            Number,
            Plus,
            Minus,
            Multiply,
            Divide,
            LParen,
            RParen,
            Unknown
        }

        public class Token
        {
            public TokenType Type { get; }
            public string Value { get; }

            public Token(TokenType type, string value)
            {
                Type = type;
                Value = value;
            }

            public override string ToString()
            {
                return $"{Type}: {Value}";
            }
        }

        public class POLIZScan
        {
            private string _text;
            private int _position;
            private List<Token> _tokens;

            public POLIZScan(string text)
            {
                _text = text;
                _position = 0;
                _tokens = new List<Token>();
            }

            public List<Token> Scan()
            {
                while (_position < _text.Length)
                {
                    char currentChar = _text[_position];

                    if (char.IsWhiteSpace(currentChar))
                    {
                        _position++;
                    }
                    else if (char.IsDigit(currentChar))
                    {
                        _tokens.Add(ScanNumber());
                    }
                    else
                    {
                        switch (currentChar)
                        {
                            case '+':
                                _tokens.Add(new Token(TokenType.Plus, currentChar.ToString()));
                                _position++;
                                break;
                            case '-':
                                _tokens.Add(new Token(TokenType.Minus, currentChar.ToString()));
                                _position++;
                                break;
                            case '*':
                                _tokens.Add(new Token(TokenType.Multiply, currentChar.ToString()));
                                _position++;
                                break;
                            case '/':
                                _tokens.Add(new Token(TokenType.Divide, currentChar.ToString()));
                                _position++;
                                break;
                            case '(':
                                _tokens.Add(new Token(TokenType.LParen, currentChar.ToString()));
                                _position++;
                                break;
                            case ')':
                                _tokens.Add(new Token(TokenType.RParen, currentChar.ToString()));
                                _position++;
                                break;
                            default:
                                _tokens.Add(new Token(TokenType.Unknown, currentChar.ToString()));
                                _position++;
                                break;
                        }
                    }
                }

                return _tokens;
            }

            private Token ScanNumber()
            {
                int start = _position;
                while (_position < _text.Length && (char.IsDigit(_text[_position])))
                {
                    _position++;
                }
                string number = _text.Substring(start, _position - start);
                return new Token(TokenType.Number, number);
            }
        }
    }
}
