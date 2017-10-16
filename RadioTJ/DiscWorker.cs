using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RadioTJ
{
	internal static class DiscWorker
	{
		private static IniWorker INI;

		private static Form F;

		static DiscWorker()
		{
			DiscWorker.INI = new IniWorker();
			DiscWorker.F = null;
		}

		public static void Initialize()
		{
			Data.IN.Range = 1;
			Data.MSG.Range = 1;
			Data.OUT.Range = 1;
			Data.IN.Max = 70;
			Data.MSG.Max = 100;
			Data.OUT.Max = 80;
			Data.IN.Value = 0;
			Data.MSG.Value = 0;
			Data.OUT.Value = 0;
			Data.IN.Line = "Rear Blue In";
			Data.MSG.Line = "Som wave";
			Data.OUT.Line = "Volume principal";
			Data.IN.ID = "IN";
            Data.MSG.ID = "MSG";
            Data.OUT.ID = "OUT";
            Data.TimeElapse = 10;
		}

		public static void LoadDisk()
		{
			try
			{
				ListManager.Clear();
				string[] files = Directory.GetFiles(Data.audioPath, "*.wav");
				for (int i = 0; i < (int)files.Length; i++)
				{
					string str = files[i];
					char[] array = new char[] { "\\".ToArray<char>()[0] };
					string[] strArrays = str.Split(array);
					string str1 = string.Concat("\\", strArrays[strArrays.Count<string>() - 1]);
					string str2 = str1.Substring(1, str1.Length - 5);
					ListManager.AddItem(str2, str1, false);
				}
				ListManager.GeraOrdem();
				ListManager.setNext();
			}
			catch
			{
			}
		}

		public static void LoadIni()
		{
			bool flag;
			try
			{
				DiscWorker.INI.loadIni();
				ListManager.Clear();
				int num = DiscWorker.INI.EnumSection("AUDIO").Count<string>() / 3;
				for (int i = 1; i <= num; i++)
				{
					string setting = DiscWorker.INI.GetSetting("AUDIO", string.Concat("ITEM_", i, "_NAME"));
					string str = DiscWorker.INI.GetSetting("AUDIO", string.Concat("ITEM_", i, "_DIR"));
					try
					{
						flag = Convert.ToBoolean(DiscWorker.INI.GetSetting("AUDIO", string.Concat("ITEM_", i, "_PRI")));
					}
					catch
					{
						flag = false;
					}
					ListManager.AddItem(setting, str, flag);
				}
				ListManager.GeraOrdem();
				Data.IN.Max = int.Parse(DiscWorker.INI.GetSetting("CONFIG", "VOL_IN_MAX"));
				Data.MSG.Max = int.Parse(DiscWorker.INI.GetSetting("CONFIG", "VOL_MSG_MAX"));
				Data.OUT.Max = int.Parse(DiscWorker.INI.GetSetting("CONFIG", "VOL_OUT_MAX"));
				Data.IN.Line = DiscWorker.INI.GetSetting("CONFIG", "IN_LINE");
				Data.TimeElapse = int.Parse(DiscWorker.INI.GetSetting("CONFIG", "TIME"));
				Data.IN.Value = Data.IN.Max;
				Data.OUT.Value = Data.OUT.Max;
				Player.setAllVolMixer();
				ListManager.setNext();
			}
			catch
			{
				DiscWorker.MakeDefaultIni();
			}
		}

		public static void MakeDefaultIni()
		{
			DiscWorker.LoadDisk();
			DiscWorker.Initialize();
			DiscWorker.openDialogToSelectLine();
			DiscWorker.SaveIni();
			Player.setAllVolMixer();
		}

		public static void openDialogToSelectLine()
		{
			try
			{
				DiscWorker.F.Invoke(new MethodInvoker(DiscWorker.F.Close));
			}
			catch
			{
			}
			try
			{
				(new Thread(new ThreadStart(DiscWorker.whaitToClose))).Start();
			}
			catch
			{
			}
		}

		public static void SaveIni()
		{
			try
			{
				DiscWorker.INI.ClearAll();
				List<VoiceItem> list = ListManager.getList();
				int num = 0;
				foreach (VoiceItem voiceItem in list)
				{
					int num1 = num + 1;
					num = num1;
					DiscWorker.INI.AddSetting("AUDIO", string.Concat("ITEM_", num1, "_NAME"), voiceItem.getName());
					DiscWorker.INI.AddSetting("AUDIO", string.Concat("ITEM_", num, "_DIR"), voiceItem.getFileName());
					IniWorker nI = DiscWorker.INI;
					string str = string.Concat("ITEM_", num, "_PRI");
					bool priori = voiceItem.getPriori();
					nI.AddSetting("AUDIO", str, priori.ToString());
				}
				DiscWorker.INI.AddSetting("CONFIG", "VOL_IN_MAX", Data.IN.Max.ToString());
				DiscWorker.INI.AddSetting("CONFIG", "VOL_MSG_MAX", Data.MSG.Max.ToString());
				DiscWorker.INI.AddSetting("CONFIG", "VOL_OUT_MAX", Data.OUT.Max.ToString());
				DiscWorker.INI.AddSetting("CONFIG", "IN_LINE", Data.IN.Line);
				DiscWorker.INI.AddSetting("CONFIG", "TIME", Data.TimeElapse.ToString());
				DiscWorker.INI.SaveSettings();
			}
			catch
			{
			}
		}

		public static void Start()
		{
			DiscWorker.Initialize();
			int num = 0;
			while (!File.Exists(Application.ExecutablePath.ToString()))
			{
				Thread.Sleep(10);
				num++;
				if (num <= 6000)
				{
					continue;
				}
				Program.RestartProgram();
			}
			DiscWorker.LoadIni();
		}

		public static void whaitToClose()
		{
			try
			{
				Thread.Sleep(60000);
				DiscWorker.F.Invoke(new MethodInvoker(DiscWorker.F.Close));
			}
			catch
			{
			}
		}
	}
}