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
                Logger.sendMessage("Starting process", Logger.MessageTag.INFO);
                Thread ta = new Thread(new ParameterizedThreadStart(fetchMails));
                ta.Name = "connector-"+i;
                ta.Start(connections[i]);
            }
        }

        public static bool isRunning()
        {
            return running;
        }

        public static void stopJob(bool notify)
        {
            Manager.running = false;
            MailPOP3.running = false;
            MailSMTP.running = false;
            if (notify)
            {
                Logger.sendMessage("Process stopped", Logger.MessageTag.INFO);
            }
        }

        private static void fetchMails(object conRawObj)
        {
            Console.WriteLine("Starting new thread with name: " + Thread.CurrentThread.Name);

            ConnectionObject conObj = (ConnectionObject)conRawObj;
            Console.WriteLine("POP3ID: " + conObj.Pop3ID + " | SMTPID: " + conObj.SmtpID + " | AddressID: " + conObj.AddressID);

            Console.WriteLine("Manager is " + (Manager.running ? "" : "NOT ") + "running.");

            while (Manager.running)
            {
                DateTime startTime = DateTime.Now;
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
                                Logger.sendMessage(null, Logger.MessageTag.DIVIDE);
                            }
                            else
                            {
                                Logger.sendMessage("Sending to E-Mail contact [" + address.AddressName + "] is disabled. Skipping...", Logger.MessageTag.INFO);
                            }
                        }
                        else
                        {
                            Logger.sendMessage("Piping mails via SMTP host [" + smtphost.Description + "] is disabled. Skipping...", Logger.MessageTag.INFO);
                        }
                    }
                    else
                    {
                        Logger.sendMessage("Fetching mails from POP3 host [" + pop3host.Description + "] is disabled. Skipping...", Logger.MessageTag.INFO);
                    }
                }
                else
                {
                    Logger.sendMessage("Connection from [" + pop3host.Description + "] over [" + smtphost.Description + "] to [" + address.AddressName + "] is disabled. Skipping...", Logger.MessageTag.INFO);
                }

                Manager.threadCount--;
                Console.WriteLine("Release lock for next waiting thread.");
                Manager.mre.Set();
                if (conObj.WaitTime.TotalMilliseconds == 0)
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
                            mainWind.UpdateStartButtonLabel();
                        }
                    }
                    return;
                }
                Console.WriteLine("Finished cycle for thread: " + Thread.CurrentThread.Name);
                Console.WriteLine("Waiting " + conObj.WaitTime.TotalMilliseconds + " milliseconds...");
                TimeSpan waitTimeFromNow = conObj.WaitTime - (DateTime.Now - startTime);
                TimeSpan partialSleepSpan = new TimeSpan(0, 0, 1);
                while (Manager.isRunning() && (waitTimeFromNow.TotalMilliseconds > 0))
                {
                    Thread.Sleep(partialSleepSpan);
                    waitTimeFromNow = waitTimeFromNow - partialSleepSpan;
                }
            }
        }
    }
}
