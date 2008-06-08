using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenPOP.MIMEParser;
using OpenPOP.POP3;

namespace POP3Pipe
{
    class MailPOP3
    {
        public static bool running = true;

        // Authentication settings
        private static int STANDARD = 110;
        private static int SSL = 995;

        public static Message[] Receive(HostConfigObject pop3config)
        {
            POPClient popClient = new POPClient();

            bool connectFailed = false;
            try
            {
                if (running)
                {
                    popClient.Connect(pop3config.Host, SSL);
                    Messenger.sendMessage("Connecting to <" + pop3config.Host + "> using SSL.", Messenger.MessageTag.INFO);
                }
            } catch (Exception)
            {
                Messenger.sendMessage("Unable to connect to <" + pop3config.Host + "> using SSL.", Messenger.MessageTag.ERROR);
                connectFailed = true;
            }
            if (connectFailed){
               try
                {
                    if (running)
                    {
                        popClient.Connect(pop3config.Host, STANDARD);
                        Messenger.sendMessage("Connecting to <" + pop3config.Host + ">.", Messenger.MessageTag.INFO);
                    }
                } catch (Exception)
                {
                    Messenger.sendMessage("Unable to connect to <" + pop3config.Host + ">.", Messenger.MessageTag.ERROR);
                    connectFailed = true;
                }
            }
            bool errorOccured = false;
            // Contacting the server and login
            
            Message[] msgArray = null;
            if (!connectFailed){
                try {
                    AuthenticationMethod auth = AuthenticationMethod.TRYBOTH;
                    popClient.Authenticate(pop3config.Username, pop3config.Password, auth);
                    Messenger.sendMessage("Login successful.", Messenger.MessageTag.ERROR);

                    int msgCount = popClient.GetMessageCount();
                    Messenger.sendMessage("Account statistics loaded. " + msgCount + " messages on server.", Messenger.MessageTag.INFO);

                    List<Message> msgs = new List<Message>();

                    // Mailbox entries always start with "1"
                    for (int i = 1; i <= msgCount; i++)
                    {
                        if (running)
                        {
                            // Receive complete email
                            Message msgObj = popClient.GetMessage(i, false);
                            if (msgObj != null)
                            {
                                msgs.Add(msgObj);
                            }
                        }
                    }

                    msgArray = (Message[])msgs.ToArray();
                }
                catch (Exception)
                {
                    errorOccured = true;
                    Messenger.sendMessage("Problem while receiving message/s.", Messenger.MessageTag.ERROR);
                }
                finally
                {
                    popClient.Disconnect();
                }
            }
            if (!errorOccured)
            {
                Messenger.sendMessage("Received " + msgArray.Length + " mails from " + pop3config.Description + ".", Messenger.MessageTag.INFO);
            }
            return msgArray;
        }
    }
}
