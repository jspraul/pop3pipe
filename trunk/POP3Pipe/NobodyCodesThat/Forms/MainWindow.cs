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
using System.Web;
using System.Text.RegularExpressions;

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
            if (SettingsObject.ListConnections.Count == 0)
            {
                MessageBox.Show("There was no connection set up.", "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                bool oneIsActive = false;
                int cons = SettingsObject.ListConnections.Count;
                for (int i = 0; i < cons; i++)
                {
                    if (SettingsObject.ListConnections[i].Active)
                    {
                        oneIsActive = true;
                        break;
                    }
                }
                if (oneIsActive)
                {
                    saveSettings();
                    changeStateStart();
                }
                else
                {
                    MessageBox.Show("There " + (cons == 1 ? "is" : "are") + " " + cons + " disabled connection" + (cons == 1 ? "" : "s") + " in list.\n" + (cons == 1 ? "This one" : "At least one of them") + " has to be active to start process.", "No Active Connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void changeStateStart()
        {
            if (btnStart.Text.Equals("START"))
            {
                decimal seconds = this.numericHours.Value * 60 * 60 + numericMinutes.Value * 60 + numericSeconds.Value;
                this.btnStart.Text = "STOP";
                CountDown.startCounter(seconds);
                Manager.runJob(SettingsObject.ListConnections);
            }
            else
            {
                this.lblCountdown.Text = "00:00:00";
                this.btnStart.Text = "START";
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

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedIndices.Count > 0)
            {
                listView.SelectedItems[0].Checked = !listView.SelectedItems[0].Checked;
                if (listView.Name.Contains("POP3"))
                {
                    editPop3Item();
                }
                else if (listView.Name.Contains("SMTP"))
                {
                    editSmtpItem();
                }
                else if (listView.Name.Contains("Address"))
                {
                    editAddressItem();
                }
                else if (listView.Name.Contains("Connection"))
                {
                    editConnectionItem();
                }
            }
        }

        private void setHelpHints()
        {
            this.helpProvider.SetHelpString(this.listViewPOP3, "Create, edit or delete a\nPOP3 host configuration setting.\nThis one specifies where\nto catch the messages from.");
            this.helpProvider.SetHelpString(this.listViewSMTP, "Create, edit or delete a\nSMTP host configuration setting.\nThis one specifies the SMTP server\nthat will be used to send the\nreceived messages to a given E-Mail address.");
            this.helpProvider.SetHelpString(this.listViewAddresses, "Create, edit or delete an\nE-Mail address.\nThis is the destination of all\nreceived messages for this connection.");
            this.helpProvider.SetHelpString(this.listViewConnections, "Create, edit or delete a\nconnection setting.\nThis describes the way of getting\nmessages from a bunch of E-Mail servers\nto a single destination server.");
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

            setHelpHints();

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

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.stopJob();
            saveSettings();
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

        public static ManualResetEvent mre = new ManualResetEvent(false);

        public static void trmain()
        {
            Thread tr = Thread.CurrentThread;

            Console.WriteLine("thread: waiting for an event");
            mre.WaitOne();
            Console.WriteLine("thread: got an event");

            for (int x = 0; x < 10; x++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(tr.Name + ": " + x);
            }
        }  

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thrd1 = new Thread(new ThreadStart(trmain));
            thrd1.Name = "thread1";

            thrd1.Start();

            for (int x = 0; x < 10; x++)
            {
                Thread.Sleep(900);
                Console.WriteLine("Main    :" + x);
                if (5 == x) mre.Set();
            }

            while (thrd1.IsAlive)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main: waiting for thread to stop...");
            }
            
            
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
            ThunderbirdDataRetrieve thunderData = new ThunderbirdDataRetrieve();
            Dictionary<string, string> addressBook = thunderData.RetrieveAddressBook();
            List<HostConfigObject>[] hosts = thunderData.RetrieveHostConfigs();

            ThunderbirdImport importWindow = new ThunderbirdImport(hosts, addressBook);
            importWindow.ShowDialog();



            //string host = "mail.gmx.net";
            //string username = "da_groovy@gmx.de";
            //string password = "";

            //// Init SmtpClient and send
            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.Host = host;

            //NetworkCredential credentials = new NetworkCredential(username, password);
            //smtpClient.Credentials = credentials;

            //Console.WriteLine("Sending nr. " + 1);
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //smtpClient.Send("da_groovy@gmx.de", "da_groovy@gmx.de", "Das is ein Titel", "Hier steht ganz ganz viel Text drin.");
        }
    }
}