namespace ProductRelase
{
    partial class RegistrationForm
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
            txtBoxPassword = new TextBox();
            txtBoxLogin = new TextBox();
            lblPassword = new Label();
            lblLogin = new Label();
            lblPassword2 = new Label();
            txtBoxPassword2 = new TextBox();
            btnReg = new Button();
            SuspendLayout();
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.Location = new Point(161, 107);
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Size = new Size(251, 23);
            txtBoxPassword.TabIndex = 10;
            // 
            // txtBoxLogin
            // 
            txtBoxLogin.Location = new Point(161, 81);
            txtBoxLogin.Name = "txtBoxLogin";
            txtBoxLogin.Size = new Size(251, 23);
            txtBoxLogin.TabIndex = 9;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(106, 110);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(49, 15);
            lblPassword.TabIndex = 8;
            lblPassword.Text = "Пароль";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(114, 84);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(41, 15);
            lblLogin.TabIndex = 7;
            lblLogin.Text = "Логин";
            // 
            // lblPassword2
            // 
            lblPassword2.AutoSize = true;
            lblPassword2.Location = new Point(46, 138);
            lblPassword2.Name = "lblPassword2";
            lblPassword2.Size = new Size(109, 15);
            lblPassword2.TabIndex = 13;
            lblPassword2.Text = "Повторите пароль";
            // 
            // txtBoxPassword2
            // 
            txtBoxPassword2.Location = new Point(161, 135);
            txtBoxPassword2.Name = "txtBoxPassword2";
            txtBoxPassword2.Size = new Size(251, 23);
            txtBoxPassword2.TabIndex = 14;
            // 
            // btnReg
            // 
            btnReg.Location = new Point(147, 164);
            btnReg.Name = "btnReg";
            btnReg.Size = new Size(164, 37);
            btnReg.TabIndex = 15;
            btnReg.Text = "Регистрация";
            btnReg.UseVisualStyleBackColor = true;
            btnReg.Click += btnReg_Click;
            // 
            // RegistrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 271);
            Controls.Add(btnReg);
            Controls.Add(txtBoxPassword2);
            Controls.Add(lblPassword2);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtBoxLogin);
            Controls.Add(lblPassword);
            Controls.Add(lblLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "RegistrationForm";
            Text = "Выпуск продукции — вход";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReg;
        private TextBox txtBoxPassword;
        private TextBox txtBoxLogin;
        private Label lblPassword;
        private Label lblLogin;
        private TextBox textBox1;
        private Label lblPassword2;
        private TextBox txtBoxPassword2;
    }
}
