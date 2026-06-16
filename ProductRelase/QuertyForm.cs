using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ProductRelase
{
    public partial class QuertyForm : Form
    {
        private string sqlStr;
        private WorkWithAccess wwAccess;
        public QuertyForm(WorkWithAccess wwA)
        {
            InitializeComponent();
            wwAccess = wwA;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = txtBoxSQL.Text.Trim();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT Наименование FROM Продукция WHERE Наименование LIKE @searchPattern";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT ТабельныйНомер, Имя, Фамилия, Отчество FROM Рабочие WHERE Профессия = @profession";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT [ТабельныйНомер], SUM(Произведено) AS [Всё], SUM(Брак) AS [ВесьБрак], " +
                "SUM(Round(([Брак] / [Произведено]) * 100, 2)) AS[ПроцентБрака] " +
                "FROM[ВыпускПродукции] GROUP BY[ТабельныйНомер]";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT Профессия, Подразделение " +
                "FROM Рабочие GROUP BY Профессия, Подразделение";
        }

        private void closeButton_Click(object sender, EventArgs e)
        { this.Close(); }

        /// <summary>
        /// Проверка SQL-запроса на безопасность
        /// </summary>
        private bool IsSafeSqlQuery(string sql)
        {
            string upperSql = sql.ToUpper().Trim();
            string[] dangerousCommands = { "DROP", "DELETE", "UPDATE", "INSERT", 
                "ALTER", "CREATE", "TRUNCATE", "EXEC", "EXECUTE" };
            foreach (string command in dangerousCommands)
                if (upperSql.StartsWith(command) || upperSql.Contains(" " + command + " "))
                    return false;
            if (!upperSql.StartsWith("SELECT"))
                return false;
            if (sql.Contains("--") || sql.Contains("/*") || sql.Contains("*/") || sql.Contains(";"))
                return false;
            if (upperSql.Contains("UNION") && upperSql.Contains("SELECT"))
                return false;
            if (upperSql.Contains("ПОЛЬЗОВАТЕЛИ"))
                return false;
            return true;
        }

        private void ExecuteAndExport(string exportFormat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (radioButton2.Checked) parameters.Add("@searchPattern", "%дуб%");
            else if (radioButton3.Checked) parameters.Add("@profession", "столяр");
            else if (radioButton4.Checked || radioButton5.Checked) { }
            else
            {
                if (string.IsNullOrEmpty(sqlStr))
                {
                    MessageBox.Show("Введите SQL-запрос", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!IsSafeSqlQuery(sqlStr))
                {
                    MessageBox.Show("Запрос содержит недопустимые команды",
                        "Ошибка безопасности", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            try
            {
                DataTable resTable = wwAccess.ExecuteParameterizedQuery(sqlStr, parameters);
                if (resTable.Rows.Count == 0)
                {
                    MessageBox.Show("Запрос не вернул результатов", "Запрос не вернул результатов",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (exportFormat == "Word")
                    ExportToWord(resTable);
                else if (exportFormat == "Excel")
                    ExportToExcel(resTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) sqlStr = txtBoxSQL.Text.Trim();
            ExecuteAndExport("Word");
            txtBoxSQL.Text = sqlStr;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) sqlStr = txtBoxSQL.Text.Trim();
            ExecuteAndExport("Excel");
            txtBoxSQL.Text = sqlStr;
        }

        private void ExportToWord(DataTable table)
        {
            try
            {
                dynamic wordApp = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application"));
                dynamic doc = wordApp.Documents.Add();
                wordApp.Visible = true;
                dynamic range = doc.Range();
                range.Text = $"Отчёт по запросу\nДата: {DateTime.Now:dd.MM.yyyy HH:mm}\n\n";
                dynamic wordTable = doc.Tables.Add(range, table.Rows.Count + 1, table.Columns.Count);
                for (int col = 0; col < table.Columns.Count; col++)
                    wordTable.Cell(1, col + 1).Range.Text = table.Columns[col].ColumnName;
                for (int row = 0; row < table.Rows.Count; row++)
                    for (int col = 0; col < table.Columns.Count; col++)
                        wordTable.Cell(row + 2, col + 1).Range.Text = table.Rows[row][col]?.ToString() ?? "";
                wordTable.Borders.Enable = 1;
                wordTable.Rows[1].Range.Font.Bold = 1;
                MessageBox.Show("Отчёт создан в Word", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {  MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ExportToExcel(DataTable table)
        {
            try
            {
                dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                dynamic workbook = excelApp.Workbooks.Add();
                dynamic sheet = workbook.ActiveSheet;
                excelApp.Visible = true;
                sheet.Cells[1, 1] = $"Отчёт по запросу от {DateTime.Now:dd.MM.yyyy HH:mm}";
                for (int col = 0; col < table.Columns.Count; col++)
                    sheet.Cells[3, col + 1] = table.Columns[col].ColumnName;
                for (int row = 0; row < table.Rows.Count; row++)
                    for (int col = 0; col < table.Columns.Count; col++)
                        sheet.Cells[row + 4, col + 1] = table.Rows[row][col]?.ToString() ?? "";
                sheet.Columns.AutoFit();
                MessageBox.Show("Отчёт создан в Excel", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            { MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
