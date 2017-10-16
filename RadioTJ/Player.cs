using System;
using System.IO;
using System.Media;
using System.Threading;
using WaveReaderDLL;

namespace RadioTJ
{
	public static class Player
	{
		public static SoundPlayer snd;

		static Player()
		{
			Player.snd = new SoundPlayer();
		}

		public static DateTime deslokTime(double doubTime)
		{
			int num = (int)(doubTime / 1);
			int num1 = (int)(doubTime % 1 * 1000);
			int num2 = num / 60;
			num %= 60;
			DateTime now = DateTime.Now;
			DateTime dateTime = now.Add(new TimeSpan(0, 0, num2, num, num1));
			return dateTime;
		}

		public static double elapseTime(DateTime End)
		{
			return DateTime.Now.Subtract(End).TotalSeconds;
		}

		public static void fade(bool mode = false)
		{
			int num = 10;
			bool flag = true;
			Player.getVolume(ref Data.IN);
			int value = Data.IN.Value;
			if (!mode)
			{
				Data.MSG.Value = 0;
				Player.setVolume(ref Data.MSG);
			}
			while (flag)
			{
				Oclock.wait(num);
				if (!mode)
				{
					value++;
					if (value > Data.IN.Max)
					{
						value = Data.IN.Max;
						flag = false;
					}
				}
				else
				{
					value--;
					if (value < 0)
					{
						value = 0;
						flag = false;
					}
				}
				Data.IN.Value = value;
				Player.setVolume(ref Data.IN);
			}
			if (mode)
			{
				Data.MSG.Value = Data.MSG.Max;
				Player.setVolume(ref Data.MSG);
			}
		}

		public static void getAllVolMixer()
		{
			Player.getVolume(ref Data.IN);
			Player.getVolume(ref Data.MSG);
			Player.getVolume(ref Data.OUT);
		}

		public static double getAudioTime(string AudioFileDir)
		{
			if (!File.Exists(AudioFileDir))
			{
				return 0;
			}
			return (new WaveReader(File.OpenRead(AudioFileDir))).TimeLength;
		}

		public static bool getVolume(ref Data.Volume Audio)
		{
			Data.Volume volume = AudioApiInterface.Get(Audio);
			if (volume.Line == null)
			{
				Audio.Value = -1;
				return false;
			}
			Audio = volume;
			Audio.Value = (int)((double)Audio.Value * (1 / Audio.Range));
			return true;
		}

		public static void noFade(bool mode = false)
		{
			if (!mode)
			{
				Data.IN.Value = Data.IN.Max;
				Data.MSG.Value = 0;
			}
			else
			{
				Data.IN.Value = 0;
				Data.MSG.Value = Data.MSG.Max;
			}
			Player.setVolume(ref Data.MSG);
			Player.setVolume(ref Data.IN);
		}

		public static void realPlay()
		{
			if (Data.Playng)
			{
				return;
			}
			Data.Playng = true;
			VoiceItem actual = ListManager.getActual();
			if (actual == null)
			{
				ListManager.setNext();
				actual = ListManager.getActual();
			}
			double time = actual.getTime();
			string str = string.Concat(Data.audioPath, actual.getFileName());
			try
			{
				Player.snd.SoundLocation = str;
				Player.snd.Load();
				Player.fade(true);
				DateTime dateTime = Player.deslokTime(time);
				Player.snd.Play();
				Player.Waiting(dateTime);
				ClockManager.resetTime();
				Data.TickToPlay = false;
				ListManager.itemSetActivit(actual, false);
			}
			catch
			{
				ListManager.removeItemBYitem(actual);
			}
			ListManager.setNext();
			Player.fade(false);
			Data.Playng = false;
		}

		public static void setAllVolMixer()
		{
			if (!Data.Playng)
			{
				Player.setVolume(ref Data.IN);
				Player.setVolume(ref Data.MSG);
				Player.setVolume(ref Data.OUT);
			}
		}

		public static bool setVolume(ref Data.Volume Audio)
		{
			Data.Volume audio = Audio;
			if (audio.Value > audio.Max)
			{
				audio.Value = audio.Max;
			}
			else if (audio.Value < 0)
			{
				audio.Value = 0;
			}
			audio.Value = (int)((double)audio.Value * audio.Range);
			audio = AudioApiInterface.Set(audio);
			if (audio.Line != null)
			{
				Audio = audio;
				return true;
			}
			Audio.Value = -1;
			return false;
		}

		public static void tryPlay()
		{
			if (!Data.Playng)
			{
				(new Thread(new ThreadStart(Player.realPlay))).Start();
			}
		}

		public static void tryPlayThis(string Name)
		{
			if (!Data.Playng)
			{
				ListManager.setNext(ListManager.getItemBYname(Name));
				Player.tryPlay();
			}
		}

		public static void Waiting(DateTime End)
		{
			int num = 0;
			while (End > DateTime.Now)
			{
				num++;
			}
		}

		public static void WaitMili(int Mile)
		{
			Player.Waiting(Player.deslokTime((double)Mile / 1000));
		}

		public static void workMode(bool Mode = false)
		{
			if (!Mode)
			{
				Data.IN.Value = 0;
				Data.MSG.Value = 0;
				Data.OUT.Value = 0;
			}
			else
			{
				Data.OUT.Value = Data.OUT.Max;
			}
			if (!Data.Playng)
			{
				Data.IN.Value = Data.IN.Max;
			}
		}
	}
}