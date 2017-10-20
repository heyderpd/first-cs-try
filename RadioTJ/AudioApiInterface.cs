using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VideoPlayerController;
using System.Windows.Forms;

namespace RadioTJ
{
	public static class AudioApiInterface
	{
        /*public static void initializeLines()
        {
            AudioManager.SetMasterVolume(100);
            AudioManager.setMasterCaptureVolume(44);
            AudioManager.SetVolumeAllCapture(77);
            AudioManager.SetVolumeAndMaxAll(Program.getPID(), 33);
        }*/

        public static string[] getLineNames()
		{
            List<string> ApplicationsNames = new List<string>();
            return ApplicationsNames.ToArray();
        }

        public static Data.Volume Get(ref Data.Volume MixLine)
        {
            switch (MixLine.ID)
            {
                case "IN":
                    MixLine.Value = (int) (AudioManager.GetVolumeAllCapture() * 100);
                    break;

                case "MSG":
                    MixLine.Value = (int) (AudioManager.GetVolumeById(Program.getPID()) * 100);
                    break;

                case "OUT":
                    MixLine.Value = (int) (AudioManager.GetMasterVolume() * 100);
                    break;

                default:
                    break;
            }
            // Console.WriteLine(MixLine.ID + " Get:" + MixLine.Value);
            return MixLine;
        }

        public static Data.Volume Set(ref Data.Volume MixLine)
		{
            switch (MixLine.ID)
            {
                case "IN":
                    AudioManager.SetVolumeAllCapture(MixLine.Value);
                    break;

                case "MSG":
                    AudioManager.SetVolumeAndMaxAll(Program.getPID(), MixLine.Value);
                    break;

                case "OUT":
                    AudioManager.SetMasterVolume(MixLine.Value);
                    break;

                default:
                    break;
            }
            // Console.WriteLine(MixLine.ID + " Set:" + MixLine.Value);
            return MixLine;
		}
	}
}