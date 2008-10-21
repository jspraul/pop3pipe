using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POP3Pipe
{
    public class Logger
    {
        public enum MessageTag
        {
            ERROR, WARNING, INFO, DIVIDE
        }

        public static MainWindow mainWind;

        /// <summary>
        ///     Send a message with a specified tag to the log console on MainWindow.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static void sendMessage(string message, MessageTag tag)
        {
            //Form form = MainWindow.ActiveForm;
            //if (form != null)
            //{
            //    if (form.GetType() == typeof(MainWindow))
            //    {
            //        mainWind = (MainWindow)form;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Foreign instance of type <" + form.GetType().Name + "> cannot send messages.");
            //        return;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("MainWindow has been closed.");
            //    Manager.stopJob(false);
            //}

            try
            {
                if (mainWind != null && !mainWind.IsDisposed && !mainWind.Disposing)
                {
                    mainWind.addMessage(message, tag);
                }
                else
                {
                    //Manager.stopJob(false);
                }
            }
            catch (StackOverflowException)
            {
                // Doesn't matter
            }
        }
    }
}
