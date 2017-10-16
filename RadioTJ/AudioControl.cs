using System;
using System.Collections.Generic;

namespace RadioTJ
{
	public static class AudioControl
	{
		public static List<AudioControl.Volume> MIXERS;

		public static bool Msg;

		public static string msgText;

		public static List<string> Text;

		static AudioControl()
		{
			AudioControl.MIXERS = new List<AudioControl.Volume>();
			AudioControl.Msg = false;
		}

		public static int Get(string Line)
		{
			int num;
			List<AudioControl.Volume>.Enumerator enumerator = AudioControl.MIXERS.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					AudioControl.Volume current = enumerator.Current;
					if (current.Mixe.Line != Line)
					{
						continue;
					}
					num = Convert.ToInt32(current.Mixe.Value);
					return num;
				}
				return -1;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return num;
		}

		public static string Liste()
		{
			string empty = string.Empty;
			foreach (AudioControl.Volume mIXER in AudioControl.MIXERS)
			{
				object obj = empty;
				object[] line = new object[] { obj, mIXER.Mixe.Line, "=", mIXER.Mixe.Value, "\n" };
				empty = string.Concat(line);
			}
			return empty;
		}

		public static void Run()
		{
			AudioControl.MIXERS.Add(new AudioControl.Volume("RADIO_LINE_IN"));
			AudioControl.MIXERS.Add(new AudioControl.Volume("RADIO_LINE_MSG"));
			AudioControl.MIXERS.Add(new AudioControl.Volume("RADIO_LINE_OUT"));
			while (true)
			{
				if (AudioControl.Msg)
				{
					AudioControl.Msg = false;
				}
			}
		}

		public static void Set(string Line, int Vol)
		{
			AudioControl.Msg = true;
			foreach (AudioControl.Volume mIXER in AudioControl.MIXERS)
			{
				if (mIXER.Mixe.Line != Line)
				{
					continue;
				}
				mIXER.Mixe.Value = Vol;
			}
		}

		public class Volume
		{
			public Data.Volume Mixe;

			public Volume(string Line)
			{
				this.Mixe.Line = Line;
			}
		}
	}
}