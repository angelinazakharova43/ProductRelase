using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductRelase
{
    public partial class AutForm : Form
    {
        private WorkForm workForm;
        private bool IsLog;
        private string strLog;
        private WorkWithAccess wwAccess;

        /// <summary>
        /// Форма для входа в систему
        /// </summary>
        /// <param name="WF">Рабочая форма</param>
        public AutForm(WorkForm WF)
        {
            IsLog = false;
            workForm = WF;
            wwAccess = new WorkWithAccess(WF.GetPath());
            InitializeComponent();
        }

        /// <summary>
        /// Осуществление входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string str;     bool success;
            strLog = txtBoxLogin.Text.Trim().ToLower();
            string strPas = txtBoxPassword.Text.Trim().ToLower();
            if (strLog == null || strLog.Length < 8 || strLog.Length > 16)
            {
                MessageBox.Show($"Ошибка логина", "Ошибка логина",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (strPas == null || strPas.Length < 8 || strPas.Length > 16)
            {
                MessageBox.Show($"Ошибка пароля", "Ошибка пароля",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            wwAccess.CheckUser(strLog, strPas, out str, out success);
            if (success)
            {
                MessageBox.Show($"{str}", "Успешный вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsLog = true;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Ошибка входа: {str}", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxLogin.Text = "";
                txtBoxPassword.Text = "";
                IsLog = false;
            }
        }

        /// <summary>
        /// Действия при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            workForm.Show();
        }

        /// <summary>
        /// Показывает, успешно ли прошёл вход в систему. Позволяет значению IsLog оставаться приватным (недоступным извне)
        /// </summary>
        /// <returns>true – успешно
        /// false – не успешно</returns>
        public bool IsLogin()
        { return IsLog; }

        /// <summary>
        /// Открытие формы регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReg_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm(workForm);
            registrationForm.ShowDialog();
        }

        /// <summary>
        /// Получение логина вошедшего пользователя. Позволяет значению strLog оставаться приватным (недоступным извне)
        /// </summary>
        /// <returns>Логин вошедшего пользователя</returns>
        public string GetUser()
        { return strLog; }
    }
}
