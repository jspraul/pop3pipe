using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class SMTPWindow : Form
    {
        public SMTPWindow()
        {
            InitializeComponent();
        }

        public bool addEntry;

        public HostConfigObject hostObj;

        private void btnOkSMTP_Click(object sender, EventArgs e)
        {
            // Check if all entries are ok
            bool fieldsOK = validation();
            if (fieldsOK)
            {
                this.hostObj = new HostConfigObject();
                this.hostObj.Description = this.txtDescription.Text;
                this.hostObj.Host = this.txtHost.Text;
                this.hostObj.Username = this.txtUsername.Text;
                this.hostObj.Password = this.txtPassword.Text;
                this.hostObj.Active = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("All fields must be filled and passwords must be equal.", "Invalid Entries", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtHost.Select();
            }
        }

        private bool validation()
        {
            bool checkOK = true;
            if (this.txtPassword.Text == null ||
                this.txtPassword.Text.Length == 0 ||
                this.txtPasswordConfirm.Text == null ||
                this.txtPasswordConfirm.Text.Length == 0 ||
                this.txtHost.Text == null ||
                this.txtHost.Text.Length == 0 ||
                this.txtDescription.Text == null ||
                this.txtDescription.Text.Length == 0 ||
                this.txtUsername.Text == null ||
                this.txtUsername.Text.Length == 0)
            {
                return false;
            }
            if (!this.txtPassword.Text.Equals(this.txtPasswordConfirm.Text))
            {
                return false;
            }
            return checkOK;
            // TODO use regex for host validation
        }

        private void SMTPWindow_Load(object sender, EventArgs e)
        {
            if (addEntry)
            {
                this.Text = "Add SMTP Host Entry";
            }
            else
            {
                this.Text = "Edit SMTP Host Settings";
            }
        }

        private void comboBoxPortVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selection = (string)this.comboBoxPortVariant.SelectedItem;
            if (selection.Equals("-- automatic --"))
            {
                this.txtPort.Enabled = false;
                this.txtPort.Text = "###";
            }
            else if (selection.Equals("STANDARD"))
            {
                this.txtPort.Enabled = false;
                this.txtPort.Text = "25";
            }
            else if (selection.Equals("SSL"))
            {
                this.txtPort.Enabled = false;
                this.txtPort.Text = "587";
            }
            if (selection.Equals("-- custom --"))
            {
                this.txtPort.Enabled = true;
                this.txtPort.Text = "";
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.txtPasswordConfirm.Text.Length > 0)
            {
                this.txtPasswordConfirm.Text = "";
            }
        }
    }
}