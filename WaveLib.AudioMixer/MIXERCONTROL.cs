using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MIXERCONTROL
	{
		[FieldOffset(0)]
		public uint cbStruct;

		[FieldOffset(4)]
		public uint dwControlID;

		[FieldOffset(8)]
		public uint dwControlType;

		[FieldOffset(12)]
		public uint fdwControl;

		[FieldOffset(16)]
		public uint cMultipleItems;

		[FieldOffset(20)]
		public string szShortName;

		[FieldOffset(36)]
		public string szName;

		[FieldOffset(100)]
		public MIXERCONTROL_BOUNDS Bounds;

		[FieldOffset(124)]
		public MIXERCONTROL_METRICS Metrics;
	}
}