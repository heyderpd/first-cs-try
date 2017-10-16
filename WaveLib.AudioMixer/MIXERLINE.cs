using System;

namespace WaveLib.AudioMixer
{
	internal struct MIXERLINE
	{
		public uint cbStruct;

		public uint dwDestination;

		public uint dwSource;

		public uint dwLineID;

		public uint fdwLine;

		public IntPtr dwUser;

		public MIXERLINE_COMPONENTTYPE dwComponentType;

		public uint cChannels;

		public uint cConnections;

		public uint cControls;

		public string szShortName;

		public string szName;

		public MIXERLINETARGET Target;
	}
}