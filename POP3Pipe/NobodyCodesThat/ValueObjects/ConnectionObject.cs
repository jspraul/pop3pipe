using System;
using System.Collections.Generic;
using System.Text;

namespace POP3Pipe
{
    public class ConnectionObject
    {
        private int pop3ID;

        public int Pop3ID
        {
            get { return pop3ID; }
            set { pop3ID = value; }
        }

        private int smtpID;

        public int SmtpID
        {
            get { return smtpID; }
            set { smtpID = value; }
        }

        private int addressID;

        public int AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }

        private TimeSpan waitTime;

        public TimeSpan WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; }
        }

        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        private bool continousMode;

        public bool ContinousMode
        {
            get { return continousMode; }
            set { continousMode = value; }
        }
    }
}
