using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class AboutBox : Form
    {
        public AboutBox(string version)
        {
            InitializeComponent();
            this.lblVersion.Text = "v" + version[0] + "." + version.Substring(1);
        }

        private void AboutBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
