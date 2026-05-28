namespace ProductRelase
{
    partial class AutForm
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
            lblLogin = new Label();
            lblPassword = new Label();
            txtBoxLogin = new TextBox();
            txtBoxPassword = new TextBox();
            btnLogin = new Button();
            button1 = new Button();
            lblGoLogin = new Label();
            SuspendLayout();
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(57, 68);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(41, 15);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "Логин";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(57, 94);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(49, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Пароль";
            // 
            // txtBoxLogin
            // 
            txtBoxLogin.Location = new Point(112, 65);
            txtBoxLogin.Name = "txtBoxLogin";
            txtBoxLogin.Size = new Size(251, 23);
            txtBoxLogin.TabIndex = 3;
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Location = new Point(112, 91);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Size = new Size(251, 23);
            txtBoxPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(148, 120);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(131, 26);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Вход";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // button1
            // 
            button1.Location = new Point(148, 148);
            button1.Name = "button1";
            button1.Size = new Size(131, 26);
            button1.TabIndex = 6;
            button1.Text = "Регистрация";
            button1.UseVisualStyleBackColor = true;
            // 
            // lblGoLogin
            // 
            lblGoLogin.AutoSize = true;
            lblGoLogin.Font = new Font("Segoe UI", 20F);
            lblGoLogin.Location = new Point(27, 9);
            lblGoLogin.Name = "lblGoLogin";
            lblGoLogin.Size = new Size(414, 37);
            lblGoLogin.TabIndex = 7;
            lblGoLogin.Text = "Войдите в свою учётную запись";
            // 
            // AutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 202);
            Controls.Add(lblGoLogin);
            Controls.Add(button1);
            Controls.Add(btnLogin);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtBoxLogin);
            Controls.Add(lblPassword);
            Controls.Add(lblLogin);
            Name = "AutForm";
            Text = "Выпуск продукции — вход";
            FormClosing += AutForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblComein;
        private Label lblLogin;
        private Label lblPassword;
        private TextBox txtBoxLogin;
        private TextBox txtBoxPassword;
        private Button btnLogin;
        private Button button1;
        private Label lblGoLogin;
    }
}