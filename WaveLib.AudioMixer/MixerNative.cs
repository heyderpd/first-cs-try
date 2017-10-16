using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	public class MixerNative
	{
		public const int MMSYSERR_BASE = 0;

		public const int WAVERR_BASE = 32;

		public const int MIXER_SHORT_NAME_CHARS = 16;

		public const int MIXER_LONG_NAME_CHARS = 64;

		public const int MAXPNAMELEN = 32;

		public const int MIXERR_BASE = 1024;

		public const int CALLBACK_WINDOW = 65536;

		public const int MM_MIXM_LINE_CHANGE = 976;

		public const int MM_MIXM_CONTROL_CHANGE = 977;

		private MixerNative()
		{
		}

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerClose(IntPtr hmx);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerGetControlDetails(IntPtr hmxobj, ref MIXERCONTROLDETAILS pmxcd, MIXER_SETCONTROLDETAILSFLAG fdwDetailsmixer);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerGetControlDetails(IntPtr hmxobj, ref MIXERCONTROLDETAILS pmxcd, uint fdwDetailsmixer);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerGetControlDetails(IntPtr hmxobj, ref MIXERCONTROLDETAILS pmxcd, MIXER_GETCONTROLDETAILSFLAG fdwDetailsmixer);

		[DllImport("winmm.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int mixerGetDevCaps(int uMxId, ref MIXERCAPS pmxcaps, int cbmxcaps);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerGetID(IntPtr hmxobj, ref int mxId, MIXER_OBJECTFLAG fdwId);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerGetLineControls(IntPtr hmxobj, ref MIXERLINECONTROLS pmxlc, MIXER_GETLINECONTROLSFLAG fdwControls);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern int mixerGetLineInfo(IntPtr hmxobj, ref MIXERLINE pmxl, MIXER_GETLINEINFOF fdwInfo);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		public static extern int mixerGetNumDevs();

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerOpen(out IntPtr phmx, int pMxId, IntPtr dwCallback, IntPtr dwInstance, uint fdwOpen);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerOpen(out IntPtr phmx, IntPtr pMxId, IntPtr dwCallback, IntPtr dwInstance, MIXER_OBJECTFLAG fdwOpen);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerOpen(out IntPtr phmx, IntPtr pMxId, IntPtr dwCallback, IntPtr dwInstance, uint fdwOpen);

		[DllImport("winmm.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int mixerSetControlDetails(IntPtr hmxobj, ref MIXERCONTROLDETAILS pmxcd, MIXER_SETCONTROLDETAILSFLAG fdwDetails);

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
		public static extern int SendMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);
	}
}