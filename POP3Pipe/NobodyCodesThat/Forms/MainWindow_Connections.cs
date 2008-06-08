using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        #region ToolMenuButtons
        private void toolMenuConnectionsAdd_Click(object sender, EventArgs e)
        {
            addConnectionItem();
        }

        private void toolMenuConnectionsEdit_Click(object sender, EventArgs e)
        {
            editConnectionItem();
        }

        private void toolMenuConnectionsDelete_Click(object sender, EventArgs e)
        {
            if (listViewConnections.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewConnections.SelectedItems[0];
                listViewConnections.Items[item.Index].Remove();

                ConnectionObject conObj = findConnectionObjInList(item);
                if (conObj != null)
                {
                    SettingsObject.ListConnections.Remove(conObj);
                }

                saveSettings();
            }
        }
        #endregion

        private void listViewConnections_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listViewConnections.SelectedIndices.Count > 0)
                {
                    this.toolStripMenuConnectionsEdit.Enabled = true;
                    this.toolStripMenuConnectionsDelete.Enabled = true;
                }
                else
                {
                    this.toolStripMenuConnectionsEdit.Enabled = false;
                    this.toolStripMenuConnectionsDelete.Enabled = false;
                }
            }
        }

        private void addConnectionItem()
        {
            tabControl.SelectTab("tabPageConnections");
            ConnectionWindow window = new ConnectionWindow();
            DialogResult result = window.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConnectionObject conObj = window.getConnectionObject();
                if (conObj != null)
                {
                    // Add to local collection
                    SettingsObject.ListConnections.Add(conObj);

                    // Get object from foreign key
                    HostConfigObject pop3Obj = SettingsObject.ListPOP3[conObj.Pop3ID];
                    HostConfigObject smtpObj = SettingsObject.ListSMTP[conObj.SmtpID];
                    AddressObject addObj = SettingsObject.ListAddress[conObj.AddressID];

                    // Add listview entry
                    ListViewItem item = new ListViewItem(new string[] { "", pop3Obj.Description, smtpObj.Description, addObj.AddressName });
                    item.Checked = true;
                    this.listViewConnections.Items.Add(item);
                    this.listViewConnections.Sort();
                }
            }
        }

        private void editConnectionItem()
        {
            tabControl.SelectTab("tabPageConnections");

            ListView.SelectedIndexCollection selectedItems = this.listViewConnections.SelectedIndices;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                ConnectionObject currentConObj = SettingsObject.ListConnections[selectedItems[0]];
                ConnectionWindow window = new ConnectionWindow(currentConObj);
                DialogResult result = window.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ConnectionObject newConObj = window.getConnectionObject();
                    if (newConObj != null)
                    {
                        // Remove old object from collection
                        SettingsObject.ListConnections.Remove(currentConObj);

                        // Add new object to collection
                        SettingsObject.ListConnections.Add(newConObj);

                        // Remove old listview entry
                        this.listViewConnections.Items.RemoveAt(selectedItems[0]);

                        // Get object from foreign key
                        HostConfigObject pop3Obj = SettingsObject.ListPOP3[newConObj.Pop3ID];
                        HostConfigObject smtpObj = SettingsObject.ListSMTP[newConObj.SmtpID];
                        AddressObject addObj = SettingsObject.ListAddress[newConObj.AddressID];

                        // Add new listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", pop3Obj.Description, smtpObj.Description, addObj.AddressName });
                        item.Checked = newConObj.Active;
                        this.listViewConnections.Items.Add(item);
                        this.listViewConnections.Sort();
                    }
                }
            }
        }

        private ConnectionObject findConnectionObjInList(ListViewItem item)
        {
            foreach (ConnectionObject conObj in SettingsObject.ListConnections)
            {
                string pop3 = item.SubItems[1].Text;
                string smtp = item.SubItems[2].Text;
                string address = item.SubItems[3].Text;

                string pop3Compare = SettingsObject.ListPOP3[conObj.Pop3ID].Description;
                string smtpCompare = SettingsObject.ListSMTP[conObj.SmtpID].Description;
                string addressCompare = SettingsObject.ListAddress[conObj.AddressID].AddressName;

                if (pop3.Equals(pop3Compare) &&
                    smtp.Equals(smtpCompare) &&
                    address.Equals(addressCompare) &&
                    item.Checked == conObj.Active)
                {
                    // Searched object was found
                    return conObj;
                }
            }
            return null;
        }

        private void listViewConnections_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SettingsObject.ListConnections[e.Item.Index].Active = e.Item.Checked;
        }
    }
}
