using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductRelase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TestProject1
{
    [TestClass]
    public class WorkWithAccessTests
    {
        private static string strPat = "C:\\Users\\Ari\\Desktop\\1 course\\Malcov\\Practic\\ProductRelase\\TestProject1\\Test.accdb";
        private WorkWithAccess wwAccess = new WorkWithAccess(strPat);

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
            wwAccess.NewUser("testuser2", "password123", out str, out success);
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
            wwAccess.NewUser("админадмин", "pass1", out str, out success);
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
            wwAccess.CheckUser("админадмин", "12345678", out str, out success);
            Assert.IsTrue(success);
        }

        /// <summary>
        /// Проверка входа с неверным паролем
        /// </summary>
        [TestMethod]
        public void CheckUserFalsePassword()
        {
            string str; bool success;
            wwAccess.CheckUser("админадмин", "incorrect", out str, out success);
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
            Assert.AreEqual(1, rowsAff);
        }

        /// <summary>
        /// Проверка метода AddLine с некорректными параметрами (несуществующая таблица)
        /// </summary>
        [TestMethod]
        public void AddLineNonExistentTableException()
        {
            var parameters = new OleDbParameter[]
            { new OleDbParameter("@Колонка", "Значение") };

            try
            {
                wwAccess.AddLine("НесуществующаяТаблица", parameters);
                Assert.Fail("Должно быть выброшено исключение OleDbException");
            }
            catch (OleDbException)
            { }
            catch (Exception ex)
            { Assert.Fail($"Ожидалось OleDbException, но получено {ex.GetType().Name}: {ex.Message}"); }
        }

        /// <summary>
        /// Проверка метода AddLine с пустым массивом параметров
        /// </summary>
        [TestMethod]
        public void AddLineEmptyParametersException()
        {
            try
            {
                wwAccess.AddLine("ТестоваяТаблица", new OleDbParameter[0]);
                Assert.Fail("Должно быть выброшено исключение ArgumentException");
            }
            catch (ArgumentException)
            { }
            catch (Exception ex)
            { Assert.Fail($"Ожидалось ArgumentException, но получено {ex.GetType().Name}: {ex.Message}"); }
        }

        /// <summary>
        /// Проверка получения схемы таблицы
        /// </summary>
        [TestMethod]
        public void GetTableSchemaSchema()
        {
            DataTable schema = wwAccess.GetTableSchema("Продукция");
            Assert.IsNotNull(schema);
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
            var queryParams = new Dictionary<string, object> { { "@searchPattern", "%Филен%" } };
            DataTable result = wwAccess.ExecuteParameterizedQuery(sql, queryParams);
            Assert.AreEqual(3, result.Rows.Count);
        }

        /// <summary>
        /// Проверка некорректного SQL
        /// </summary>
        [TestMethod]
        public void ExecuteParameterizedQueryInvalidSqlException()
        {
            try
            {
                wwAccess.ExecuteParameterizedQuery("НЕКОРРЕКТНЫЙ SQL ЗАПРОС", new Dictionary<string, object>());
                Assert.Fail("Должно быть выброшено исключение");
            }
            catch (OleDbException)
            { }
            catch (Exception ex)
            { Assert.Fail($"Ожидалось OleDbException, но получено {ex.GetType().Name}: {ex.Message}"); }
        }

        /// <summary>
        /// Проверка корректности многократного открытия и закрытия соединения
        /// </summary>
        [TestMethod]
        public void MultipleOpenClose_ShouldNotCauseErrors()
        {
            for (int i = 0; i < 10; i++)
            {
                DataTable table = wwAccess.GetDataFromTable("Рабочие");
                Assert.IsNotNull(table, $"Итерация {i}: таблица не должна быть null");
            }
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

        /// <summary>
        /// Проверка метода GetPath
        /// </summary>
        [TestMethod]
        public void GetPathReturnPath()
        {
            var form = new WorkForm();
            var pathField = typeof(WorkForm).GetField("path", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            pathField.SetValue(form, "Test.accdb");
            string path = form.GetPath();
            Assert.AreEqual("Test.accdb", path);
        }

        /// <summary>
        /// Проверка метода GetPath до установки пути
        /// </summary>
        [TestMethod]
        public void GetPathBeforeSetting()
        {
            var newForm = new WorkForm();
            string path = newForm.GetPath();
            Assert.IsNull(path, "До установки путь должен быть null");
        }

        [TestMethod]
        public void IsEnabledNullRoleException()
        {
            WorkForm workForm = new WorkForm();
            InvokeIsEnabled(workForm, null);
            Assert.IsFalse(GetControlEnabled("cmbBoxTable", workForm));
            Assert.IsFalse(GetControlEnabled("btnFindLine", workForm));
            Assert.IsFalse(GetControlEnabled("btnAddLine", workForm));
            Assert.IsFalse(GetControlEnabled("btnChangeLine", workForm));
            Assert.IsFalse(GetControlEnabled("btnDeleteLine", workForm));
            Assert.IsFalse(GetControlEnabled("btnCreateReport", workForm));
        }

        /// <summary>
        /// Вызов приватного IsEnabled
        /// </summary>
        private void InvokeIsEnabled(WorkForm form, string role)
        {
            var method = typeof(WorkForm).GetMethod("IsEnabled",
                BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null) Assert.Fail();
            method.Invoke(form, new object[] { role });
        }

        /// <summary>
        /// Получает состояние Enabled для контрола по имени
        /// </summary>
        private bool GetControlEnabled(string controlName, WorkForm workForm)
        {
            var controls = workForm.Controls.Find(controlName, true);
            if (controls.Length > 0)
            { return controls[0].Enabled; }
            Assert.Fail();
            return false;
        }
    }

    [TestClass]
    public class QuertyFormSecurityTests
    {
        /// <summary>
        /// Проверка безопасности SQL — SELECT
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQuerySelectTrue()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "Метод IsSafeSqlQuery существует");
            bool result = (bool)method.Invoke(form,
                new object[] { "SELECT * FROM Продукция WHERE Цена > 100" });
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Проверка безопасности SQL — !SELECT
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryNotSelect()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            bool result = (bool)method.Invoke(form,
                new object[] { "SHOW TABLES" });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверка безопасности SQL — DROP TABLE
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryDropFalse()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            bool result = (bool)method.Invoke(form,
                new object[] { "DROP TABLE Продукция" });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверка безопасности SQL — комментарии
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryCommentFalse()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            bool result = (bool)method.Invoke(form,
                new object[] { "SELECT * FROM Продукция -- вредоносный код" });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверка безопасности SQL — UNION SELECT
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryUnionFalse()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            bool result = (bool)method.Invoke(form,
                new object[] { "SELECT Название FROM Продукция UNION SELECT Пароль FROM Пользователи" });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверка безопасности SQL — таблица пользователей
        /// </summary>
        [TestMethod]
        public void IsSafeSqlQueryUsersFalse()
        {
            var form = new QuertyForm(null);
            var method = typeof(QuertyForm).GetMethod("IsSafeSqlQuery",
                BindingFlags.NonPublic | BindingFlags.Instance);
            bool result = (bool)method.Invoke(form,
                new object[] { "SELECT * FROM Пользователи" });
            Assert.IsFalse(result);
        }
    }

    [TestClass]
    public class ExportMethodsTests
    {
        private QuertyForm quertyForm;
        private DataTable testDataTable;

        [TestInitialize]
        public void Setup()
        { quertyForm = new QuertyForm(null); }

        /// <summary>
        /// Проверка существования и работы ExportToWord
        /// </summary>
        [TestMethod]
        public void ExportToWordMethod()
        {
            var method = typeof(QuertyForm).GetMethod("ExportToWord",
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method);
            Assert.AreEqual(1, method.GetParameters().Length);
            Assert.AreEqual(typeof(DataTable), method.GetParameters()[0].ParameterType);
        }

        /// <summary>
        /// Проверка существования и работы ExportToExcel
        /// </summary>
        [TestMethod]
        public void ExportToExcelMethod()
        {
            var method = typeof(QuertyForm).GetMethod("ExportToExcel",
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method);
            Assert.AreEqual(1, method.GetParameters().Length);
            Assert.AreEqual(typeof(DataTable), method.GetParameters()[0].ParameterType);
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
