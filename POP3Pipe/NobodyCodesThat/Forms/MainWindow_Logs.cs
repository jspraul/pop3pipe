using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace POP3Pipe
{
    public partial class MainWindow : Form
    {
        private string tempMsg;
        private Messenger.MessageTag messageTag;
        private static int richTextBoxPosition;

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
                    case Messenger.MessageTag.ERROR:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Red;
                        break;
                    case Messenger.MessageTag.WARNING:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Yellow;
                        break;
                    case Messenger.MessageTag.INFO:
                        msgType = this.messageTag.ToString() + ": ";
                        msgColor = Color.Blue;
                        break;
                    case Messenger.MessageTag.DIVIDE:
                        msgType = "------------------------------------";
                        break;
                    default:
                        break;
                }
                if (this.messageTag != Messenger.MessageTag.DIVIDE)
                {
                    string timeString = DateTime.Now.ToLongTimeString() + " ";
                    formatRichText(timeString, FontStyle.Bold, Color.Black);
                }
                formatRichText(msgType, FontStyle.Bold, msgColor);
                if (messageTag != Messenger.MessageTag.DIVIDE)
                {
                    formatRichText(tempMsg, FontStyle.Regular, msgColor);
                }
                textLog.AppendText(" \r\n");
                textLog.ScrollToCaret();
            }
        }

        private void formatRichText(string text, FontStyle style, Color color)
        {
            textLog.AppendText(text);
            if (richTextBoxPosition < textLog.Text.Length)
            {
                int foundHere = textLog.Find(text, richTextBoxPosition, RichTextBoxFinds.None);
                if (foundHere != -1)
                {
                    textLog.SelectionFont = new Font("Courier New", 8, style);
                    textLog.SelectionColor = color;
                    richTextBoxPosition += text.Length;
                }
            }
        }

        public void addMessage(string message, Messenger.MessageTag tag)
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
