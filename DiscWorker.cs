using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace $safeprojectname$
{
    static class DiscWorker
    {
        static IniWorker INI = new IniWorker();
        public static void Start()
        {
            Initialize();
            LoadIni();
        }
        public static void Initialize()
        {
            Data.IN.Range = 0.15;
            Data.MSG.Range = 1.0;
            Data.OUT.Range = 0.70;

            Data.IN.Max = 90;
            Data.MSG.Max = 100;
            Data.OUT.Max = 90;

            Data.IN.Value = 0;
            Data.MSG.Value = 0;
            Data.OUT.Value = 0;

            Data.IN.Line = "Rear Blue In";
            Data.MSG.Line = "Som wave";
            Data.OUT.Line = "Volume principal";

            Data.TimeElapse = 10;
        }
        public static void MakeDefaultIni()
        {
            LoadDisk();
            Initialize();
            SaveIni();
            Player.setAllVolMixer();
        }
        public static void LoadDisk()
        {
            try
            {
                ListItemManager.Clear();
                String[] filePaths = Directory.GetFiles($safeprojectname$.Data.audioPath, "*.wav");
                string[] tempDir;
                String tempPath, tempName;
                foreach (String filePath in filePaths)
                {
                    tempDir = filePath.Split(new char[] { (@"\").ToArray()[0] });
                    tempPath = @"\" + tempDir[tempDir.Count() -1];
                    tempName = tempPath.Substring(1, tempPath.Length - 5);
                    ListItemManager.Add(tempName, tempPath);
                }
                ListItemManager.GeraOrdem();
            }
            catch {}
        }
        public static void SaveIni()
        {
            try
            {
                INI.ClearAll();

                List<VoiceItem> LVI = ListItemManager.getList();
                int i = 0;
                foreach (VoiceItem Item in LVI)
                {
                    INI.AddSetting("AUDIO", "ITEM_" + ++i + "_NAME", Item.getName());
                    INI.AddSetting("AUDIO", "ITEM_" + i + "_DIR", Item.getFileName());
                }

                INI.AddSetting("CONFIG", "VOL_IN_MAX", (Data.IN.Max).ToString());
                INI.AddSetting("CONFIG", "VOL_MSG_MAX", (Data.MSG.Max).ToString());
                INI.AddSetting("CONFIG", "VOL_OUT_MAX", (Data.OUT.Max).ToString());

                INI.AddSetting("CONFIG", "IN_LINE", Data.IN.Line);
                INI.AddSetting("CONFIG", "TIME", (Data.TimeElapse).ToString());

                INI.SaveSettings();
            }
            catch { }
        }
        public static void LoadIni()
        {
            try
            {
                INI.loadIni();
                ListItemManager.Clear();

                string name, dir;
                int listaCount = (INI.EnumSection("AUDIO")).Count() / 2;
                for (int i = 1; i <= listaCount; ++i)
                {
                    name = INI.GetSetting("AUDIO", "ITEM_" + i + "_NAME");
                    dir = INI.GetSetting("AUDIO", "ITEM_" + i + "_DIR");
                    ListItemManager.Add(name, dir);
                }
                ListItemManager.GeraOrdem();

                Data.IN.Max = int.Parse(INI.GetSetting("CONFIG", "VOL_IN_MAX"));
                Data.MSG.Max = int.Parse(INI.GetSetting("CONFIG", "VOL_MSG_MAX"));
                Data.OUT.Max = int.Parse(INI.GetSetting("CONFIG", "VOL_OUT_MAX"));

                Data.IN.Line = INI.GetSetting("CONFIG", "IN_LINE");
                Data.TimeElapse = int.Parse(INI.GetSetting("CONFIG", "TIME"));

                Data.IN.Value = Data.IN.Max;
                Data.OUT.Value = Data.OUT.Max;
                Player.setAllVolMixer();
            }
            catch {
                MakeDefaultIni();
            }
        }
    }
}