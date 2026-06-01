using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace ProductRelase
{
    public class BDUser
    {
        private string LogText;
        private string PassWord;
        private string Role;

        /// <summary>
        /// Пользователь
        /// </summary>
        /// <param name="logText">Логин (для регистрации/входа)</param>
        /// <param name="passWord">Пароль (для регистрации/входа)</param>
        /// <exception cref="Exception"> Неверный логин или пароль</exception>
        public BDUser(string logText, string passWord, WorkWhisConnection Conn)
        {
            string str;
            if (CheckLogin(logText, out str) && CheckPassword(passWord, out str))
            {
                LogText = logText;
                PassWord = passWord;
                Role = Conn.GiveRole(LogText);
            }
            else
            {
                throw new Exception(str);
            }
        }

        /// <summary>
        /// Проверка логина
        /// </summary>
        /// <param name="Log">Логин (8-16 символов)</param>
        /// <param name="text">Текст ошибки</param>
        /// <returns>true — логин соответствует условиям, 
        /// false — логин не соответствует условиям</returns>
        private bool CheckLogin(string Log, out string text)
        {
            if (Log == null || Log == "")
            {
                text = "Логин не может быть пустым";
                return false;
            }
            if (Log.Length < 8 || Log.Length > 16)
            {
                text = "Логин должен быть длинной от 8 до 16 символов";
                return false;
            }
            text = "";
            return true;
        }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="Pass">Пароль (8-16 символов)</param>
        /// <param name="text">Текст ошибки</param>
        /// <returns>true — пароль соответствует условиям
        /// false — пароль не соответствует условиям</returns>
        private bool CheckPassword(string Pass, out string text)
        {
            if (Pass == null || Pass == "")
            {
                text = "Пароль не может быть пустым";
                return false;
            }
            if (Pass.Length < 8 || Pass.Length > 16)
            {
                text = "Пароль должен быть длинной от 8 до 16 символов";
                return false;
            }
            text = "";
            return true;
        }

        /// <summary>
        /// Получение роли
        /// </summary>
        /// <returns>0 – админ, от 1 до 6 — пользователи, -1 – пользователь без разрешений</returns>
        public int CheckRole()
        {
            switch (Role)
            {
                case "админ":
                    return 0;
                case "пользователь1":
                    return 1;
                case "пользователь2":
                    return 2;
                case "пользователь3":
                    return 3;
                case "пользователь4":
                    return 4;
                case "пользователь5":
                    return 5;
                case "пользователь6":
                    return 6;
                default:
                    return -1;
            }
        }
    }
}
