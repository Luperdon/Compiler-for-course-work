using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerV2.Model
{
    public class TextModel
    {
        public string editingFileName;
        public int id;
        public string text;
        public Operation lastUserOperation;
        public bool isSaved;
        public string lastText;
        public TextModel(string fileName,int id, string text, Operation lastOperation, bool isSaved, string lastText)
        {
            this.editingFileName = fileName;
            this.id = id;
            this.text = text;
            this.lastUserOperation = lastOperation;
            this.isSaved = isSaved;
            this.lastText = lastText;
        }
    }
}
