using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenPOP.MIMEParser;

namespace POP3Pipe
{
    class Manager
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);

        private static int threadCount;

        private static bool running;

        private static int jobsInQueue;
        private static int jobsFinished;

        /// <summary>
        ///     Start a job for the given connection.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="currentConnection"></param>
        public static void runJob(List<ConnectionObject> connections)
        {
            if (connections == null)
            {
                return;
            }
            Manager.jobsInQueue = connections.Count;
            Manager.running = true;
            for (int i = 0; i < connections.Count; i++)
            {
                Messenger.sendMessage("Starting process", Messenger.MessageTag.INFO);
                Thread ta = new Thread(new ParameterizedThreadStart(fetchMails));
                ta.Name = "connector-"+i;
                ta.Start(connections[i]);
            }
        }

        public static bool isRunning()
        {
            return running;
        }

        public static void stopJob()
        {
            Manager.running = false;
            MailPOP3.running = false;
            MailSMTP.running = false;
            Messenger.sendMessage("Process stopped", Messenger.MessageTag.INFO);
        }

        private static void fetchMails(object conRawObj)
        {
            Console.WriteLine("Starting new thread with name: " + Thread.CurrentThread.Name);

            ConnectionObject conObj = (ConnectionObject)conRawObj;
            Console.WriteLine("POP3ID: " + conObj.Pop3ID + " | SMTPID: " + conObj.SmtpID + " | AddressID: " + conObj.AddressID);

            Console.WriteLine("Manager is " + (Manager.running ? "" : "NOT ") + "running.");

            while (Manager.running)
            {
                Manager.threadCount++;
                if (Manager.threadCount > 1)
                {
                    Console.WriteLine("Waiting for previous finished thread. ThreadCount: "+Manager.threadCount);
                    Manager.mre.WaitOne();
                }

                Console.WriteLine("Try to run job...");

                HostConfigObject pop3host = SettingsObject.ListPOP3[conObj.Pop3ID];
                HostConfigObject smtphost = SettingsObject.ListSMTP[conObj.SmtpID];
                AddressObject address = SettingsObject.ListAddress[conObj.AddressID];
                if (conObj.Active)
                {
                    if (pop3host.Active)
                    {
                        if (smtphost.Active)
                        {
                            if (address.Active)
                            {
                                Console.WriteLine("Running job now...");

                                CountDown.resetCounter();
                                Message[] msgs = MailPOP3.Receive(pop3host);
                                if (msgs != null)
                                {
                                    if (msgs.Length > 0)
                                    {
                                        MailSMTP.Send(msgs, smtphost, address);
                                    }
                                }
                                Messenger.sendMessage(null, Messenger.MessageTag.DIVIDE);
                            }
                            else
                            {
                                Messenger.sendMessage("Sending to E-Mail contact <" + address.AddressName + "> is disabled. Skipping...", Messenger.MessageTag.INFO);
                            }
                        }
                        else
                        {
                            Messenger.sendMessage("Piping mails via SMTP host <" + smtphost.Description + "> is disabled. Skipping...", Messenger.MessageTag.INFO);
                        }
                    }
                    else
                    {
                        Messenger.sendMessage("Fetching mails from POP3 host <" + pop3host.Description + "> is disabled. Skipping...", Messenger.MessageTag.INFO);
                    }
                }
                else
                {
                    Messenger.sendMessage("Connection from <" + pop3host.Description + "> over <" + smtphost.Description + "> to <" + address.AddressName + "> is disabled. Skipping...", Messenger.MessageTag.INFO);
                }

                Manager.threadCount--;
                Console.WriteLine("Release lock for next waiting thread.");
                Manager.mre.Set();
                if (conObj.WaitTime.Milliseconds == 0)
                {
                    Manager.jobsFinished++;
                    Console.WriteLine("Single run mode active, finishing...");
 
                    // All single run jobs are finished
                    if (Manager.jobsInQueue == Manager.jobsFinished)
                    {
                        Console.WriteLine("All done, finish.");

                        MainWindow mainWind = (MainWindow)MainWindow.ActiveForm;
                        if (mainWind != null && !mainWind.Disposing && !mainWind.IsDisposed)
                        {
                            mainWind.UpdateStartButton();
                        }
                    }
                    return;
                }
                Console.WriteLine("Finished cycle for thread: " + Thread.CurrentThread.Name);
                Console.WriteLine("Waiting " + conObj.WaitTime.Milliseconds + " milliseconds...");
                Thread.Sleep(conObj.WaitTime.Milliseconds);
            }
        }
    }
}
