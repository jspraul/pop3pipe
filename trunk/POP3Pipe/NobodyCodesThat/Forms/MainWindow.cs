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
using Updater;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            loadSettings();
            Logger.mainWind = this;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (SettingsObject.ListConnections == null || SettingsObject.ListConnections.Count == 0)
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
                    this.loadingCircle.FadeIn();
                    changeStartLabel();
                }
                else
                {
                    MessageBox.Show("There " + (cons == 1 ? "is" : "are") + " " + cons + " disabled connection" + (cons == 1 ? "" : "s") + " in list.\n" + (cons == 1 ? "This one" : "At least one of them") + " has to be active to start process.", "No Active Connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
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
            FileOperations.readConfigFile();

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
            FileOperations.writeConfigFile();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Manager.stopJob(false);
            StopComManager();
            saveSettings();
        }

        public void UpdateStartButtonLabel()
        {
            if (btnStart.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                btnStart.Invoke(new MethodInvoker(UpdateStartButtonLabel));
            }
            else
            {
                // you are in this method on the correct thread.
                changeStartLabel();
            }
        }

        public void UpdateStartButtonEnabled()
        {
            if (btnStart.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                btnStart.Invoke(new MethodInvoker(UpdateStartButtonEnabled));
            }
            else
            {
                // you are in this method on the correct thread.
                changeStartEnabled(true);
            }
        }

        public void UpdateStartButtonDisabled()
        {
            if (btnStart.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                btnStart.Invoke(new MethodInvoker(UpdateStartButtonDisabled));
            }
            else
            {
                // you are in this method on the correct thread.
                changeStartEnabled(false);
            }
        }

        private void changeStartLabel()
        {
            if (btnStart.Text.Equals("START"))
            {
                //decimal seconds = this.numericHours.Value * 60 * 60 + numericMinutes.Value * 60 + numericSeconds.Value;
                //CountDown.startCounter(seconds);
                //Manager.runJob(SettingsObject.ListConnections);
                bool started = StartComManager();
                if (started)
                {
                    this.btnStart.Text = "STOP";
                    UpdateStartButtonDisabled();
                }
            }
            else
            {
                //this.lblCountdown.Text = "00:00:00";
                this.btnStart.Text = "START";
                //Manager.stopJob(true);
                //CountDown.stopCounter();
                StopComManager();
            }
        }

        private void changeStartEnabled(bool enabled)
        {
            this.btnStart.Enabled = enabled;
        }

        public void refreshCountdown(string count)
        {
            countStrg = count;
            UpdateCountdown();
        }

        private string countStrg;

        private void UpdateCountdown()
        {
            //if (lblCountdown.InvokeRequired)
            //{
            //    // call back on this same method, but in a different thread.
            //    lblCountdown.Invoke(new MethodInvoker(UpdateCountdown));
            //}
            //else
            //{
            //    // you are in this method on the correct thread.
            //    lblCountdown.Text = countStrg;
            //}
        }

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            saveSettings();
        }

        /// <summary>
        ///     Used for tab control resize process.
        /// </summary>
        private int difference;

        public static ManualResetEvent mre = new ManualResetEvent(false);

        private void button2_Click(object sender, EventArgs e)
        {
            ThunderbirdDataRetrieve thunderData = new ThunderbirdDataRetrieve();
            Dictionary<string, string> addressBook = thunderData.RetrieveAddressBook();
            List<HostConfigObject>[] hosts = thunderData.RetrieveHostConfigs();

            ThunderbirdImport importWindow = new ThunderbirdImport(hosts, addressBook);
            importWindow.ShowDialog();

        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new UpdateStarter().Update(Program.info, false);
        }

        private void revisionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new UpdateStarter().Overview(Program.info);
        }

        private void aboutThisProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new AboutBox(Program.info.Version).Show();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl control = (TabControl)sender;
            if (control.SelectedTab != control.TabPages["tabLogs"])
            {
                this.tabControl.Size = new Size(tabControl.Size.Width - difference, tabControl.Size.Height);
                this.textLog.Size = new Size(textLog.Size.Width - difference, textLog.Size.Height);
                this.difference = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formatRichText("21:20:55 ", FontStyle.Regular, Color.Black);
            formatRichText("INFO ", FontStyle.Bold, Color.Blue);
            formatRichText("Running process ", FontStyle.Regular, Color.Blue);
            this.textLog.AppendText("\n");
        }

        private void clearConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textLog.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.loadingCircle.FadeIn();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.loadingCircle.FadeOut();
        }

        /// <summary>
        ///     Function used to fade out a form using a user defined number
        ///     of steps.
        /// </summary>
        /// <param name="f">The Windows form to fade out</param>
        /// <param name="NumberOfSteps">The number of steps used to fade the form</param>
        public static void FadeForm(Form form, byte NumberOfSteps, bool fadeIn)
        {
            float StepVal = (float)(100f / NumberOfSteps);
            float fOpacity = 0;
            if (!fadeIn)
            {
                fOpacity = 100f;
            }
            for (byte b = 0; b < NumberOfSteps; b++)
            {
                form.Opacity = fOpacity / 100;
                form.Refresh();
                if (fadeIn)
                {
                    fOpacity += StepVal;
                }
                else
                {
                    fOpacity -= StepVal;
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //FadeForm(this, 50, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ManagerSMTP man = new ManagerSMTP();
            man.testSmtp();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SettingsObject.CacheUIDs = new Dictionary<int,List<string>>();
            List<string> list1 = new List<string>();
            list1.Add("111");
            list1.Add("222");
            list1.Add("333");
            list1.Add("444");
            list1.Add("555");
            list1.Add("666");
            List<string> list2 = new List<string>();
            list2.Add("1011");
            list2.Add("2022");
            list2.Add("3033");
            list2.Add("4044");
            list2.Add("5055");
            list2.Add("6066");
            List<string> list3 = new List<string>();
            list3.Add("1211");
            list3.Add("2222");
            list3.Add("3233");
            list3.Add("4244");
            list3.Add("5255");
            list3.Add("6266");
            SettingsObject.CacheUIDs.Add(1, list1);
            SettingsObject.CacheUIDs.Add(2, list2);
            SettingsObject.CacheUIDs.Add(3, list3);

            //FileOperations.writeUIDCacheFile();
            FileOperations.readUIDCacheFile();
        }
    }
}