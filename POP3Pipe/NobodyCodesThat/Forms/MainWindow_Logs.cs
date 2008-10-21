using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        private string tempMsg;
        private Logger.MessageTag messageTag;

        private void UpdateRichTextBox()
        {
            if (textLog.InvokeRequired)
            {
                // call back on this same method, but in a different thread.
                textLog.Invoke(new MethodInvoker(UpdateRichTextBox));
            }
            else
            {
                // you are in this method on the correct thread.
                string msgType = "";
                Color msgColor = Color.Black;
                switch (this.messageTag)
                {
                    case Logger.MessageTag.ERROR:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Red;
                        break;
                    case Logger.MessageTag.WARNING:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Yellow;
                        break;
                    case Logger.MessageTag.INFO:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Blue;
                        break;
                    case Logger.MessageTag.DIVIDE:
                        msgType = "------------------------------------";
                        break;
                    default:
                        break;
                }
                if (this.messageTag != Logger.MessageTag.DIVIDE)
                {
                    string timeString = DateTime.Now.ToLongTimeString() + " ";
                    formatRichText(timeString, FontStyle.Bold, Color.Black);
                }
                formatRichText(msgType, FontStyle.Bold, msgColor);
                if (messageTag != Logger.MessageTag.DIVIDE)
                {
                    formatRichText(tempMsg, FontStyle.Regular, msgColor);
                }
                textLog.AppendText(" \r\n");
                textLog.ScrollToCaret();
            }
        }

        /// <summary>
        ///     Used synchronized implementation method because of asynchronous message display.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style"></param>
        /// <param name="color"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void formatRichText(string text, FontStyle style, Color color)
        {
            int startPos = textLog.SelectionStart;
            textLog.AppendText(text);
            textLog.SelectionStart = startPos;
            textLog.SelectionLength = text.Length;
            textLog.SelectionFont = new Font("Courier New", 8, style);
            textLog.SelectionColor = color;
            textLog.SelectionStart += text.Length;
        }

        public void addMessage(string message, Logger.MessageTag tag)
        {
            tempMsg = message;
            messageTag = tag;
            UpdateRichTextBox();
            tempMsg = "";
            messageTag = 0;
        }

        private bool switchedToLog = false;

        private void textLog_TextChanged(object sender, EventArgs e)
        {
            if (!switchedToLog)
            {
                tabControl.SelectTab("tabLogs");
                switchedToLog = true;
            }
        }
    }
}
