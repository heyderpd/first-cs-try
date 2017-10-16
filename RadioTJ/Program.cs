using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RadioTJ
{
	internal static class Program
	{
        private static int PID;
        private static Mutex m;

		private static bool isRunning()
		{
			bool flag = false;
			Program.m = new Mutex(true, "RadioTJ", out flag);
			return !flag;
		}

        public static int getPID()
        {
            return Program.PID;
        }

        [STAThread]
		private static void Main(string[] arg)
		{
			if (Program.isRunning())
			{
				return;
			}

            Program.PID = Process.GetCurrentProcess().Id;
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Data.Initialize();
			DiscWorker.Start();
			Application.Run(new Form1());
		}

		public static void RestartProgram()
		{
			Process.Start(Application.ExecutablePath.ToString(), "RECALL");
			Program.ShutdownProgram();
		}

		public static void ShutdownProgram()
		{
			Application.Exit();
			Environment.Exit(1);
		}

		public static void TryRegWrite()
		{
			string str = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
			string str1 = string.Concat("\"", Application.ExecutablePath.ToString(), "\" -START");
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(str, true);
			registryKey.SetValue("RadioTJ", str1);
			if (str1 != (string)registryKey.GetValue("RadioTJ"))
			{
				MessageBox.Show("Falha ao registrar o aplicativo na inicialização do Windows.\nExecute o aplicativo como administrador.");
			}
		}
	}
}