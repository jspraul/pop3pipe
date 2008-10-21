using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System;

namespace POP3Pipe
{
    /// <summary>
    ///     Manages the whole communication process.
    /// </summary>
    public partial class MainWindow : Form
    {
        private bool StartComManager()
        {
            if (!this.threadComManager.IsBusy)
            {
                this.threadComManager.RunWorkerAsync();
                return true;
            }
            return false;
        }

        private void StopComManager()
        {
            UpdateStartButtonDisabled();
            this.threadPOP3.CancelAsync();
            this.threadSMTP.CancelAsync();
            this.threadComManager.CancelAsync();
            this.loadingCircle.FadeOut();
        }

        private void threadComManager_DoWork(object sender, DoWorkEventArgs e)
        {
            // Set the stop button to enabled = true
            UpdateStartButtonEnabled();

            Console.WriteLine("threadComManager_DoWork");
            List<ConnectionObject> cons = SettingsObject.ListConnections;
            DateTime[] startTimes = new DateTime[cons.Count];
            bool running = true;
            while (running && !this.threadComManager.CancellationPending)
            {
                for (int i = 0; i < cons.Count; i++)
                {
                    if (cons[i].Active && !this.threadComManager.CancellationPending)
                    {
                        Console.WriteLine("Checking job " + (i + 1));
                        bool startNow = false;
                        if (startTimes[i].Equals(new DateTime()))
                        {
                            // Starting process for the first time
                            Console.WriteLine("Starting job " + (i + 1) + " for the first time. -> " + DateTime.Now.ToLongTimeString());
                            Logger.sendMessage("Starting process " + (i + 1), Logger.MessageTag.INFO);
                            startNow = true;
                        }
                        else
                        {
                            if (cons[i].ContinousMode)
                            {
                                // Check if configured wait time was reached
                                TimeSpan difference = DateTime.Now - startTimes[i];
                                if (difference >= cons[i].WaitTime)
                                {
                                    Console.WriteLine("Running process " + (i + 1) + " -> " + DateTime.Now.ToLongTimeString());
                                    Logger.sendMessage("Running process " + (i + 1), Logger.MessageTag.INFO);
                                    startNow = true;
                                }
                            }
                        }
                        if (startNow)
                        {
                            while (this.threadPOP3.IsBusy || this.threadSMTP.IsBusy)
                            {
                                Console.WriteLine("Waiting for previous thread.");
                                Thread.Sleep(1000);
                            }
                            startTimes[i] = DateTime.Now;
                            if (!this.threadComManager.CancellationPending)
                            {
                                this.threadPOP3.RunWorkerAsync(cons[i]);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }

            // Wait for real finish of all sub threads
            while (this.threadPOP3.IsBusy || this.threadSMTP.IsBusy)
            {
                Console.WriteLine("Waiting for finished sub threads...");
                Thread.Sleep(1000);
            }
        }

        private void threadComManager_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("threadComManager_ProgressChanged");
        }

        private void threadComManager_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("threadComManager_RunWorkerCompleted");

            // Set the start button to enabled = true
            UpdateStartButtonEnabled();
        }
    }
}
