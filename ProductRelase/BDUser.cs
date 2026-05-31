using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace ProductRelase
{
    internal class BDUser
    {
        private string LogText;
        private string PassWord;
        private string Role;

        /// <summary>
        /// Пользователь
        /// </summary>
        /// <param name="logText">Логин (для регистрации)</param>
        /// <param name="passWord">Пароль (для регистрации)</param>
        /// <param name="role">Роль (для проверки разрешений)</param>
        /// <exception cref="Exception"> Неверный логин или пароль</exception>
        public BDUser(string logText, string passWord, string role)
        {
            string str;
            if (CheckLogin(logText, out str) && CheckPassword(passWord, out str))
            {
                LogText = logText;
                PassWord = passWord;
                Role = role;
            }
            else
            {
                throw new Exception(str);
            }

            Role = role;
        }

        /// <summary>
        /// Проверка логина
        /// </summary>
        /// <param name="Log">Логин (8-16 символов)</param>
        /// <param name="text">Текст ошибки</param>
        /// <returns>true — логин соответствует условиям
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
    }
}
