using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Cryptography;

namespace ProductRelase
{
    internal class WorkWhisConnection
    {
        public string Path;
        private string connStr;
        OleDbConnection conn;

        /// <summary>
        /// Объект для работы с БД
        /// </summary>
        /// <param name="path">Путь к БД</param>
        public WorkWhisConnection(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Установка соединения
        /// </summary>
        private void GetConnect()
        {
            try
            {
                connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path};";
                conn = new OleDbConnection(connStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка во время установления соединения: {ex.Message}",
                    "Ошибка во время установления соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Открытие соединения
        /// </summary>
        public void OpenConnect()
        {
            if (conn == null)
                GetConnect();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии соединения: {ex.Message}", "Ошибка открытия соединения",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие соединения
        /// </summary>
        public void CloseConnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        public void NewUser(string userLogin, string userPassword, string userRole)
        {

        }
    }
}
