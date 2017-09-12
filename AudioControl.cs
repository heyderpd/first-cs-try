using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace $safeprojectname$
{
    public static class AudioControl
    {
        public class Volume
        {
            public Data.Volume Mixe;
            public Volume(String Line)
            {
                Mixe.Line = Line;
            }
        }

        public static List<Volume> MIXERS = new List<Volume>();

        public static bool Msg = false;
        public static String msgText;
        public static List<String> Text;

        public static void Run()
        {
            MIXERS.Add(new Volume("RADIO_LINE_IN"));
            MIXERS.Add(new Volume("RADIO_LINE_MSG"));
            MIXERS.Add(new Volume("RADIO_LINE_OUT"));

            while(true)
            {
                if(Msg)
                {
                    Msg = false;
                    //MessageBox.Show(Liste());
                }
            }
        }
        public static void Set(String Line, int Vol)
        {
            Msg = true;
            using (var enumerator = MIXERS.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var Obj = enumerator.Current;
                    if (Obj.Mixe.Line == Line)
                    {
                        Obj.Mixe.Value = Vol;
                    }
                }
            }
        }
        public static int Get(String Line)
        {
            foreach (var Obj in MIXERS)
            {
                if (Obj.Mixe.Line == Line)
                {
                    return Obj.Mixe.Value;
                }
            }
            return -1;
        }
        public static String Liste()
        {
            String Out = String.Empty;
            foreach (var Obj in MIXERS)
            {
                Out += Obj.Mixe.Line + "=" + Obj.Mixe.Value + "\n";
            }
            return Out;
        }
    }
}
