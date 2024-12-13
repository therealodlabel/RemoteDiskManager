namespace RemoteDiskManager
{
    partial class PasswordForm
    {
        private System.Windows.Forms.MaskedTextBox txtPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            this.txtPassword = new System.Windows.Forms.MaskedTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(12, 12);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(12, 38);
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(137, 38);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // PasswordForm
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.ClientSize = new System.Drawing.Size(224, 73);
            this.Name = "PasswordForm";
            this.Text = "Enter Password";
            this.ResumeLayout(false);
        }
    }
}