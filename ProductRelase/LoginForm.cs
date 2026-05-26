namespace ProductRelase
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрытие приложения до начала работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnBtn_Click(object sender, EventArgs e)
        {
            string path = ConnPath();
            if (path != null)
            {
                WorkForm workForm = new WorkForm(this);
                workForm.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// Получение пути к файлу с базой даннных
        /// </summary>
        /// <returns></returns>
        private string ConnPath()
        {
            string path = null;
            OpenFileDialog fileDialog = new OpenFileDialog();

            try
            {
                fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                    MessageBox.Show($"Выбран файл: {path}");
                }
                else
                {
                    MessageBox.Show("Выбран не файл");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выборе файла базы данных");
            }

            return path;
        }
    }
}
