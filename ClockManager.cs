using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $safeprojectname$
{
    public static class ClockManager
    {
        private static int TimeCount = 0;

        public static void resetTime()
        {
            TimeCount = 0;
        }

        public static bool tickToElapse()
        {
            TimeCount += 300;   //este é o tmepo do clock do form
            String SecondS, MinuteS;
            int Minute = TimeCount / 1000 / 60;
            int Second = TimeCount / 1000 % 60;
            if (Second < 10)
                SecondS = "0" + Second;
            else
                SecondS = "" + Second;
            if (Minute < 10)
                MinuteS = "0" + Minute;
            else
                MinuteS = "" + Minute;
            Data.TimeElapseShow = MinuteS + ":" + SecondS;
            if(Minute >= Data.TimeElapse)
            {
                TimeCount = 0;
                return true;
            }
            else
                return  false;
        }

        public static void seeTheTime()
        {
            //Player.setAllVolMixer();
            Data.Working = getNow();
            if (Data.Working)
            {
                Data.TickToPlay = tickToElapse();

                Player.workMode(true);

                if (Data.TickToPlay && !Data.Playng)
                    Player.tryPlay();
            }
            else
            {
                Data.TimeElapseShow = "Sleeping";

                Player.workMode(false);
            }
            Player.setAllVolMixer();
        }

        public static bool getNow()
        {
            DateTime Now = DateTime.Now;
            bool    Day = false, Hour = false;
            if (Now.DayOfWeek != DayOfWeek.Saturday && Now.DayOfWeek != DayOfWeek.Sunday)
                Day = true;
            if (Now.Hour >= Data.Weakup && Now.Hour < Data.Sleep)
                Hour = true;
            return Day && Hour;
        }
    }
}
