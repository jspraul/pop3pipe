using System;
using System.Collections.Generic;
using System.Text;

namespace POP3Pipe
{
    public class AddressObject
    {
        private string addressName;

        public string AddressName
        {
            get { return addressName; }
            set { addressName = value; }
        }

        private string addressEMail;

        public string AddressEMail
        {
            get { return addressEMail; }
            set { addressEMail = value; }
        }

        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
