using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompilerV2.Model;

namespace CompilerV2.Presenter
{
    /// <summary>
    /// Рекурсивный спуск по грамматике E→TA, T→FB, F→num|id|(E);
    /// тетрады при разборе; ПОЛИЗ — алгоритм Дейкстры (сортировочная станция).
    /// </summary>
    public static class PolizLabAnalyzer
    {
        public static PolizAnalysisResult Analyze(string expression)
        {
            expression = expression?.Trim();
            var result = new PolizAnalysisResult();
            if (string.IsNullOrWhiteSpace(expression))
            {
                result.SyntaxErrors.Add("Введите выражение для анализа.");
                return result;
            }

            var lex = PolizLexer.Scan(expression);
            foreach (var t in lex.Tokens)
            {
                if (t.Type == PolizTokenType.End)
                {
                    continue;
                }

                result.LexemeRows.Add((t.Lexeme, PolizLexer.TokenKindRu(t.Type), $"{t.Start + 1}–{t.End}"));
            }

            foreach (var err in lex.Errors)
            {
                result.LexicalErrors.Add(err);
            }

            if (lex.Errors.Count > 0)
            {
                result.Warnings.Add("Из-за лексических ошибок построение тетрад и ПОЛИЗа не выполняется.");
                return result;
            }

            result.HasOnlyIntegerLiterals = lex.Tokens.All(t =>
                t.Type == PolizTokenType.End ||
                t.Type == PolizTokenType.Number ||
                t.Type == PolizTokenType.Plus ||
                t.Type == PolizTokenType.Minus ||
                t.Type == PolizTokenType.Mul ||
                t.Type == PolizTokenType.Div ||
                t.Type == PolizTokenType.IntDiv ||
                t.Type == PolizTokenType.Mod ||
                t.Type == PolizTokenType.Pow ||
                t.Type == PolizTokenType.LParen ||
                t.Type == PolizTokenType.RParen);

            var parser = new RdParser(lex.Tokens, result.Tetrads, result.SyntaxErrors);
            parser.ParseE();
            if (result.SyntaxErrors.Count > 0)
            {
                result.Tetrads.Clear();
                result.Warnings.Add("Из-за синтаксических ошибок построение тетрад и ПОЛИЗа не выполняется.");
                return result;
            }

            if (parser.Position != lex.Tokens.Count - 1 || lex.Tokens[parser.Position].Type != PolizTokenType.End)
            {
                result.Tetrads.Clear();
                result.SyntaxErrors.Add("Синтаксическая ошибка: лишние символы после конца выражения.");
                result.Warnings.Add("Построение тетрад и ПОЛИЗа не выполняется.");
                return result;
            }

            if (!result.HasOnlyIntegerLiterals)
            {
                result.Warnings.Add(
                    "Выражение содержит идентификаторы: построение ПОЛИЗа и вычисление значения выполняются только для выражений из целых чисел.");
            }

            var infixForYard = lex.Tokens.Where(t => t.Type != PolizTokenType.End).ToList();
            try
            {
                if (result.HasOnlyIntegerLiterals)
                {
                    var rpn = ShuntingYard.ToRpn(infixForYard);
                    result.Poliz = string.Join(" ", rpn.Select(t => t.Lexeme));
                    result.ComputedValue = RpnEvaluator.Evaluate(rpn);
                }
            }
            catch (DivideByZeroException)
            {
                result.Warnings.Add("Деление на ноль при вычислении ПОЛИЗа.");
                result.ComputedValue = null;
            }
            catch (OverflowException)
            {
                result.Warnings.Add("Переполнение при вычислении целочисленного выражения.");
                result.ComputedValue = null;
            }
            catch (Exception ex)
            {
                result.Warnings.Add("Ошибка при построении или вычислении ПОЛИЗа: " + ex.Message);
                result.ComputedValue = null;
            }

            return result;
        }

