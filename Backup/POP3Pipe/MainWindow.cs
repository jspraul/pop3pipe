using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using OpenPOP;
using OpenPOP.POP3;
using System.Collections;
using POP3Pipe.Properties;
using System.Net.Mail;
using System.Net;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            loadSettings();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (listViewPOP3.Items.Count > 0)
            {
                saveSettings();
                changeStateStart();
            }
        }

        private void changeStateStart()
        {
            if (btnStart.Text.Equals("START"))
            {
                decimal seconds = numericHours.Value * 60 * 60 + numericMinutes.Value * 60 + numericSeconds.Value;
                btnStart.Text = "STOP";
                CountDown.startCounter(seconds);
                Manager.runJob(SettingsObject.ListConnections[0]);
            }
            else
            {
                lblCountdown.Text = "00:00:00";
                btnStart.Text = "START";
                Manager.stopJob();
                CountDown.stopCounter();
            }
        }

        private ListViewItem[] convert2ListView(IList input)
        {
            ListViewItem[] output = new ListViewItem[] { };
            if (input != null && input.Count > 0)
            {
                if (input[0].GetType() == typeof(HostConfigObject))
                {
                    List<HostConfigObject> hostCol = (List<HostConfigObject>)input;
                    output = new ListViewItem[hostCol.Count];
                    int i = 0;
                    foreach (HostConfigObject hostObj in hostCol)
                    {
                        // Add listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", hostObj.Description, hostObj.Host, hostObj.Username });
                        item.Checked = hostObj.Active;
                        output[i] = item;
                        i++;
                    }
                }
                else if (input[0].GetType() == typeof(AddressObject))
                {
                    List<AddressObject> addCol = (List<AddressObject>)input;
                    output = new ListViewItem[addCol.Count];
                    int i = 0;
                    foreach (AddressObject addObj in addCol)
                    {
                        // Add listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", addObj.AddressName, addObj.AddressEMail });
                        item.Checked = addObj.Active;
                        output[i] = item;
                        i++;
                    }
                }
                else if (input[0].GetType() == typeof(ConnectionObject))
                {
                    List<ConnectionObject> conCol = (List<ConnectionObject>)input;
                    output = new ListViewItem[conCol.Count];
                    int i = 0;
                    foreach (ConnectionObject conObj in conCol)
                    {
                        // Get object from foreign key
                        HostConfigObject pop3Obj = SettingsObject.ListPOP3[conObj.Pop3ID];
                        HostConfigObject smtpObj = SettingsObject.ListSMTP[conObj.SmtpID];
                        AddressObject addObj = SettingsObject.ListAddress[conObj.AddressID];
                        // Add listview entry
                        ListViewItem item = new ListViewItem(new string[] { "", pop3Obj.Description, smtpObj.Description, addObj.AddressName });
                        item.Checked = conObj.Active;
                        output[i] = item;
                        i++;
                    }
                }
            }
            return output;
        }

        /// <summary>
        ///     Perform a full settings load.
        /// </summary>
        private void loadSettings()
        {
            if (!File.Exists(Settings.Default.ConfigFile))
            {
                return;
            }
            FileOperations.readFile();

            // Clear all list views
            listViewPOP3.Items.Clear();
            listViewSMTP.Items.Clear();
            listViewAddresses.Items.Clear();
            listViewConnections.Items.Clear();

            // Insert all items
            listViewPOP3.Items.AddRange(convert2ListView(SettingsObject.ListPOP3));
            listViewSMTP.Items.AddRange(convert2ListView(SettingsObject.ListSMTP));
            listViewAddresses.Items.AddRange(convert2ListView(SettingsObject.ListAddress));
            listViewConnections.Items.AddRange(convert2ListView(SettingsObject.ListConnections));

            if (listViewConnections.Items.Count > 0)
            {
                tabControl.SelectTab("tabPageConnections");
            }

            // Sort listview items ascending
            //listViewConnections.Sort();
            //listViewPOP3.Sort();
            //listViewSMTP.Sort();
            //listViewAddresses.Sort();
        }

        private void saveSettings()
        {
            FileOperations.writeFile();
        }

        private void listPOP3Server_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            saveSettings();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.stopJob();
            saveSettings();
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

                // TODO: implement single node delete
                saveSettings();
            }
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

                // TODO: implement single node delete
                saveSettings();
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

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addPOP3Item();
        }

        private string tempMsg;
        private Messenger.MessageTag messageTag;
        private static int richTextBoxPosition;

        private void UpdateRichTextBox()
        {
            if (textLog.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                textLog.Invoke(new MethodInvoker(UpdateRichTextBox));
            }
            else
            {
                // you are in this method on the correct thread.
                string msgType = "";
                Color msgColor = Color.Black;
                switch (this.messageTag)
                {
                    case Messenger.MessageTag.ERROR:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Red;
                        break;
                    case Messenger.MessageTag.WARNING:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Yellow;
                        break;
                    case Messenger.MessageTag.INFO:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Blue;
                        break;
                    case Messenger.MessageTag.DIVIDE:
                        msgType = "------------------------------------";
                        break;
                    default:
                        break;
                }
                if (this.messageTag != Messenger.MessageTag.DIVIDE)
                {
                    string timeString = DateTime.Now.ToLongTimeString() + " ";
                    formatRichText(timeString, FontStyle.Bold, Color.Black);
                }
                formatRichText(msgType, FontStyle.Bold, msgColor);
                if (messageTag != Messenger.MessageTag.DIVIDE)
                {
                    formatRichText(tempMsg, FontStyle.Regular, msgColor);
                }
                textLog.AppendText(" \r\n");
                textLog.ScrollToCaret();
            }
        }

        private void formatRichText(string text, FontStyle style, Color color)
        {
            textLog.AppendText(text);
            if (richTextBoxPosition < textLog.Text.Length)
            {
                int foundHere = textLog.Find(text, richTextBoxPosition, RichTextBoxFinds.None);
                if (foundHere !=-1)
                {
                    textLog.SelectionFont = new Font("Courier New", 8, style);
                    textLog.SelectionColor = color;
                    richTextBoxPosition += text.Length;
                }
            }
        }

        public void addMessage(string message, Messenger.MessageTag tag)
        {
            tempMsg = message;
            messageTag = tag;
            UpdateRichTextBox();
            tempMsg = "";
            messageTag = 0;
        }

        public void UpdateStartButton()
        {
            if (btnStart.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                btnStart.Invoke(new MethodInvoker(UpdateStartButton));
            }
            else
            {
                // you are in this method on the correct thread.
                changeStateStart();
            }
        }

        public void refreshCountdown(string count)
        {
            countStrg = count;
            UpdateCountdown();
        }

        private string countStrg;

        private void UpdateCountdown()
        {
            if (lblCountdown.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                lblCountdown.Invoke(new MethodInvoker(UpdateCountdown));
            }
            else
            {
                // you are in this method on the correct thread.
                lblCountdown.Text = countStrg;
            }
        }

        private bool switchedToLog = false;

        private void textLog_TextChanged(object sender, EventArgs e)
        {
            if (!switchedToLog)
            {
                tabControl.SelectedIndex = 1;
                switchedToLog = true;
            }
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            saveSettings();
        }

        /// <summary>
        ///     Used for tab control resize process.
        /// </summary>
        private int difference;

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (btnExpand.Text.Equals(">>>"))
            {
                int oldTabWidth = tabControl.Size.Width;
                int newTabWidth = this.Size.Width - 20;
                this.difference = newTabWidth - oldTabWidth;
                tabControl.Size = new Size(tabControl.Size.Width + difference, tabControl.Size.Height);
                textLog.Size = new Size(textLog.Size.Width + difference, textLog.Size.Height);
                btnExpand.Location = new Point(btnExpand.Location.X + difference, btnExpand.Location.Y);
                btnExpand.Text = "<<<";
            }
            else
            {
                tabControl.Size = new Size(tabControl.Size.Width - difference, tabControl.Size.Height);
                textLog.Size = new Size(textLog.Size.Width - difference, textLog.Size.Height);
                btnExpand.Location = new Point(btnExpand.Location.X - difference, btnExpand.Location.Y);
                btnExpand.Text = ">>>";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string host = "mail.gmx.net";
            //int port = 465; //587;
            //string username = "da_groovy@gmx.de";
            //string password = "h8tt0r1Hanz0";
            //OpenSmtp.Mail.Smtp smtp = new OpenSmtp.Mail.Smtp();
            //smtp.Host = host;
            //smtp.Port = port;
            //smtp.Username = username;
            //smtp.Password = password;
            //smtp.SendTimeout = 3000;
            //smtp.RecieveTimeout = 3000;

            //OpenSmtp.Mail.MailMessage message = new OpenSmtp.Mail.MailMessage();
            //message.Body = "Hallo das ist Testmail";
            //ArrayList destinations = new ArrayList();
            //destinations.Add("da_groovy@gmx.de");
            //message.To = destinations;

            ////smtp.SendMail(message);
            //smtp.SendMail("stiffler-meister@web.de", "da_groovy@gmx.de", "Das is ein Titel", "Hier steht ganz ganz viel Text drin.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string host = "mail.gmx.net";
            string username = "da_groovy@gmx.de";
            string password = "";

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = host;

            NetworkCredential credentials = new NetworkCredential(username, password);
            smtpClient.Credentials = credentials;

            Console.WriteLine("Sending nr. " + 1);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send("da_groovy@gmx.de", "da_groovy@gmx.de", "Das is ein Titel", "Hier steht ganz ganz viel Text drin.");
        }

        private void toolMenuPop3Add_Click(object sender, EventArgs e)
        {
            addPOP3Item();
        }

        private void addPOP3Item()
        {
            tabControl.SelectTab("tabPagePOP3");
            POP3Window window = new POP3Window();
            window.addEntry = true;
            window.ShowDialog();
            HostConfigObject hostObj = window.hostObj;
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

        private void editPOP3Item()
        {
            tabControl.SelectTab("tabPagePOP3");
            POP3Window window = new POP3Window();
            window.addEntry = false;
            window.ShowDialog();
            HostConfigObject serverObj = window.hostObj;
            if (serverObj != null)
            {
                ListViewItem item = new ListViewItem(new string[] { "", serverObj.Description, serverObj.Host, serverObj.Username });
                item.Checked = true;
                this.listViewPOP3.Items.Add(item);
                this.listViewPOP3.Sort();
            }
        }

        private void addSMTPItem()
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

        private void editSMTPItem()
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

        private void addAddressItem()
        {
            tabControl.SelectTab("tabPageAddresses");
            AddressWindow window = new AddressWindow();
            window.ShowDialog();
            AddressObject addObj = window.addObj;
            if (addObj != null)
            {
                // Add to local collection
                SettingsObject.ListAddress.Add(addObj);
                // Add to listview
                ListViewItem item = new ListViewItem(new string[] { "", addObj.AddressName, addObj.AddressEMail });
                item.Checked = true;
                this.listViewAddresses.Items.Add(item);
                this.listViewAddresses.Sort();
            }
        }

        private void addConnectionItem()
        {
            tabControl.SelectTab("tabPageConnections");
            ConnectionWindow window = new ConnectionWindow();
            window.addEntry = true;
            window.ShowDialog();
            ConnectionObject conObj = window.conObj;
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
                window.OpenWindow(currentConObj.Pop3ID, currentConObj.SmtpID, currentConObj.AddressID);
                ConnectionObject conObj = window.conObj;
                if (conObj != null)
                {
                    // Remove old object from collection
                    SettingsObject.ListConnections.Remove(currentConObj);
                    // Add to local collection
                    SettingsObject.ListConnections.Add(conObj);
                    // Get object from foreign key
                    HostConfigObject pop3Obj = SettingsObject.ListPOP3[conObj.Pop3ID];
                    HostConfigObject smtpObj = SettingsObject.ListSMTP[conObj.SmtpID];
                    AddressObject addObj = SettingsObject.ListAddress[conObj.AddressID];

                    // Remove old listview entry
                    this.listViewConnections.Items.RemoveAt(selectedItems[0]);
                    // Add listview entry
                    ListViewItem item = new ListViewItem(new string[] { "", pop3Obj.Description, smtpObj.Description, addObj.AddressName });
                    item.Checked = true;
                    this.listViewConnections.Items.Add(item);
                }
            }
        }

        private void listViewPOP3_DoubleClick(object sender, EventArgs e)
        {
            addPOP3Item();
        }

        private void addAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAddressItem();
        }

        private void addConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addConnectionItem();
        }

        private void toolMenuSmtpAdd_Click(object sender, EventArgs e)
        {
            addSMTPItem();
        }

        private void toolMenuConnectionsAdd_Click(object sender, EventArgs e)
        {
            addConnectionItem();
        }

        private void toolMenuAddressAdd_Click(object sender, EventArgs e)
        {
            addAddressItem();
        }

        private void toolMenuConnectionsEdit_Click(object sender, EventArgs e)
        {
            editConnectionItem();
        }
    }
}