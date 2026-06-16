using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductRelase
{
    public partial class AddForm : Form
    {
        private List<TextBox> textBoxes = new List<TextBox>();
        private DataTable schemaTable;
        private WorkWithAccess wwAccess;
        private string tabName;
        private byte numAct;
        private DataRow myRow;
        private WorkForm workForm;
        Dictionary<string, string> foreignKeys;

        //Константы положения объектов
        int y = 20;
        int x = 20;
        int width = 200;
        int height = 25;
        int space = 35;

        public AddForm(string table, WorkWithAccess wwA, byte act, WorkForm WF, DataGridViewRow row = null)
        {
            InitializeComponent();
            wwAccess = wwA;
            numAct = act;
            tabName = table;
            workForm = WF;
            foreignKeys = wwAccess.GetForeignKeysForTable(tabName);
            if (row != null) myRow = ((DataRowView)row.DataBoundItem).Row;
            try
            {
                schemaTable = wwAccess.GetTableSchema(table);
                CreateControls();
                if (numAct == 3 && myRow != null) FillControlsWithData();
            }
            catch (Exception ex)
            { MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка получения схемы таблицы",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void CreateControls()
        {
            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                DataColumn column = schemaTable.Columns[i];

                Label lbl = new Label();
                lbl.Text = column.ColumnName + ":";
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(width, height);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Name = $"lbl{column.ColumnName}";
                Controls.Add(lbl);

                if (foreignKeys.ContainsKey(column.ColumnName))
                {
                    string relatedTable = foreignKeys[column.ColumnName];
                    ComboBox cmb = new ComboBox();
                    cmb.Location = new Point(x + width + 10, y);
                    cmb.Size = new Size(width, height);
                    cmb.Tag = column;
                    cmb.Name = $"txt{column.ColumnName}";
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                    LoadComboBoxItems(cmb, relatedTable);
                    Controls.Add(cmb);
                    textBoxes.Add(null);
                }
                else
                {
                    TextBox txt = new TextBox();
                    txt.Location = new Point(x + width + 10, y);
                    txt.Size = new Size(width, height);
                    txt.Tag = column;
                    txt.Name = $"txt{column.ColumnName}";
                    Controls.Add(txt);
                    textBoxes.Add(txt);
                }

                y += space;
            }
            CreateActBtn();

            Button btnCancel = new Button();
            btnCancel.Text = "Закрыть";
            btnCancel.Location = new Point(x + width + 20, y + 10);
            btnCancel.Size = new Size(90, 30);
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);

            this.Width = x + width + 10 + width + width;
            this.Height = x + y + 10 + 30 + height + x;
        }

        private void LoadComboBoxItems(ComboBox cmb, string relatedTable)
        {
            try
            {
                DataTable data = wwAccess.GetDataFromTable(relatedTable);
                cmb.DataSource = data;
                cmb.DisplayMember = data.Columns[0].ColumnName;
                cmb.ValueMember = data.Columns[0].ColumnName;
            }
            catch (Exception ex)
            { MessageBox.Show($"Ошибка загрузки данных для поля: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void CreateActBtn()
        {
            Button btnAct = new Button();
            btnAct.Location = new Point(x + width - 90, y + 10);
            btnAct.Size = new Size(90, 30);
            switch (numAct)
            {
                case 1:
                    btnAct.Text = "Найти";
                    btnAct.Click += btnFind_Click;
                    Controls.Add(btnAct);
                    return;
                case 2:
                    btnAct.Text = "Добавить";
                    btnAct.Click += BtnAdd_Click;
                    Controls.Add(btnAct);
                    return;
                default:
                    btnAct.Text = "Изменить";
                    btnAct.Click += btnChange_Click;
                    Controls.Add(btnAct);
                    return;

            }
        }

        private void FillControlsWithData()
        {
            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                DataColumn column = schemaTable.Columns[i];
                if (foreignKeys.ContainsKey(column.ColumnName))
                {
                    ComboBox cmb = (ComboBox)Controls[$"txt{column.ColumnName}"];
                    if (cmb != null && myRow[column.ColumnName] != DBNull.Value)
                        cmb.SelectedValue = myRow[column.ColumnName];
                }
                else
                {
                    TextBox txtBox = (TextBox)Controls[$"txt{column.ColumnName}"];
                    if (txtBox != null && myRow[column.ColumnName] != DBNull.Value)
                        txtBox.Text = myRow[column.ColumnName].ToString();
                }
            }
        }

        /// <summary>
        /// Добавление записи в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                DataColumn column = schemaTable.Columns[i];
                Control ctrl = Controls[$"txt{column.ColumnName}"];
                bool isEmpty = false;
                if (foreignKeys.ContainsKey(column.ColumnName))
                {
                    ComboBox cmb = ctrl as ComboBox;
                    if (cmb != null && cmb.SelectedIndex == -1) isEmpty = true;
                }
                else
                {
                    TextBox txt = ctrl as TextBox;
                    if (txt != null && string.IsNullOrWhiteSpace(txt.Text)) isEmpty = true;
                }
                if (isEmpty)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка");
                    return;
                }
            }
            try
            {
                OleDbParameter[] parameters = new OleDbParameter[schemaTable.Columns.Count];
                int i = 0;
                foreach (DataColumn col in schemaTable.Columns)
                {
                    Control ctrl = Controls[$"txt{col.ColumnName}"];
                    object val;
                    if (foreignKeys.ContainsKey(col.ColumnName))
                    {
                        ComboBox cmb = ctrl as ComboBox;
                        val = cmb?.SelectedValue;
                    }
                    else
                    {
                        TextBox txtBox = ctrl as TextBox;
                        val = txtBox.Text;

                        if (col.DataType == typeof(int))
                        {
                            if (int.TryParse(txtBox.Text, out int intVal)) val = intVal;
                            else
                            {
                                MessageBox.Show($"Неверный формат для поля {col.ColumnName}. Ожидается числовой тип",
                                    "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (col.DataType == typeof(decimal))
                        {
                            if (decimal.TryParse(txtBox.Text, out decimal decVal)) val = decVal;
                            else
                            {
                                MessageBox.Show($"Неверный формат для поля {col.ColumnName}. Ожидается денежный тип",
                                    "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    parameters[i] = new OleDbParameter($"{col.ColumnName}", val);
                    i++;
                }
                int rowAff = wwAccess.AddLine(tabName, parameters);
                if (rowAff > 0)
                    MessageBox.Show("Данные успешно добавлены", "Данные добавлены",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Не удалось добавить данные", "Данные не добавлены",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            { MessageBox.Show($"Не удалось добавить данные: {ex.Message}", "Данные не добавлены",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < schemaTable.Columns.Count; i++)
            {
                DataColumn column = schemaTable.Columns[i];
                Control ctrl = Controls[$"txt{column.ColumnName}"];
                bool isEmpty = false;
                if (foreignKeys.ContainsKey(column.ColumnName))
                {
                    ComboBox cmb = ctrl as ComboBox;
                    if (cmb != null && cmb.SelectedIndex == -1) isEmpty = true;
                }
                else
                {
                    TextBox txt = ctrl as TextBox;
                    if (txt != null && string.IsNullOrWhiteSpace(txt.Text)) isEmpty = true;
                }
                if (isEmpty)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            try
            {
                OleDbParameter[] parameters = new OleDbParameter[schemaTable.Columns.Count];
                List<string> oldValues = new List<string>();
                List<string> columnNames = new List<string>();
                int i = 0;
                foreach (DataColumn col in schemaTable.Columns)
                {
                    Control ctrl = Controls[$"txt{col.ColumnName}"];
                    object val;
                    columnNames.Add(col.ColumnName);

                    if (foreignKeys.ContainsKey(col.ColumnName))
                    {
                        ComboBox cmb = ctrl as ComboBox;
                        val = cmb?.SelectedValue;
                    }
                    else
                    {
                        TextBox txtBox = ctrl as TextBox;
                        val = txtBox.Text;
                        if (col.DataType == typeof(int))
                        {
                            if (int.TryParse(txtBox.Text, out int intVal)) val = intVal;
                            else
                            {
                                MessageBox.Show($"Неверный формат для поля {col.ColumnName}. Ожидается числовой тип",
                                    "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else if (col.DataType == typeof(decimal))
                        {
                            if (decimal.TryParse(txtBox.Text, out decimal decVal)) val = decVal;
                            else
                            {
                                MessageBox.Show($"Неверный формат для поля {col.ColumnName}. Ожидается денежный тип",
                                    "Ошибка типа данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    parameters[i] = new OleDbParameter(col.ColumnName, val);
                    if (myRow[col.ColumnName] != DBNull.Value) oldValues.Add(myRow[col.ColumnName].ToString());
                    else oldValues.Add("");
                    i++;
                }
                int rowsAffected = wwAccess.UpdateLine(tabName, parameters, oldValues, columnNames);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно изменены", "Данные изменены",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось изменить данные",
                        "Данные не изменены", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось изменить данные: {ex.Message}", "Данные не изменены",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DataTable table = wwAccess.GetDataFromTable(tabName);
            DataView dataView = new DataView(table);
            List<string> filters = new List<string>();
            foreach (TextBox txt in textBoxes)
                if (!string.IsNullOrWhiteSpace(txt.Text))
                    filters.Add($"[{txt.Name.Substring(3)}] LIKE '{txt.Text.Trim()}'");
            if (filters.Count > 0)
                dataView.RowFilter = string.Join(" AND ", filters);
            workForm.UpdateFilters(dataView);
            this.Close();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        { this.Close(); }
    }
}
