using System;
using System.Collections.Generic;
using System.Text;

namespace POP3Pipe
{
    public class HostConfigObject
    {
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string host;
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        private string email;
        public string EMail
        {
            get { return email; }
            set { email = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
