using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class AddressWindow : Form
    {
        public AddressWindow()
        {
            InitializeComponent();
        }

        public AddressWindow(AddressObject addrObj)
        {
            InitializeComponent();
            this.addrObj = addrObj;
        }

        private AddressObject addrObj;

        public AddressObject getAddressObject()
        {
            return this.addrObj;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Check if all entries are ok
            bool fieldsOK = validation();
            if (fieldsOK)
            {
                if (this.addrObj == null)
                {
                    this.addrObj = new AddressObject();
                }
                if (this.txtDescription.Text == null || this.txtDescription.Text.Length == 0)
                {
                    this.txtDescription.Text = this.txtEMail.Text;
                }
                this.addrObj.AddressName = this.txtDescription.Text;
                this.addrObj.AddressEMail = this.txtEMail.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool validation()
        {
            bool checkOK = true;
            if (this.txtEMail.Text == null ||
                this.txtEMail.Text.Length == 0)
            {
                return false;
            }
            return checkOK;
            // TODO use regex for email address validation
        }

        private void AddressWindow_Load(object sender, EventArgs e)
        {
            // This window is in EDIT mode
            if (this.addrObj != null)
            {
                this.Text = "Edit E-Mail Address Entry";

                this.txtDescription.Text = this.addrObj.AddressName;
                this.txtEMail.Text = this.addrObj.AddressEMail;
            }
            else
            {
                this.Text = "Add E-Mail Address Entry";
            }
        }
    }
}