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
        ///     Initialize window without parameters.
        /// </summary>
        public ConnectionWindow()
        {
            InitializeComponent();
            this.listPOP3 = SettingsObject.ListPOP3;
            this.listSMTP = SettingsObject.ListSMTP;
            this.listAddress = SettingsObject.ListAddress;
            ConnectionWindow_Load();
        }

        /// <summary>
        ///     Initialize window with given connection settings.
        /// </summary>
        public ConnectionWindow(ConnectionObject conObj)
        {
            InitializeComponent();
            this.conObj = conObj;
            this.listPOP3 = SettingsObject.ListPOP3;
            this.listSMTP = SettingsObject.ListSMTP;
            this.listAddress = SettingsObject.ListAddress;
            ConnectionWindow_Load();
        }


        /// <summary>
        ///     Opens the window without any options set. Used for adding a connection.
        /// </summary>
        /// <returns></returns>
        public DialogResult OpenWindow()
        {
            return this.ShowDialog();
        }

        /// <summary>
        ///     Open the window with predefined options. Used when editing a connection.
        /// </summary>
        /// <param name="pop3Id"></param>
        /// <param name="smtpId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public DialogResult OpenWindow(int pop3Id, int smtpId, int addressId)
        {
            this.comboPOP3.SelectedIndex = pop3Id + 1;
            this.comboSMTP.SelectedIndex = smtpId + 1;
            this.comboAddress.SelectedIndex = addressId + 1;
            return this.OpenWindow();
        }

        public bool addEntry;
        public ConnectionObject conObj;

        public List<HostConfigObject> listPOP3 = new List<HostConfigObject>();
        public List<HostConfigObject> listSMTP = new List<HostConfigObject>();
        public List<AddressObject> listAddress = new List<AddressObject>();

        private void btnOk_Click(object sender, EventArgs e)
        {
            // TODO: check fields for validity
            this.conObj = new ConnectionObject();
            this.conObj.Pop3ID = this.comboPOP3.SelectedIndex - 1; //  <-- -1 because first line is "please select"
            this.conObj.SmtpID = this.comboSMTP.SelectedIndex - 1;
            this.conObj.AddressID = this.comboAddress.SelectedIndex - 1;

            int hours = (int)this.numericHours.Value;
            int minutes = (int)this.numericMinutes.Value;
            int seconds = (int)this.numericSeconds.Value;
            this.conObj.WaitTime = new TimeSpan(hours, minutes, seconds);

            this.conObj.ContinousMode = this.checkBoxCycling.Checked;

            this.conObj.Active = true;
            this.Close();
        }

        private void ConnectionWindow_Load()
        {
            string standard = "-- Please select --";
            comboPOP3.Items.Add(standard);
            comboSMTP.Items.Add(standard);
            comboAddress.Items.Add(standard);
            foreach (HostConfigObject popObj in this.listPOP3)
            {
                comboPOP3.Items.Add(popObj.Description);
            }
            foreach (HostConfigObject smtpObj in this.listSMTP)
            {
                comboSMTP.Items.Add(smtpObj.Description);
            }
            foreach (AddressObject addObj in this.listAddress)
            {
                comboAddress.Items.Add(addObj.AddressName);
            }

            // Predefine given settings
            if (this.conObj != null)
            {
                this.comboPOP3.SelectedIndex = this.conObj.Pop3ID;
                this.comboSMTP.SelectedIndex = this.conObj.SmtpID;
                this.comboAddress.SelectedIndex = this.conObj.AddressID;
                this.checkBoxCycling.Checked = this.conObj.ContinousMode;
                if (this.conObj.WaitTime != null)
                {
                    this.numericHours.Value = this.conObj.WaitTime.Hours;
                    this.numericMinutes.Value = this.conObj.WaitTime.Minutes;
                    this.numericSeconds.Value = this.conObj.WaitTime.Seconds;

                    // Adjust "Times per Hour" too
                    adjustTimesPerHour();
                }
            }
            else
            {
                comboPOP3.SelectedIndex = 0;
                comboSMTP.SelectedIndex = 0;
                comboAddress.SelectedIndex = 0;
            }
        }

        private void checkBoxCycling_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            this.groupBox1.Enabled = checkBox.Checked;
        }

        private int oldMinValue = 0;
        private int oldSecValue = 0;

        private void numericAutoSwitcher(object sender)
        {
            NumericUpDown switch1 = (NumericUpDown)sender;
            NumericUpDown switch2;
            int oldValue;
            int newValue = (int)switch1.Value;

            if (switch1 == this.numericSeconds)
            {
                oldValue = this.oldSecValue;
                this.oldSecValue = newValue;
                switch2 = this.numericMinutes;
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
                switch2.Value++;
                switch1.Value = 0;
            }

            // User is scrolling down
            if (oldValue == 0 && newValue == -1)
            {
                if (switch2.Value > 0)
                {
                    switch2.Value--;
                    switch1.Value = 59;
                }
            }
        }

        private void adjustTimesPerHour()
        {
            int hours = (int)this.numericHours.Value;
            int minutes = (int)this.numericMinutes.Value;
            int seconds = (int)this.numericSeconds.Value;
            float minutesComplete = (hours * 60) + minutes + (seconds / 60);
            this.numericTimesPerHour.Value = (int)(60 / minutesComplete);
        }

        private void adjustHoursMinutesSeconds()
        {
            int times = (int)this.numericTimesPerHour.Value;
            int hours = 0;
            float minutesFloat = 60f / (float)times;
            int minutes = (int)(minutesFloat);
            float secondsFloat = 60 * ((minutesFloat) - minutes);
            int seconds = (int)secondsFloat;
            if (minutes == 60)
            {
                hours = 1;
                minutes = 0;
            }
            this.numericHours.Value = hours;
            this.numericMinutes.Value = minutes;
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
    }
}