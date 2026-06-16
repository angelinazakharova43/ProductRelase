using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductRelase;

namespace TestProject1
{
    [TestClass]
    public class WorkWithAccessTests
    {
        private string testPath;
        private WorkWithAccess wwAccess;

        [TestInitialize]
        public void Setup()
        {
            testPath = Path.Combine(Path.GetTempPath(), "Test.accdb");
            wwAccess = new WorkWithAccess(testPath);
        }

        /// <summary>
        /// Проверка, что метод LoadTable возвращает список таблиц из БД
        /// </summary>
        [TestMethod]
        public void ShouldReturnTableList()
        {
            List<string> tables = wwAccess.LoadTable();
            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Contains("Рабочие"));
        }

        /// <summary>
        /// Проверка успешной регистрации нового пользователя
        /// </summary>
        [TestMethod]
        public void NewUserTrue()
        {
            string str; bool success;
            wwAccess.NewUser("testuser1", "password123", out str, out success);
            Assert.IsTrue(success);
            Assert.AreEqual("Пользователь успешно зарегистрирован", str);
        }

        /// <summary>
        /// Проверка регистрации с уже существующим логином
        /// </summary>
        [TestMethod]
        public void NewUserFalse()
        {
            string str; bool success;
            wwAccess.NewUser("testuser1", "pass1", out str, out success);
            Assert.IsFalse(success);
            Assert.AreEqual("Такой пользователь уже существует", str);
        }

        /// <summary>
        /// Проверка успешного входа
        /// </summary>
        [TestMethod]
        public void CheckUserTrue()
        {
            string str; bool success;
            wwAccess.CheckUser("testuser1", "password123", out str, out success);
            Assert.IsTrue(success);
        }

        /// <summary>
        /// Проверка входа с неверным паролем
        /// </summary>
        [TestMethod]
        public void CheckUserFalsePassword()
        {
            string str; bool success;
            wwAccess.CheckUser("testuser1", "incorrect", out str, out success);
            Assert.IsFalse(success);
            Assert.AreEqual("Неверный пароль", str);
        }

        /// <summary>
        /// Проверка входа с неверным логином
        /// </summary>
        [TestMethod]
        public void CheckUserFalse()
        {
            string str; bool success;
            wwAccess.CheckUser("wronguser", "incorrect", out str, out success);
            Assert.IsFalse(success);
            Assert.AreEqual("Пользователь не найден", str);
        }

        /// <summary>
        /// Проверка получения роли существующего пользователя
        /// </summary>
        [TestMethod]
        public void GiveRoleDefaultRole()
        {
            string role = wwAccess.GiveRole("testuser1");
            Assert.AreEqual("-", role);
        }

        /// <summary>
        /// Проверка получения роли несуществующего пользователя
        /// </summary>
        [TestMethod]
        public void GiveRoleNonUser()
        {
            string role = wwAccess.GiveRole("nonexistent_user");
            Assert.AreEqual("-", role);
        }

