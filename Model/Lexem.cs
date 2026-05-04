using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CompilerV2.Model
{
    public class Lexem
    {
        private int LexemCode;
        public string lexemName;
        private ResourceManager _res;
        public int lexemCode
        {
            get { return LexemCode; }
            set
            {
                if (value == 1)
                {
                    LexemCode = 1;
                    lexemType = LexemType.Identificator;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 2)
                {
                    LexemCode = 2;
                    lexemType = LexemType.KeyWord;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 3)
                {
                    LexemCode = 3;
                    lexemType = LexemType.Partial;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 4)
                {
                    LexemCode = 4;
                    lexemType = LexemType.AssignmentOperator;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 5)
                {
                    LexemCode = 5;
                    lexemType = LexemType.String;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 6)
                {
                    LexemCode = 6;
                    lexemType = LexemType.F_stringAssign;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 7)
                {
                    LexemCode = 7;
                    lexemType = LexemType.Minus;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 8)
                {
                    LexemCode = 8;
                    lexemType = LexemType.Plus;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 9)
                {
                    LexemCode = 9;
                    lexemType = LexemType.FunctionalPartial;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 10)
                {
                    LexemCode = 10;
                    lexemType = LexemType.MinusComlexInt;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 11)
                {
                    LexemCode = 11;
                    lexemType = LexemType.PlusComplexInt;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 12)
                {
                    LexemCode = 12;
                    lexemType = LexemType.Int;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 13)
                {
                    LexemCode = 13;
                    lexemType = LexemType.Double;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 14)
                {
                    LexemCode = 14;
                    lexemType = LexemType.MinusComplexDouble;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 15)
                {
                    LexemCode = 15;
                    lexemType = LexemType.PlusComplexDouble;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 16)
                {
                    LexemCode = 16;
                    lexemType = LexemType.OpenScobe;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 17)
                {
                    LexemCode = 17;
                    lexemType = LexemType.CloseScobe;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 18)
                {
                    LexemCode = 18;
                    lexemType = LexemType.Ending;
                    lexemName = _res.GetString(lexemType.ToString());
                }
                else if (value == 19)
                {
                    LexemCode = 19;
                    lexemType = LexemType.Error;
                    lexemName = _res.GetString(lexemType.ToString());
                }
            }
        }
        public enum LexemType
        {
            Identificator, //code 1
            KeyWord,// code 2
            Partial, // code 3
            AssignmentOperator, // code 4
            String, // code 5
            F_stringAssign, // code 6
            Minus, // code 7
            Plus, // code 8
            FunctionalPartial, // code 9
            MinusComlexInt, // code 10
            PlusComplexInt, // code 11
            Int, // code 12
            Double, // code 13
            MinusComplexDouble, // code 14
            PlusComplexDouble, // code 15
            OpenScobe, // code 16
            CloseScobe, // code 17
            Ending, // code 18
            Error // code 19
        };
        public string lexemContaintment;
        public LexemType lexemType;
        public int lexemStartPosition;
        public int lexemEndPosition;
        public Lexem(int code, string containtment, int startPosition, int endPosition, ResourceManager res) 
        {
            _res = res;
            lexemCode = code;
            lexemContaintment = containtment;
            lexemStartPosition = startPosition;
            lexemEndPosition = endPosition;
        }
    }
}
