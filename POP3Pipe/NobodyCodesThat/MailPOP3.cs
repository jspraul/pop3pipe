using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenPOP.MIMEParser;
using OpenPOP.POP3;

namespace POP3Pipe
{
    /// <summary>
    ///     DEPRECATED, use <c>ManagerPOP3</c> instead
    /// </summary>
    class MailPOP3
    {
        public static bool running = true;

        // Authentication settings
        private static int STANDARD = 110;
        private static int SSL = 995;

        private static bool Connect(POPClient popClient, string host, int port)
        {
            bool connected = false;
            try
            {
                if (running)
                {
                    popClient.Connect(host, port);
                    Logger.sendMessage("Connecting to [" + host + "] using port [" + port + "]" + (port == SSL ? " SSL" : "") + ".", Logger.MessageTag.INFO);
                    connected = true;
                }
            }
            catch (Exception)
            {
                Logger.sendMessage("Unable to connect to [" + host + "] using port [" + port + "]" + (port == SSL ? " SSL" : "") + ".", Logger.MessageTag.ERROR);
            }
            return connected;
        }

        public static Message[] Receive(HostConfigObject pop3config)
        {
            POPClient popClient = new POPClient();
            // Set timeouts to 5 seconds
            popClient.ReceiveTimeOut = 5000;
            popClient.SendTimeOut = 5000;
            bool connected = false;

            // Port is set to automatic, try SSL first, then STANDARD
            if (pop3config.Port == 0)
            {
                Console.WriteLine("Automatic port is activated for POP3 host.");
                Console.WriteLine("Trying to connect with SSL.");
                connected = Connect(popClient,pop3config.Host, SSL);
                if (!connected)
                {
                    Console.WriteLine("Connection denied.");
                    Console.WriteLine("Trying to connect at standard port [" + STANDARD + "].");
                    connected = Connect(popClient, pop3config.Host, STANDARD);
                    if (!connected)
                    {
                        Console.WriteLine("Connection denied.");
                    }
                    else
                    {
                        Console.WriteLine("Connection granted.");
                    }
                }
                else
                {
                    Console.WriteLine("Connection granted.");
                }
            }
            else
            {
                Console.WriteLine("Currently activated port: " + pop3config.Port);
                Console.WriteLine("Trying to connect at this port.");
                connected = Connect(popClient,pop3config.Host, pop3config.Port);
                if (!connected)
                {
                    Console.WriteLine("Connection denied.");
                }
                else
                {
                    Console.WriteLine("Connection granted.");
                }
            }

            bool errorOccured = false;
            // Contacting the server and login
            
            Message[] msgArray = null;
            if (connected){
                try {
                    Console.WriteLine("Starting authentication.");
                    AuthenticationMethod auth = AuthenticationMethod.TRYBOTH;
                    popClient.Authenticate(pop3config.Username, pop3config.Password, auth);
                    Logger.sendMessage("Login successful.", Logger.MessageTag.INFO);
                    Console.WriteLine("login successful.");

                    int msgCount = popClient.GetMessageCount();
                    Logger.sendMessage("Account statistics loaded. [" + msgCount + "] messages on server.", Logger.MessageTag.INFO);
                    Console.WriteLine("Account statistics loaded. [" + msgCount + "] messages on server.");

                    List<Message> msgs = new List<Message>();

                    //System.Windows.Forms.MessageBox.Show("Fetching first 3 messages only (Bugfixing)");
                    // Mailbox entries always start with "1"
                    for (int i = 1; i <= 3; i++) // msgCount
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
                    Logger.sendMessage("Problem while receiving message/s.", Logger.MessageTag.ERROR);
                }
                finally
                {
                    popClient.Disconnect();
                }
            }
            if (!errorOccured)
            {
                Logger.sendMessage("Received " + (msgArray != null ? msgArray.Length.ToString() : "no") + " mails from " + pop3config.Description + ".", Logger.MessageTag.INFO);
            }
            return msgArray;
        }
    }
}
