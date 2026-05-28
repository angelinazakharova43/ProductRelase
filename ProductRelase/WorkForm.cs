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
        private bool IsCon;
        private string path;
        AutForm autForm;
        /// <summary>
        /// Создание новой рабочей формы
        /// </summary>
        public WorkForm()
        {
            IsCon = false;
            InitializeComponent();
        }

        /// <summary>
        /// Переход к форме входа
        /// </summary>
        public void NewLogin()
        {
            this.Hide();
            autForm = new AutForm(this);
            autForm.ShowDialog();

            if (autForm != null && autForm.IsLogin())
            {
                lstBoxTables.Enabled = true;
                btnFindLine.Enabled = true;
                btnAddLine.Enabled = true;
                btnChangeLine.Enabled = true;
                btnDeleteLine.Enabled = true;
                btnCreateReport.Enabled = true;
            }
            else
            {
                lstBoxTables.Enabled = false;
                btnFindLine.Enabled = false;
                btnAddLine.Enabled = false;
                btnChangeLine.Enabled = false;
                btnDeleteLine.Enabled = false;
                btnCreateReport.Enabled = false;
            }
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
        /// Смена учётной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogChange_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите выйти из учётной записи?",
                "Выход из учётной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                NewLogin();
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
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }

        /// <summary>
        /// Получение пути к файлу с базой даннных
        /// </summary>
        /// <returns></returns>
        private string ConnPath()
        {
            path = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            try
            {
                fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                    MessageBox.Show($"Выбран файл: {path}", "Файл выбран",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Файл выбран", "Файл не выбран",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе файла базы данных: {ex.Message}",
                    "Ошибка при выборе файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return path;
        }

        private void tabCon_Click(object sender, EventArgs e)
        {
            path = ConnPath();
            if (path != null)
            {
                btnLogChange.Visible = true;
                NewLogin();
            }
            else
            {
                btnLogChange.Visible = false;

                lstBoxTables.Enabled = false;
                btnFindLine.Enabled = false;
                btnAddLine.Enabled = false;
                btnChangeLine.Enabled = false;
                btnDeleteLine.Enabled = false;
                btnCreateReport.Enabled = false;
            }
        }
    }
}
