using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Threading;
using System.Collections;

namespace POP3Pipe
{
    /// <summary>
    ///     Manages the POP3 email fetching process.
    /// </summary>
    public partial class MainWindow : Form
    {
        private void threadPOP3_DoWork(object sender, DoWorkEventArgs e)
        {
            ConnectionObject con = (ConnectionObject)e.Argument;
            if (con == null || this.threadPOP3.CancellationPending || this.threadComManager.CancellationPending)
            {
                Console.WriteLine("CANCELLED  threadPOP3_DoWork");
                return;
            }
            ManagerPOP3 manPop3 = new ManagerPOP3();
            manPop3.running = true;
            OpenPOP.MIMEParser.Message[] msgs = manPop3.Receive(con);
            //this.threadPOP3.ReportProgress(100);
            ArrayList list = new ArrayList(2);
            list.Add(con);
            list.Add(msgs);
            e.Result = list;
        }

        private void threadPOP3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("threadPOP3_ProgressChanged -> " + e.ProgressPercentage + "%");
        }

        private void threadPOP3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("threadPOP3_RunWorkerCompleted");
            if (!this.threadPOP3.CancellationPending && !this.threadComManager.CancellationPending)
            {
                this.threadSMTP.RunWorkerAsync(e.Result);
            }
        }
    }
}
