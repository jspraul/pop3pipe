using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using POP3Pipe.Properties;
using System.Collections.Generic;

namespace POP3Pipe
{
    class FileOperations
    {
        public static void writeFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.RemoveAll();
                XmlNode root = doc.CreateElement("config");

                // Connections listing
                XmlElement cons = doc.CreateElement("connections");
                foreach (ConnectionObject dataObject in SettingsObject.ListConnections)
                {
                    XmlElement connect = doc.CreateElement("connect");
                    connect.SetAttribute("pop3id", dataObject.Pop3ID.ToString());
                    connect.SetAttribute("smtpid", dataObject.SmtpID.ToString());
                    connect.SetAttribute("emailid", dataObject.AddressID.ToString());
                    connect.SetAttribute("active", dataObject.Active.ToString());
                    // Repetition Times for cycling
                    XmlElement times = doc.CreateElement("times");
                    times.SetAttribute("hours", dataObject.WaitTime.Hours.ToString());
                    times.SetAttribute("minutes", dataObject.WaitTime.Minutes.ToString());
                    times.SetAttribute("seconds", dataObject.WaitTime.Seconds.ToString());
                    connect.AppendChild(times);
                    cons.AppendChild(connect);
                }
                root.AppendChild(cons);

                // POP3 Server listing
                XmlElement pops = doc.CreateElement("pop3hosts");
                foreach (HostConfigObject dataObject in SettingsObject.ListPOP3)
                {
                    XmlElement pop3 = doc.CreateElement("pop3");
                    pop3.SetAttribute("description", dataObject.Description);
                    pop3.SetAttribute("host", dataObject.Host);
                    pop3.SetAttribute("username", dataObject.Username);
                    pop3.SetAttribute("password", dataObject.Password);
                    pop3.SetAttribute("active", dataObject.Active.ToString());
                    pops.AppendChild(pop3);
                }
                root.AppendChild(pops);

                // SMTP Server listing
                XmlElement smtphosts = doc.CreateElement("smtphosts");
                foreach (HostConfigObject dataObject in SettingsObject.ListSMTP)
                {
                    XmlElement smtp = doc.CreateElement("smtp");
                    smtp.SetAttribute("description", dataObject.Description);
                    //smtp.SetAttribute("email", dataObject.MailAddress);
                    smtp.SetAttribute("host", dataObject.Host);
                    smtp.SetAttribute("username", dataObject.Username);
                    smtp.SetAttribute("password", dataObject.Password);
                    smtp.SetAttribute("active", dataObject.Active.ToString());
                    smtphosts.AppendChild(smtp);
                }
                root.AppendChild(smtphosts);

                // Mail Address listing
                XmlElement addresses = doc.CreateElement("mailaddresses");
                foreach (AddressObject dataObject in SettingsObject.ListAddress)
                {
                    XmlElement add = doc.CreateElement("address");
                    add.SetAttribute("name", dataObject.AddressName);
                    add.SetAttribute("email", dataObject.AddressEMail);
                    add.SetAttribute("active", dataObject.Active.ToString());
                    addresses.AppendChild(add);
                }
                root.AppendChild(addresses);

                doc.AppendChild(root);
                XmlTextWriter writer = new XmlTextWriter(Settings.Default.ConfigFile, null);
                writer.Formatting = Formatting.Indented;
                doc.WriteTo(writer);
                writer.Close();
            }
            catch (Exception)
            {
                Messenger.sendMessage("Cannot write config file!", Messenger.MessageTag.ERROR);
            }
        }

        public static void readFile()
        {
            SettingsObject.ListConnections = new List<ConnectionObject>();
            SettingsObject.ListPOP3 = new List<HostConfigObject>();
            SettingsObject.ListSMTP = new List<HostConfigObject>();
            SettingsObject.ListAddress = new List<AddressObject>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Settings.Default.ConfigFile);
                XmlNode root = doc.SelectSingleNode("config");

                // Read connections
                XmlNode connections = root.SelectSingleNode("connections");
                if (connections != null)
                {
                    XmlNodeList dataNodes = connections.ChildNodes;
                    // Iterates through data nodes
                    foreach (XmlNode connect in dataNodes)
                    {
                        ConnectionObject item = new ConnectionObject();
                        item.Pop3ID = getIntValue(connect, "pop3id");
                        item.SmtpID = getIntValue(connect, "smtpid");
                        item.AddressID = Convert.ToInt32(getValue(connect, "emailid"));
                        item.Active = getBoolValue(connect, "active");

                        // Repetition Times for cycling
                        XmlNode timecycle = connect.ChildNodes[0];
                        int hours = getIntValue(timecycle, "hours");
                        int minutes = getIntValue(timecycle, "minutes");
                        int seconds = getIntValue(timecycle, "seconds");
                        TimeSpan time = new TimeSpan(hours, minutes, seconds);
                        item.WaitTime = time;
                        SettingsObject.ListConnections.Add(item);
                    }
                }

                // Read POP3 Hosts
                XmlNode pops = root.SelectSingleNode("pop3hosts");
                if (pops != null)
                {
                    XmlNodeList dataNodes = pops.ChildNodes;
                    // Iterates through data nodes
                    foreach (XmlNode pop3 in dataNodes)
                    {
                        HostConfigObject item = new HostConfigObject();
                        item.Description = getValue(pop3, "description");
                        item.Host = getValue(pop3, "host");
                        item.Username = getValue(pop3, "username");
                        item.Password = getValue(pop3, "password");
                        item.Active = getBoolValue(pop3, "active");
                        SettingsObject.ListPOP3.Add(item);
                    }
                }

                // Read SMTP Hosts
                XmlNode smtps = root.SelectSingleNode("smtphosts");
                if (smtps != null)
                {
                    XmlNodeList dataNodes = smtps.ChildNodes;
                    // Iterates through data nodes
                    foreach (XmlNode smtp in dataNodes)
                    {
                        HostConfigObject item = new HostConfigObject();
                        item.Description = getValue(smtp, "description");
                        //item.MailAddress = getValue(smtp, "email");
                        item.Host = getValue(smtp, "host");
                        item.Username = getValue(smtp, "username");
                        item.Password = getValue(smtp, "password");
                        item.Active = getBoolValue(smtp, "active");
                        SettingsObject.ListSMTP.Add(item);
                    }
                }

                // Read mail addresses
                XmlNode addresses = root.SelectSingleNode("mailaddresses");
                if (addresses != null)
                {
                    XmlNodeList dataNodes = addresses.ChildNodes;
                    // Iterates through data nodes
                    foreach (XmlNode add in dataNodes)
                    {
                        AddressObject item = new AddressObject();
                        item.AddressName = getValue(add, "name");
                        item.AddressEMail = getValue(add, "email");
                        item.Active = getBoolValue(add, "active");
                        SettingsObject.ListAddress.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                Messenger.sendMessage("Cannot load config!", Messenger.MessageTag.ERROR);
            }
        }

        private static string getValue(XmlNode node, string attribute){
            XmlAttribute att = node.Attributes[attribute];
            if (att == null) return null;
            else return att.Value;
        }

        private static int getIntValue(XmlNode node, string attribute)
        {
            string temp = getValue(node, attribute);
            if (temp == null) return 0;
            else return Convert.ToInt16(temp);
        }

        private static bool getBoolValue(XmlNode node, string attribute)
        {
            string temp = getValue(node, attribute);
            if (temp == null) return false;
            else return Convert.ToBoolean(temp);
        }

    }
}
