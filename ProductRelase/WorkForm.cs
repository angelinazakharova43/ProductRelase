using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProductRelase
{
    public partial class WorkForm : Form
    {
        private LoginForm loginForm;
        public WorkForm(LoginForm LF)
        {
            InitializeComponent();

            loginForm = LF;
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            loginForm.Close();
            this.Close();
        }
    }
}