        private sealed class RdParser
        {
            private readonly List<PolizToken> _tokens;
            private readonly List<Tetrad> _tetrads;
            private readonly List<string> _errors;
            private int _temp = 1;
            private int _pos;

            public int Position => _pos;

            public RdParser(List<PolizToken> tokens, List<Tetrad> tetrads, List<string> errors)
            {
                _tokens = tokens;
                _tetrads = tetrads;
                _errors = errors;
                _pos = 0;
            }

            private PolizToken Cur => _pos < _tokens.Count ? _tokens[_pos] : null;

            public string ParseE()
            {
                string v = ParseT();
                if (v == null)
                {
                    return null;
                }

                return ParseA(v);
            }

            private string ParseA(string acc)
            {
                if (Cur == null)
                {
                    return acc;
                }

                if (Cur.Type == PolizTokenType.Plus || Cur.Type == PolizTokenType.Minus)
                {
                    string opLex = Cur.Lexeme;
                    _pos++;
                    string right = ParseT();
                    if (right == null)
                    {
                        return null;
                    }

                    string t = NewTemp();
                    _tetrads.Add(new Tetrad(opLex, acc, right, t));
                    return ParseA(t);
                }

                return acc;
            }

            public string ParseT()
            {
                string v = ParseF();
                if (v == null)
                {
                    return null;
                }

                return ParseB(v);
            }

            private string ParseB(string acc)
            {
                if (Cur == null)
                {
                    return acc;
                }

                if (IsMulGroup(Cur.Type))
                {
                    string opLex = Cur.Lexeme;
                    _pos++;
                    string right = ParseF();
                    if (right == null)
                    {
                        return null;
                    }

                    string t = NewTemp();
                    _tetrads.Add(new Tetrad(opLex, acc, right, t));
                    return ParseB(t);
                }

                return acc;
            }

            private static bool IsMulGroup(PolizTokenType t)
            {
                return t == PolizTokenType.Mul || t == PolizTokenType.Div || t == PolizTokenType.IntDiv
                    || t == PolizTokenType.Mod || t == PolizTokenType.Pow;
            }

            private string ParseF()
            {
                if (Cur == null || Cur.Type == PolizTokenType.End)
                {
                    int pos = Cur != null ? Cur.Start + 1 : (_tokens.Count >= 2 ? _tokens[_tokens.Count - 2].End + 1 : 1);
                    _errors.Add($"Синтаксическая ошибка: пропущен операнд (ожидалось число, идентификатор или '(') у позиции {pos}.");
                    return null;
                }

                if (Cur.Type == PolizTokenType.Number || Cur.Type == PolizTokenType.Identifier)
                {
                    string v = Cur.Lexeme;
                    _pos++;
                    return v;
                }

                if (Cur.Type == PolizTokenType.LParen)
                {
                    _pos++;
                    string inner = ParseE();
                    if (inner == null)
                    {
                        return null;
                    }

                    if (Cur == null || Cur.Type != PolizTokenType.RParen)
                    {
                        int p = Cur != null ? Cur.Start + 1 : _tokens[_pos - 1].End + 1;
                        _errors.Add($"Синтаксическая ошибка: пропущена закрывающая скобка ')' (позиция {p}).");
                        return null;
                    }

                    _pos++;
                    return inner;
                }

                _errors.Add($"Синтаксическая ошибка: ожидалось число, идентификатор или '('; найдено «{Cur.Lexeme}» на позиции {Cur.Start + 1}.");
                return null;
            }

            private string NewTemp() => "t" + (_temp++);
        }