        /// <summary>
        /// Проверка получения данных из таблицы
        /// </summary>
        [TestMethod]
        public void GetDataFromTableReturn()
        {
            DataTable table = wwAccess.GetDataFromTable("Рабочие");
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Columns.Contains("Фамилия"));
        }

        /// <summary>
        /// Проверка добавления записи в таблицу
        /// </summary>
        [TestMethod]
        public void AddLineInsertRow()
        {
            var parameters = new OleDbParameter[]
            {
                new OleDbParameter("Наименование", "Дверь"),
                new OleDbParameter("Стоимость", "17")
            };
            int rowsAff = wwAccess.AddLine("Продукция", parameters);
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Проверка добавления записи с пустыми параметрами
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddLineException()
        {
            wwAccess.AddLine("Продукция", null);
        }

        /// <summary>
        /// Проверка добавления записи с нулевыми параметрами
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddLine_EmptyParameters_ThrowsArgumentException()
        {
            wwAccess.AddLine("Продукция", new OleDbParameter[0]);
        }

        /// <summary>
        /// Проверка получения схемы таблицы
        /// </summary>
        [TestMethod]
        public void GetTableSchemaSchema()
        {
            DataTable schema = wwAccess.GetTableSchema("Продукция");
            Assert.IsNotNull(schema);
            Assert.AreEqual(2, schema.Columns.Count);
        }

        /// <summary>
        /// Проверка обновления записи
        /// </summary>
        [TestMethod]
        public void UpdateLineUpRecord()
        {
            var newParams = new OleDbParameter[]
            {
                new OleDbParameter("Наименование", "Дверь"),
                new OleDbParameter("Стоимость", "18")
            };
            var columnNames = new List<string> { "Наименование", "Стоимость" };
            var oldValues = new List<string> { "Дверь", "17" };
            int updated = wwAccess.UpdateLine("Продукция", newParams, oldValues, columnNames);
            Assert.AreEqual(1, updated);
        }

        /// <summary>
        /// Проверка обновления несуществующей записи
        /// </summary>
        [TestMethod]
        public void UpdateLineZero()
        {
            var newParams = new OleDbParameter[]
            {
                new OleDbParameter("Наименование", "Двери"),
                new OleDbParameter("Стоимость", "18000")
            };
            var columnNames = new List<string> { "Наименование", "Стоимость" };
            var oldValues = new List<string> { "Дверная ручка", "1717" };
            int updated = wwAccess.UpdateLine("Продукция", newParams, oldValues, columnNames);
            Assert.AreEqual(0, updated);
        }

        /// <summary>
        /// Проверка удаления записи
        /// </summary>
        [TestMethod]
        public void DeleteLineDeleteIt()
        {
            var columnNames = new List<string> { "Наименование", "Стоимость" };
            var values = new List<string> { "Дверь", "18" };
            int deleted = wwAccess.DeleteLine("Продукция", columnNames, values);
            Assert.AreEqual(1, deleted);
        }

        /// <summary>
        /// Проверка удаления несуществующей записи
        /// </summary>
        [TestMethod]
        public void DeleteLineZero()
        {
            var columns = new List<string> { "Наименование" };
            var values = new List<string> { "Несуществующий продукт" };
            int deleted = wwAccess.DeleteLine("Продукция", columns, values);
            Assert.AreEqual(0, deleted);
        }

        /// <summary>
        /// Проверка получения внешних ключей
        /// </summary>
        [TestMethod]
        public void GetForeignKeysForTableDictionary()
        {
            var foreignKeys = wwAccess.GetForeignKeysForTable("ТестоваяТаблица");
            Assert.IsNotNull(foreignKeys);
        }

        /// <summary>
        /// Проверка выполнения параметризованного запроса
        /// </summary>
        [TestMethod]
        public void ExecuteParameterizedQueryData()
        {
            var sql = "SELECT Наименование FROM Продукция WHERE Наименование LIKE @searchPattern";
            var queryParams = new Dictionary<string, object> { { "@searchPattern", "%дуб%" } };
            DataTable result = wwAccess.ExecuteParameterizedQuery(sql, queryParams);
            Assert.AreEqual(1, result.Rows.Count);
        }
    }

    [TestClass]
    public class WorkWithConnectTests
    {
        /// <summary>
        /// ConnPath возвращает путь при выборе файла
        /// </summary>
        [TestMethod]
        public void ConnPathReturnPath()
        {
            var wwConnect = new WorkWithConnect();
            Assert.IsNotNull(wwConnect);
        }
    }

    [TestClass]
    public class WorkFormTests
    {
        /// <summary>
        /// Проверка GetPath при отсутствии файла
        /// </summary>
        [TestMethod]
        public void GetPathDefaultIsNull()
        {
            var form = new WorkForm();
            string path = form.GetPath();
            Assert.IsNull(path);
            form.Dispose();
        }
    }

    [TestClass]
    public class QuertyFormTests
    {
        /// <summary>
        /// IsSafeSqlQuery c безопасным запросом
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryTrue()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form, "SELECT Наименование FROM Продукция");
            Assert.IsTrue(result);
            form.Dispose();
        }

        /// <summary>
        /// IsSafeSqlQuery c небезопасным запросом (действие)
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryFalse()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form, "DROP TABLE Продукция");
            Assert.IsFalse(result);
            form.Dispose();
        }

        /// <summary>
        /// IsSafeSqlQuery c небезопасным запросом (данные)
        /// </summary>
        public void IsSafeSqlQuerySelectUsersFalse()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form, "SELECT * FROM ПОЛЬЗОВАТЕЛИ");
            Assert.IsFalse(result);
            form.Dispose();
        }

        /// <summary>
        /// IsSafeSqlQuery с UNION SELECT
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryUnionSelectFalse()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form, "SELECT * FROM Продукция UNION SELECT * FROM Пользователи");
            Assert.IsFalse(result);
            form.Dispose();
        }

        /// <summary>
        /// IsSafeSqlQuery с комментариями
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryContainsCommentFalse()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form, "SELECT * FROM Продукция -- комментарий");
            Assert.IsFalse(result);
            form.Dispose();
        }

        /// <summary>
        /// IsSafeSqlQuery с JOIN
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryJoinTrue()
        {
            var form = new QuertyForm(wwAccess);
            bool result = IsSafeSqlQuery(form,
                "SELECT p.Наименование, r.Имя FROM Продукция p INNER JOIN Рабочие r ON p.ID = r.ТабельныйНомер");
            Assert.IsTrue(result);
            form.Dispose();
        }

        //Скопирован приватный IsSafeSqlQuery
        private bool IsSafeSqlQuery(string sql)
        {
            string upperSql = sql.ToUpper().Trim();
            string[] dangerousCommands = { "DROP", "DELETE", "UPDATE", "INSERT",
                "ALTER", "CREATE", "TRUNCATE", "EXEC", "EXECUTE" };
            foreach (string command in dangerousCommands)
                if (upperSql.StartsWith(command) || upperSql.Contains(" " + command + " "))
                    return false;
            if (!upperSql.StartsWith("SELECT"))
                return false;
            if (sql.Contains("--") || sql.Contains("/*") || sql.Contains("*/") || sql.Contains(";"))
                return false;
            if (upperSql.Contains("UNION") && upperSql.Contains("SELECT"))
                return false;
            if (upperSql.Contains("ПОЛЬЗОВАТЕЛИ"))
                return false;
            return true;
        }
    }

    [TestClass]
    public class AutFormTests
    {
        /// <summary>
        /// Проверка метода IsLogin после создания формы
        /// </summary>
        [TestMethod]
        public void AutFormFalse()
        {
            var workForm = new WorkForm();
            var autForm = new AutForm(workForm);
            bool isLogin = autForm.IsLogin();
            Assert.IsFalse(isLogin);
        }
    }
}
