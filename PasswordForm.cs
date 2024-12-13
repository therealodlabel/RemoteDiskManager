using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDiskManager
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        public SecureString SecurePassword { get; private set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SecurePassword = new SecureString();
            foreach (char c in txtPassword.Text)
            {
                SecurePassword.AppendChar(c);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}