using CompilerV2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CompilerV2.Presenter
{
    public class SemanticCheckRow
    {
        public string Rule { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public string Position { get; set; }
    }

    public class SemanticAnalysisResult
    {
        public List<SemanticCheckRow> Rows { get; } = new List<SemanticCheckRow>();
        public List<ErrorPair> Errors { get; } = new List<ErrorPair>();
        public string AstText { get; set; } = string.Empty;
        public int ErrorCount => Errors.Count;
    }

    public class SemanticAnalyzer
    {
        private readonly List<Lexem> _lexems;
        private readonly string _sourceText;
        private readonly Dictionary<string, string> _symbolTable = new Dictionary<string, string>(StringComparer.Ordinal);
        private readonly List<AssignmentAstNode> _acceptedNodes = new List<AssignmentAstNode>();

        public SemanticAnalyzer(List<Lexem> lexems, string sourceText)
        {
            _lexems = lexems ?? new List<Lexem>();
            _sourceText = sourceText ?? string.Empty;
        }

        public SemanticAnalysisResult Analyze()
        {
            var result = new SemanticAnalysisResult();
            int position = 0;
            while (position < _lexems.Count)
            {
                if (!TryParseStatement(ref position, out StatementInfo statement))
                {
                    continue;
                }

                bool hasError = false;
                string statementPosition = GetPositionText(statement.Identifier.lexemStartPosition);

                if (_symbolTable.ContainsKey(statement.Identifier.lexemContaintment))
                {
                    hasError = true;
                    string message = $"Ошибка: идентификатор \"{statement.Identifier.lexemContaintment}\" уже объявлен ранее.";
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 1: уникальность идентификаторов",
                        Status = "Ошибка",
                        Details = message,
                        Position = statementPosition
                    });
                    AddError(result, message, statement.Identifier);
                }
                else
                {
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 1: уникальность идентификаторов",
                        Status = "OK",
                        Details = $"Идентификатор \"{statement.Identifier.lexemContaintment}\" объявлен впервые.",
                        Position = statementPosition
                    });
                }

                string argumentType = GetArgumentType(statement.Argument, out bool isKnownIdentifier);
                if (argumentType != "Number")
                {
                    hasError = true;
                    string message = isKnownIdentifier
                        ? $"Ошибка: тип аргумента \"{statement.Argument.lexemContaintment}\" несовместим с format, ожидался числовой тип."
                        : $"Ошибка: тип аргумента \"{statement.Argument.lexemContaintment}\" несовместим, ожидалось число в экспоненциальной форме.";
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 2: совместимость типов",
                        Status = "Ошибка",
                        Details = message,
                        Position = GetPositionText(statement.Argument.lexemStartPosition)
                    });
                    AddError(result, message, statement.Argument);
                }
                else
                {
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 2: совместимость типов",
                        Status = "OK",
                        Details = "Тип аргумента совместим с форматированием.",
                        Position = GetPositionText(statement.Argument.lexemStartPosition)
                    });
                }

                if (statement.Argument.lexemCode == 10 || statement.Argument.lexemCode == 11 || statement.Argument.lexemCode == 14 || statement.Argument.lexemCode == 15)
                {
                    bool isInRange = TryParseScientific(statement.Argument.lexemContaintment, out double parsedNumber);
                    if (!isInRange || double.IsInfinity(parsedNumber) || double.IsNaN(parsedNumber))
                    {
                        hasError = true;
                        string message = $"Ошибка: значение \"{statement.Argument.lexemContaintment}\" выходит за допустимые пределы типа Double.";
                        result.Rows.Add(new SemanticCheckRow
                        {
                            Rule = "Правило 3: допустимые значения",
                            Status = "Ошибка",
                            Details = message,
                            Position = GetPositionText(statement.Argument.lexemStartPosition)
                        });
                        AddError(result, message, statement.Argument);
                    }
                    else
                    {
                        result.Rows.Add(new SemanticCheckRow
                        {
                            Rule = "Правило 3: допустимые значения",
                            Status = "OK",
                            Details = $"Число {parsedNumber.ToString("G", CultureInfo.InvariantCulture)} находится в допустимых пределах.",
                            Position = GetPositionText(statement.Argument.lexemStartPosition)
                        });
                    }
                }
                else
                {
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 3: допустимые значения",
                        Status = "Пропуск",
                        Details = "Проверка диапазона применяется только к числовым литералам.",
                        Position = GetPositionText(statement.Argument.lexemStartPosition)
                    });
                }

                if (statement.Argument.lexemCode == 1)
                {
                    if (!_symbolTable.ContainsKey(statement.Argument.lexemContaintment))
                    {
                        hasError = true;
                        string message = $"Ошибка: идентификатор \"{statement.Argument.lexemContaintment}\" не объявлен ранее.";
                        result.Rows.Add(new SemanticCheckRow
                        {
                            Rule = "Правило 4: использование идентификаторов",
                            Status = "Ошибка",
                            Details = message,
                            Position = GetPositionText(statement.Argument.lexemStartPosition)
                        });
                        AddError(result, message, statement.Argument);
                    }
                    else
                    {
                        result.Rows.Add(new SemanticCheckRow
                        {
                            Rule = "Правило 4: использование идентификаторов",
                            Status = "OK",
                            Details = $"Идентификатор \"{statement.Argument.lexemContaintment}\" объявлен ранее.",
                            Position = GetPositionText(statement.Argument.lexemStartPosition)
                        });
                    }
                }
                else
                {
                    result.Rows.Add(new SemanticCheckRow
                    {
                        Rule = "Правило 4: использование идентификаторов",
                        Status = "OK",
                        Details = "В выражении используется числовой литерал, проверка объявления не требуется.",
                        Position = GetPositionText(statement.Argument.lexemStartPosition)
                    });
                }

                if (!hasError)
                {
                    _symbolTable[statement.Identifier.lexemContaintment] = "String";
                    _acceptedNodes.Add(new AssignmentAstNode
                    {
                        IdentifierName = statement.Identifier.lexemContaintment,
                        FormatSpecifier = statement.FormatString.lexemContaintment,
                        ArgumentValue = statement.Argument.lexemContaintment,
                        ArgumentNodeType = statement.Argument.lexemCode == 1 ? "IdentifierNode" : "ScientificNumberNode"
                    });
                }
            }

            result.AstText = BuildAstText();
            return result;
        }

        private bool TryParseStatement(ref int position, out StatementInfo statement)
        {
            statement = null;
            if (position >= _lexems.Count)
            {
                return false;
            }

            int start = position;
            int need = 9;
            if (start + need > _lexems.Count)
            {
                position = _lexems.Count;
                return false;
            }

            Lexem id = _lexems[start];
            Lexem assignment = _lexems[start + 1];
            Lexem fString = _lexems[start + 2];
            Lexem dot = _lexems[start + 3];
            Lexem format = _lexems[start + 4];
            Lexem open = _lexems[start + 5];
            Lexem argument = _lexems[start + 6];
            Lexem close = _lexems[start + 7];
            Lexem semicolon = _lexems[start + 8];

            bool validShape = id.lexemCode == 1
                && assignment.lexemCode == 4
                && fString.lexemCode == 6
                && dot.lexemCode == 9
                && format.lexemCode == 2
                && open.lexemCode == 16
                && (argument.lexemCode == 1 || argument.lexemCode == 10 || argument.lexemCode == 11 || argument.lexemCode == 14 || argument.lexemCode == 15)
                && close.lexemCode == 17
                && semicolon.lexemCode == 18;

            if (!validShape)
            {
                while (position < _lexems.Count && _lexems[position].lexemCode != 18)
                {
                    position++;
                }
                if (position < _lexems.Count && _lexems[position].lexemCode == 18)
                {
                    position++;
                }
                return false;
            }

            statement = new StatementInfo
            {
                Identifier = id,
                FormatString = fString,
                Argument = argument
            };
            position += need;
            return true;
        }

        private string BuildAstText()
        {
            if (_acceptedNodes.Count == 0)
            {
                return "AST не построено: нет корректных конструкций для семантического дерева.";
            }

            var sb = new StringBuilder();
            sb.AppendLine("ProgramNode");
            for (int i = 0; i < _acceptedNodes.Count; i++)
            {
                AssignmentAstNode node = _acceptedNodes[i];
                bool isLast = i == _acceptedNodes.Count - 1;
                string branch = isLast ? "└── " : "├── ";
                string indent = isLast ? "    " : "│   ";

                sb.AppendLine($"{branch}AssignmentNode");
                sb.AppendLine($"{indent}├── name: \"{node.IdentifierName}\"");
                sb.AppendLine($"{indent}├── type: StringNode");
                sb.AppendLine($"{indent}└── value: FormatCallNode");
                sb.AppendLine($"{indent}    ├── specifier: \"{node.FormatSpecifier}\"");
                sb.AppendLine($"{indent}    └── argument: {node.ArgumentNodeType}");
                sb.AppendLine($"{indent}        └── value: {node.ArgumentValue}");
            }

            return sb.ToString().TrimEnd();
        }

        private void AddError(SemanticAnalysisResult result, string message, Lexem lexem)
        {
            result.Errors.Add(new ErrorPair(message, lexem, lexem.lexemStartPosition, lexem.lexemEndPosition));
        }

        private string GetArgumentType(Lexem argument, out bool isKnownIdentifier)
        {
            isKnownIdentifier = false;
            if (argument.lexemCode == 1)
            {
                isKnownIdentifier = _symbolTable.ContainsKey(argument.lexemContaintment);
                return isKnownIdentifier ? _symbolTable[argument.lexemContaintment] : "Unknown";
            }

            if (argument.lexemCode == 10 || argument.lexemCode == 11 || argument.lexemCode == 14 || argument.lexemCode == 15)
            {
                return "Number";
            }

            return "Unknown";
        }

        private bool TryParseScientific(string value, out double number)
        {
            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out number);
        }

        private string GetPositionText(int absolutePosition)
        {
            int line = 1;
            int column = 1;
            for (int i = 0; i < _sourceText.Length && i < absolutePosition; i++)
            {
                if (_sourceText[i] == '\n')
                {
                    line++;
                    column = 1;
                }
                else
                {
                    column++;
                }
            }

            return $"строка {line}, символ {column}";
        }

        private class StatementInfo
        {
            public Lexem Identifier { get; set; }
            public Lexem FormatString { get; set; }
            public Lexem Argument { get; set; }
        }

        private class AssignmentAstNode
        {
            public string IdentifierName { get; set; }
            public string FormatSpecifier { get; set; }
            public string ArgumentNodeType { get; set; }
            public string ArgumentValue { get; set; }
        }
    }
}
