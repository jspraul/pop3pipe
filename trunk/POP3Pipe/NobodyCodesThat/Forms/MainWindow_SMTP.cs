﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        #region ToolMenuButtons
        private void toolMenuSmtpAdd_Click(object sender, EventArgs e)
        {
            addSmtpItem();
        }

        private void toolMenuSmtpEdit_Click(object sender, EventArgs e)
        {
            editSmtpItem();
        }

        private void toolMenuSmtpDelete_Click(object sender, EventArgs e)
        {
            if (listViewSMTP.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewSMTP.SelectedItems[0];
                listViewSMTP.Items[item.Index].Remove();

                HostConfigObject hostObj = findSMTPObjInList(item);
                if (hostObj != null)
                {
                    SettingsObject.ListSMTP.Remove(hostObj);
                }

                // TODO: implement single node delete
                saveSettings();
            }
        }
        #endregion

        private void listViewSMTP_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listViewSMTP.SelectedIndices.Count > 0)
                {
                    this.toolStripMenuSmtpEdit.Enabled = true;
                    this.toolStripMenuSmtpDelete.Enabled = true;
                }
                else
                {
                    this.toolStripMenuSmtpEdit.Enabled = false;
                    this.toolStripMenuSmtpDelete.Enabled = false;
                }
            }
        }

        private void addSmtpItem()
        {
            tabControl.SelectTab("tabPageSMTP");
            SMTPWindow window = new SMTPWindow();
            window.addEntry = true;
            window.ShowDialog();
            HostConfigObject hostObj = window.hostObj;
            if (hostObj != null)
            {
                // Add to local collection
                SettingsObject.ListSMTP.Add(hostObj);
                // Add to listview
                ListViewItem item = new ListViewItem(new string[] { "", hostObj.Description, hostObj.Host, hostObj.Username });
                item.Checked = true;
                this.listViewSMTP.Items.Add(item);
                this.listViewSMTP.Sort();
            }
        }

        private void editSmtpItem()
        {
            tabControl.SelectTab("tabPageSMTP");
            SMTPWindow window = new SMTPWindow();
            window.addEntry = false;
            window.ShowDialog();
            HostConfigObject serverObj = window.hostObj;
            if (serverObj != null)
            {
                ListViewItem item = new ListViewItem(new string[] { "", serverObj.Description, serverObj.Host, serverObj.Username });
                item.Checked = true;
                this.listViewSMTP.Items.Add(item);
                this.listViewSMTP.Sort();
            }
        }

        private HostConfigObject findSMTPObjInList(ListViewItem item)
        {
            foreach (HostConfigObject hostObj in SettingsObject.ListSMTP)
            {
                string descr = item.SubItems[1].Text;
                string host = item.SubItems[2].Text;
                string username = item.SubItems[3].Text;
                if (descr.Equals(hostObj.Description) &&
                    host.Equals(hostObj.Host) &&
                    username.Equals(hostObj.Username) &&
                    item.Checked == hostObj.Active)
                {
                    // Searched object was found
                    return hostObj;
                }
            }
            return null;
        }

        private void listViewSMTP_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SettingsObject.ListSMTP[e.Item.Index].Active = e.Item.Checked;
        }
    }
}
