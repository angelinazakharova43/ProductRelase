using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ProductRelase
{
    public partial class WorkForm : Form
    {
        private LoginForm loginForm;

        /// <summary>
        /// Создание новой рабочей формы
        /// </summary>
        /// <param name="LF">Форма запуска</param>
        public WorkForm(LoginForm LF)
        {
            InitializeComponent();
            NotInLog();
            loginForm = LF;
        }

        /// <summary>
        /// Отрисовка страниц, если не совершён вход в учётную запись
        /// </summary>
        private void NotInLog()
        {
            logPage.Parent = tabCon;

            tablePage.Parent = null;
            findPage.Parent = null;
            addLinePage.Parent = null;
            editPage.Parent = null;
            reportPage.Parent = null;

            btnLogChange.Visible = false;
        }

        /// <summary>
        /// Отрисовка страниц, если совершён вход в учётную запись
        /// </summary>
        private void InLog()
        {
            logPage.Parent = null;

            tablePage.Parent = tabCon;
            findPage.Parent = tabCon;
            addLinePage.Parent = tabCon;
            editPage.Parent = tabCon;
            reportPage.Parent = tabCon;

            btnLogChange.Visible = true;
        }

        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Вход в учётную запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            InLog();
        }

        /// <summary>
        /// Смена учётной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogChange_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите выйти из учётной записи?",
                "Выход из учётной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                NotInLog();
        }

        /// <summary>
        /// Действия в случае закрытия приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите закрыть приложение?",
                "Закрытие приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                loginForm.Close();
            else
                e.Cancel = true;
        }
    }
}
