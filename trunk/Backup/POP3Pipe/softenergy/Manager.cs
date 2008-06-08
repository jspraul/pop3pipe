using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenPOP.MIMEParser;

namespace POP3Pipe
{
    class Manager
    {
        private static bool running;
        private static bool firstTime;

        private static TimeSpan waittime;
        private static ConnectionObject conObj;
        private static string smtpServer;
        private static string smtpName;
        private static string smtpPassword;

        private static DateTime startTime;

        /// <summary>
        ///     Start a job for the given connection.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="currentConnection"></param>
        public static void runJob(ConnectionObject currentConnection)
        {
            Manager.waittime = currentConnection.WaitTime;
            Manager.smtpServer = SettingsObject.ListSMTP[currentConnection.SmtpID].Host;
            Manager.smtpName = SettingsObject.ListSMTP[currentConnection.SmtpID].Username;
            Manager.smtpPassword = SettingsObject.ListSMTP[currentConnection.SmtpID].Password;

            Manager.conObj = currentConnection;

            startTime = DateTime.Now;
            Messenger.sendMessage("Starting process", Messenger.MessageTag.INFO);
            Thread ta = new Thread(new ThreadStart(fetchMails));
            running = true;
            firstTime = true;
            ta.Start();
        }

        public static bool isRunning()
        {
            return running;
        }

        public static void stopJob()
        {
            running = false;
            MailPOP3.running = false;
            MailSMTP.running = false;
            Messenger.sendMessage("Process stopped", Messenger.MessageTag.INFO);
        }

        private static void fetchMails()
        {
            while (running)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan difference = currentTime - startTime;
                if (firstTime || (difference > waittime))
                {
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
                    startTime = DateTime.Now;
                }
                firstTime = false;
                if (waittime.TotalMilliseconds == 0)
                {
                    MainWindow mainWind = (MainWindow)MainWindow.ActiveForm;
                    if (mainWind != null && !mainWind.Disposing && !mainWind.IsDisposed)
                    {
                        mainWind.UpdateStartButton();
                    }
                    return;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
