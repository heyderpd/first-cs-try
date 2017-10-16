using System;
using System.Collections;
using System.IO;

namespace RadioTJ
{
	public class IniWorker
	{
		private string iniFilePath = Data.iniPath;

		private Hashtable keyPairs = new Hashtable();

		public IniWorker()
		{
		}

		public void AddSetting(string sectionName, string settingName, string settingValue)
		{
			IniWorker.SectionPair sectionPair = new IniWorker.SectionPair();
			sectionPair.Section = sectionName;
			sectionPair.Key = settingName;
			if (this.keyPairs.ContainsKey(sectionPair))
			{
				this.keyPairs.Remove(sectionPair);
			}
			this.keyPairs.Add(sectionPair, settingValue);
		}

		public void AddSetting(string sectionName, string settingName)
		{
			this.AddSetting(sectionName, settingName, null);
		}

		public void ClearAll()
		{
			this.keyPairs.Clear();
		}

		public void DeleteSetting(string sectionName, string settingName)
		{
			IniWorker.SectionPair sectionPair = new IniWorker.SectionPair();
			sectionPair.Section = sectionName;
			sectionPair.Key = settingName;
			if (this.keyPairs.ContainsKey(sectionPair))
			{
				this.keyPairs.Remove(sectionPair);
			}
		}

		public string[] EnumSection(string sectionName)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (IniWorker.SectionPair key in this.keyPairs.Keys)
			{
				if (key.Section != sectionName)
				{
					continue;
				}
				arrayLists.Add(key.Key);
			}
			return (string[])arrayLists.ToArray(typeof(string));
		}

		public string GetSetting(string sectionName, string settingName)
		{
			IniWorker.SectionPair sectionPair = new IniWorker.SectionPair();
			sectionPair.Section = sectionName;
			sectionPair.Key = settingName;
			return (string)this.keyPairs[sectionPair];
		}

		public void loadIni()
		{
			IniWorker.SectionPair sectionPair = new IniWorker.SectionPair();
			TextReader streamReader = null;
			string i = null;
			string str = null;
			string[] strArrays = null;
			string str1 = this.iniFilePath;
			if (File.Exists(str1))
			{
				try
				{
					try
					{
						this.ClearAll();
						streamReader = new StreamReader(str1);
						for (i = streamReader.ReadLine(); i != null; i = streamReader.ReadLine())
						{
							if (i != "")
							{
								if (!i.StartsWith("[") || !i.EndsWith("]"))
								{
									char[] chrArray = new char[] { '=' };
									strArrays = i.Split(chrArray, 2);
									string str2 = null;
									if (str == null)
									{
										str = "ROOT";
									}
									sectionPair.Section = str;
									sectionPair.Key = strArrays[0];
									if ((int)strArrays.Length > 1)
									{
										str2 = strArrays[1];
									}
									this.keyPairs.Add(sectionPair, str2);
								}
								else
								{
									str = i.Substring(1, i.Length - 2);
								}
							}
						}
					}
					catch
					{
					}
				}
				finally
				{
					streamReader.Close();
				}
			}
		}

		public void SaveSettings(string newFilePath)
		{
			ArrayList arrayLists = new ArrayList();
			string item = "";
			string empty = string.Empty;
			foreach (IniWorker.SectionPair key in this.keyPairs.Keys)
			{
				if (arrayLists.Contains(key.Section))
				{
					continue;
				}
				arrayLists.Add(key.Section);
			}
			foreach (string arrayList in arrayLists)
			{
				empty = string.Concat(empty, "[", arrayList, "]\r\n");
				foreach (IniWorker.SectionPair sectionPair in this.keyPairs.Keys)
				{
					if (sectionPair.Section != arrayList)
					{
						continue;
					}
					item = (string)this.keyPairs[sectionPair];
					if (item != null)
					{
						item = string.Concat("=", item);
					}
					empty = string.Concat(empty, sectionPair.Key, item, "\r\n");
				}
				empty = string.Concat(empty, "\r\n");
			}
			try
			{
				TextWriter streamWriter = new StreamWriter(newFilePath);
				streamWriter.Write(string.Empty);
				streamWriter.Write(empty);
				streamWriter.Close();
			}
			catch
			{
			}
		}

		public void SaveSettings()
		{
			this.SaveSettings(this.iniFilePath);
		}

		private struct SectionPair
		{
			public string Section;

			public string Key;
		}
	}
}