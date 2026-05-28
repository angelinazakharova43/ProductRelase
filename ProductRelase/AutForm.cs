using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductRelase
{
    public partial class AutForm : Form
    {
        private WorkForm workForm;
        private bool IsLog;
        public AutForm(WorkForm WF)
        {
            IsLog = false;
            workForm = WF;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            IsLog = true;
        }

        private void AutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            workForm.Show();
        }

        /// <summary>
        /// Поазывает, успешно ли прошёл вход в систему. Позволяет значению IsLog оставаться приватным (недоступным извне)
        /// </summary>
        /// <returns>true – успешно
        /// false – не успешно</returns>
        public bool IsLogin()
        {
            return IsLog;
        }
    }
}
