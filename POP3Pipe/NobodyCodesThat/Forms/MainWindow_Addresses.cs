using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        private void addAddressItem()
        {
            tabControl.SelectTab("tabPageAddresses");
            AddressWindow window = new AddressWindow();
            DialogResult result = window.ShowDialog();
            if (result == DialogResult.OK)
            {
                AddressObject addrObj = window.getAddressObject();
                if (addrObj != null)
                {
                    // Add to local collection
                    SettingsObject.ListAddress.Add(addrObj);

                    // Add to listview
                    ListViewItem item = new ListViewItem(new string[] { "", addrObj.AddressName, addrObj.AddressEMail });
                    item.Checked = true;
                    this.listViewAddresses.Items.Add(item);
                    this.listViewAddresses.Sort();
                }
            }
        }

        private void editAddressItem()
        {
            tabControl.SelectTab("tabPageAddresses");

            ListView.SelectedIndexCollection selectedItems = this.listViewAddresses.SelectedIndices;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                AddressObject currentAddrObj = SettingsObject.ListAddress[selectedItems[0]];
                AddressWindow window = new AddressWindow(currentAddrObj);
                DialogResult result = window.ShowDialog();
                if (result == DialogResult.OK)
                {
                    AddressObject newAddrObj = window.getAddressObject();
                    if (newAddrObj != null)
                    {
                        // Remove old object from collection
                        SettingsObject.ListAddress.Remove(currentAddrObj);

                        // Add new object to collection
                        SettingsObject.ListAddress.Add(newAddrObj);

                        // Remove old listview entry
                        this.listViewAddresses.Items.RemoveAt(selectedItems[0]);

                        // Add new listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", newAddrObj.AddressName, newAddrObj.AddressEMail });
                        item.Checked = newAddrObj.Active;
                        this.listViewAddresses.Items.Add(item);
                        this.listViewAddresses.Sort();
                    }
                }
            }
        }

        private void addAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAddressItem();
        }

        private void toolMenuAddressAdd_Click(object sender, EventArgs e)
        {
            addAddressItem();
        }

        private void toolMenuAdressesEdit_Click(object sender, EventArgs e)
        {
            editAddressItem();
        }

        private void addConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addConnectionItem();
        }

        private void listViewAddresses_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listViewAddresses.SelectedIndices.Count > 0)
                {
                    this.toolStripMenuAddressesEdit.Enabled = true;
                    this.toolStripMenuAddressesDelete.Enabled = true;
                }
                else
                {
                    this.toolStripMenuAddressesEdit.Enabled = false;
                    this.toolStripMenuAddressesDelete.Enabled = false;
                }
            }
        }

        private void toolMenuAddressDelete_Click(object sender, EventArgs e)
        {
            if (listViewAddresses.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewAddresses.SelectedItems[0];
                listViewAddresses.Items[item.Index].Remove();

                AddressObject addrObj = findAddressObjInList(item);
                if (addrObj != null)
                {
                    SettingsObject.ListAddress.Remove(addrObj);
                }

                // TODO: implement single node delete
                saveSettings();
            }
        }

        private AddressObject findAddressObjInList(ListViewItem item)
        {
            foreach (AddressObject addrObj in SettingsObject.ListAddress)
            {
                string name = item.SubItems[1].Text;
                string address = item.SubItems[2].Text;
                if (name.Equals(addrObj.AddressName) &&
                    address.Equals(addrObj.AddressEMail) &&
                    item.Checked == addrObj.Active)
                {
                    // Searched object was found
                    return addrObj;
                }
            }
            return null;
        }

        private void listViewAddresses_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SettingsObject.ListAddress[e.Item.Index].Active = e.Item.Checked;
        }
    }
}
