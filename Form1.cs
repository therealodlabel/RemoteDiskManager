using RemoteDiskManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskSpaceAndUserManagement
{
    public partial class Form1 : Form
    {
        private TextBox txtAdminUsername;
        private TextBox txtAdminPassword;
        private TextBox txtComputerName;
        private Button btnConnect;
        private ListBox lstUsers;
        private Button btnDeleteUser;
        private Label lblFreeSpace;

        public Form1()
        {
            InitializeComponent();

            // Create the controls
            txtAdminUsername = new TextBox();
            txtAdminPassword = new TextBox();
            txtComputerName = new TextBox();
            btnConnect = new Button();
            lstUsers = new ListBox();
            btnDeleteUser = new Button();
            lblFreeSpace = new Label();

            // Add the controls to the form
            this.Controls.Add(txtAdminUsername);
            this.Controls.Add(txtAdminPassword);
            this.Controls.Add(txtComputerName);
            this.Controls.Add(btnConnect);
            this.Controls.Add(lstUsers);
            this.Controls.Add(btnDeleteUser);
            this.Controls.Add(lblFreeSpace);

            // Set the control properties
            txtAdminUsername.Location = new Point(12, 12);
            txtAdminUsername.Size = new Size(200, 20);
            txtAdminPassword.Location = new Point(12, 38);
            txtAdminPassword.PasswordChar = '*';
            txtAdminPassword.Size = new Size(200, 20);
            txtComputerName.Location = new Point(12, 64);
            txtComputerName.Size = new Size(200, 20);
            btnConnect.Location = new Point(12, 90);
            btnConnect.Size = new Size(75, 23);
            btnConnect.Text = "Connect";
            lstUsers.Location = new Point(12, 119);
            lstUsers.Size = new Size(200, 212);
            btnDeleteUser.Location = new Point(12, 337);
            btnDeleteUser.Size = new Size(75, 23);
            btnDeleteUser.Text = "Delete User";
            lblFreeSpace.Location = new Point(218, 12);
            lblFreeSpace.Size = new Size(200, 20);

            // Add event handlers
            btnConnect.Click += new EventHandler(btnConnect_Click);
            btnDeleteUser.Click += new EventHandler(btnDeleteUser_Click);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Get the secure password
            SecureString password = GetPassword();
            if (password == null) return; // Password entry cancelled

            // Connect to the remote computer and get data
            try
            {
                using (Runspace runspace = RunspaceFactory.CreateRunspace())
                {
                    runspace.Open();

                    using (PowerShell ps = PowerShell.Create())
                    {
                        ps.Runspace = runspace;
                        ps.AddCredential(new PSCredential(txtAdminUsername.Text, password));

                        // Get disk space
                        ps.AddCommand("Get-WmiObject").AddArgument("Win32_LogicalDisk").AddParameter("Filter", "DeviceID='C:'");
                        var diskSpaceResult = ps.Invoke();
                        if (diskSpaceResult.Count > 0)
                        {
                            lblFreeSpace.Text = $"Free Space: {((ulong)diskSpaceResult[0].Properties["FreeSpace"].Value).ToString("#,##0")} bytes";
                        }
                        else
                        {
                            MessageBox.Show("Error retrieving disk space information.");
                        }

                        // Get users
                        ps.Commands.Clear();
                        ps.AddCommand("Get-ChildItem").AddArgument("C:\\Users").AddParameter("Directory");
                        var userResult = ps.Invoke();
                        lstUsers.Items.Clear();
                        foreach (var user in userResult)
                        {
                            lstUsers.Items.Add(user.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to remote computer: {ex.Message}");
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            // Confirmation dialog
            if (MessageBox.Show($"Are you sure you want to delete user '{lstUsers.SelectedItem}'?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Delete the user
                DeleteUser(lstUsers.SelectedItem.ToString());

                // Refresh the user list
                btnConnect_Click(sender, e);
            }
        }

        private void DeleteUser(string username)
        {
            // Get the secure password
            SecureString password = GetPassword();
            if (password == null) return; // Password entry cancelled

            try
            {
                using (Runspace runspace = RunspaceFactory.CreateRunspace())
                {
                    runspace.Open();

                    using (PowerShell ps = PowerShell.Create())
                    {
                        ps.Runspace = runspace;
                        ps.AddCredential(new PSCredential(txtAdminUsername.Text, password));
                        ps.AddCommand("Remove-Item").AddArgument("C:\\Users\\" + username).AddParameter("Recurse").AddParameter("Force");
                        ps.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}");
            }
        }

        private SecureString GetPassword()
        {
            SecureString password = new SecureString();
            using (var form = new PasswordForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    password = form.SecurePassword;
                    return password;
                }
            }
            return null;
        }
    }
}