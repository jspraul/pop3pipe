using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace POP3Pipe
{
    static class SettingsObject
    {
        private static List<ConnectionObject> listConnections;

        public static List<ConnectionObject> ListConnections
        {
            get { return listConnections; }
            set { listConnections = value; }
        }

        private static List<HostConfigObject> listPOP3;

        public static List<HostConfigObject> ListPOP3
        {
            get { return listPOP3; }
            set { listPOP3 = value; }
        }

        private static List<HostConfigObject> listSMTP;

        public static List<HostConfigObject> ListSMTP
        {
            get { return listSMTP; }
            set { listSMTP = value; }
        }

        private static List<AddressObject> listAddress;

        public static List<AddressObject> ListAddress
        {
            get { return listAddress; }
            set { listAddress = value; }
        }
    }
}
