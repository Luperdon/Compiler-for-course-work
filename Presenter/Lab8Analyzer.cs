using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerV2.Presenter
{
    public class Lab8Analyzer
    {
        public enum TokenType
        {
            Integer, Plus, Minus, Multiply, Divide, Modulo, LParen, RParen, EOF, Unknown
        }

        public class Token
        {
            public TokenType Type { get; }
            public string Value { get; }
            public int Position { get; }

            public Token(TokenType type, string value, int position)
            {
                Type = type;
                Value = value;
                Position = position;
            }
        }

        public class Lexer
        {
            private readonly string _text;
            private int _pos;

            public Lexer(string text)
            {
                _text = text;
                _pos = 0;
            }

            private char Current => _pos < _text.Length ? _text[_pos] : '\0';

            public Token GetNextToken()
            {
                while (char.IsWhiteSpace(Current))
                    _pos++;

                int start = _pos;

                if (char.IsDigit(Current))
                {
                    while (char.IsDigit(Current)) _pos++;
                    return new Token(TokenType.Integer, _text.Substring(start, _pos - start), start);
                }

                switch (Current)
                {
                    case '+': _pos++; return new Token(TokenType.Plus, "+", start);
                    case '-': _pos++; return new Token(TokenType.Minus, "-", start);
                    case '*': _pos++; return new Token(TokenType.Multiply, "*", start);
                    case '/': _pos++; return new Token(TokenType.Divide, "/", start);
                    case '%': _pos++; return new Token(TokenType.Modulo, "%", start);
                    case '(': _pos++; return new Token(TokenType.LParen, "(", start);
                    case ')': _pos++; return new Token(TokenType.RParen, ")", start);
                    case '\0': return new Token(TokenType.EOF, "", _pos);
                    default:
                        return new Token(TokenType.Unknown, Current.ToString(), _pos++);
                }
            }
        }

        public class ParserError
        {
            public string Message { get; set; }
            public int Position { get; set; }

            public ParserError(string message, int position)
            {
                Message = message;
                Position = position;
            }
        }

        public class Parser
        {
            private readonly Lexer _lexer;
            private Token _currentToken;
            public List<ParserError> Errors { get; }

            public Parser(string input)
            {
                _lexer = new Lexer(input);
                Errors = new List<ParserError>();
                _currentToken = _lexer.GetNextToken();
            }

            private void Eat(TokenType type)
            {
                if (_currentToken.Type == type)
                    _currentToken = _lexer.GetNextToken();
                else
                    Errors.Add(new ParserError($"Ожидался токен {type}, найден {_currentToken.Type}", _currentToken.Position));
            }

            public void Parse()
            {
                ParseExpr();
                if (_currentToken.Type != TokenType.EOF)
                {
                    Errors.Add(new ParserError("Лишний ввод после конца выражения", _currentToken.Position));
                }
            }

            private void ParseExpr()
            {
                ParseExprAddSub();
                while (_currentToken.Type == TokenType.Multiply || _currentToken.Type == TokenType.Divide || _currentToken.Type == TokenType.Modulo)
                {
                    Eat(_currentToken.Type);
                    ParseExprAddSub();
                }
            }

            private void ParseExprAddSub()
            {
                ParseTerm();
                while (_currentToken.Type == TokenType.Plus || _currentToken.Type == TokenType.Minus)
                {
                    Eat(_currentToken.Type);
                    ParseTerm();
                }
            }

            private void ParseTerm()
            {
                if (_currentToken.Type == TokenType.Integer)
                {
                    Eat(TokenType.Integer);
                }
                else if (_currentToken.Type == TokenType.LParen)
                {
                    Eat(TokenType.LParen);
                    ParseExpr();
                    if (_currentToken.Type == TokenType.RParen)
                        Eat(TokenType.RParen);
                    else
                        Errors.Add(new ParserError("Ожидалась закрывающая скобка", _currentToken.Position));
                }
                else
                {
                    Errors.Add(new ParserError("Ожидалось число или открывающая скобка", _currentToken.Position));
                    _currentToken = _lexer.GetNextToken();
                }
            }
        }


    }
}
