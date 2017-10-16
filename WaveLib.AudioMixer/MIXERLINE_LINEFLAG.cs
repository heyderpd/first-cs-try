using System;

namespace WaveLib.AudioMixer
{
	public enum MIXERLINE_LINEFLAG : uint
	{
		ACTIVE = 1,
		DISCONNECTED = 32768,
		SOURCE = 2147483648
	}
}