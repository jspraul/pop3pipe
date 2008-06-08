using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class ThunderbirdImport : Form
    {
        public ThunderbirdImport(List<HostConfigObject>[] hosts, Dictionary<string, string> addressBook)
        {
            InitializeComponent();

            if (hosts != null && hosts.Length == 2 && addressBook != null)
            {
                List<HostConfigObject> pop3Hosts = hosts[0];
                for (int i = 0; i < pop3Hosts.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new string[]{ "", pop3Hosts[i].Description, pop3Hosts[i].Host, pop3Hosts[i].Username});
                    item.Checked = true;
                    this.listViewPOP3.Items.Add(item);
                }

                List<HostConfigObject> smtpHosts = hosts[1];
                for (int i = 0; i < smtpHosts.Count; i++)
                {
                    ListViewItem item = new ListViewItem(new string[] { "", smtpHosts[i].Description, smtpHosts[i].Host, smtpHosts[i].Username });
                    item.Checked = true;
                    this.listViewSMTP.Items.Add(item);
                }

                foreach (string email in addressBook.Keys)
                {
                    ListViewItem item = new ListViewItem(new string[] { "", addressBook[email], email });
                    item.Checked = true;
                    this.listViewAddresses.Items.Add(item);
                }
            }
        }
    }
}
