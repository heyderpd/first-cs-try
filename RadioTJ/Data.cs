using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RadioTJ
{
	public static class Data
	{
		public const string Info = "Programa: RadioTJ\nCriado por: Heyder Pestana Dias\nContato: heyderpd@gmail.com\nVersão: 1.16.0\nUltima Atualização: 07/02/14";

		public static Thread Thr;

		public static Data.Volume IN;

		public static Data.Volume MSG;

		public static Data.Volume OUT;

		public static bool Working;

		public static bool TickToPlay;

		public static bool Playng;

		public static string programPath;

		public static string audioPath;

		public static string keyPath;

		public static string iniPath;

		public static string TimeElapseShow;

		public static int Weakup;

		public static int Sleep;

		public static int TimeElapse;

		static Data()
		{
			Data.Thr = null;
			Data.Working = false;
			Data.TickToPlay = false;
			Data.Playng = false;
			Data.programPath = Data.GetAppDir();
			Data.audioPath = string.Concat(Data.programPath, "\\AUDIO");
			Data.keyPath = string.Concat(Data.programPath, "\\Data.ini");
			Data.iniPath = string.Concat(Data.programPath, "\\Data.ini");
			Data.TimeElapseShow = null;
			Data.Weakup = 9;
			Data.Sleep = 19;
			Data.TimeElapse = 10;
		}

		public static string GetAppDir()
		{
			string str = Application.ExecutablePath.ToString();
			string[] strArrays = str.Split(new char[] { '\\' });
			string str1 = strArrays[strArrays.Count<string>() - 1];
			string str2 = str.Substring(0, str.Length - (1 + str1.Length));
			return str2;
		}

		public static void Initialize()
		{
			Data.Working = false;
			Data.TickToPlay = false;
			Data.Playng = false;
			Data.programPath = Data.GetAppDir();
			Data.audioPath = string.Concat(Data.programPath, "\\AUDIO");
			Data.keyPath = string.Concat(Data.programPath, "\\Data.ini");
			Data.iniPath = string.Concat(Data.programPath, "\\Data.ini");
			Data.Weakup = 9;
			Data.Sleep = 19;
			Data.TimeElapse = 10;
			Data.TimeElapseShow = null;
		}

		public struct Volume
		{
			public uint ID;

			public int Max;

			public int Value;

			public double Range;

			public string Line;
		}
	}
}