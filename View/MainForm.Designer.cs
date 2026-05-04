using System.Drawing;
using System.Windows.Forms;

namespace CompilerV2.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.файоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выделитьВсёToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьВкладкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.текстToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.постановкаЗадачиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.грамматикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.классификацияГрамматикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.методАнализаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тестовыйПримерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЛитературыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исходныйКодПрограммыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.пускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вызовСправкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.языкПрограммыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.русскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.английскийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.PagesCB = new System.Windows.Forms.ComboBox();
            this.TabLabel = new System.Windows.Forms.Label();
            this.InfoButton = new System.Windows.Forms.Button();
            this.QuestionButton = new System.Windows.Forms.Button();
            this.StartEndButton = new System.Windows.Forms.Button();
            this.PasteButton = new System.Windows.Forms.Button();
            this.CutButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FolderButton = new System.Windows.Forms.Button();
            this.FileButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.NumericLB = new System.Windows.Forms.ListBox();
            this.UpperRichTextBox = new System.Windows.Forms.RichTextBox();
            this.LowerTabs = new System.Windows.Forms.TabControl();
            this.ScanPage = new System.Windows.Forms.TabPage();
            this.ScanerDataGridView = new System.Windows.Forms.DataGridView();
            this.Parser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainMenu.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.LowerTabs.SuspendLayout();
            this.ScanPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanerDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.MainMenu.Font = new System.Drawing.Font("Bahnschrift", 14.25F);
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файоToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.текстToolStripMenuItem,
            this.пускToolStripMenuItem,
            this.справкToolStripMenuItem,
            this.языкПрограммыToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(999, 34);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Главное меню";
            // 
            // файоToolStripMenuItem
            // 
            this.файоToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.файоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файоToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.файоToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.файоToolStripMenuItem.Name = "файоToolStripMenuItem";
            this.файоToolStripMenuItem.Size = new System.Drawing.Size(77, 30);
            this.файоToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.создатьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.создатьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.открытьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.открытьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.сохранитьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.сохранитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.сохранитьКакToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.сохранитьКакToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.выходToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.выходToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменитьToolStripMenuItem,
            this.повторитьToolStripMenuItem,
            this.вырезатьToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.вставитьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.выделитьВсёToolStripMenuItem,
            this.закрытьВкладкуToolStripMenuItem});
            this.правкаToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.правкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(96, 30);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменитьToolStripMenuItem
            // 
            this.отменитьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.отменитьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.отменитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.отменитьToolStripMenuItem.Text = "Отменить";
            this.отменитьToolStripMenuItem.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // повторитьToolStripMenuItem
            // 
            this.повторитьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.повторитьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.повторитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            this.повторитьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.повторитьToolStripMenuItem.Text = "Повторить";
            this.повторитьToolStripMenuItem.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.вырезатьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.вырезатьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            this.вырезатьToolStripMenuItem.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.копироватьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.копироватьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.вставитьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.вставитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.удалитьToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.удалитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // выделитьВсёToolStripMenuItem
            // 
            this.выделитьВсёToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.выделитьВсёToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.выделитьВсёToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.выделитьВсёToolStripMenuItem.Name = "выделитьВсёToolStripMenuItem";
            this.выделитьВсёToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.выделитьВсёToolStripMenuItem.Text = "Выделить всё";
            this.выделитьВсёToolStripMenuItem.Click += new System.EventHandler(this.выделитьВсёToolStripMenuItem_Click);
            // 
            // закрытьВкладкуToolStripMenuItem
            // 
            this.закрытьВкладкуToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.закрытьВкладкуToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.закрытьВкладкуToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.закрытьВкладкуToolStripMenuItem.Name = "закрытьВкладкуToolStripMenuItem";
            this.закрытьВкладкуToolStripMenuItem.Size = new System.Drawing.Size(238, 30);
            this.закрытьВкладкуToolStripMenuItem.Text = "Закрыть файл";
            this.закрытьВкладкуToolStripMenuItem.Click += new System.EventHandler(this.закрытьВкладкуToolStripMenuItem_Click);
            // 
            // текстToolStripMenuItem
            // 
            this.текстToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.текстToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.постановкаЗадачиToolStripMenuItem,
            this.грамматикаToolStripMenuItem,
            this.классификацияГрамматикиToolStripMenuItem,
            this.методАнализаToolStripMenuItem,
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem,
            this.тестовыйПримерToolStripMenuItem,
            this.списокЛитературыToolStripMenuItem,
            this.исходныйКодПрограммыToolStripMenuItem,
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem});
            this.текстToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.текстToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.текстToolStripMenuItem.Name = "текстToolStripMenuItem";
            this.текстToolStripMenuItem.Size = new System.Drawing.Size(83, 30);
            this.текстToolStripMenuItem.Text = "Текст";
            // 
            // постановкаЗадачиToolStripMenuItem
            // 
            this.постановкаЗадачиToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.постановкаЗадачиToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.постановкаЗадачиToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.постановкаЗадачиToolStripMenuItem.Name = "постановкаЗадачиToolStripMenuItem";
            this.постановкаЗадачиToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.постановкаЗадачиToolStripMenuItem.Text = "Постановка задачи";
            this.постановкаЗадачиToolStripMenuItem.Click += new System.EventHandler(this.постановкаЗадачиToolStripMenuItem_Click);
            // 
            // грамматикаToolStripMenuItem
            // 
            this.грамматикаToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.грамматикаToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.грамматикаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.грамматикаToolStripMenuItem.Name = "грамматикаToolStripMenuItem";
            this.грамматикаToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.грамматикаToolStripMenuItem.Text = "Грамматика";
            this.грамматикаToolStripMenuItem.Click += new System.EventHandler(this.грамматикаToolStripMenuItem_Click);
            // 
            // классификацияГрамматикиToolStripMenuItem
            // 
            this.классификацияГрамматикиToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.классификацияГрамматикиToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.классификацияГрамматикиToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.классификацияГрамматикиToolStripMenuItem.Name = "классификацияГрамматикиToolStripMenuItem";
            this.классификацияГрамматикиToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.классификацияГрамматикиToolStripMenuItem.Text = "Классификация грамматики";
            this.классификацияГрамматикиToolStripMenuItem.Click += new System.EventHandler(this.классификацияГрамматикиToolStripMenuItem_Click);
            // 
            // методАнализаToolStripMenuItem
            // 
            this.методАнализаToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.методАнализаToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.методАнализаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.методАнализаToolStripMenuItem.Name = "методАнализаToolStripMenuItem";
            this.методАнализаToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.методАнализаToolStripMenuItem.Text = "Метод анализа";
            this.методАнализаToolStripMenuItem.Click += new System.EventHandler(this.методАнализаToolStripMenuItem_Click);
            // 
            // диагностикаИНейтрализацияОшибокToolStripMenuItem
            // 
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.Name = "диагностикаИНейтрализацияОшибокToolStripMenuItem";
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.Text = "Диагностика и нейтрализация ошибок";
            this.диагностикаИНейтрализацияОшибокToolStripMenuItem.Click += new System.EventHandler(this.диагностикаИНейтрализацияОшибокToolStripMenuItem_Click);
            // 
            // тестовыйПримерToolStripMenuItem
            // 
            this.тестовыйПримерToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.тестовыйПримерToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.тестовыйПримерToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.тестовыйПримерToolStripMenuItem.Name = "тестовыйПримерToolStripMenuItem";
            this.тестовыйПримерToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.тестовыйПримерToolStripMenuItem.Text = "Тестовый пример";
            this.тестовыйПримерToolStripMenuItem.Click += new System.EventHandler(this.тестовыйПримерToolStripMenuItem_Click);
            // 
            // списокЛитературыToolStripMenuItem
            // 
            this.списокЛитературыToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.списокЛитературыToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.списокЛитературыToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.списокЛитературыToolStripMenuItem.Name = "списокЛитературыToolStripMenuItem";
            this.списокЛитературыToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.списокЛитературыToolStripMenuItem.Text = "Список литературы";
            this.списокЛитературыToolStripMenuItem.Click += new System.EventHandler(this.списокЛитературыToolStripMenuItem_Click);
            // 
            // исходныйКодПрограммыToolStripMenuItem
            // 
            this.исходныйКодПрограммыToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.исходныйКодПрограммыToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.исходныйКодПрограммыToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.исходныйКодПрограммыToolStripMenuItem.Name = "исходныйКодПрограммыToolStripMenuItem";
            this.исходныйКодПрограммыToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.исходныйКодПрограммыToolStripMenuItem.Text = "Исходный код программы";
            this.исходныйКодПрограммыToolStripMenuItem.Click += new System.EventHandler(this.исходныйКодПрограммыToolStripMenuItem_Click);
            // 
            // размерШрифтаВОкнеВыводавводаToolStripMenuItem
            // 
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.Name = "размерШрифтаВОкнеВыводавводаToolStripMenuItem";
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.Size = new System.Drawing.Size(481, 30);
            this.размерШрифтаВОкнеВыводавводаToolStripMenuItem.Text = "Размер шрифта в окне вывода\\ввода";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.BackColor = System.Drawing.Color.DimGray;
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
            this.toolStripComboBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "12",
            "14",
            "16",
            "18",
            "20"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 41);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_TextUpdate);
            this.toolStripComboBox1.TextUpdate += new System.EventHandler(this.toolStripComboBox1_TextUpdate);
            // 
            // пускToolStripMenuItem
            // 
            this.пускToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.пускToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.пускToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.пускToolStripMenuItem.Name = "пускToolStripMenuItem";
            this.пускToolStripMenuItem.Size = new System.Drawing.Size(71, 30);
            this.пускToolStripMenuItem.Text = "Пуск";
            this.пускToolStripMenuItem.Click += new System.EventHandler(this.StartEndButton_Click);
            // 
            // справкToolStripMenuItem
            // 
            this.справкToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.справкToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.вызовСправкиToolStripMenuItem});
            this.справкToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.справкToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.справкToolStripMenuItem.Name = "справкToolStripMenuItem";
            this.справкToolStripMenuItem.Size = new System.Drawing.Size(108, 30);
            this.справкToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.оПрограммеToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.оПрограммеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // вызовСправкиToolStripMenuItem
            // 
            this.вызовСправкиToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.вызовСправкиToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.вызовСправкиToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.вызовСправкиToolStripMenuItem.Name = "вызовСправкиToolStripMenuItem";
            this.вызовСправкиToolStripMenuItem.Size = new System.Drawing.Size(247, 30);
            this.вызовСправкиToolStripMenuItem.Text = "Вызов справки";
            this.вызовСправкиToolStripMenuItem.Click += new System.EventHandler(this.вызовСправкиToolStripMenuItem_Click);
            // 
            // языкПрограммыToolStripMenuItem
            // 
            this.языкПрограммыToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.языкПрограммыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.русскийToolStripMenuItem,
            this.английскийToolStripMenuItem});
            this.языкПрограммыToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.языкПрограммыToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.языкПрограммыToolStripMenuItem.Name = "языкПрограммыToolStripMenuItem";
            this.языкПрограммыToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
            this.языкПрограммыToolStripMenuItem.Text = "Язык программы";
            this.языкПрограммыToolStripMenuItem.Visible = false;
            // 
            // русскийToolStripMenuItem
            // 
            this.русскийToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.русскийToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.русскийToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.русскийToolStripMenuItem.Name = "русскийToolStripMenuItem";
            this.русскийToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.русскийToolStripMenuItem.Text = "Русский";
            this.русскийToolStripMenuItem.Click += new System.EventHandler(this.русскийToolStripMenuItem_Click);
            // 
            // английскийToolStripMenuItem
            // 
            this.английскийToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.английскийToolStripMenuItem.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.английскийToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.английскийToolStripMenuItem.Name = "английскийToolStripMenuItem";
            this.английскийToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.английскийToolStripMenuItem.Text = "Английский";
            this.английскийToolStripMenuItem.Click += new System.EventHandler(this.английскийToolStripMenuItem_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.White;
            this.TopPanel.Controls.Add(this.PagesCB);
            this.TopPanel.Controls.Add(this.TabLabel);
            this.TopPanel.Controls.Add(this.InfoButton);
            this.TopPanel.Controls.Add(this.QuestionButton);
            this.TopPanel.Controls.Add(this.StartEndButton);
            this.TopPanel.Controls.Add(this.PasteButton);
            this.TopPanel.Controls.Add(this.CutButton);
            this.TopPanel.Controls.Add(this.CopyButton);
            this.TopPanel.Controls.Add(this.RightButton);
            this.TopPanel.Controls.Add(this.LeftButton);
            this.TopPanel.Controls.Add(this.SaveButton);
            this.TopPanel.Controls.Add(this.FolderButton);
            this.TopPanel.Controls.Add(this.FileButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 34);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(999, 69);
            this.TopPanel.TabIndex = 2;
            // 
            // PagesCB
            // 
            this.PagesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PagesCB.BackColor = System.Drawing.Color.White;
            this.PagesCB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PagesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PagesCB.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.PagesCB.FormattingEnabled = true;
            this.PagesCB.Location = new System.Drawing.Point(543, 31);
            this.PagesCB.Margin = new System.Windows.Forms.Padding(4);
            this.PagesCB.MaximumSize = new System.Drawing.Size(571, 0);
            this.PagesCB.Name = "PagesCB";
            this.PagesCB.Size = new System.Drawing.Size(249, 32);
            this.PagesCB.TabIndex = 11;
            this.PagesCB.SelectedIndexChanged += new System.EventHandler(this.PagesCB_SelectedIndexChanged);
            // 
            // TabLabel
            // 
            this.TabLabel.AutoSize = true;
            this.TabLabel.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Bold);
            this.TabLabel.Location = new System.Drawing.Point(543, 7);
            this.TabLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TabLabel.Name = "TabLabel";
            this.TabLabel.Size = new System.Drawing.Size(120, 26);
            this.TabLabel.TabIndex = 4;
            this.TabLabel.Text = "Недавние:";
            // 
            // InfoButton
            // 
            this.InfoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoButton.BackColor = System.Drawing.Color.Transparent;
            this.InfoButton.BackgroundImage = global::CompilerV2.Properties.Resources.info_icon;
            this.InfoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoButton.Location = new System.Drawing.Point(928, 7);
            this.InfoButton.Margin = new System.Windows.Forms.Padding(4);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(57, 53);
            this.InfoButton.TabIndex = 10;
            this.InfoButton.UseVisualStyleBackColor = false;
            this.InfoButton.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // QuestionButton
            // 
            this.QuestionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestionButton.BackColor = System.Drawing.Color.Transparent;
            this.QuestionButton.BackgroundImage = global::CompilerV2.Properties.Resources.question_icon;
            this.QuestionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.QuestionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QuestionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QuestionButton.Location = new System.Drawing.Point(864, 7);
            this.QuestionButton.Margin = new System.Windows.Forms.Padding(4);
            this.QuestionButton.Name = "QuestionButton";
            this.QuestionButton.Size = new System.Drawing.Size(57, 53);
            this.QuestionButton.TabIndex = 9;
            this.QuestionButton.UseVisualStyleBackColor = false;
            this.QuestionButton.Click += new System.EventHandler(this.вызовСправкиToolStripMenuItem_Click);
            // 
            // StartEndButton
            // 
            this.StartEndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartEndButton.BackColor = System.Drawing.Color.Transparent;
            this.StartEndButton.BackgroundImage = global::CompilerV2.Properties.Resources.start_icon;
            this.StartEndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StartEndButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartEndButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartEndButton.Location = new System.Drawing.Point(800, 7);
            this.StartEndButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartEndButton.Name = "StartEndButton";
            this.StartEndButton.Size = new System.Drawing.Size(57, 53);
            this.StartEndButton.TabIndex = 8;
            this.StartEndButton.UseVisualStyleBackColor = false;
            this.StartEndButton.Click += new System.EventHandler(this.StartEndButton_Click);
            // 
            // PasteButton
            // 
            this.PasteButton.BackColor = System.Drawing.Color.Transparent;
            this.PasteButton.BackgroundImage = global::CompilerV2.Properties.Resources.paste_icon;
            this.PasteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PasteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasteButton.Location = new System.Drawing.Point(479, 7);
            this.PasteButton.Margin = new System.Windows.Forms.Padding(4);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(57, 53);
            this.PasteButton.TabIndex = 7;
            this.PasteButton.UseVisualStyleBackColor = false;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // CutButton
            // 
            this.CutButton.BackColor = System.Drawing.Color.Transparent;
            this.CutButton.BackgroundImage = global::CompilerV2.Properties.Resources.cut_icon;
            this.CutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CutButton.Location = new System.Drawing.Point(415, 7);
            this.CutButton.Margin = new System.Windows.Forms.Padding(4);
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(57, 53);
            this.CutButton.TabIndex = 6;
            this.CutButton.UseVisualStyleBackColor = false;
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.Color.Transparent;
            this.CopyButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CopyButton.BackgroundImage")));
            this.CopyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CopyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyButton.Location = new System.Drawing.Point(351, 7);
            this.CopyButton.Margin = new System.Windows.Forms.Padding(4);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(57, 53);
            this.CopyButton.TabIndex = 5;
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // RightButton
            // 
            this.RightButton.BackColor = System.Drawing.Color.Transparent;
            this.RightButton.BackgroundImage = global::CompilerV2.Properties.Resources.right_icon;
            this.RightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RightButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RightButton.Location = new System.Drawing.Point(287, 7);
            this.RightButton.Margin = new System.Windows.Forms.Padding(4);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(56, 53);
            this.RightButton.TabIndex = 4;
            this.RightButton.UseVisualStyleBackColor = false;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // LeftButton
            // 
            this.LeftButton.BackColor = System.Drawing.Color.Transparent;
            this.LeftButton.BackgroundImage = global::CompilerV2.Properties.Resources.left_icon;
            this.LeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LeftButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftButton.Location = new System.Drawing.Point(223, 7);
            this.LeftButton.Margin = new System.Windows.Forms.Padding(4);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(57, 53);
            this.LeftButton.TabIndex = 3;
            this.LeftButton.UseVisualStyleBackColor = false;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.BackgroundImage = global::CompilerV2.Properties.Resources.save_file_icon;
            this.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(144, 7);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(57, 53);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FolderButton
            // 
            this.FolderButton.BackColor = System.Drawing.Color.Transparent;
            this.FolderButton.BackgroundImage = global::CompilerV2.Properties.Resources.file_in_folder_icon;
            this.FolderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FolderButton.Location = new System.Drawing.Point(80, 7);
            this.FolderButton.Margin = new System.Windows.Forms.Padding(4);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(57, 53);
            this.FolderButton.TabIndex = 1;
            this.FolderButton.UseVisualStyleBackColor = false;
            this.FolderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // FileButton
            // 
            this.FileButton.BackColor = System.Drawing.Color.Transparent;
            this.FileButton.BackgroundImage = global::CompilerV2.Properties.Resources.file_icon;
            this.FileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileButton.Location = new System.Drawing.Point(16, 7);
            this.FileButton.Margin = new System.Windows.Forms.Padding(4);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(57, 53);
            this.FileButton.TabIndex = 0;
            this.FileButton.UseVisualStyleBackColor = false;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MainPanel.Controls.Add(this.splitContainer1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 103);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(999, 522);
            this.MainPanel.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.NumericLB);
            this.splitContainer1.Panel1.Controls.Add(this.UpperRichTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LowerTabs);
            this.splitContainer1.Size = new System.Drawing.Size(999, 522);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 4;
            // 
            // NumericLB
            // 
            this.NumericLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NumericLB.BackColor = System.Drawing.Color.RoyalBlue;
            this.NumericLB.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumericLB.ForeColor = System.Drawing.SystemColors.Control;
            this.NumericLB.FormattingEnabled = true;
            this.NumericLB.IntegralHeight = false;
            this.NumericLB.ItemHeight = 24;
            this.NumericLB.Location = new System.Drawing.Point(5, 4);
            this.NumericLB.Margin = new System.Windows.Forms.Padding(4);
            this.NumericLB.Name = "NumericLB";
            this.NumericLB.Size = new System.Drawing.Size(59, 221);
            this.NumericLB.TabIndex = 3;
            // 
            // UpperRichTextBox
            // 
            this.UpperRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperRichTextBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.UpperRichTextBox.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpperRichTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.UpperRichTextBox.Location = new System.Drawing.Point(63, 4);
            this.UpperRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.UpperRichTextBox.Name = "UpperRichTextBox";
            this.UpperRichTextBox.ShortcutsEnabled = false;
            this.UpperRichTextBox.Size = new System.Drawing.Size(932, 221);
            this.UpperRichTextBox.TabIndex = 0;
            this.UpperRichTextBox.Text = "";
            this.UpperRichTextBox.WordWrap = false;
            this.UpperRichTextBox.TextChanged += new System.EventHandler(this.UpperRichTextBox_TextChanged);
            // 
            // LowerTabs
            // 
            this.LowerTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LowerTabs.Controls.Add(this.ScanPage);
            this.LowerTabs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LowerTabs.Font = new System.Drawing.Font("Bahnschrift", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LowerTabs.Location = new System.Drawing.Point(0, 0);
            this.LowerTabs.Margin = new System.Windows.Forms.Padding(4);
            this.LowerTabs.Name = "LowerTabs";
            this.LowerTabs.SelectedIndex = 0;
            this.LowerTabs.Size = new System.Drawing.Size(999, 233);
            this.LowerTabs.TabIndex = 1;
            // 
            // ScanPage
            // 
            this.ScanPage.Controls.Add(this.ScanerDataGridView);
            this.ScanPage.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScanPage.Location = new System.Drawing.Point(4, 31);
            this.ScanPage.Margin = new System.Windows.Forms.Padding(4);
            this.ScanPage.Name = "ScanPage";
            this.ScanPage.Padding = new System.Windows.Forms.Padding(4);
            this.ScanPage.Size = new System.Drawing.Size(991, 198);
            this.ScanPage.TabIndex = 0;
            this.ScanPage.Text = "Парсер";
            this.ScanPage.UseVisualStyleBackColor = true;
            // 
            // ScanerDataGridView
            // 
            this.ScanerDataGridView.AllowUserToAddRows = false;
            this.ScanerDataGridView.AllowUserToDeleteRows = false;
            this.ScanerDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ScanerDataGridView.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.ScanerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScanerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Parser,
            this.Location});
            this.ScanerDataGridView.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ScanerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScanerDataGridView.Location = new System.Drawing.Point(4, 4);
            this.ScanerDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.ScanerDataGridView.Name = "ScanerDataGridView";
            this.ScanerDataGridView.ReadOnly = true;
            this.ScanerDataGridView.RowHeadersWidth = 51;
            this.ScanerDataGridView.Size = new System.Drawing.Size(983, 190);
            this.ScanerDataGridView.TabIndex = 0;
            // 
            // Parser
            // 
            this.Parser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Parser.HeaderText = "Результаты сканирования";
            this.Parser.MinimumWidth = 6;
            this.Parser.Name = "Parser";
            this.Parser.ReadOnly = true;
            // 
            // Location
            // 
            this.Location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Location.HeaderText = "Местоположение";
            this.Location.MinimumWidth = 6;
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // MessageColumn
            // 
            this.MessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MessageColumn.HeaderText = "Местоположение";
            this.MessageColumn.MinimumWidth = 6;
            this.MessageColumn.Name = "MessageColumn";
            this.MessageColumn.ReadOnly = true;
            this.MessageColumn.Width = 125;
            // 
            // Mess
            // 
            this.Mess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Mess.HeaderText = "Сообщение";
            this.Mess.MinimumWidth = 6;
            this.Mess.Name = "Mess";
            this.Mess.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(999, 625);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(925, 590);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Компиляторный редактор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.LowerTabs.ResumeLayout(false);
            this.ScanPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScanerDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MainMenu;
        private ToolStripMenuItem файоToolStripMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private ToolStripMenuItem текстToolStripMenuItem;
        private ToolStripMenuItem пускToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem отменитьToolStripMenuItem;
        private ToolStripMenuItem повторитьToolStripMenuItem;
        private ToolStripMenuItem вырезатьToolStripMenuItem;
        private ToolStripMenuItem копироватьToolStripMenuItem;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem выделитьВсёToolStripMenuItem;
        private ToolStripMenuItem постановкаЗадачиToolStripMenuItem;
        private ToolStripMenuItem грамматикаToolStripMenuItem;
        private ToolStripMenuItem классификацияГрамматикиToolStripMenuItem;
        private ToolStripMenuItem методАнализаToolStripMenuItem;
        private ToolStripMenuItem диагностикаИНейтрализацияОшибокToolStripMenuItem;
        private ToolStripMenuItem тестовыйПримерToolStripMenuItem;
        private ToolStripMenuItem списокЛитературыToolStripMenuItem;
        private ToolStripMenuItem исходныйКодПрограммыToolStripMenuItem;
        private ToolStripMenuItem справкToolStripMenuItem;
        private ToolStripMenuItem вызовСправкиToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private Panel TopPanel;
        private Panel MainPanel;
        private Button FileButton;
        private Button PasteButton;
        private Button CutButton;
        private Button CopyButton;
        private Button RightButton;
        private Button LeftButton;
        private Button SaveButton;
        private Button FolderButton;
        private Button InfoButton;
        private Button QuestionButton;
        private Button StartEndButton;
        private ToolStripMenuItem размерШрифтаВОкнеВыводавводаToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox1;
        public RichTextBox UpperRichTextBox;
        private Label TabLabel;
        private ComboBox PagesCB;
        private ToolStripMenuItem языкПрограммыToolStripMenuItem;
        private ToolStripMenuItem русскийToolStripMenuItem;
        private ToolStripMenuItem английскийToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ListBox NumericLB;
        private ToolStripMenuItem закрытьВкладкуToolStripMenuItem;
        private TabControl LowerTabs;
        private TabPage ScanPage;
        private DataGridView ScanerDataGridView;
        private DataGridViewTextBoxColumn MessageColumn;
        private DataGridViewTextBoxColumn Mess;
        private DataGridViewTextBoxColumn Parser;
        private DataGridViewTextBoxColumn Location;
    }
}