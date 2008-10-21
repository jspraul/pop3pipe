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
using System.Net.Mime;

namespace POP3Pipe
{
    /// <summary>
    ///     DEPRECATED, use <c>ManagerSMTP</c> instead
    /// </summary>
    class MailSMTP
    {
        public static bool running = true;

        private static MailMessage mapper(Message msg, AddressObject addObj, HostConfigObject smtpObj)
        {
            Console.WriteLine("-------------------------");
            MailMessage message = new MailMessage();
            MailAddressCollection toCol = new MailAddressCollection();
            
            Console.WriteLine("message.To: " + addObj.AddressEMail);
            message.To.Add(addObj.AddressEMail);
            
            Console.WriteLine("message.From: " + msg.FromEmail);
            Console.WriteLine("message.ReplyTo: " + smtpObj.EMail);
            message.From = new MailAddress(smtpObj.EMail);
            
            //
            //AttachmentCollection attCol = new Collection<System.Net.Mail.Attachment>() as AttachmentCollection;
            foreach (OpenPOP.MIMEParser.Attachment att in msg.Attachments)
	        {
                Console.WriteLine(" --- Attachment ContentFileName: " + (att.ContentFileName != null ? att.ContentFileName : "null"));
                Console.WriteLine(" --- Attachment ContentType: " + (att.ContentType != null ? att.ContentType : "null"));
                Console.WriteLine(" --- Attachment ContentFileName: " + (att.ContentFileName != null ? att.ContentFileName : "null"));
                Console.WriteLine(" --- Attachment ContentType: " + (att.RawBytes != null ? att.RawBytes.Length.ToString() : "null"));
                Console.WriteLine(" --- Attachment InBytes: " + att.InBytes.ToString());
                Console.WriteLine(" --- Attachment NotAttachment: " + att.NotAttachment.ToString());
                Console.WriteLine(" --- Attachment RawAttachment: " + (att.RawAttachment != null ? att.RawAttachment.Substring(0,60) : "null"));
                Console.WriteLine(" --- Attachment DefaultReportFileName: " + (att.DefaultReportFileName != null ? att.DefaultReportFileName : "null"));
                Console.WriteLine(" --- Attachment DefaultMIMEFileName: " + (att.DefaultMIMEFileName != null ? att.DefaultMIMEFileName : "null"));
                Console.WriteLine(" --- Attachment DefaultFileName: " + (att.DefaultFileName != null ? att.DefaultFileName : "null"));
                Console.WriteLine(" --- Attachment DefaultFileName2: " + (att.DefaultFileName2 != null ? att.DefaultFileName2 : "null"));
                Console.WriteLine(" --- Attachment DecodeAsText: " + (att.DecodeAsText() != null ? att.DecodeAsText().Substring(0,60) : "null"));
                Console.WriteLine(" --- Attachment DecodedAttachment: " + (att.DecodedAttachment != null ? att.DecodedAttachment.Length.ToString() : "null"));
                Console.WriteLine(" --- Attachment ContentCharset: " + (att.ContentCharset != null ? att.ContentCharset : "null"));
                Console.WriteLine(" --- Attachment ContentDescription: " + (att.ContentDescription != null ? att.ContentDescription : "null"));
                Console.WriteLine(" --- Attachment ContentFormat: " + (att.ContentFormat != null ? att.ContentFormat : "null"));
                Console.WriteLine(" --- Attachment ContentLength: " + att.ContentLength.ToString());

                if (!att.NotAttachment)
                {
                    System.Net.Mail.Attachment newAtt = new System.Net.Mail.Attachment(new MemoryStream(att.DecodedAttachment), new ContentType(att.ContentType));


                    //Console.WriteLine(" --- Attachment ContentDisposition: " + att.ContentDisposition);
                    //newAtt.ContentDisposition = new ContentDisposition(att.ContentDisposition);
                    Console.WriteLine(" --- Attachment ContentId: " + att.ContentID);
                    newAtt.ContentId = att.ContentID;

                    string encoding = att.ContentTransferEncoding;
                    Console.WriteLine(" --- Attachment TransferEncoding: " + encoding);
                    if (encoding.Equals("base64", StringComparison.InvariantCultureIgnoreCase))
                    {
                        newAtt.TransferEncoding = TransferEncoding.Base64;
                    }
                    else if (encoding.Equals("quotedprintable", StringComparison.InvariantCultureIgnoreCase))
                    {
                        newAtt.TransferEncoding = TransferEncoding.QuotedPrintable;
                    }
                    else if (encoding.Equals("sevenbit", StringComparison.InvariantCultureIgnoreCase))
                    {
                        newAtt.TransferEncoding = TransferEncoding.SevenBit;
                    }
                    else
                    {
                        newAtt.TransferEncoding = TransferEncoding.Unknown;
                    }
                    message.Attachments.Add(newAtt);
                }
                
                
	        }
            //
            //MailAddressCollection bccCol = new MailAddressCollection();
            //foreach (string bcc in msg.BCC)
            //{
            //    message.Bcc.Add(new MailAddress(bcc));
            //}
            //
            //MailAddressCollection ccCol = new MailAddressCollection();
            //foreach (string cc in msg.CC)
            //{
            //    message.CC.Add(new MailAddress(cc));
            //}
            //message.Headers = (NameValueCollection)msg.CustomHeaders.Values;

            for (int i = 0; i < msg.MessageBody.Count; i++)
            {
                if (msg.MessageBody[i] != null)
                {
                    string body = (string)msg.MessageBody[i];

                    Console.WriteLine("message.Body[" + i + "]: " + body.Substring(0, 60) + "...");
                    message.Body = body;
                }
            }
            
            string enc = msg.ContentEncoding;
            if (enc != null)
            {
                Console.WriteLine("message.ContentEncoding: " + enc);
                if (enc.Contains("UTF8"))
                {
                    message.BodyEncoding = Encoding.UTF8;
                }
                else if (enc.Contains("UTF7"))
                {
                    message.BodyEncoding = Encoding.UTF7;
                }
                else if (enc.Contains("UTF32"))
                {
                    message.BodyEncoding = Encoding.UTF32;
                }
                else if (enc.Contains("Unicode"))
                {
                    message.BodyEncoding = Encoding.Unicode;
                }
                else if (enc.Contains("ASCII"))
                {
                    message.BodyEncoding = Encoding.ASCII;
                }
                else
                {
                    message.BodyEncoding = Encoding.Default;
                }
            }
            
            

            //message.Notification = msg.DispositionNotificationTo != null ? true : false;
            message.Priority = MailPriority.Normal; // msg.Importance;

            message.ReplyTo = new MailAddress(msg.FromEmail);

            string subject = "";
            if (msg.Subject != null)
            {
                subject = msg.Subject;
            }
            Console.WriteLine("message.Subject: " + subject);
            message.Subject = subject;
            
            return message;
        }

