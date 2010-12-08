using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Updater;

namespace POP3Pipe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Program.info.ModuleName = "pop3pipe";
            //Program.info.Version = "080";
            //Program.info.Build = "25062008";
            //Program.info.UpdateLocations = new string[] { "http://pop3pipe.googlecode.com/svn/trunk" };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        //public static ProgramInfo info = new ProgramInfo();
    }
}