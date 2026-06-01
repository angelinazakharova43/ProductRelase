using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductRelase
{
    public partial class AddForm : Form
    {
        private List<TextBox> textBoxes = new List<TextBox>();
        private DataTable sourceTable;

        //Константы положения объектов
        int y = 20;
        int width = 200;
        int height = 25;
        int space = 35;

        public AddForm(DataTable tables)
        {
            InitializeComponent();
            sourceTable = tables;

            CreateLabelsAndTextBox();
        }

        private void CreateLabelsAndTextBox()
        {
            for (int i = 0; i < sourceTable.Columns.Count; i++)
            {
                DataColumn column = sourceTable.Columns[i];

                Label lbl = new Label();
                lbl.Text = column.ColumnName + ":";
                lbl.Location = new Point(20, y);
                lbl.Size = new Size(width, height);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Name = "lbl" + column.ColumnName;
                Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Location = new Point(20 + width + 10, y);
                txt.Size = new Size(width, height);
                txt.Name = "txt" + column.ColumnName;
                txt.Tag = column;
                Controls.Add(txt);
                textBoxes.Add(txt);

                y += space;
            }

            Button btnAdd = new Button();
            btnAdd.Text = "Добавить";
            btnAdd.Location = new Point(20 + width + 10 - 100, y + 10);
            btnAdd.Size = new Size(90, 30);
            btnAdd.Click += BtnAdd_Click;
            Controls.Add(btnAdd);

            Button btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Location = new Point(20 + width + 10 + 10, y + 10);
            btnCancel.Size = new Size(90, 30);
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
