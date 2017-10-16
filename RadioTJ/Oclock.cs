using System;
using System.Threading;

namespace RadioTJ
{
	public static class Oclock
	{
		public static DateTime Now;

		static Oclock()
		{
			Oclock.Now = DateTime.Now;
		}

		public static int getMiles()
		{
			Oclock.GetNow();
			return Oclock.Now.Millisecond;
		}

		public static void GetNow()
		{
			Oclock.Now = DateTime.Now;
		}

		public static long getticks()
		{
			Oclock.GetNow();
			return Oclock.Now.Ticks;
		}

		public static int Randomize()
		{
			string str = Oclock.getticks().ToString();
			str = str.Substring(str.Length - 2, 2);
			int num = 0;
			try
			{
				num = short.Parse(str);
			}
			catch
			{
			}
			return num;
		}

		public static void wait(int Mili)
		{
			Thread.Sleep(Mili);
		}
	}
}