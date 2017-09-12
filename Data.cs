using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $safeprojectname$
{
    public static class Data
    {
        public struct Volume
        {
            public int Value;
            public int Max;
            public double Range;
            public String Line;
        }
        public static Volume IN;
        public static Volume MSG;
        public static Volume OUT;

        public static bool Working = false;
        public static bool TickToPlay = false;
        public static bool Playng = false;

        public static String programPath = System.IO.Directory.GetCurrentDirectory();
        public static String audioPath = programPath + @"\AUDIO";
        public static String keyPath = programPath + @"\Data.ini";
        public static String iniPath = programPath + @"\Data.ini";

        public static int Weakup = 9;
        public static int Sleep = 19;

        public static int TimeElapse = 10;
        public static String TimeElapseShow = null;
        //public const int TimeFade = 1; //(dado em segundos) com auidoINranger=30 o idela foi rate=150 tempo final 4,5sec de fade

        public static String NextName = null;
    }
}
