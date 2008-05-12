using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenPOP.MIMEParser;
using System.Collections;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Net;

namespace POP3Pipe
{
    class MailSMTP
    {
        public static bool running = true;

        private static MailMessage mapper(Message msg)
        {
            MailMessage message = new MailMessage();
            MailAddressCollection toCol = new MailAddressCollection();
            for (int i = 0; i < msg.TO.Length; i++)
			{
                message.To.Add(msg.TO[i]);
			}
            message.From = new MailAddress(msg.From);
            //
            AttachmentCollection attCol = new Collection<System.Net.Mail.Attachment>() as AttachmentCollection;
            foreach (System.Net.Mail.Attachment att in msg.Attachments)
	        {
		         message.Attachments.Add(att);
	        }
            //
            MailAddressCollection bccCol = new MailAddressCollection();
            foreach (string bcc in msg.BCC)
            {
                message.Bcc.Add(new MailAddress(bcc));
            }
            //
            MailAddressCollection ccCol = new MailAddressCollection();
            foreach (string cc in msg.CC)
            {
                message.CC.Add(new MailAddress(cc));
            }
            //message.Headers = (NameValueCollection)msg.CustomHeaders.Values;
            message.Body = (string)msg.MessageBody[0];
            message.BodyEncoding = Encoding.GetEncoding(msg.ContentEncoding);
            //message.Notification = msg.DispositionNotificationTo != null ? true : false;
            message.Priority = MailPriority.Normal; // msg.Importance;
            message.ReplyTo = new MailAddress(msg.ReplyTo);
            message.Subject = msg.Subject;
            return message;
        }

        public static void Send(Message[] messages, HostConfigObject smtpObj, AddressObject addObj)
        {
            bool errorOccured = false;
            try
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    MailMessage message = mapper(messages[i]);
                    //message.From = new MailAddress(smtpObj.MailAddress);
                    message.To.Clear();
                    message.To.Add(new MailAddress(addObj.AddressEMail));

                    // Init SmtpClient and send
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = smtpObj.Host;
                    //smtpClient.UseDefaultCredentials = true;
                    //smtpClient.Port = 995; // 995 SSL | 110 normal
                    //smtpClient.EnableSsl = true;
                    NetworkCredential credentials = new NetworkCredential(smtpObj.Username, smtpObj.Password);
                    smtpClient.Credentials = credentials;

                    Console.WriteLine("Sending nr. " + (i+1));
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                errorOccured = true;
                Messenger.sendMessage(ex.Message, Messenger.MessageTag.ERROR);
                Console.WriteLine(ex.Message);
            }
            if (!errorOccured)
            {
                Messenger.sendMessage("Sent [" + messages.Length + "] mails to " + addObj.AddressName, Messenger.MessageTag.INFO);
            }
        }
    }
}
