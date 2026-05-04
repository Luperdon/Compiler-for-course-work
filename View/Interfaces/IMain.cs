using CompilerV2.Model;
using CompilerV2.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerV2.View.Interfaces
{
    public interface IMain
    {

        event EventHandler<string> CreateFile;
        event EventHandler<string> OpenFile;
        event EventHandler<string> SaveFile;
        event EventHandler<string> StartEnd;
        event EventHandler Repeat;
        event EventHandler<string> SaveAsFile;
        event FormClosingEventHandler CloseProgram;
        event EventHandler<int> SelectPage;
        event EventHandler<Operation> ChangeLastUserOperation;
        event EventHandler NullLastUserOperation;
        event EventHandler<string> SetNewFileSavedStr;
        event EventHandler<string> SetNewFileOpenedStr;
        event EventHandler<string> SetNewFileCreatedStr;
        event EventHandler<string> ThrowLastTextToModel;
        event EventHandler<string> ThrowNewTextToModel;
        event EventHandler CloseCurrentPage;


        void Message(string message);
        void InsertFileText(string text);
        void RepeatToView(Operation operate);
        void AddTab (string fileName, int idOfPage);
        string GetText();
        void SetLastText(string text);
        void SetPage(int pageId);
        void SetFlagToComboBoxItem(int currentPageId, bool flag);
        void EditCurrentPageName(string newName);
        void DeleteDeletedPage();
        void DeleteAllPages();
        void ClearPairs();
        void FillScanerDGV(List<Lexem> lexemsList);
        void FillErrorsDGV(List<ErrorPair> errorsList);
        bool IsSemanticTabSelected();
        void FillSemanticTable(List<SemanticCheckRow> rows);
        void FillAstOutput(string astText);
        bool IsPolizTabSelected();
        void FillPolizAnalysis(PolizAnalysisResult result);
    }
}
