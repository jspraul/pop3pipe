using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace POP3Pipe
{
    class CountDown
    {
        private static decimal seconds;
        private static bool stopped;
        private static Thread countThread;

        public static void startCounter(decimal secs)
        {
            seconds = secs;
            countThread = new Thread(new ThreadStart(count));
            countThread.Start();
        }

        public static void resetCounter()
        {
            if (!countThread.IsAlive && seconds != 0)
            {
                countThread = new Thread(new ThreadStart(count));
                countThread.Start();
            }
        }

        public static void stopCounter()
        {
            stopped = true;
            //countThread.Abort();
        }

        private static void count()
        {
            TimeSpan current = new TimeSpan(0,0,Convert.ToInt16(seconds));
            TimeSpan period = new TimeSpan(0,0,1);
            while (current.TotalSeconds > -1 && !stopped)
            {
                string output;
                if (current.TotalSeconds == 0)
                {
                    output = "..WAIT..";
                }
                else
                {
                    int hh = current.Hours;
                    int mm = current.Minutes;
                    int ss = current.Seconds;
                    output = (hh < 10 ? ("0" + hh) : hh.ToString()) + ":" + (mm < 10 ? ("0" + mm) : mm.ToString()) + ":" + (ss < 10 ? ("0" + ss) : ss.ToString());
                }
                MainWindow mainWind = (MainWindow)MainWindow.ActiveForm;
                if (mainWind != null && !mainWind.Disposing && !mainWind.IsDisposed)
                {
                    mainWind.refreshCountdown(output);
                }
                Thread.Sleep(1000);
                current = current.Subtract(period);
            }
        }
    }
}
