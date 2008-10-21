using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class ConnectionWindow : Form
    {
        /// <summary>
        ///     Initialize window to add a new entry.
        /// </summary>
        public ConnectionWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initialize window edit the given connection.
        /// </summary>
        public ConnectionWindow(ConnectionObject conObj)
        {
            InitializeComponent();
            this.conObj = conObj;
        }

        public ConnectionObject conObj;

        public ConnectionObject getConnectionObject()
        {
            return this.conObj;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Check if all entries are ok
            bool fieldsOK = validation();
            if (fieldsOK)
            {
                ConnectionObject newConObj = new ConnectionObject();
                newConObj.Active = this.conObj != null ? this.conObj.Active : true;
                newConObj.ContinousMode = this.checkBoxCycling.Checked;
                int hours = (int)this.numericHours.Value;
                int minutes = (int)this.numericMinutes.Value;
                int seconds = (int)this.numericSeconds.Value;
                newConObj.WaitTime = new TimeSpan(hours, minutes, seconds);
                newConObj.Order = this.conObj != null ? this.conObj.Order : 0;
                newConObj.Pop3ID = this.comboPOP3.SelectedIndex - 1; //  <-- -1 because first line is "please select"
                newConObj.SmtpID = this.comboSMTP.SelectedIndex - 1;
                newConObj.AddressID = this.comboAddress.SelectedIndex - 1;
                this.conObj = newConObj;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
         }

        private bool validation()
        {
            bool checkOK = true;
            StringBuilder errorMsg = new StringBuilder();
            if (this.comboPOP3.SelectedIndex == 0 || this.comboPOP3.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "POP3 Host");
                if (checkOK)
                {
                    this.comboPOP3.Select();
                }
                checkOK = false;
            }
            if (this.comboSMTP.SelectedIndex == 0 || this.comboSMTP.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "SMTP Host");
                if (checkOK)
                {
                    this.comboSMTP.Select();
                }
                checkOK = false;
            }
            if (this.comboAddress.SelectedIndex == 0 || this.comboAddress.Text.Length == 0)
            {
                errorMsg.Append((errorMsg.Length > 0 ? ", " : "") + "E-Mail-Address");
                if (checkOK)
                {
                    this.comboAddress.Select();
                }
                checkOK = false;
            }

            string msgEmpty = errorMsg.Length > 0 ? ("The following fields are not properly filled: " + errorMsg.ToString() + "\r\n") : "";
            errorMsg = new StringBuilder(msgEmpty);

            if (!checkOK)
            {
                MessageBox.Show(errorMsg.ToString(), "Invalid Entries", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return checkOK;
            // TODO use regex for host validation
        }

        private void ConnectionWindow_Load(object sender, EventArgs e)
        {
            string standard = "-- Please select --";
            this.comboPOP3.Items.Add(standard);
            this.comboSMTP.Items.Add(standard);
            this.comboAddress.Items.Add(standard);
            foreach (HostConfigObject popObj in SettingsObject.ListPOP3)
            {
                this.comboPOP3.Items.Add(popObj.Description);
            }
            foreach (HostConfigObject smtpObj in SettingsObject.ListSMTP)
            {
                this.comboSMTP.Items.Add(smtpObj.Description);
            }
            foreach (AddressObject addObj in SettingsObject.ListAddress)
            {
                this.comboAddress.Items.Add(addObj.AddressName);
            }

            // This window is in EDIT mode
            if (this.conObj != null)
            {
                this.comboPOP3.SelectedIndex = this.conObj.Pop3ID + 1;
                this.comboSMTP.SelectedIndex = this.conObj.SmtpID + 1;
                this.comboAddress.SelectedIndex = this.conObj.AddressID + 1;
                this.checkBoxCycling.Checked = this.conObj.ContinousMode;
                if (this.conObj.WaitTime != null)
                {
                    this.numericHours.Value = this.conObj.WaitTime.Hours;
                    this.numericMinutes.Value = this.conObj.WaitTime.Minutes;
                    this.numericSeconds.Value = this.conObj.WaitTime.Seconds;

                    // Adjust "Times per Hour" too
                    adjustTimesPerHour();
                }

                this.Text = "Edit Connection Entry";
            }
            else
            {
                this.comboPOP3.SelectedIndex = 0;
                this.comboSMTP.SelectedIndex = 0;
                this.comboAddress.SelectedIndex = 0;

                this.Text = "Add Connection Entry";
            }
        }

        private void checkBoxCycling_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            this.groupBoxTime.Enabled = checkBox.Checked;
        }

        private int oldMinValue = 0;
        private int oldSecValue = 0;

        private void numericAutoSwitcher(object sender)
        {
            NumericUpDown switch1 = (NumericUpDown)sender;
            NumericUpDown switch2 = null;
            NumericUpDown switch3 = null;
            int oldValue;
            int newValue = (int)switch1.Value;

            if (switch1 == this.numericSeconds)
            {
                oldValue = this.oldSecValue;
                this.oldSecValue = newValue;
                switch2 = this.numericMinutes;
                switch3 = this.numericHours;
            }
            else if (switch1 == this.numericMinutes)
            {
                oldValue = this.oldMinValue;
                this.oldMinValue = newValue;
                switch2 = this.numericHours;
            }
            else
            {
                return;
            }

            // User is scrolling up
            if (oldValue == 59 && newValue == 60)
            {
                if (switch2.Value < 59)
                {
                    switch2.Value++;
                    switch1.Value = 0;
                }
                else
                {
                    if (switch3 != null)
                    {
                        switch3.Value++;
                        switch2.Value = 0;
                    }
                }
            }

            // User is scrolling down
            if (oldValue == 0 && newValue == -1)
            {
                if (switch2.Value > 0)
                {
                    switch2.Value--;
                    switch1.Value = 59;
                }
                else
                {
                    if (switch3 != null && switch3.Value > 0)
                    {
                        switch3.Value--;
                        switch2.Value = 59;
                    }
                    else if (switch2.Value == 0)
                    {
                        switch1.Value = 0;
                    }
                }
            }

            // Reached the minimum value
            if (this.numericMinutes.Value == 0 && this.numericHours.Value == 0 && this.numericSeconds.Value < 30)
            {
                this.numericSeconds.Value = 30;
            }
        }

        private void adjustTimesPerHour()
        {
            int hours = (int)this.numericHours.Value;
            int minutes = (int)this.numericMinutes.Value;
            int seconds = (int)this.numericSeconds.Value;
            if (hours < 0 || minutes < 0 || seconds < 0)
            {
                return;
            }
            float minutesComplete = (hours * 60) + minutes + (seconds / 60);
            if (minutesComplete == 0)
            {
                //this.numericSeconds.Value = 30;
                return;
            }
            this.numericTimesPerHour.Value = (decimal)(60 / minutesComplete);
        }

        private void adjustHoursMinutesSeconds()
        {
            decimal times = this.numericTimesPerHour.Value;
            decimal hours = 0;
            decimal minutes = 60 / times;
            decimal seconds = 60 * (minutes % 60);
            if (minutes > 59)
            {
                hours = minutes / 60;
                minutes = minutes % 60;
            }
            if (hours >= this.numericHours.Maximum)
            {
                hours = this.numericHours.Maximum;
            }
            this.numericHours.Value = hours;
            if (minutes >= this.numericMinutes.Maximum)
            {
                minutes = this.numericMinutes.Maximum;
            }
            this.numericMinutes.Value = minutes;
            if (seconds >= this.numericSeconds.Maximum)
            {
                seconds = this.numericSeconds.Maximum;
            }
            this.numericSeconds.Value = seconds;
        }

        private void numericField_ValueChanged(object sender, EventArgs e)
        {
            if (!((NumericUpDown)sender).Focused)
            {
                return;
            }
            numericAutoSwitcher(sender);

            // Check out which widget is currently selected
            if (this.numericHours.Focused || this.numericMinutes.Focused || this.numericSeconds.Focused)
            {
                adjustTimesPerHour();
            }
            else
            {
                adjustHoursMinutesSeconds();
            }
        }

        private void checkBoxLeaveMessages_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxReceiveNewOnly.Enabled = this.checkBoxLeaveMessages.Checked;
        }
    }
}