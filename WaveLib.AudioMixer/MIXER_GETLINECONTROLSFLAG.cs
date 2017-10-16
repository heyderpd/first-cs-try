using System;

namespace WaveLib.AudioMixer
{
	public enum MIXER_GETLINECONTROLSFLAG
	{
		ALL = 0,
		ONEBYID = 1,
		ONEBYTYPE = 2,
		QUERYMASK = 15
	}
}