using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRelase
{
    internal class WorkWithConnect
    {
        public WorkWithConnect() { }

        /// <summary>
        /// Диалоговое окно с выбором файла
        /// </summary>
        /// <returns>Путь к файлу</returns>
        public string ConnPath()
        {
            string path = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            fileDialog.Filter = "Access Database (*.accdb)|*.accdb";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == DialogResult.OK) path = fileDialog.FileName;
            return path;
        }
    }
}
