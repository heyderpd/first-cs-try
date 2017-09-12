using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;

namespace $safeprojectname$
{
    static class Program
    {
        


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] arg)
        {
            //new Thread(AudioControl.Run).Start();
            ArgumentationCat(arg);
            if (!createdNew)
            {
                //MessageBox.Show("bye");
                return;
            }

            DiscWorker.Start(); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static bool createdNew = false;
        static void ArgumentationCat(String[] arg)
        {

            try
            {
                if (arg[0].Equals("-start"))
                {
                    while (!File.Exists(myName()))
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    System.Diagnostics.Process.Start(myName());
                    return;
                }
            }
            catch { }

            Mutex m = new Mutex(true, "myApp", out createdNew);
        }

        static String myName()
        {
            return System.IO.Directory.GetCurrentDirectory() + "\\$safeprojectname$.exe";
        }
        /*static int getNow()
        {
            return (DateTime.Now.Minute * 100) + DateTime.Now.Second;
        }

        static int getEnd()
        {
            int Min = DateTime.Now.Minute;
            int Sec = DateTime.Now.Second;
            Min += 3;
            if (Min >= 60)
                Min = 0;
            return (Min * 100) + Sec;
        }*/
    }
}
