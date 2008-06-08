using System;
using System.Collections.Generic;
using System.Text;

namespace POP3Pipe
{
    public class Messenger
    {
        public enum MessageTag
        {
            ERROR, WARNING, INFO, DIVIDE
        }
        public static int MESSAGE_TAG_ERROR = 0;
        public static int MESSAGE_TAG_WARNING = 1;
        public static int MESSAGE_TAG_INFO = 2;
        public static int MESSAGE_DIVIDE = 3;

        private static MainWindow mainWind;

        /// <summary>
        ///     Send a message with a specified tag to the log console on MainWindow.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static void sendMessage(string message, MessageTag tag)
        {
            System.Windows.Forms.Form form = MainWindow.ActiveForm;
            if (form != null)
            {
                mainWind = (MainWindow)form;
            }
            else
            {
                Console.WriteLine("MainWindow has been closed.");
            }

            try
            {
                if (mainWind != null && !mainWind.IsDisposed && !mainWind.Disposing)
                {
                    mainWind.addMessage(message, tag);
                }
                else
                {
                    Manager.stopJob();
                }
            }
            catch (StackOverflowException)
            {
                // Doesn't matter
            }
        }
    }
}
