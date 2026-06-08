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
        { this.Close(); }

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
                wwAccess = new WorkWithAccess(path);
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
            LoadTab();
        }

        /// <summary>
        /// Загрузка/обновление данных выбранной таблицы
        /// </summary>
        private void LoadTab()
        {
            try
            {
                DataTable table = wwAccess.GetDataFromTable(cmbBoxTable.Text);
                dataGridView1.DataSource = table;
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка загрузки выбранной таблицы",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открытие формы добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLine_Click(object sender, EventArgs e)
        {
            NewAddForm(2);
        }

        /// <summary>
        /// Открытие формы поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindLine_Click(object sender, EventArgs e)
        {
            NewAddForm(1);
        }

        /// <summary>
        /// Открытие формы изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeLine_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для изменения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NewAddForm(3);
        }

        /// <summary>
        /// Открытие формы удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteLine_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку для удаления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                    List<string> columnNames = new List<string>();
                    List<string> values = new List<string>();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        string colName = dataGridView1.Columns[i].Name;
                        object cellValue = selectedRow.Cells[i].Value;
                        if (cellValue != null && cellValue != DBNull.Value)
                        {
                            columnNames.Add(colName);
                            values.Add(cellValue.ToString());
                        }
                    }
                    int n = wwAccess.DeleteLine(cmbBoxTable.Text, columnNames, values);
                    if (n > 0)
                    {
                        MessageBox.Show("Запись удалена", "Запись удалена",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTab();
                    }
                    else MessageBox.Show("Запись не найдена", "Запись не найдена",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                { MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка удаления записи",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }


        public void UpdateFilters(DataView dataView)
        {
            if (dataView != null) dataGridView1.DataSource = dataView;
            else
            {
                DataTable table = wwAccess.GetDataFromTable(cmbBoxTable.Text);
                dataGridView1.DataSource = table;
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            }
        }

        /// <summary>
        /// Открытие формы добавления/поиска/удаления/изменения
        /// </summary>
        /// <param name="i">Номер совершаемого действия. 
        /// 1 — найти запись, 
        /// 2 — добавить запись, 
        /// 3 — изменить запись, 
        private void NewAddForm(byte i)
        {
            if (cmbBoxTable.SelectedItem == null)
                MessageBox.Show("Сначала выберите таблицу", "Элемент не выбран",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                AddForm addForm = new AddForm(cmbBoxTable.Text, wwAccess, i, this, dataGridView1.CurrentRow);
                addForm.Show();
            }
        }
    }
}
