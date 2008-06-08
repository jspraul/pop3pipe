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

        public AddressObject addObj;

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Check if all entries are ok
            bool fieldsOK = validation();
            if (fieldsOK)
            {
                this.addObj = new AddressObject();
                this.addObj.AddressName = this.txtDescription.Text;
                this.addObj.AddressEMail = this.txtEMail.Text;
                this.addObj.Active = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("All fields must be filled.", "Invalid Entries", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtDescription.Select();
            }
        }

        private bool validation()
        {
            bool checkOK = true;
            if (this.txtDescription.Text == null ||
                this.txtDescription.Text.Length == 0 ||
                this.txtEMail.Text == null ||
                this.txtEMail.Text.Length == 0)
            {
                return false;
            }
            return checkOK;
            // TODO use regex for email address validation
        }
    }
}