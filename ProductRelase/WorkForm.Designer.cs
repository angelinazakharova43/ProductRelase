namespace ProductRelase
{
    partial class WorkForm
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
            label1 = new Label();
            CloseBtn = new Button();
            btnLogChange = new Button();
            filePage = new TabPage();
            btnCreateReport = new Button();
            btnFindLine = new Button();
            btnDeleteLine = new Button();
            btnChangeLine = new Button();
            btnAddLine = new Button();
            lstBoxTables = new ListBox();
            lblGetTable = new Label();
            dataGridView1 = new DataGridView();
            tabCon = new TabControl();
            filePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabCon.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(98, 26);
            label1.Name = "label1";
            label1.Size = new Size(0, 37);
            label1.TabIndex = 0;
            // 
            // CloseBtn
            // 
            CloseBtn.Location = new Point(619, 463);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(171, 25);
            CloseBtn.TabIndex = 6;
            CloseBtn.Text = "Выход из приложения";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // btnLogChange
            // 
            btnLogChange.Location = new Point(442, 463);
            btnLogChange.Name = "btnLogChange";
            btnLogChange.Size = new Size(171, 25);
            btnLogChange.TabIndex = 8;
            btnLogChange.Text = "Сменить учетную запись";
            btnLogChange.UseVisualStyleBackColor = true;
            btnLogChange.Visible = false;
            btnLogChange.Click += btnLogChange_Click;
            // 
            // filePage
            // 
            filePage.Controls.Add(btnCreateReport);
            filePage.Controls.Add(btnFindLine);
            filePage.Controls.Add(btnDeleteLine);
            filePage.Controls.Add(btnChangeLine);
            filePage.Controls.Add(btnAddLine);
            filePage.Controls.Add(lstBoxTables);
            filePage.Controls.Add(lblGetTable);
            filePage.Controls.Add(dataGridView1);
            filePage.Location = new Point(4, 24);
            filePage.Name = "filePage";
            filePage.Padding = new Padding(3);
            filePage.Size = new Size(770, 421);
            filePage.TabIndex = 1;
            filePage.Text = "Файл";
            filePage.UseVisualStyleBackColor = true;
            // 
            // btnCreateReport
            // 
            btnCreateReport.Enabled = false;
            btnCreateReport.Location = new Point(655, 263);
            btnCreateReport.Name = "btnCreateReport";
            btnCreateReport.Size = new Size(108, 38);
            btnCreateReport.TabIndex = 7;
            btnCreateReport.Text = "Создать отчёт";
            btnCreateReport.UseVisualStyleBackColor = true;
            // 
            // btnFindLine
            // 
            btnFindLine.Enabled = false;
            btnFindLine.Location = new Point(655, 44);
            btnFindLine.Name = "btnFindLine";
            btnFindLine.Size = new Size(108, 38);
            btnFindLine.TabIndex = 6;
            btnFindLine.Text = "Найти запись";
            btnFindLine.UseVisualStyleBackColor = true;
            // 
            // btnDeleteLine
            // 
            btnDeleteLine.Enabled = false;
            btnDeleteLine.Location = new Point(655, 175);
            btnDeleteLine.Name = "btnDeleteLine";
            btnDeleteLine.Size = new Size(108, 38);
            btnDeleteLine.TabIndex = 5;
            btnDeleteLine.Text = "Удалить";
            btnDeleteLine.UseVisualStyleBackColor = true;
            // 
            // btnChangeLine
            // 
            btnChangeLine.Enabled = false;
            btnChangeLine.Location = new Point(655, 131);
            btnChangeLine.Name = "btnChangeLine";
            btnChangeLine.Size = new Size(108, 38);
            btnChangeLine.TabIndex = 4;
            btnChangeLine.Text = "Редактировать";
            btnChangeLine.UseVisualStyleBackColor = true;
            // 
            // btnAddLine
            // 
            btnAddLine.Enabled = false;
            btnAddLine.Location = new Point(655, 87);
            btnAddLine.Name = "btnAddLine";
            btnAddLine.Size = new Size(108, 38);
            btnAddLine.TabIndex = 3;
            btnAddLine.Text = "Добавить";
            btnAddLine.UseVisualStyleBackColor = true;
            // 
            // lstBoxTables
            // 
            lstBoxTables.BorderStyle = BorderStyle.FixedSingle;
            lstBoxTables.Enabled = false;
            lstBoxTables.Font = new Font("Segoe UI", 14F);
            lstBoxTables.FormattingEnabled = true;
            lstBoxTables.Location = new Point(167, 13);
            lstBoxTables.Name = "lstBoxTables";
            lstBoxTables.Size = new Size(265, 27);
            lstBoxTables.TabIndex = 2;
            // 
            // lblGetTable
            // 
            lblGetTable.AutoSize = true;
            lblGetTable.Font = new Font("Segoe UI", 14F);
            lblGetTable.Location = new Point(9, 13);
            lblGetTable.Name = "lblGetTable";
            lblGetTable.Size = new Size(152, 25);
            lblGetTable.TabIndex = 1;
            lblGetTable.Text = "Выбор таблицы:";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(9, 44);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(640, 369);
            dataGridView1.TabIndex = 0;
            // 
            // tabCon
            // 
            tabCon.Controls.Add(filePage);
            tabCon.Location = new Point(12, 12);
            tabCon.Name = "tabCon";
            tabCon.SelectedIndex = 0;
            tabCon.Size = new Size(778, 449);
            tabCon.TabIndex = 7;
            tabCon.Click += tabCon_Click;
            // 
            // WorkForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 494);
            Controls.Add(btnLogChange);
            Controls.Add(CloseBtn);
            Controls.Add(tabCon);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "WorkForm";
            Text = "Выпуск продукции";
            FormClosing += WorkForm_FormClosing;
            filePage.ResumeLayout(false);
            filePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabCon.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button CloseBtn;
        private Button btnLogChange;
        private TabPage reportPage;
        private TabPage editPage;
        private TabPage addLinePage;
        private TabPage findPage;
        private TabPage filePage;
        private Button btnChangeLine;
        private Button btnAddLine;
        private ListBox lstBoxTables;
        private Label lblGetTable;
        private DataGridView dataGridView1;
        private TabControl tabCon;
        private Button btnDeleteLine;
        private Button btnCreateReport;
        private Button btnFindLine;
    }
}