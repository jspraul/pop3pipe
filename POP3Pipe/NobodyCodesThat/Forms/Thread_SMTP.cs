using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Threading;
using System.Collections;

namespace POP3Pipe
{
    /// <summary>
    ///     Manages the SMTP email sending process.
    /// </summary>
    public partial class MainWindow : Form
    {
        private void threadSMTP_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList list = (ArrayList)e.Argument;
            if (list == null || this.threadSMTP.CancellationPending || this.threadComManager.CancellationPending)
            {
                Console.WriteLine("CANCELLED  threadSMTP_DoWork");
                return;
            }
            ConnectionObject con = list.Count == 2 ? (ConnectionObject)list[0] : null;
            if (con == null)
            {
                return;
            }
            HostConfigObject hostSmtp = SettingsObject.ListSMTP[con.SmtpID];
            AddressObject destinationAddress = SettingsObject.ListAddress[con.AddressID];
            OpenPOP.MIMEParser.Message[] msgs = (OpenPOP.MIMEParser.Message[])list[1];
            if (msgs != null)
            {
                if (msgs.Length > 0)
                {
                    ManagerSMTP manSmtp = new ManagerSMTP();
                    manSmtp.running = true;
                    manSmtp.Send(msgs, hostSmtp, destinationAddress);
                }
            }
            //this.threadSMTP.ReportProgress(100);
        }

        private void threadSMTP_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("threadSMTP_ProgressChanged -> " + e.ProgressPercentage + "%");
        }

        private void threadSMTP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("threadSMTP_RunWorkerCompleted");
            Logger.sendMessage(null, Logger.MessageTag.DIVIDE);
        }
    }
}
