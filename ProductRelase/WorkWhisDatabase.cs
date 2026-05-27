using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProductRelase
{
    internal class WorkWhisDatabase
    {
        public string Path;
        private string connStr;
        OleDbConnection conn;
        public WorkWhisDatabase(string path)
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
                MessageBox.Show($"Ошибка во время установления соединения: {ex.Message}");
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
                MessageBox.Show($"Ошибка при открытии соединения: {ex.Message}");
            }
        }

        public void CloseConnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
