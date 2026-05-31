namespace ProductRelase
{
    public partial class RegistrationForm : Form
    {

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (txtBoxPassword.Text.Length == txtBoxPassword.Text.Length)
            {
                try
                {
                    BDUser newUser = new BDUser(txtBoxLogin.Text, txtBoxPassword.Text, "Админ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка логина или пароля",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBoxLogin.Text = "";
                    txtBoxPassword.Text = "";
                    return;
                }

                MessageBox.Show("Новый пользователь загружен в систему", "Успешная регистрация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка пароля",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Close();
        }
    }
}
