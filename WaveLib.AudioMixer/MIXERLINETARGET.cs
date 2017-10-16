using System;

namespace WaveLib.AudioMixer
{
	public struct MIXERLINETARGET
	{
		public uint dwType;

		public uint dwDeviceID;

		public ushort wMid;

		public ushort wPid;

		public uint vDriverVersion;

		public string szPname;
	}
}