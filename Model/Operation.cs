using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerV2.Model
{
    public class Operation
    {
        public string containtment;
        public bool isCut;
        public bool isDelete;
        public bool isPaste;
        public Operation(string containts, bool isCut, bool isDelete, bool isPaste)
        {
            containtment = containts;
            this.isCut = isCut;
            this.isDelete = isDelete;
            this.isPaste = isPaste;
        }
    }
}
