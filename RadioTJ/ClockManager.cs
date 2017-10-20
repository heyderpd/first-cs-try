using System;
using System.Diagnostics;

namespace RadioTJ
{
	public static class ClockManager
	{
		private static int TimeCount;

		public static bool modeNew;

		public static bool modeOld;

		private static bool ShutDown;

		static ClockManager()
		{
		}

		public static bool getNow()
		{
			Oclock.GetNow();
			if (ClockManager.ShutDown)
			{
				return false;
			}
			if (Oclock.Now.Hour == 6 && Oclock.Now.Minute > 0 && Oclock.Now.Minute < 2)
			{
				ClockManager.ShutDown = true;
				Process.Start("shutdown", "/r /f /t 180 /c \"RadioTJ: PC ira reiniciar em 3 min\"");
			}
			bool flag = false;
			bool flag1 = false;
			if (Oclock.Now.DayOfWeek != DayOfWeek.Saturday && Oclock.Now.DayOfWeek != DayOfWeek.Sunday)
			{
				flag = true;
			}
			if (Oclock.Now.Hour >= Data.Weakup && Oclock.Now.Hour < Data.Sleep)
			{
				flag1 = true;
			}
			if (flag)
			{
				return flag1;
			}
			return false;
		}

		public static void resetTime()
		{
			ClockManager.TimeCount = 0;
		}

		public static void seeTheTime()
		{
			Data.Working = ClockManager.getNow();
			if (!Data.Working)
			{
				Data.TimeElapseShow = "[DESLIGADO]";
				ClockManager.modeNew = false;
				Player.workMode(false);
			}
			else
			{
				Data.TickToPlay = ClockManager.tickToElapse();
				ClockManager.modeNew = true;
				Player.workMode(true);
				if (Data.TickToPlay && !Data.Playng)
				{
					Player.tryPlay();
				}
			}
			if (ClockManager.switToOn())
			{
				ListManager.GeraOrdem();
			}
			ClockManager.modeOld = ClockManager.modeNew;
			Player.setAllVolMixer();
		}

		public static bool switToOn()
		{
			if (ClockManager.modeNew && !ClockManager.modeOld)
			{
				return true;
			}
			return false;
		}

		public static bool tickToElapse()
		{
			string str;
			string str1;
			ClockManager.TimeCount += 300;
			int timeCount = ClockManager.TimeCount / 1000 / 60;
			int num = ClockManager.TimeCount / 1000 % 60;
			str = (num >= 10 ? string.Concat(num) : string.Concat("0", num));
			str1 = (timeCount >= 10 ? string.Concat(timeCount) : string.Concat("0", timeCount));
			Data.TimeElapseShow = string.Concat("[", str1, ":", str, "]");
			if (timeCount < Data.TimeElapse)
			{
				return false;
			}
			ClockManager.TimeCount = 0;
			return true;
		}
	}
}