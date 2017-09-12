using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace $safeprojectname$
{
    public static class Player
    {
        public static SoundPlayer snd = new SoundPlayer();
        
        public static void tryPlay()    // verificar se o arquivo existe antes!
        {
            if (Data.Playng)
                return;

            //Data.TickToPlay = false;
            Data.Playng = true; 

            fade(true);
            Play((ListItemManager.getNext()).getFileName());
            fade();

            Data.Playng = false; 
        }

        public static void fade(bool mode = false) // Start Msg mode is true
        {
            int rate = (int)(5 / Data.IN.Range); //(int)((Data.TimeFade * 1000) / (100 * Data.IN.Range)); // por algum motivo de diferenca no processamento a outra maquina precisou trocar de 100 para 800 para corrigir o tempo do fade antigo:(100 * Data.IN.Range)
            bool Run = true;
            getVolume(ref Data.IN);
            int VOL = Data.IN.Value;

            if (!mode)
            {
                Data.MSG.Value = 0;
                setVolume(ref Data.MSG);
            }
            while (Run)
            {
                System.Threading.Thread.Sleep(rate);
                if (mode)
                {
                    VOL -= 1;
                    if (VOL < 0)
                    {
                        VOL = 0;
                        Run = false;
                    }
                }
                else
                {
                    VOL += 1;
                    if (VOL > Data.IN.Max)
                    {
                        VOL = Data.IN.Max;
                        Run = false;
                    }
                }
                Data.IN.Value = VOL;
                setVolume(ref Data.IN);
            }
            if (mode)
            {
                Data.MSG.Value = Data.MSG.Max;
                setVolume(ref Data.MSG);
            }
        }

        public static void workMode(bool Mode = false)
        {
            if (Mode)
                Data.OUT.Value = Data.OUT.Max;
            else
                Data.OUT.Value = 0;
            if (!Data.Playng)
                Data.IN.Value = Data.IN.Max;
            //setVolume(ref Data.OUT);
        }

        public static bool setVolume(ref Data.Volume Audio)
        {
            Data.Volume Temp = Audio;
            if (Temp.Value > Temp.Max)
                Temp.Value = Temp.Max;
            else
            {
                if (Temp.Value < 0)
                    Temp.Value = 0;
            }
            Temp.Value = (int)(Temp.Value * Temp.Range);
            Temp = AudioApiInterface.Set(Temp);
            if (Temp.Line != null)
            {
                Audio = Temp;
                return true;
            }
            else
            {
                Audio.Value = -1;
                return false;
            }
        }

        public static bool getVolume(ref Data.Volume Audio)
        {
            Data.Volume Temp = AudioApiInterface.Get(Audio);
            if (Temp.Line != null)
            {
                Audio = Temp;
                Audio.Value = (int)(Audio.Value * (1 / Audio.Range));
                return true;
            }
            else
            {
                Audio.Value = -1;
                return false;
            }
        }

        public static void Play(String AudioDiretory)
        {
            try
            {
                snd.SoundLocation = Data.audioPath + AudioDiretory;
                snd.Load();
                snd.PlaySync();

                ClockManager.resetTime();
                Data.TickToPlay = false;
            }
            catch
            {
                ListItemManager.DelByDir(AudioDiretory);
            }
            ListItemManager.goToNext();
        }

        public static void tryPlayThis(String Name)
        {
            if (!Data.Playng)
            {
                ListItemManager.setNext(Name);
                tryPlay();
            }
        }

        public static void setAllVolMixer()
        {
            setVolume(ref Data.IN);
            setVolume(ref Data.MSG);
            setVolume(ref Data.OUT);
        }

        public static void getAllVolMixer()
        {
            getVolume(ref Data.IN);
            getVolume(ref Data.MSG);
            getVolume(ref Data.OUT);
        }
    }
}