        public static void Send(Message[] messages, HostConfigObject smtpObj, AddressObject addObj)
        {
            Console.WriteLine("Prepare messages for sending.");
            bool errorOccured = false;
            try
            {
                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient();
                Console.WriteLine("SMTP - Host: " + smtpObj.Host);
                smtpClient.Host = smtpObj.Host;
                //smtpClient.UseDefaultCredentials = true;
                Console.WriteLine("SMTP - Port: " + smtpObj.Port);
                smtpClient.Port = smtpObj.Port;
                //smtpClient.EnableSsl = true;
                Console.WriteLine("SMTP - Username: " + smtpObj.Username);
                Console.WriteLine("SMTP - Password: " + smtpObj.Password);
                NetworkCredential credentials = new NetworkCredential(smtpObj.Username, smtpObj.Password);
                smtpClient.Credentials = credentials;

                //System.Windows.Forms.MessageBox.Show("Sending first 3 messages only (Bugfixing)");
                for (int i = 0; i < 3; i++) // messages.Length
                {
                    MailMessage message = mapper(messages[i], addObj, smtpObj);
                    Console.WriteLine("Sending msg nr. " + (i+1));
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                errorOccured = true;
                Logger.sendMessage(ex.Message, Logger.MessageTag.ERROR);
                Console.WriteLine(ex.Message);
            }
            if (!errorOccured)
            {
                Logger.sendMessage("Sent [" + messages.Length + "] mails to " + addObj.AddressName, Logger.MessageTag.INFO);
            }
        }
    }
}
