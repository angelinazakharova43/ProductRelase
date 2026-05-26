namespace ProductRelase
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            ConnBtn = new Button();
            CloseBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(109, 32);
            label1.Name = "label1";
            label1.Size = new Size(249, 37);
            label1.TabIndex = 0;
            label1.Text = "Выпуск продукции";
            // 
            // ConnBtn
            // 
            ConnBtn.Location = new Point(143, 89);
            ConnBtn.Name = "ConnBtn";
            ConnBtn.Size = new Size(182, 46);
            ConnBtn.TabIndex = 1;
            ConnBtn.Text = "Выбор базы данных для подключения";
            ConnBtn.UseVisualStyleBackColor = true;
            ConnBtn.Click += ConnBtn_Click;
            // 
            // CloseBtn
            // 
            CloseBtn.Location = new Point(143, 141);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(182, 46);
            CloseBtn.TabIndex = 2;
            CloseBtn.Text = "Закрыть приложение";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 271);
            Controls.Add(CloseBtn);
            Controls.Add(ConnBtn);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "LoginForm";
            Text = "Выпуск продукции — вход";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button ConnBtn;
        private Button CloseBtn;
    }
}
