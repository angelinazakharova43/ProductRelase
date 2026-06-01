namespace ProductRelase
{
    public partial class RegistrationForm : Form
    {
        WorkWhisConnection wwConn;

        /// <summary>
        /// Форма регистрации
        /// </summary>
        public RegistrationForm(WorkForm WF)
        {
            wwConn = new WorkWhisConnection(WF.GetPath());
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
                try
                {
                    BDUser newUser = new BDUser(txtBoxLogin.Text.Trim().ToLower(), txtBoxPassword.Text.Trim().ToLower());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка логина или пароля",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CleanTxt();
                    return;
                }

                string str;
                if (!wwConn.NewUser(txtBoxLogin.Text.Trim().ToLower(), txtBoxPassword.Text.Trim().ToLower(), out str))
                {
                    MessageBox.Show($"Ошибка при регистрации: {str}", "Ошибка при регистрации",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Новый пользователь загружен в систему", "Успешная регистрация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
