using CompilerV2.Model;
using CompilerV2.Presenter;
using CompilerV2.View;
using CompilerV2.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerV2.Presenter
{
    public class MainPresenter
    {
        private readonly IMain _view;
        private List<Lexem> lexemsList;
        private string editingFileName = "";
        private bool isSaved = true;
        private Operation lastUserOperation;
        private List<TextModel> textModels = new List<TextModel>();
        private int currentPageId;
        private string CreateFileStr = "Файл успешно создан";
        private string FileSaved = "Файл успешно сохранён";
        private string FileOpened = "Файл успешно открыт";
        public string CloseUnsavedStr = "Есть несохранённые вкладки. Вы уверены, что хотите закрыть программу?";
        public string CloseSavedStr = "Вы уверены, что хотите закрыть программу?";
        public string SaveNoPathStr = "Не выбран редактируемый файл. Желаете создать файл?";
        public string CloseProgramStr = "Закрытие программы";
        public string MessageNoPathStr = "Отсутствует редактируемый файл";
        public ResourceManager _res;

        public MainPresenter(IMain view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            TextModel defaultModel = new TextModel("Новый файл.txt", 1, "", null, true, "");
            textModels.Add(defaultModel);
            currentPageId = 1;
            editingFileName = "Новый файл.txt";
            isSaved = true;
            _view.AddTab(editingFileName, defaultModel.id);
            _view.SetPage(defaultModel.id);
            _view.CreateFile += CreateFile;
            _view.OpenFile += OpenFile;
            _view.SaveFile += SaveFile;
            _view.SaveAsFile += SaveAsFile;
            _view.CloseProgram += CloseProgram;
            _view.Repeat += Repeat;
            _view.SelectPage += SelectPage;
            _view.ChangeLastUserOperation += ChangeLastUserOperation;
            _view.NullLastUserOperation += NullLastUserOperation;
            _view.SetNewFileSavedStr += SetNewFileSavedStr;
            _view.SetNewFileOpenedStr += SetNewFileOpenedStr;
            _view.SetNewFileCreatedStr += SetNewFileCreatedStr;
            _view.ThrowLastTextToModel += SetLastTextOnModel;
            _view.ThrowNewTextToModel += SetNewTextOnModel;
            _view.CloseCurrentPage += CloseCurrentPage;
            _view.StartEnd += StartEnd;
        }


        public void StartEnd(object sender, string textToAnalyze)
        {
            if (string.IsNullOrWhiteSpace(textToAnalyze))
            {
                if (_view.IsPolizTabSelected())
                {
                    _view.FillPolizAnalysis(PolizLabAnalyzer.Analyze(textToAnalyze ?? ""));
                    return;
                }

                List<ErrorPair> emptyErrors = new List<ErrorPair>
                {
                    new ErrorPair("Для сканирования необходимо ввести текст", null, 0, 0)
                };
                _view.FillErrorsDGV(emptyErrors);
                _view.FillSemanticTable(new List<SemanticCheckRow>
                {
                    new SemanticCheckRow
                    {
                        Rule = "Обработка ввода",
                        Status = "Ошибка",
                        Details = "Для семантического анализа необходимо ввести текст.",
                        Position = "-"
                    }
                });
                _view.FillAstOutput("AST не построено: входной текст пуст.");
                return;
            }

            if (_view.IsPolizTabSelected())
            {
                PolizAnalysisResult polizResult = PolizLabAnalyzer.Analyze(textToAnalyze);
                _view.FillPolizAnalysis(polizResult);
                return;
            }

            lexemsList = new List<Lexem>();
            Lexer lexer = new Lexer(textToAnalyze, _res);
            lexemsList = lexer.Scan();

            if (_view.IsSemanticTabSelected())
            {
                SemanticAnalyzer analyzer = new SemanticAnalyzer(lexemsList, textToAnalyze);
                SemanticAnalysisResult semanticResult = analyzer.Analyze();
                _view.FillSemanticTable(semanticResult.Rows);
                _view.FillAstOutput($"{semanticResult.AstText}{Environment.NewLine}{Environment.NewLine}Количество ошибок: {semanticResult.ErrorCount}");
                return;
            }

            Parser parser = new Parser(lexemsList, textToAnalyze);
            List<ErrorPair> errorsList = new List<ErrorPair>();
            parser.Parse();
            errorsList = parser.stateLog;
            if (errorsList.Count == 0)
            {
                _view.FillErrorsDGV(new List<ErrorPair>
                {
                    new ErrorPair("Синтаксический анализ выполнен успешно", null, 0, 0)
                });
                return;
            }

            _view.FillErrorsDGV(parser.stateLog);
            //_view.FillScanerDGV(lexemsList);
        }

        public void SetLastTextOnModel(object sender, string lastText)
        {
            textModels.Find(x => x.id == currentPageId).lastText = lastText;
        }
        public void SetNewTextOnModel(object sender, string  newText)
        {
            textModels.Find(x => x.id == currentPageId).text = newText;
        }
        public void SetNewFileSavedStr(object sender, string newString)
        {
            FileSaved = newString;
        }
        public void SetNewFileOpenedStr(object sender, string newString)
        {
            FileOpened = newString;
        }
        public void SetNewFileCreatedStr(object sender, string newString)
        {
            CreateFileStr = newString;
        }

        public void SelectPage(object sender, int index)
        {
            TextModel model = textModels.Find(x => x.id == index);
            if (model != null)
            {
                if (currentPageId != -1)
                {
                    int currentPageIndex = textModels.FindIndex(x => x.id == currentPageId);
                    textModels[currentPageIndex].text = _view.GetText();
                    textModels[currentPageIndex].isSaved = isSaved;
                    textModels[currentPageIndex].lastText = _view.GetText();
                }
                _view.SetLastText(model.lastText);
                currentPageId = model.id;
                editingFileName = model.editingFileName;
                isSaved = model.isSaved;
                lastUserOperation = null;
                _view.InsertFileText(model.text);
                _view.SetPage(model.id);
            }
        }
        public void CloseCurrentPage(object sender, EventArgs e)
        {
            if (textModels.Count == 1)
            {
                textModels.Clear();
                _view.ClearPairs();
                TextModel defaultModel = new TextModel("Новый файл.txt", 1, "", null, true, "");
                textModels.Add(defaultModel);
                currentPageId = 1;
                editingFileName = "Новый файл.txt";
                isSaved = true;
                _view.DeleteAllPages();
                _view.AddTab(editingFileName, defaultModel.id);
                _view.SetPage(defaultModel.id);
            }
            else if (textModels.Count > 1)
            {
                textModels.Remove(textModels.Find(x => x.id == currentPageId));
                currentPageId = -1;
                _view.DeleteDeletedPage();
                SelectPage(this, textModels.First().id);
            }
        }

        public void Repeat(object sender, EventArgs e)
        {
            if (lastUserOperation != null)
            {
                _view.RepeatToView(lastUserOperation);
            }
        }
        public void ChangeLastUserOperation(object sender, Operation lastUserOperation)
        {
            textModels.Find(x => x.id == currentPageId).isSaved = false;
            this.lastUserOperation = lastUserOperation ?? throw new ArgumentNullException(nameof(lastUserOperation));
            isSaved = false;
            _view.SetFlagToComboBoxItem(currentPageId, isSaved);
        }
        public void NullLastUserOperation(object sender, EventArgs e)
        {
            textModels.Find(x => x.id == currentPageId).isSaved = false;
            this.lastUserOperation = null;
            isSaved = false;
            _view.SetFlagToComboBoxItem(currentPageId, isSaved);
        }


        public void CreateFile(object sender, string fileName)
        {
            System.IO.File.Create(fileName).Close();
            _view.Message(CreateFileStr);
        }

        public void CloseProgram(object sender, FormClosingEventArgs e)
        {
            if (textModels.Find(x=>x.isSaved == false)!=null)
            {
                if (MessageBox.Show(CloseUnsavedStr, CloseProgramStr, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (MessageBox.Show(CloseSavedStr, CloseProgramStr, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void OpenFile(object sender, string fileDestination)
        {
            foreach (var textModel in textModels)
            {
                if (textModel.editingFileName == fileDestination)
                {
                    SelectPage(this, textModels.Find(x => x.editingFileName == fileDestination).id);
                    return;
                }
            }
            string fileText = File.ReadAllText(fileDestination);
            string fileName = Path.GetFileName(fileDestination);
            int generatedId = new Random().Next();
            while (!(textModels.Find(x=>x.id == generatedId) == null))
            {
                generatedId = new Random().Next();
            }
            int currentPageIndex = textModels.FindIndex(x => x.id == currentPageId);
            textModels[currentPageIndex].text = _view.GetText();
            textModels[currentPageIndex].isSaved = isSaved;
            textModels[currentPageIndex].lastText = _view.GetText();
            TextModel newTextModel = new TextModel(fileDestination,generatedId, fileText, lastUserOperation, isSaved, fileText);
            textModels.Add(newTextModel);
            _view.AddTab(fileName, newTextModel.id);
            currentPageId = newTextModel.id;
            lastUserOperation = null;
            editingFileName = fileDestination;
            _view.InsertFileText(fileText);
            _view.SetLastText(fileText);
            isSaved = true;
            _view.Message(FileOpened);
        }

        public void SaveFile(object sender, string textToSave)
        {
            if (!string.IsNullOrEmpty(editingFileName))
            {
                System.IO.File.WriteAllText(editingFileName,textToSave);
                textModels.Find(x => x.id == currentPageId).isSaved = true;
                isSaved = true;
                _view.SetFlagToComboBoxItem(currentPageId, isSaved);
                _view.Message(FileSaved);
            }
            else
            {
                if (MessageBox.Show(SaveNoPathStr, MessageNoPathStr, MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt";
                    if (saveFileDialog.ShowDialog()==DialogResult.OK)
                    {
                        editingFileName = saveFileDialog.FileName;
                        System.IO.File.WriteAllText(editingFileName, textToSave);
                        textModels.Find(x => x.id == currentPageId).isSaved = true;
                        isSaved = true;
                        _view.SetFlagToComboBoxItem(currentPageId, isSaved);
                        _view.Message(FileSaved);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public void SaveAsFile(object sender, string textToSave)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.EditCurrentPageName(Path.GetFileName(saveFileDialog.FileName));
                textModels.Find(x => x.editingFileName == this.editingFileName).editingFileName = saveFileDialog.FileName;
                System.IO.File.WriteAllText(editingFileName, textToSave);
                isSaved = true;
                editingFileName = saveFileDialog.FileName;
                _view.Message(FileSaved);
            }
        }
    }
}