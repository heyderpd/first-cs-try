using System;

namespace WaveLib.AudioMixer
{
	public struct MIXERCAPS
	{
		public short wMid;

		public short wPid;

		public int vDriverVersion;

		public string szPname;

		public int fdwSupport;

		public int cDestinations;
	}
}