        private static class ShuntingYard
        {
            public static List<PolizToken> ToRpn(List<PolizToken> infix)
            {
                var output = new List<PolizToken>();
                var stack = new Stack<PolizToken>();

                foreach (var tok in infix)
                {
                    if (tok.Type == PolizTokenType.Number)
                    {
                        output.Add(tok);
                    }
                    else if (tok.Type == PolizTokenType.Identifier)
                    {
                        throw new InvalidOperationException("В ПОЛИЗе допускаются только целые числа.");
                    }
                    else if (IsOperator(tok.Type))
                    {
                        int p1 = Precedence(tok.Type);
                        while (stack.Count > 0 && IsOperator(stack.Peek().Type))
                        {
                            int p2 = Precedence(stack.Peek().Type);
                            if (p2 < p1)
                            {
                                break;
                            }

                            output.Add(stack.Pop());
                        }

                        stack.Push(tok);
                    }
                    else if (tok.Type == PolizTokenType.LParen)
                    {
                        stack.Push(tok);
                    }
                    else if (tok.Type == PolizTokenType.RParen)
                    {
                        bool found = false;
                        while (stack.Count > 0)
                        {
                            var top = stack.Pop();
                            if (top.Type == PolizTokenType.LParen)
                            {
                                found = true;
                                break;
                            }

                            output.Add(top);
                        }

                        if (!found)
                        {
                            throw new InvalidOperationException($"Лишняя закрывающая скобка на позиции {tok.Start + 1}.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Недопустимый токен в выражении.");
                    }
                }

                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    if (top.Type == PolizTokenType.LParen)
                    {
                        throw new InvalidOperationException("Лишняя открывающая скобка: не хватает ')'.");
                    }

                    output.Add(top);
                }

                return output;
            }

            private static bool IsOperator(PolizTokenType t)
            {
                return t == PolizTokenType.Plus || t == PolizTokenType.Minus || t == PolizTokenType.Mul
                    || t == PolizTokenType.Div || t == PolizTokenType.IntDiv || t == PolizTokenType.Mod
                    || t == PolizTokenType.Pow;
            }

            /// <summary>Приоритет: * / // % ** выше + - .</summary>
            private static int Precedence(PolizTokenType t)
            {
                switch (t)
                {
                    case PolizTokenType.Plus:
                    case PolizTokenType.Minus:
                        return 1;
                    case PolizTokenType.Mul:
                    case PolizTokenType.Div:
                    case PolizTokenType.IntDiv:
                    case PolizTokenType.Mod:
                    case PolizTokenType.Pow:
                        return 2;
                    default:
                        return 0;
                }
            }
        }

        private static class RpnEvaluator
        {
            public static int Evaluate(List<PolizToken> rpn)
            {
                var st = new Stack<int>();
                foreach (var t in rpn)
                {
                    if (t.Type == PolizTokenType.Number)
                    {
                        st.Push(checked(int.Parse(t.Lexeme)));
                    }
                    else
                    {
                        if (st.Count < 2)
                        {
                            throw new InvalidOperationException("Некорректное выражение для вычисления.");
                        }

                        int b = st.Pop();
                        int a = st.Pop();
                        st.Push(Apply(a, b, t.Type));
                    }
                }

                if (st.Count != 1)
                {
                    throw new InvalidOperationException("Некорректное выражение для вычисления.");
                }

                return st.Pop();
            }

            private static int Apply(int a, int b, PolizTokenType op)
            {
                checked
                {
                    switch (op)
                    {
                        case PolizTokenType.Plus: return a + b;
                        case PolizTokenType.Minus: return a - b;
                        case PolizTokenType.Mul: return a * b;
                        case PolizTokenType.Div:
                            if (b == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            return a / b;
                        case PolizTokenType.IntDiv:
                            if (b == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            return a / b;
                        case PolizTokenType.Mod:
                            if (b == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            return a % b;
                        case PolizTokenType.Pow:
                            return IntPow(a, b);
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            /// <summary>Целочисленное возведение в степень, левоассоциативно как в грамматике.</summary>
            private static int IntPow(int a, int b)
            {
                if (b < 0)
                {
                    throw new InvalidOperationException("Отрицательная степень для целых.");
                }

                int r = 1;
                for (int k = 0; k < b; k++)
                {
                    r = checked(r * a);
                }

                return r;
            }
        }
    }
}
