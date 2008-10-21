using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace POP3Pipe
{
    public partial class SMTPWindow : Form
    {
        public SMTPWindow()
        {
            InitializeComponent();
        }

        public SMTPWindow(HostConfigObject hostObj)
        {
            InitializeComponent();
            this.hostObj = hostObj;
        }

        public HostConfigObject hostObj;

        public HostConfigObject getHostConfigObject()
        {
            return this.hostObj;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Check if all entries are ok
            bool fieldsOK = validation();
            if (fieldsOK)
            {
                if (this.hostObj == null)
                {
                    this.hostObj = new HostConfigObject();
                }
                this.hostObj.Username = this.txtUsername.Text;
                this.hostObj.Password = this.txtPassword.Text;
                // If description is empty use the host name
                if (this.txtDescription.Text == null || this.txtDescription.Text.Length == 0)
                {
                    this.txtDescription.Text = this.txtHost.Text;
                }
                this.hostObj.Description = this.txtDescription.Text;
                this.hostObj.Host = this.txtHost.Text;
                // Differ between port variants
                if (this.txtPort.Text.Equals("###"))
                {
                    this.hostObj.Port = 0;
                }
                else if (this.txtPort.Text.Length == 0)
                {
                    MessageBox.Show("Enter a value as port number.");
                    return;
                }
                else
                {
                    int portNr = 0;
                    int.TryParse(this.txtPort.Text, out portNr);
                    this.hostObj.Port = portNr;
                }
                this.hostObj.EMail = this.txtEmail.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool validation()
        {
            bool checkOK = true;
            StringBuilder errorMsg = new StringBuilder();
            if (this.txtHost.Text == null || this.txtHost.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "SMTP Host");
                if (checkOK)
                {
                    this.txtHost.Select();
                }
                checkOK = false;
            }
            if (this.comboBoxPortVariant.Text == null || this.comboBoxPortVariant.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Port");
                if (checkOK)
                {
                    this.comboBoxPortVariant.Select();
                }
                checkOK = false;
            }
            if (this.txtPort.Text == null || this.txtPort.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Port Number");
                if (checkOK)
                {
                    this.txtPort.Select();
                }
                checkOK = false;
            }
            if (this.txtUsername.Text == null || this.txtUsername.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Username");
                if (checkOK)
                {
                    this.txtUsername.Select();
                }
                checkOK = false;
            }
            if (this.txtPassword.Text == null || this.txtPassword.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Password");
                if (checkOK)
                {
                    this.txtPassword.Select();
                }
                checkOK = false;
            }
            if (this.txtPasswordConfirm.Text == null || this.txtPasswordConfirm.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "Password Confirm");
                if (checkOK)
                {
                    this.txtPasswordConfirm.Select();
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
                    this.txtPassword.Select();
                }
                checkOK = false;
            }

            // Check if dataset is already existing
            HostConfigObject hostObj = findSMTPObjInList();
            if (hostObj != null && this.hostObj == null)
            {
                errorMsg.Append("This SMTP Host has already been entered.");
                checkOK = false;
            }

            if (!checkOK)
            {
                MessageBox.Show(errorMsg.ToString(), "Invalid Entries", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return checkOK;
            // TODO use regex for host validation
        }

        private void SMTPWindow_Load(object sender, EventArgs e)
        {
            // This window is in EDIT mode
            if (this.hostObj != null)
            {
                this.Text = "Edit SMTP Host Entry";

                this.txtDescription.Text = this.hostObj.Description;
                this.txtHost.Text = this.hostObj.Host;
                this.txtPassword.Text = this.hostObj.Password;
                this.txtPasswordConfirm.Text = this.hostObj.Password;
                this.txtUsername.Text = this.hostObj.Username;
                this.txtEmail.Text = this.hostObj.EMail;
                if (this.hostObj.Port == 0)
                {
                    this.txtPort.Text = "###";
                    this.comboBoxPortVariant.SelectedIndex = 0;
                }
                else
                {
                    if (this.hostObj.Port == 25)
                    {
                        this.comboBoxPortVariant.SelectedIndex = 1;
                    }
                    else if (this.hostObj.Port == 587)
                    {
                        this.comboBoxPortVariant.SelectedIndex = 2;
                    }
                    else
                    {
                        this.comboBoxPortVariant.SelectedIndex = 3;
                    }
                    this.txtPort.Text = this.hostObj.Port.ToString();
                }
            }
            else
            {
                this.Text = "Add SMTP Host Entry";
                this.comboBoxPortVariant.SelectedIndex = 0;
            }
        }

        private HostConfigObject findSMTPObjInList()
        {
            foreach (HostConfigObject hostObj in SettingsObject.ListSMTP)
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
                this.txtPort.Text = "465";
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

        private string portTemp = "";

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            Regex regex = new Regex(@"^((\d+)|(###))$");
            if (regex.IsMatch(textbox.Text))
            {
                this.portTemp = textbox.Text;
            }
            else
            {
                textbox.Text = this.portTemp;
            }
        }
    }
}