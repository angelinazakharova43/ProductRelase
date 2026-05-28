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
        public WorkWhisConnection(string path)
        {
            Path = path;
        }

        public void GetConnect()
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

        public void CloseConnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
