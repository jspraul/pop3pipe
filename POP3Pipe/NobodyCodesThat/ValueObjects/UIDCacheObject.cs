using System;
using System.Collections.Generic;
using System.Text;

namespace POP3Pipe
{
    public class UIDCacheObject
    {
        private int connectionID;

        public int ConnectionID
        {
            get { return this.connectionID; }
            set { this.connectionID = value; }
        }

        private List<int> mailUIDs = new List<int>();

        public List<int> MailUIDs
        {
            get { return this.mailUIDs; }
            set { this.mailUIDs = value; }
        }

        public void appendUID(int uid)
        {
            this.mailUIDs.Add(uid);
        }

        public void removeUID(int uid)
        {
            this.mailUIDs.Remove(uid);
        }
    }
}
