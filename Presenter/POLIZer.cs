using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompilerV2.Model;
using static CompilerV2.Presenter.POLIZLexer;

namespace CompilerV2.Presenter
{
    public class POLIZer
    {
        private List<Token> _tokens;
        private int _position;
        private List<Token> _poliz;
        private List<string> _messages;

        public POLIZer(string expression)
        {
            var scanner = new POLIZScan(expression);
            _tokens = scanner.Scan();
            _position = 0;
            _poliz = new List<Token>();
            _messages = new List<string>();
        }

        public List<string> AnalyzeAndBuildPOLIZ()
        {
            _messages.Clear();
            try
            {
                _poliz.Clear();
                ParseE();
                if (_position != _tokens.Count)
                {
                    _messages.Add($"Синтаксическая ошибка: лишние токены после конца выражения на позиции {_position}");
                }
                else
                {
                    _messages.Add("Построение ПОЛИЗа завершено успешно:");
                    _messages.Add(PolizToString());
                    int result = EvaluatePOLIZ();
                    _messages.Add($"Результат вычисления: {result}");
                }
            }
            catch (Exception ex)
            {
                _messages.Add($"Ошибка: {ex.Message}");
            }
            return new List<string>(_messages);
        }

        private string PolizToString()
        {
            var sb = new StringBuilder();
            foreach (var token in _poliz)
            {
                sb.Append(token.Value + " ");
            }
            return sb.ToString().Trim();
        }

        private int EvaluatePOLIZ()
        {
            Stack<int> stack = new Stack<int>();

            foreach (var token in _poliz)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        stack.Push(int.Parse(token.Value));
                        break;
                    case TokenType.Plus:
                        {
                            int b = stack.Pop();
                            int a = stack.Pop();
                            stack.Push(a + b);
                        }
                        break;
                    case TokenType.Minus:
                        {
                            int b = stack.Pop();
                            int a = stack.Pop();
                            stack.Push(a - b);
                        }
                        break;
                    case TokenType.Multiply:
                        {
                            int b = stack.Pop();
                            int a = stack.Pop();
                            stack.Push(a * b);
                        }
                        break;
                    case TokenType.Divide:
                        {
                            int b = stack.Pop();
                            int a = stack.Pop();
                            if (b == 0)
                                throw new DivideByZeroException("Деление на ноль");
                            stack.Push(a / b);
                        }
                        break;
                    default:
                        throw new Exception($"Ошибка вычисления: неожиданный токен {token.Type}");
                }
            }

            if (stack.Count != 1)
                throw new Exception("Ошибка вычисления: стек не пуст в конце вычисления");

            return stack.Pop();
        }

        private Token CurrentToken => _position < _tokens.Count ? _tokens[_position] : null;

        private void Eat(TokenType expected)
        {
            if (CurrentToken == null || CurrentToken.Type != expected)
            {
                throw new Exception($"Синтаксическая ошибка: ожидался {expected}, найдено {CurrentToken?.Type} на позиции {_position}");
            }
            _position++;
        }

        // Грамматика
        private void ParseE()
        {
            ParseT();
            ParseA();
        }

        private void ParseA()
        {
            if (CurrentToken != null && (CurrentToken.Type == TokenType.Plus || CurrentToken.Type == TokenType.Minus))
            {
                var op = CurrentToken;
                Eat(CurrentToken.Type);
                ParseT();
                _poliz.Add(op);
                ParseA();
            }
        }

        private void ParseT()
        {
            ParseO();
            ParseB();
        }

        private void ParseB()
        {
            if (CurrentToken != null && (CurrentToken.Type == TokenType.Multiply || CurrentToken.Type == TokenType.Divide))
            {
                var op = CurrentToken;
                Eat(CurrentToken.Type);
                ParseO();
                _poliz.Add(op);
                ParseB();
            }
        }

        private void ParseO()
        {
            if (CurrentToken == null)
                throw new Exception($"Синтаксическая ошибка: неожиданный конец выражения на позиции {_position}");

            if (CurrentToken.Type == TokenType.Number)
            {
                _poliz.Add(CurrentToken);
                Eat(TokenType.Number);
            }
            else if (CurrentToken.Type == TokenType.LParen)
            {
                Eat(TokenType.LParen);
                ParseE();
                Eat(TokenType.RParen);
            }
            else
            {
                throw new Exception($"Синтаксическая ошибка: ожидалось число или '(' на позиции {_position}");
            }
        }
    }
}
