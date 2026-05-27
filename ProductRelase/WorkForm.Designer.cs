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
            tabCon = new TabControl();
            logPage = new TabPage();
            btnOpen = new Button();
            lblGetLogPass = new Label();
            lblPassword = new Label();
            lblLogin = new Label();
            txtPassword = new TextBox();
            txtLodin = new TextBox();
            tablePage = new TabPage();
            findPage = new TabPage();
            addLinePage = new TabPage();
            editPage = new TabPage();
            reportPage = new TabPage();
            btnLogChange = new Button();
            tabCon.SuspendLayout();
            logPage.SuspendLayout();
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
            CloseBtn.Text = "Выход";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // tabCon
            // 
            tabCon.Controls.Add(logPage);
            tabCon.Controls.Add(tablePage);
            tabCon.Controls.Add(findPage);
            tabCon.Controls.Add(addLinePage);
            tabCon.Controls.Add(editPage);
            tabCon.Controls.Add(reportPage);
            tabCon.Location = new Point(12, 12);
            tabCon.Name = "tabCon";
            tabCon.SelectedIndex = 0;
            tabCon.Size = new Size(778, 449);
            tabCon.TabIndex = 7;
            // 
            // logPage
            // 
            logPage.Controls.Add(btnOpen);
            logPage.Controls.Add(lblGetLogPass);
            logPage.Controls.Add(lblPassword);
            logPage.Controls.Add(lblLogin);
            logPage.Controls.Add(txtPassword);
            logPage.Controls.Add(txtLodin);
            logPage.Location = new Point(4, 24);
            logPage.Name = "logPage";
            logPage.Padding = new Padding(3);
            logPage.Size = new Size(770, 421);
            logPage.TabIndex = 0;
            logPage.Text = "Вход";
            logPage.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(161, 149);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(171, 25);
            btnOpen.TabIndex = 8;
            btnOpen.Text = "Войти";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // lblGetLogPass
            // 
            lblGetLogPass.AutoSize = true;
            lblGetLogPass.Font = new Font("Segoe UI", 18F);
            lblGetLogPass.Location = new Point(54, 27);
            lblGetLogPass.Name = "lblGetLogPass";
            lblGetLogPass.Size = new Size(459, 32);
            lblGetLogPass.TabIndex = 4;
            lblGetLogPass.Text = "Для входа введите свой логин и пароль";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(54, 120);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(49, 15);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Пароль";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(54, 94);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(41, 15);
            lblLogin.TabIndex = 2;
            lblLogin.Text = "Логин";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(117, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(306, 23);
            txtPassword.TabIndex = 1;
            // 
            // txtLodin
            // 
            txtLodin.Location = new Point(117, 91);
            txtLodin.Name = "txtLodin";
            txtLodin.Size = new Size(306, 23);
            txtLodin.TabIndex = 0;
            // 
            // tablePage
            // 
            tablePage.Location = new Point(4, 24);
            tablePage.Name = "tablePage";
            tablePage.Padding = new Padding(3);
            tablePage.Size = new Size(770, 421);
            tablePage.TabIndex = 1;
            tablePage.Text = "Просмотр таблиц";
            tablePage.UseVisualStyleBackColor = true;
            // 
            // findPage
            // 
            findPage.Location = new Point(4, 24);
            findPage.Name = "findPage";
            findPage.Size = new Size(770, 421);
            findPage.TabIndex = 3;
            findPage.Text = "Поиск";
            findPage.UseVisualStyleBackColor = true;
            // 
            // addLinePage
            // 
            addLinePage.Location = new Point(4, 24);
            addLinePage.Name = "addLinePage";
            addLinePage.Size = new Size(770, 421);
            addLinePage.TabIndex = 6;
            addLinePage.Text = "Добавить запись";
            addLinePage.UseVisualStyleBackColor = true;
            // 
            // editPage
            // 
            editPage.Location = new Point(4, 24);
            editPage.Name = "editPage";
            editPage.Size = new Size(770, 421);
            editPage.TabIndex = 2;
            editPage.Text = "Редактирование и ударение";
            editPage.UseVisualStyleBackColor = true;
            // 
            // reportPage
            // 
            reportPage.Location = new Point(4, 24);
            reportPage.Name = "reportPage";
            reportPage.Size = new Size(770, 421);
            reportPage.TabIndex = 5;
            reportPage.Text = "Отчёт";
            reportPage.UseVisualStyleBackColor = true;
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
            tabCon.ResumeLayout(false);
            logPage.ResumeLayout(false);
            logPage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button CloseBtn;
        private TabControl tabCon;
        private TabPage logPage;
        private TabPage tablePage;
        private TabPage editPage;
        private TabPage findPage;
        private TabPage reportPage;
        private TabPage addLinePage;
        private Label lblGetLogPass;
        private Label lblPassword;
        private Label lblLogin;
        private TextBox txtPassword;
        private TextBox txtLodin;
        private Button btnOpen;
        private Button btnLogChange;
    }
}