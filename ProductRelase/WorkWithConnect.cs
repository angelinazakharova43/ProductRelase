using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRelase
{
    internal class WorkWithConnect
    {
        public WorkWithConnect() { }

        public string ConnPath()
        {
            string path = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            try
            {
                fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе файла базы данных: {ex.Message}",
                    "Ошибка при выборе файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return path;
        }
    }
}
