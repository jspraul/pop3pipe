using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace POP3Pipe
{
    public class ThunderbirdDataRetrieve
    {
        /// <summary>
        ///     Scans the hosts file of the Thunderbird application
        ///     and puts all found and valid entries into a specific list.
        ///     
        ///     The array of lists contains:
        ///         index [0] = POP3 Hosts
        ///         index [1] = SMTP Hosts
        /// </summary>
        /// <returns>Array of two host config lists.</returns>
        public List<HostConfigObject>[] RetrieveHostConfigs()
        {
            List<HostConfigObject>[] hosts = null;
            string fullpath = GetThunderbirdFolder();
            if (fullpath != null)
            {
                string hostsFilePath = fullpath + "\\signons.txt";
                if (File.Exists(hostsFilePath))
                {
                    string[] eMailData = null;
                    try
                    {
                        eMailData = File.ReadAllLines(hostsFilePath);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Another process currently uses the host config file.");
                        return null;
                    }

                    if (eMailData != null)
                    {
                        List<HostConfigObject> pop3Hosts = new List<HostConfigObject>();
                        List<HostConfigObject> smtpHosts = new List<HostConfigObject>();
                        HostConfigObject hostObj = null;
                        string smtpPattern = "smtp://";
                        string pop3Pattern = "mailbox://";
                        bool readPasswordNextLine = false;
                        for (int i = 0; i < eMailData.Length; i++)
                        {
                            int smtpExists = eMailData[i].IndexOf(smtpPattern);
                            int pop3Exists = eMailData[i].IndexOf(pop3Pattern);
                            if (smtpExists >= 0 || pop3Exists >= 0)
                            {
                                int thisStart = smtpExists >= 0 ? (smtpExists + smtpPattern.Length) : (pop3Exists + pop3Pattern.Length);
                                hostObj = new HostConfigObject();
                                string[] lineParts = eMailData[i].Substring(thisStart).Split(new char[] { '@' });
                                hostObj.Username = HttpUtility.UrlDecode(lineParts[0]);
                                hostObj.Host = HttpUtility.UrlDecode(lineParts[1]);
                                hostObj.Description = hostObj.Host;
                                hostObj.Active = true;

                                if (smtpExists >= 0)
                                {
                                    smtpHosts.Add(hostObj);
                                }
                                else
                                {
                                    pop3Hosts.Add(hostObj);
                                }
                                continue;
                            }

                            if (eMailData[i].Contains("=password="))
                            {
                                readPasswordNextLine = true;
                                continue;
                            }

                            if (readPasswordNextLine)
                            {
                                string encodedPassword = eMailData[i].Substring(1);
                                byte[] decodedByteArray = Convert.FromBase64String(encodedPassword);
                                ASCIIEncoding enc = new ASCIIEncoding();
                                string decodedPassword = enc.GetString(decodedByteArray);
                                hostObj.Password = decodedPassword;
                                readPasswordNextLine = false;
                            }
                        }
                        hosts = new List<HostConfigObject>[] {pop3Hosts, smtpHosts};
                    }
                }
            }
            return hosts;
        }

        /// <summary>
        ///     Gets the Thunderbird application folder.
        ///     This is a user account specific configuration,
        ///     that means it may work for WinXP only !
        /// </summary>
        /// <returns></returns>
        private string GetThunderbirdFolder()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            string fullpath = appdata + "\\Thunderbird";
            string profilesFile = fullpath + "\\profiles.ini";
            if (File.Exists(profilesFile))
            {
                string[] fileContent = File.ReadAllLines(profilesFile);
                string subfolder = null;
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if (fileContent[i].Contains("Path="))
                    {
                        subfolder = fileContent[i].Substring(fileContent[i].IndexOf("Path=") + 5);
                        break;
                    }
                }
                if (subfolder != null)
                {
                    return fullpath + "\\" + subfolder;
                }
            }
            return null;
        }

        /// <summary>
        ///     Scans the addressbook file of the Thunderbird application
        ///     and puts all found and valid entries into a dictionary.
        ///     
        ///     The E-Mail address is the key, no duplicate addresses possible.
        /// </summary>
        /// <returns>Dictionary with E-Mail address as key und the description as value.</returns>
        public Dictionary<string, string> RetrieveAddressBook()
        {
            Dictionary<string, string> addresses = null;
            string fullpath = GetThunderbirdFolder();
            if (fullpath != null)
            {
                string addrBookFilePath = fullpath + "\\abook.mab";
                if (File.Exists(addrBookFilePath))
                {
                    string addressBook = null;
                    try
                    {
                        addressBook = File.ReadAllText(addrBookFilePath);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Another process currently uses the address book.");
                        return null;
                    }
                    
                    if (addressBook != null)
                    {
                        addresses = new Dictionary<string, string>();

                        // Get key value
                        Regex regExKey = new Regex(@"\(([a-zA-Z0-9]+?)=DisplayName");
                        Match match = regExKey.Match(addressBook);
                        if (match == null)
                        {
                            Console.WriteLine("ERROR");
                            return null;
                        }
                        string keyName = match.Groups[1].Value;
                        //////

                        Regex regEx2 = new Regex(@"\[[a-zA-Z0-9]*?(?:(?:\(\^((?:[a-zA-Z0-9]*?)[\^|=](?:[a-zA-Z0-9]*?))\).*?)+)\]");
                        addressBook = addressBook.Replace('\r', ' ');
                        addressBook = addressBook.Replace('\n', ' ');
                        MatchCollection matches2 = regEx2.Matches(addressBook);
                        Dictionary<string, string>[] references = new Dictionary<string, string>[matches2.Count];
                        for (int i = 0; i < matches2.Count; i++)
                        {
                            Match item = matches2[i];
                            foreach (Capture innerItem in item.Groups[1].Captures)
                            {
                                string[] keyValuePair = innerItem.Value.Split(new char[] { '^' });
                                if (keyValuePair == null || keyValuePair.Length != 2)
                                {
                                    continue;
                                }
                                if (references[i] == null)
                                {
                                    references[i] = new Dictionary<string, string>();
                                }
                                references[i].Add(keyValuePair[0], keyValuePair[1]);
                            }
                        }

                        Regex regEx = new Regex(@"\(([a-zA-Z0-9]+?)=([\w-\.]+@(?:[\w-]+\.)+[\w-]{2,4})\)");
                        MatchCollection matches = regEx.Matches(addressBook);
                        foreach (Match item in matches)
                        {
                            string email = item.Groups[2].Value;
                            if (!addresses.ContainsKey(email))
                            {
                                string index = item.Groups[1].Value;
                                // Get the reference index
                                string nameIndex = null;
                                for (int i = 0; i < references.Length; i++)
                                {
                                    if (references[i].ContainsValue(index))
                                    {
                                        if (!references[i].ContainsKey(keyName))
                                        {
                                            continue;
                                        }
                                        nameIndex = references[i][keyName];
                                        break;
                                    }
                                }
                                if (nameIndex == null)
                                {
                                    continue;
                                }
                                Regex regExName = new Regex(@"\(" + nameIndex + @".*?=(.*?)\)");
                                MatchCollection matchesName = regExName.Matches(addressBook);
                                if (matchesName == null)
                                {
                                    break;
                                }
                                Match matchName;
                                if (matchesName.Count > 1)
                                {
                                    matchName = matchesName[1];
                                }
                                else
                                {
                                    matchName = matchesName[0];
                                }
                                string addressName = matchName.Groups[1].Value;
                                addressName = addressName.Replace('$', '%');
                                addressName = HttpUtility.UrlDecode(addressName);
                                addresses.Add(email.ToLower(), addressName);
                            }
                        }
                    }
                }
            }
            return addresses;
        }
    }
}
