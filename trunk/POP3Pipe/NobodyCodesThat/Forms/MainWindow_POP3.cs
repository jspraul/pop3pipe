using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        #region ToolMenuButtons
        private void toolMenuPop3Add_Click(object sender, EventArgs e)
        {
            addPop3Item();
        }

        private void toolMenuPop3Edit_Click(object sender, EventArgs e)
        {
            editPop3Item();
        }

        private void toolMenuPop3Delete_Click(object sender, EventArgs e)
        {
            if (listViewPOP3.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewPOP3.SelectedItems[0];
                listViewPOP3.Items[item.Index].Remove();

                HostConfigObject hostObj = findPOP3ObjInList(item);
                if (hostObj != null)
                {
                    SettingsObject.ListPOP3.Remove(hostObj);
                }

                saveSettings();
            }
        }
        #endregion

        private void listViewPOP3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listViewPOP3.SelectedIndices.Count > 0)
                {
                    this.toolStripMenuPop3Edit.Enabled = true;
                    this.toolStripMenuPop3Delete.Enabled = true;
                }
                else
                {
                    this.toolStripMenuPop3Edit.Enabled = false;
                    this.toolStripMenuPop3Delete.Enabled = false;
                }
            }
        }

        private void addPop3Item()
        {
            tabControl.SelectTab("tabPagePOP3");
            POP3Window window = new POP3Window();
            DialogResult result = window.ShowDialog();
            if (result == DialogResult.OK)
            {
                HostConfigObject hostObj = window.getHostConfigObject();
                if (hostObj != null)
                {
                    // Add to local collection
                    SettingsObject.ListPOP3.Add(hostObj);

                    // Add to listview
                    ListViewItem item = new ListViewItem(new string[] { "", hostObj.Description, hostObj.Host, hostObj.Username });
                    item.Checked = true;
                    this.listViewPOP3.Items.Add(item);
                    this.listViewPOP3.Sort();
                }
            }
        }

        private void editPop3Item()
        {
            tabControl.SelectTab("tabPagePOP3");

            ListView.SelectedIndexCollection selectedItems = this.listViewPOP3.SelectedIndices;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                HostConfigObject currentPop3Obj = SettingsObject.ListPOP3[selectedItems[0]];
                POP3Window window = new POP3Window(currentPop3Obj);
                DialogResult result = window.ShowDialog();
                if (result == DialogResult.OK)
                {
                    HostConfigObject newPop3Obj = window.getHostConfigObject();
                    if (newPop3Obj != null)
                    {
                        // Remove old object from collection
                        SettingsObject.ListPOP3.Remove(currentPop3Obj);

                        // Add new object to collection
                        SettingsObject.ListPOP3.Add(newPop3Obj);

                        // Remove old listview entry
                        this.listViewPOP3.Items.RemoveAt(selectedItems[0]);

                        // Add new listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", newPop3Obj.Description, newPop3Obj.Host, newPop3Obj.Username });
                        item.Checked = newPop3Obj.Active;
                        this.listViewPOP3.Items.Add(item);
                        this.listViewPOP3.Sort();
                    }
                }
            }
        }

        private HostConfigObject findPOP3ObjInList(ListViewItem item)
        {
            foreach (HostConfigObject hostObj in SettingsObject.ListPOP3)
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

        private void listViewPOP3_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SettingsObject.ListPOP3[e.Item.Index].Active = e.Item.Checked;
        }
    }
}
