using System.Collections.Generic;

namespace CompilerV2.Model
{
    /// <summary>Тетрада (op, arg1, arg2, result).</summary>
    public sealed class Tetrad
    {
        public string Op { get; }
        public string Arg1 { get; }
        public string Arg2 { get; }
        public string Result { get; }

        public Tetrad(string op, string arg1, string arg2, string result)
        {
            Op = op;
            Arg1 = arg1;
            Arg2 = arg2;
            Result = result;
        }
    }

    /// <summary>Результат анализа выражения для вкладки «ПОЛИЗ».</summary>
    public sealed class PolizAnalysisResult
    {
        public List<(string Lexeme, string Kind, string Position)> LexemeRows { get; } =
            new List<(string, string, string)>();

        public List<string> LexicalErrors { get; } = new List<string>();
        public List<string> SyntaxErrors { get; } = new List<string>();
        public List<string> Warnings { get; } = new List<string>();

        public List<Tetrad> Tetrads { get; } = new List<Tetrad>();

        /// <summary>ПОЛИЗ (обратная польская запись), построенный алгоритмом Дейкстры.</summary>
        public string Poliz { get; set; } = "";

        public int? ComputedValue { get; set; }

        public bool HasOnlyIntegerLiterals { get; set; }
    }
}
