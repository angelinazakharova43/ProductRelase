namespace ProductRelase
{
    public partial class RegistrationForm : Form
    {
        WorkWithAccess wwConn;

        /// <summary>
        /// Форма регистрации
        /// </summary>
        public RegistrationForm(WorkForm WF)
        {
            wwConn = new WorkWithAccess(WF.GetPath());
            InitializeComponent();
        }

        /// <summary>
        /// Попытка регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReg_Click(object sender, EventArgs e)
        {
            if (txtBoxPassword.Text == txtBoxPassword2.Text)
            {
                string str; bool success;
                string strLog = txtBoxLogin.Text.Trim().ToLower();
                string strPas = txtBoxPassword.Text.Trim().ToLower();
                if (strLog == null || strLog.Length < 8 || strLog.Length > 16)
                {
                    MessageBox.Show("Логин должен содержать от 8 до 16 символов", "Ошибка логина",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (strPas == null || strPas.Length < 8 || strPas.Length > 16)
                {
                    MessageBox.Show("Пароль должен содержать от 8 до 16 символов", "Ошибка пароля",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                wwConn.NewUser(txtBoxLogin.Text.Trim().ToLower(), txtBoxPassword.Text.Trim().ToLower(), out str, out success);
                if (!success)
                {
                    MessageBox.Show($"Ошибка при регистрации: {str}", "Ошибка при регистрации",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else MessageBox.Show("Новый пользователь загружен в систему", "Успешная регистрация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка пароля",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CleanTxt();
                return;
            }

            this.Close();
        }

        /// <summary>
        /// Очистка полей при неудачной регистрации
        /// </summary>
        private void CleanTxt()
        {
            txtBoxLogin.Text = "";
            txtBoxPassword.Text = "";
            txtBoxPassword2.Text = "";
        }
    }
}
