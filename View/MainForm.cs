using CompilerV2.Model;
using CompilerV2.Presenter;
using CompilerV2.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace CompilerV2.View
{
    public partial class MainForm : Form, IMain
    {
        private MainPresenter _presenter;
        private int selectionIndex = 0;
        private string lastText = "";
        private bool isCutEnabled = false;
        private Dictionary<int, int> pairsOfPages = new Dictionary<int, int>();
        private bool isEnabledToTextChanged = true;
        private TabPage semanticTabPage;
        private DataGridView semanticDataGridView;
        private RichTextBox astRichTextBox;

        public MainForm()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);
            _presenter._res = new ResourceManager("CompilerV2.Resources.Resource_ru", typeof(MainForm).Assembly);
            lastText = UpperRichTextBox.Text;
            UpperRichTextBox.DragEnter += new DragEventHandler(MainForm_DragEnter);
            UpperRichTextBox.DragDrop += new DragEventHandler(MainForm_DragDrop);
            UpperRichTextBox.AllowDrop = true;
            UpperRichTextBox.VScroll += (s, e) => SyncScroll();
            UpperRichTextBox.TextChanged += (s, e) => UpdateLineNumbers();
            UpperRichTextBox.SelectionChanged += (s, e) => SyncScroll();
            this.SizeChanged += (s, e) => UpdateLineNumbers();
            NumericLB.SelectionMode = SelectionMode.None;
            UpdateLineNumbers();
            InitializeSemanticTab();
        }

        public event EventHandler<string> CreateFile;
        public event EventHandler<string> OpenFile;
        public event EventHandler<string> SaveFile;
        public event EventHandler<string> StartEnd;
        public event EventHandler Repeat;
        public event EventHandler<string> SaveAsFile;
        public event FormClosingEventHandler CloseProgram;
        public event EventHandler<int> SelectPage;
        public event EventHandler<Operation> ChangeLastUserOperation;
        public event EventHandler NullLastUserOperation;
        public event EventHandler<string> SetNewFileSavedStr;
        public event EventHandler<string> SetNewFileOpenedStr;
        public event EventHandler<string> SetNewFileCreatedStr;
        public event EventHandler<string> ThrowLastTextToModel;
        public event EventHandler<string> ThrowNewTextToModel;
        public event EventHandler CloseCurrentPage;
        public event EventHandler<string> POLIZ;
        private void InitializeSemanticTab()
        {
            semanticTabPage = new TabPage("КЗУ");
            var semanticSplit = new SplitContainer();
            semanticSplit.Dock = DockStyle.Fill;
            semanticSplit.Orientation = Orientation.Horizontal;
            semanticSplit.SplitterDistance = 120;
            semanticSplit.FixedPanel = FixedPanel.Panel1;

            semanticDataGridView = new DataGridView();
            semanticDataGridView.AllowUserToAddRows = false;
            semanticDataGridView.AllowUserToDeleteRows = false;
            semanticDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            semanticDataGridView.BackgroundColor = Color.LightSteelBlue;
            semanticDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            semanticDataGridView.Dock = DockStyle.Fill;
            semanticDataGridView.ReadOnly = true;
            semanticDataGridView.RowHeadersWidth = 51;
            semanticDataGridView.Columns.Add("Rule", "Правило");
            semanticDataGridView.Columns.Add("Status", "Статус");
            semanticDataGridView.Columns.Add("Details", "Сообщение");
            semanticDataGridView.Columns.Add("Position", "Позиция");
            semanticDataGridView.Columns["Rule"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            semanticDataGridView.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            semanticDataGridView.Columns["Details"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            semanticDataGridView.Columns["Position"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            astRichTextBox = new RichTextBox();
            astRichTextBox.Dock = DockStyle.Fill;
            astRichTextBox.ReadOnly = true;
            astRichTextBox.WordWrap = false;
            astRichTextBox.BackColor = Color.WhiteSmoke;
            astRichTextBox.Font = new Font("Consolas", 10);

            semanticSplit.Panel1.Controls.Add(semanticDataGridView);
            semanticSplit.Panel2.Controls.Add(astRichTextBox);
            semanticTabPage.Controls.Add(semanticSplit);
            LowerTabs.Controls.Add(semanticTabPage);
        }



        public void ClearPairs()
        {
            pairsOfPages.Clear();
        }
        public void DeleteDeletedPage()
        {
            int startIndexToFix = PagesCB.SelectedIndex;
            Dictionary<int, int> buffDict = new Dictionary<int, int>();
            pairsOfPages.Remove(PagesCB.SelectedIndex);
            PagesCB.Items.Remove(PagesCB.SelectedItem);
            foreach (var pair in pairsOfPages)
            {
                if (pair.Key > startIndexToFix)
                {
                    buffDict.Add(pair.Key - 1, pair.Value);
                }
                else
                {
                    buffDict.Add(pair.Key, pair.Value);
                }
            }
            pairsOfPages = buffDict;

        }
        //Удаление всех вкладок
        public void DeleteAllPages()
        {
            isEnabledToTextChanged = false;
            UpperRichTextBox.Clear();
            PagesCB.Items.Clear();
            isEnabledToTextChanged = true;
        }
        //Редактирование имени текущей вкладки
        public void EditCurrentPageName(string newName)
        {
            PagesCB.Items[PagesCB.SelectedIndex] = newName;
            PagesCB.Update();
        }
        //Метод установки вкладки
        public void SetPage(int pageId)
        {
            PagesCB.SelectedIndex = pairsOfPages.FirstOrDefault(x => x.Value == pageId).Key;
        }
        //Метод добавления вкладки
        public void AddTab(string fileName, int idOfPage)
        {
            pairsOfPages.Add(PagesCB.Items.Count, idOfPage);
            PagesCB.Items.Add(fileName);
            PagesCB.SelectedIndex = pairsOfPages.FirstOrDefault(x => x.Value == idOfPage).Key;
        }
        //Закрытие вкладки
        private void закрытьВкладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CloseCurrentPage?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Обработка смены вкладки
        private void PagesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PagesCB.Focused)
                {
                    SelectPage?.Invoke(this, pairsOfPages[PagesCB.SelectedIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Обработка ввода комбинаций
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Сохранение
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveButton_Click(this, EventArgs.Empty);
                return true;
            }
            //Вырезать
            else if (keyData == (Keys.Control | Keys.X))
            {
                CutButton_Click(this, EventArgs.Empty);
            }
            //Копировать
            else if (keyData == (Keys.Control | Keys.C))
            {
                CopyButton_Click(this, EventArgs.Empty);
            }
            //Вставить
            else if (keyData == (Keys.Control | Keys.V))
            {
                PasteButton_Click(this, EventArgs.Empty);
            }
            //Открытие файла
            else if (keyData == (Keys.Control | Keys.O))
            {
                FolderButton_Click(this, EventArgs.Empty);
                return true;
            }
            //Выбрать всё
            else if (keyData == (Keys.Control | Keys.A))
            {
                UpperRichTextBox.SelectAll();
            }
            //Отмена
            else if (keyData == (Keys.Control | Keys.Z))
            {
                if (UpperRichTextBox.CanUndo)
                {
                    UpperRichTextBox.Undo();
                }
                return true;
            }
            //Повтор отмененного действия
            else if (keyData == (Keys.Control | Keys.Y))
            {
                if (UpperRichTextBox.CanRedo)
                {
                    UpperRichTextBox.Redo();
                }
                return true;
            }
            //Создание нового файла
            else if (keyData == (Keys.Control | Keys.N))
            {
                FileButton_Click(this, EventArgs.Empty);
            }
            //Закрытие вкладки
            else if (keyData == (Keys.Control | Keys.W))
            {
                try
                {
                    CloseCurrentPage?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Вызов справки
            else if (keyData == Keys.F1)
            {
                OpenHtmlPage("справка");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        ////Блок работы с нумерацией строк
        //Синхронизация скроллинга
        private void SyncScroll()
        {
            int firstIndex = UpperRichTextBox.GetCharIndexFromPosition(new Point(0, 3));
            int firstLine = UpperRichTextBox.GetLineFromCharIndex(firstIndex);

            if (firstLine < NumericLB.Items.Count)
            {
                NumericLB.TopIndex = firstLine;
            }
        }
        //Обновление нумерации
        private void UpdateLineNumbers()
        {
            int totalLines = UpperRichTextBox.Lines.Length;

            NumericLB.BeginUpdate();
            NumericLB.Items.Clear();
            for (int i = 1; i <= totalLines; i++)
            {
                NumericLB.Items.Add(i.ToString());
            }
            NumericLB.EndUpdate();

            SyncScroll();
        }
        //Кнопка перевода на другой шрифт
        private void toolStripComboBox1_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                UpperRichTextBox.Font = new Font("Bahnschrift", float.Parse(toolStripComboBox1.Text));
                ScanerDataGridView.Font = new Font("Bahnschrift", float.Parse(toolStripComboBox1.Text));
                if (semanticDataGridView != null)
                {
                    semanticDataGridView.Font = new Font("Bahnschrift", float.Parse(toolStripComboBox1.Text));
                }
                NumericLB.Font = new Font("Bahnschrift", float.Parse(toolStripComboBox1.Text));
                NumericLB.ItemHeight = int.Parse(toolStripComboBox1.Text);
                UpdateLineNumbers();
                UpperRichTextBox.Update();
                ScanerDataGridView.Update();
                semanticDataGridView?.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ////Блок оповещений
        //Метод для оповещения путём MessageBox
        public void Message(string message)
        {
            MessageBox.Show(message);
        }


        ////Блок "раскидки" текста
        //Метод получения текста
        public string GetText()
        {
            return UpperRichTextBox.Text;
        }
        //Метод установки текста до изменения
        public void SetLastText(string text)
        {
            lastText = text;
        }
        //Метод установки текста в поле для ввода
        public void InsertFileText(string text)
        {
            UpperRichTextBox.Text = text;
        }
        //Метод "повтор в текст"
        public void RepeatToView(Operation operation)
        {
            try
            {
                if (operation.isDelete == false)
                {
                    int position = UpperRichTextBox.SelectionStart;
                    UpperRichTextBox.Text = UpperRichTextBox.Text.Insert(UpperRichTextBox.SelectionStart, operation.containtment);
                    lastText = UpperRichTextBox.Text;
                    UpperRichTextBox.Focus();
                    UpperRichTextBox.SelectionStart = position + operation.containtment.Length;
                }
                else
                {
                    if (UpperRichTextBox.SelectionStart > 0)
                    {
                        int position = UpperRichTextBox.SelectionStart;
                        UpperRichTextBox.Text = UpperRichTextBox.Text.Remove(position - 1, 1);
                        lastText = UpperRichTextBox.Text;
                        UpperRichTextBox.Focus();
                        UpperRichTextBox.SelectionStart = position - operation.containtment.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Отмена
        private void LeftButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpperRichTextBox.CanUndo)
                {
                    UpperRichTextBox.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Повтор
        private void RightButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (UpperRichTextBox.CanRedo)
                {
                    UpperRichTextBox.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Копирование
        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpperRichTextBox.Copy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Вырезать
        private void CutButton_Click(object sender, EventArgs e)
        {
            try
            {
                isCutEnabled = true;
                UpperRichTextBox.Cut();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Вставить
        private void PasteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    UpperRichTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Обработка кнопки "Удалить"
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UpperRichTextBox.SelectedText.Length > 0)
            {
                UpperRichTextBox.SelectedText = "";
            }
        }
        //Обработка кнопки "Выделить всё"
        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpperRichTextBox.SelectAll();
        }
        //Обработка ввода текста
        private void UpperRichTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isEnabledToTextChanged) return;

                string newText = UpperRichTextBox.Text;
                ThrowNewTextToModel?.Invoke(this, newText);
                if (newText.Length > lastText.Length) // Ввод символа
                {
                    int diff = newText.Length - lastText.Length;
                    int startDiff = 0;
                    while (startDiff < lastText.Length &&
                           startDiff < newText.Length &&
                           lastText[startDiff] == newText[startDiff])
                    {
                        startDiff++;
                    }

                    string insertedText = newText.Substring(startDiff, diff);
                    if (diff == 1)
                    {
                        ChangeLastUserOperation?.Invoke(this, new Operation(insertedText, false, false, false));
                    }
                    else
                    {
                        ChangeLastUserOperation?.Invoke(this, new Operation(insertedText, false, false, true));
                    }
                }
                else if (newText.Length < lastText.Length) // Удаление символа
                {
                    int diff = lastText.Length - newText.Length;
                    int startDiff = 0;
                    while (startDiff < lastText.Length &&
                           startDiff < newText.Length &&
                           lastText[startDiff] == newText[startDiff])
                    {
                        startDiff++;
                    }

                    string deletedText = lastText.Substring(startDiff, diff);
                    if (diff == 1 && isCutEnabled == false)
                    {
                        ChangeLastUserOperation?.Invoke(this, new Operation(deletedText, false, true, false));
                    }
                    else
                    {
                        NullLastUserOperation?.Invoke(this, EventArgs.Empty);
                        isCutEnabled = false;
                    }
                }
                int firstVisibleLine = UpperRichTextBox.GetLineFromCharIndex(UpperRichTextBox.GetCharIndexFromPosition(new Point(0, 0)));
                lastText = newText;
                UpperRichTextBox.Focus();
                ThrowLastTextToModel?.Invoke(this, lastText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ////Блок перевода
        //Метод для перевода интерфейса
        private void UpdateControlsText(Control control, ResourceManager res)
        {
            ScanerDataGridView.Columns[0].HeaderText = res.GetString("Mess");
            ScanerDataGridView.Columns[1].HeaderText = res.GetString("MessageColumn");
            _presenter.CloseProgramStr = res.GetString("CloseProgramStr");
            _presenter.CloseSavedStr = res.GetString("CloseSavedStr");
            _presenter.CloseUnsavedStr = res.GetString("CloseUnsavedStr");
            _presenter.MessageNoPathStr = res.GetString("MessageNoPathStr");
            _presenter.SaveNoPathStr = res.GetString("SaveNoPathStr");
            TabLabel.Text = res.GetString("TabLabel");
            SetNewFileSavedStr?.Invoke(this, res.GetString("FileSaved"));
            SetNewFileOpenedStr?.Invoke(this, res.GetString("FileOpened"));
            SetNewFileCreatedStr?.Invoke(this, res.GetString("FileCreated"));

            foreach (var item in this.MainMenuStrip.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    UpdateMenuItems(menuItem, res);
                }
            }
        }
        //Метод обновления текста MenuStrip
        private void UpdateMenuItems(ToolStripMenuItem menuItem, ResourceManager res)
        {
            if (!string.IsNullOrEmpty(menuItem.Name))
            {
                string newText = res.GetString(menuItem.Name);
                if (!string.IsNullOrEmpty(newText))
                    menuItem.Text = newText;
            }


            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    UpdateMenuItems(subMenuItem, res);
                }
            }
        }
        //Метод для установки\удаления флажка "* " при изменении\сохранении
        public void SetFlagToComboBoxItem(int pageToEditId, bool flag)
        {
            if (flag)
            {
                string currentPageName = PagesCB.Items[pairsOfPages.FirstOrDefault(x => x.Value == pageToEditId).Key].ToString();
                if (currentPageName.StartsWith("* "))
                {
                    PagesCB.Items[pairsOfPages.FirstOrDefault(x => x.Value == pageToEditId).Key] = PagesCB.Items[pairsOfPages.FirstOrDefault(x => x.Value == pageToEditId).Key].ToString().Remove(0, 2);
                }
            }
            else
            {
                string currentPageName = PagesCB.Items[pairsOfPages.FirstOrDefault(x => x.Value == pageToEditId).Key].ToString();
                if (!currentPageName.StartsWith("* "))
                {
                    PagesCB.Items[pairsOfPages.FirstOrDefault(x => x.Value == pageToEditId).Key] = "* " + currentPageName;
                }
            }
        }
        //Выбор русского языка
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
                _presenter._res = new ResourceManager("CompilerV2.Resources.Resource_ru", typeof(MainForm).Assembly);
                UpdateControlsText(this, _presenter._res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Выбор английского языка
        private void английскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                _presenter._res = new ResourceManager("CompilerV2.Resources.Resource_en", typeof(MainForm).Assembly);
                UpdateControlsText(this, _presenter._res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ////Блок работы с файлом
        //Создание файла
        private void FileButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog.FileName;
                CreateFile?.Invoke(this, saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Открытие готового файла
        private void FolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Текстовые файлы(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog.FileName;
                isEnabledToTextChanged = false;
                OpenFile?.Invoke(this, filename);
                isEnabledToTextChanged = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Сохранение файла
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFile?.Invoke(this, UpperRichTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Сохранить как
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsFile?.Invoke(this, UpperRichTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ////Блок "не для первой лабы"
        //Пуск
        private void StartEndButton_Click(object sender, EventArgs e)
        {
            try
            {
                StartEnd?.Invoke(this, UpperRichTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Обработка закрытия программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseProgram?.Invoke(this, e);
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        ////Блок обработки Drag&Drop
        //Обработка Drag&Drop
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Обработка Drag&Drop
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    if (File.Exists(filePath))
                    {
                        OpenFile?.Invoke(this, filePath);
                    }
                    else
                    {
                        MessageBox.Show("Невозможно открыть файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        ////Блок информации о сканировании
        //Заполнение ScanerDGV
        public void FillScanerDGV(List<Lexem> lexemsList)
        {
            ScanerDataGridView.Rows.Clear();
            foreach (var item in lexemsList)
            {
                ScanerDataGridView.Rows.Add(item.lexemCode,item.lexemName,item.lexemContaintment,item.lexemStartPosition+1 + "-"+item.lexemEndPosition);
            }
        }
        public void FillErrorsDGV(List<ErrorPair> errorsList)
        {
            ScanerDataGridView.Rows.Clear();
            foreach (var item in errorsList)
            {
                ScanerDataGridView.Rows.Add(item.errorMessage, $"{item.posStart} - {item.posEnd}");
            }
        }

        public bool IsSemanticTabSelected()
        {
            return LowerTabs.SelectedTab == semanticTabPage;
        }

        public void FillSemanticTable(List<SemanticCheckRow> rows)
        {
            if (semanticDataGridView == null)
            {
                return;
            }

            semanticDataGridView.Rows.Clear();
            foreach (var row in rows)
            {
                semanticDataGridView.Rows.Add(row.Rule, row.Status, row.Details, row.Position);
            }
        }

        public void FillAstOutput(string astText)
        {
            if (astRichTextBox == null)
            {
                return;
            }

            astRichTextBox.Text = astText ?? string.Empty;
        }

        //public void FillPOLIZ(List<string> messagesList)
        //{
        //    POLIZDGV.Rows.Clear();
        //    foreach (string msg in messagesList)
        //    {
        //        POLIZDGV.Rows.Add(msg);
        //    }
        //}

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("справка");
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("о_программе");
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("исходный_код");
        }

        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("список_источников");
        }

        private void тестовыйПримерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("тестовые_примеры");
        }

        private void диагностикаИНейтрализацияОшибокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("диагностика_и_нейтрализация");
        }

        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("метод_анализа");
        }

        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("классификация_грамматики");
        }

        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("грамматика");
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHtmlPage("постановка_задачи");
        }

        private void POLIZBtn_Click(object sender, EventArgs e)
        {
            POLIZ?.Invoke(this, UpperRichTextBox.Text);
        }

        private void OpenHtmlPage(string pageBaseName)
        {
            try
            {
                string localizedFileName = ResolveLocalizedHtmlName(pageBaseName);
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View", localizedFileName);

                if (!File.Exists(htmlPath))
                {
                    MessageBox.Show($"HTML-файл не найден: {htmlPath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = htmlPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ResolveLocalizedHtmlName(string pageBaseName)
        {
            string viewDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View");
            string cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            string twoLetterName = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            string[] candidateNames = new[]
            {
                $"{pageBaseName}.{cultureName}.html",
                $"{pageBaseName}.{twoLetterName}.html",
                $"{pageBaseName}_{cultureName}.html",
                $"{pageBaseName}_{twoLetterName}.html",
                $"{pageBaseName}.html"
            };

            foreach (string candidateName in candidateNames)
            {
                if (File.Exists(Path.Combine(viewDir, candidateName)))
                {
                    return candidateName;
                }
            }

            return $"{pageBaseName}.html";
        }
    }
}
