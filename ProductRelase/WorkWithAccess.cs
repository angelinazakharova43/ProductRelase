using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace ProductRelase
{
    public class WorkWithAccess
    {
        public string Path;
        private string connStr;
        OleDbConnection conn;

        /// <summary>
        /// Объект для работы с БД
        /// </summary>
        /// <param name="path">Путь к БД</param>
        public WorkWithAccess(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Открытие соединения
        /// </summary>
        private void OpenConnect()
        {
            if (conn == null)
                try
                {
                    string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Path};";
                    conn = new OleDbConnection(connStr);
                }
                catch (Exception ex)
                {
                    conn = null;
                }

            if (conn == null)
                throw new ArgumentNullException();

            try
            {
                conn.Open();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Закрытие соединения
        /// </summary>
        private void CloseConnect()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <param name="userPassword">Пароль</param>
        /// <param name="str">Сообщение об ошибке/успешной регистрации</param>
        /// <returns>true — создан, false — не создан</returns>
        public void NewUser(string userLogin, string userPassword, out string str, out bool success)
        {
            OpenConnect();
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Пользователи WHERE Логин = @userLogin";
                using (OleDbCommand checkCommand = new OleDbCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count == 0) //Пользователя нет
                    {
                        ItsAdd(userLogin, Hash(userPassword), "Пользователь1", out str);
                        success = true;
                    }
                    else
                    {
                        str = "Такой пользователь уже есть";
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
                success = false;
            }
            finally { CloseConnect(); }
        }

        /// <summary>
        /// Попытка входа
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <param name="userPassword">Пароль</param>
        /// <param name="str">Ошибка/уведомдение об успешном входе</param>
        /// <param name="success">Возвращает: true — вход осуществлён, false — нет</param>
        public void CheckUser(string userLogin, string userPassword, out string str, out bool success)
        {
            OpenConnect();
            try
            {
                string selectQuery = $"SELECT пароль FROM Пользователи WHERE логин = @userLogin";
                using (OleDbCommand checkCommand = new OleDbCommand(selectQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                    object result = checkCommand.ExecuteScalar();
                    if (result != null)
                    {
                        str = result.ToString().Trim().ToLower(); //Возвращаем хэш из БД
                        success = true;
                    }
                    else
                    {
                        str = "Неверный пароль"; //Возвращаем ошибку
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                str = $"{ex.Message}";
                success = false;
            }
            finally { CloseConnect(); }
            if (!success) return;
            if (Hash(userPassword) == str)
            {
                str = "Вход успешно совершён";
                success = true;
            }
            else
            {
                str = "Неверный пароль";
                success = false;
            }
        }

        /// <summary>
        /// Запрос на добавление
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <param name="userPassword">Хэш пароля</param>
        /// <param name="userRole">Роль</param>
        /// <param name="str">Возвращает сообщение об успешном добавлении пользователя</param>
        private void ItsAdd(string userLogin, string userPassword, string userRole, out string str)
        {
            string insertQuery = "INSERT INTO Пользователи (Логин, Пароль, Роль) " +
                "VALUES (@userLogin, @userPassword, @userRole)";
            using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, conn))
            {
                insertCommand.Parameters.AddWithValue("@userLogin", userLogin);
                insertCommand.Parameters.AddWithValue("@userPassword", userPassword);
                insertCommand.Parameters.AddWithValue("@userRole", userRole);
                insertCommand.ExecuteNonQuery();
            }
            str = "Пользователь добавлен в систему";
        }

        /// <summary>
        /// Получение роли пользователя
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <returns>Возвращает роль пользователя</returns>
        public string GiveRole(string userLogin)
        {
            OpenConnect();
            OleDbCommand checkCommand = null;     object result = null;
            string selectQuery = $"SELECT роль FROM Пользователи WHERE логин = @userLogin";
            try
            {
                checkCommand = new OleDbCommand(selectQuery, conn);
                checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                result = checkCommand.ExecuteScalar();
                if (result != null) return result.ToString().Trim().ToLower();
                else return "-";
            }
            catch (Exception ex)
            {
                return "-";
            }
            finally
            {
                if (checkCommand != null) checkCommand.Dispose();
                CloseConnect();
            }
        }

        /// <summary>
        /// Получение списка всех таблиц
        /// </summary>
        /// <param name="str">Сообщение об ошибке. В случае успеза равно ""</param>
        /// <returns>Список всех таблиц</returns>
        public List<string> LoadTable(out string str)
        {
            OpenConnect();
            str = "";
            List<string> tableNames = new List<string>();                           //Имя бд, имя схемы, имя таблицы, тип объекта
            try
            {
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                foreach (DataRow row in schemaTable.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    tableNames.Add(tableName);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            finally
            { CloseConnect(); }
            return tableNames;
        }

        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="userPassword">Пароль</param>
        /// <returns>Хэш пароля</returns>
        private string Hash(string userPassword)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(userPassword);
            byte[] hash = SHA256.HashData(bytes);
            string usLogHash = Convert.ToHexString(hash);
            return usLogHash.Trim().ToLower();
        }

        /// <summary>
        /// Получение всей таблицы по имени
        /// </summary>
        /// <param name="selectTable">Имя таблицы</param>
        /// <param name="str">Сообщение об ошибке. В случае успеха равно ""</param>
        /// <returns>Таблицу с нужным именем</returns>
        public DataTable GetDataFromTable(string selectTable, out string str)
        {
            str = "";
            DataTable table = new DataTable();
            OpenConnect();
            try
            {
                using (OleDbCommand command = new OleDbCommand($"SELECT * FROM [{selectTable}]", conn))
                using (OleDbDataReader reader = command.ExecuteReader())
                table.Load(reader);
            }
            catch (Exception ex)
            { str = ex.Message; }
            finally
            { CloseConnect(); }
            return table;
        }
    }
}
