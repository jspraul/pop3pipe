using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class POP3Window : Form
    {
        public POP3Window()
        {
            InitializeComponent();
        }

        public bool addEntry;

        public HostConfigObject hostObj;

        private void btnOk_Click(object sender, EventArgs e)
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
            StringBuilder errorMsg = new StringBuilder();
            if (this.txtDescription.Text == null || this.txtDescription.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Description");
                if (checkOK)
                {
                    txtDescription.Select();
                }
                checkOK = false;
            }
            if (this.txtHost.Text == null || this.txtHost.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Host");
                if (checkOK)
                {
                    txtHost.Select();
                }
                checkOK = false;
            }
            if (this.txtUsername.Text == null || this.txtUsername.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Username");
                if (checkOK)
                {
                    txtUsername.Select();
                }
                checkOK = false;
            }
            if (this.txtPassword.Text == null || this.txtPassword.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Password");
                if (checkOK)
                {
                    txtPassword.Select();
                }
                checkOK = false;
            }
            if (this.txtPasswordConfirm.Text == null || this.txtPasswordConfirm.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Password Confirm");
                if (checkOK)
                {
                    txtPasswordConfirm.Select();
                }
                checkOK = false;
            }

            string msgEmpty = errorMsg.Length > 0 ? ("The following fields have not been filled: " + errorMsg.ToString() + "\r\n") : "";
            errorMsg = new StringBuilder(msgEmpty);
            
            // Check for valid password
            if (!this.txtPassword.Text.Equals(this.txtPasswordConfirm.Text))
            {
                errorMsg.Append("Passwords must be equal. Try again.\r\n");
                if (checkOK)
                {
                    txtPassword.Select();
                }
                checkOK = false;
            }

            // Check if dataset is already existing
            HostConfigObject hostObj = findPOP3ObjInList();
            if (hostObj != null)
            {
                errorMsg.Append("This POP3 Host has already been entered.");
                checkOK = false;
            }

            if (!checkOK)
            {
                MessageBox.Show(errorMsg.ToString(), "Invalid Entries", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return checkOK;
            // TODO use regex for host validation
        }

        private void POP3Window_Load(object sender, EventArgs e)
        {
            if (addEntry)
            {
                this.Text = "Add POP3 Host Entry";
            }
            else
            {
                this.Text = "Edit POP3 Host Settings";
            }
        }

        private HostConfigObject findPOP3ObjInList()
        {
            foreach (HostConfigObject hostObj in SettingsObject.ListPOP3)
            {
                if (this.txtDescription.Text.Equals(hostObj.Description) &&
                    this.txtHost.Text.Equals(hostObj.Host) &&
                    this.txtUsername.Text.Equals(hostObj.Username) &&
                    this.txtPassword.Text.Equals(hostObj.Password))
                {
                    // Searched object was found
                    return hostObj;
                }
            }
            return null;
        }
    }
}