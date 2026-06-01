using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

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
                    MessageBox.Show($"Ошибка создания подключения: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn = null;
                }

            if (conn == null)
            {
                MessageBox.Show("Не удалось создать подключение",
                    "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии соединения: {ex.Message}. " +
                    "Проверьте, существует ли файл и не занят ли он другим приложением",
                    "Ошибка открытия соединения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие соединения
        /// </summary>
        private void CloseConnect()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <param name="userPassword">Пароль</param>
        /// <param name="str">Сообщение об ошибке/успешной регистрации</param>
        /// <returns>true — создан, false — не создан</returns>
        public bool NewUser(string userLogin, string userPassword, out string str)
        {
            bool createNewUser;
            OpenConnect();
            try
            {
                QuertyAddOrChech(true, userLogin, userPassword, out str, out createNewUser);
            }
            catch (Exception ex)
            {
                str = ex.Message;
                createNewUser = false;
                CloseConnect();
                return createNewUser;
            }
            CloseConnect();

            return createNewUser;
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
                QuertyAddOrChech(false, userLogin, userPassword, out str, out success);
            }
            catch (Exception ex)
            {
                str = $"{ex.Message}";
                success = false;
            }
            if (!success)
            {
                CloseConnect();
                return;
            }
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

            CloseConnect();
        }

        /// <summary>
        /// Проверка существованя пользователя
        /// </summary>
        /// <param name="IsAdd">true — запрос на добавление, false — запрос на вход</param>
        /// <param name="userLogin">Логин</param>
        /// <param name="userPassword">Пароль</param>
        /// <param name="str">Сообщение об ошибке или успешном входе (в случае входа возвращает хэш пароля)</param>
        /// <param name="success">Если вход: true — вход осуществлён, false — нет.
        /// Если добавление: true — пользователь добавлен, false — нет</param>
        private void QuertyAddOrChech
            (bool IsAdd, string userLogin, string userPassword, out string str, out bool success)
        {
            string checkQuery = "SELECT COUNT(*) FROM Пользователи WHERE Логин = @userLogin";
            using (OleDbCommand checkCommand = new OleDbCommand(checkQuery, conn))
            {
                checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0) //Пользователя нет
                {
                    if (IsAdd)
                    {
                        ItsAdd(userLogin, Hash(userPassword), "Пользователь1", out str);
                        success = true;
                    }
                    else
                    {
                        str = "Такого пользователя нет";
                        success = false;
                    }
                }
                else //Пользователь есть
                {
                    if (IsAdd)
                    {
                        str = "Такой пользователь уже есть";
                        success = false;
                    }
                    else
                    {
                        ItsCheck(userLogin, out str, out success);
                    }
                }
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
        /// Запрос на получение хэша пароля (на вход)
        /// </summary>
        /// <param name="userLogin">Логин</param>
        /// <param name="str">Возвращает хэш/сообщение о неудачном поиске</param>
        /// <param name="success">true — можно пытаться осуществить вход, false — нельзя</param>
        private void ItsCheck(string userLogin, out string str, out bool success)
        {
            string selectQuery = $"SELECT пароль FROM Пользователи WHERE логин = @userLogin";
            using (OleDbCommand checkCommand = new OleDbCommand(selectQuery, conn))
            {
                checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                object result = checkCommand.ExecuteScalar();
                if (result != null)
                {
                    str = result.ToString().Trim().ToLower();
                    success = true;
                }
                else
                {
                    str = "Ошибка пароля в базе данных";
                    success = false;
                }
            }
        }

        public string GiveRole(string userLogin)
        {
            string selectQuery = $"SELECT роль FROM Пользователи WHERE логин = @userLogin";
            using (OleDbCommand checkCommand = new OleDbCommand(selectQuery, conn))
            {
                checkCommand.Parameters.AddWithValue("@userLogin", userLogin);
                object result = checkCommand.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString().Trim().ToLower();
                }
                else
                {
                    return "-";
                }
            }
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
    }
}
