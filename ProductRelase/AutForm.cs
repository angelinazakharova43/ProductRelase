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
        private WorkWhisConnection wwConn; 

        /// <summary>
        /// Форма для входа в систему
        /// </summary>
        /// <param name="WF">Рабочая форма</param>
        public AutForm(WorkForm WF)
        {
            IsLog = false;
            workForm = WF;
            wwConn = new WorkWhisConnection(WF.GetPath());
            InitializeComponent();
        }

        /// <summary>
        /// Осуществление входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string str;
            bool success;
            try
            {
                BDUser newUser = new BDUser(txtBoxLogin.Text.Trim().ToLower(), txtBoxPassword.Text.Trim().ToLower());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка логина или пароля",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxLogin.Text = "";
                txtBoxPassword.Text = "";
                return;
            }

            wwConn.CheckUser(txtBoxLogin.Text.Trim().ToLower(), txtBoxPassword.Text.Trim().ToLower(), out str, out success);
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
        /// Поазывает, успешно ли прошёл вход в систему. Позволяет значению IsLog оставаться приватным (недоступным извне)
        /// </summary>
        /// <returns>true – успешно
        /// false – не успешно</returns>
        public bool IsLogin()
        {
            return IsLog;
        }

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
    }
}
