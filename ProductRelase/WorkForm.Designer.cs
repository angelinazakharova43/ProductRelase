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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            CloseBtn = new Button();
            button7 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(98, 26);
            label1.Name = "label1";
            label1.Size = new Size(278, 37);
            label1.TabIndex = 0;
            label1.Text = "Доступные действия:";
            // 
            // button1
            // 
            button1.Location = new Point(58, 83);
            button1.Name = "button1";
            button1.Size = new Size(171, 43);
            button1.TabIndex = 1;
            button1.Text = "Просмотр таблиц";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(58, 132);
            button2.Name = "button2";
            button2.Size = new Size(171, 43);
            button2.TabIndex = 2;
            button2.Text = "Добавление данных";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(58, 181);
            button3.Name = "button3";
            button3.Size = new Size(171, 43);
            button3.TabIndex = 3;
            button3.Text = "Редактирование данных";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(235, 83);
            button4.Name = "button4";
            button4.Size = new Size(171, 43);
            button4.TabIndex = 4;
            button4.Text = "Удаление данных";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(235, 132);
            button5.Name = "button5";
            button5.Size = new Size(171, 43);
            button5.TabIndex = 5;
            button5.Text = "Поиск данных";
            button5.UseVisualStyleBackColor = true;
            // 
            // CloseBtn
            // 
            CloseBtn.Location = new Point(139, 230);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(171, 43);
            CloseBtn.TabIndex = 6;
            CloseBtn.Text = "Выход";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += this.CloseBtn_Click;
            // 
            // button7
            // 
            button7.Location = new Point(235, 181);
            button7.Name = "button7";
            button7.Size = new Size(171, 43);
            button7.TabIndex = 7;
            button7.Text = "Формирование отчёта";
            button7.UseVisualStyleBackColor = true;
            // 
            // WorkForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(471, 310);
            Controls.Add(button7);
            Controls.Add(CloseBtn);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "WorkForm";
            Text = "Выпуск продукции — выбор действия";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button CloseBtn;
        private Button button7;
    }
}