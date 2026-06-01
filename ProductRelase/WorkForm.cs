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
        private string path;
        private AutForm autForm;
        private AddForm addForm;
        private BDUser ActuallUser;
        private WorkWhisConnection wwConn;

        /// <summary>
        /// Создание новой рабочей формы
        /// </summary>
        public WorkForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переход к форме входа
        /// </summary>
        public void NewLogin()
        {
            cmbBoxTable.Items.Clear();
            dataGridView1.DataSource = null;
            this.Hide();
            autForm = new AutForm(this);
            autForm.ShowDialog();

            if (autForm != null && autForm.IsLogin())
            {
                ActuallUser = autForm.GetUser();
                IsEnabled(ActuallUser.CheckRole());
                wwConn = new WorkWhisConnection(path);
                LoadTables();
            }
            else
            {
                IsEnabled(-1);
            }
        }

        /// <summary>
        /// Настройка возможности взаимодействия с элементами интерфейса
        /// </summary>
        /// <param name="en">от -1 до 6</param>
        private void IsEnabled(int en)
        {
            if (en == 6)
                en = 0; //Для исключения двух одинаковых case`ов

            switch (en)
            {
                case (0):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = true;
                        btnCreateReport.Enabled = true;
                        return;
                    }
                case 1:
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = false;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case 2:
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case 3:
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case 4:
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = true;
                        return;
                    }
                case 5:
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = false;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = true;
                        return;
                    }
                default:
                    {
                        cmbBoxTable.Enabled = false;
                        btnFindLine.Enabled = false;
                        btnAddLine.Enabled = false;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
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
        /// <returns>Возвращает путь к файлу</returns>
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

        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                IsEnabled(-1);
            }
        }

        /// <summary>
        /// Получение пути к файлу, выбранному в диалоговом окне
        /// </summary>
        /// <returns>Путь к рабочему файлу</returns>
        public string GetPath()
        {
            return path;
        }

        private void LoadTables()
        {
            cmbBoxTable.Items.Clear();
            List<string> tableNames = new List<string>();
            string str;
            tableNames = wwConn.LoadTable(out str);
            if (str == "")
                foreach (string row in tableNames)
                {
                    if (ActuallUser.CheckRole() == 0 || row.Trim().ToLower() != "пользователи")
                        cmbBoxTable.Items.Add(row);
                }
            else
                MessageBox.Show($"Ошибка: {str}", "Ошибка загрузки таблиц",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cmbBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            DataTable table = wwConn.GetDataFromTable(cmbBoxTable.Text, out str);
            if (str == "")
                dataGridView1.DataSource = table;
            else
                MessageBox.Show($"Ошибка: {str}", "Ошибка загрузки выбранной таблицы",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            string str = "";
            if (cmbBoxTable.SelectedItem == null)
                MessageBox.Show("Сначала выберите таблицу", "Элемент не выбран",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DataTable table = wwConn.GetDataFromTable(cmbBoxTable.Text, out str);
                if (str == "")
                {
                    addForm = new AddForm(table);
                    addForm.Show();
                }
                else
                    MessageBox.Show($"Ошибка: {str}", "Ошибка загрузки выбранной таблицы",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
