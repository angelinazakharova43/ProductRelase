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
        private string Role;
        private AutForm autForm;
        private AddForm addForm;
        private WorkWithAccess wwAccess;
        private WorkWithConnect wwConnect = new WorkWithConnect();

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
            Role = wwAccess.GiveRole(autForm.GetUser());
            if (autForm != null && autForm.IsLogin())
            {
                IsEnabled(Role);
                wwAccess = new WorkWithAccess(path);
                LoadTables();
            }
            else IsEnabled("-");
        }

        /// <summary>
        /// Настройка возможности взаимодействия с элементами интерфейса
        /// </summary>
        /// <param name="en">от -1 до 6</param>
        private void IsEnabled(string en)
        {
            if (en == "пользователь6") en = "админ"; //Для исключения двух одинаковых case`ов
            switch (en)
            {
                case ("админ"):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = true;
                        btnCreateReport.Enabled = true;
                        return;
                    }
                case ("пользователь1"):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = false;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case ("пользователь2"):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = false;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case ("пользователь3"):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = false;
                        return;
                    }
                case ("пользователь4"):
                    {
                        cmbBoxTable.Enabled = true;
                        btnFindLine.Enabled = true;
                        btnAddLine.Enabled = true;
                        btnChangeLine.Enabled = true;
                        btnDeleteLine.Enabled = false;
                        btnCreateReport.Enabled = true;
                        return;
                    }
                case ("пользователь5"):
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
        {  this.Close(); }

        /// <summary>
        /// Смена учётной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogChange_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите выйти из учётной записи?",
                "Выход из учётной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) NewLogin();
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
            if (result != DialogResult.Yes) e.Cancel = true;
        }

        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCon_Click(object sender, EventArgs e)
        {
            path = wwConnect.ConnPath();
            if (path != null)
            {
                btnLogChange.Visible = true;
                NewLogin();
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Файл не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnLogChange.Visible = false;
                IsEnabled("-");
            }
        }

        /// <summary>
        /// Получение пути к файлу, выбранному в диалоговом окне
        /// </summary>
        /// <returns>Путь к рабочему файлу</returns>
        public string GetPath()
        { return path; }

        /// <summary>
        /// Загрузка имён таблиц в комбобокс
        /// </summary>
        private void LoadTables()
        {
            cmbBoxTable.Items.Clear();
            List<string> tableNames = new List<string>();
            string str;
            tableNames = wwAccess.LoadTable(out str);
            if (str == "")
                foreach (string row in tableNames)
                    if (Role == "админ" || row.Trim().ToLower() != "пользователи")
                        cmbBoxTable.Items.Add(row);
            else MessageBox.Show($"Ошибка: {str}", "Ошибка загрузки таблиц",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Выбор таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            DataTable table = wwAccess.GetDataFromTable(cmbBoxTable.Text, out str);
            if (str == "")
                dataGridView1.DataSource = table;
            else
                MessageBox.Show($"Ошибка: {str}", "Ошибка загрузки выбранной таблицы",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Открытие формы добавления/поиска/редактирования/удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLine_Click(object sender, EventArgs e)
        {
            string str = "";
            if (cmbBoxTable.SelectedItem == null)
                MessageBox.Show("Сначала выберите таблицу", "Элемент не выбран",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DataTable table = wwAccess.GetDataFromTable(cmbBoxTable.Text, out str);
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
