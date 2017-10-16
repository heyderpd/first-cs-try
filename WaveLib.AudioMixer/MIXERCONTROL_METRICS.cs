using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MIXERCONTROL_METRICS
	{
		[FieldOffset(0)]
		public uint cSteps;

		[FieldOffset(0)]
		public uint cbCustomData;

		[FieldOffset(0)]
		public uint dwReserved1;

		[FieldOffset(4)]
		public uint dwReserved2;

		[FieldOffset(8)]
		public uint dwReserved3;

		[FieldOffset(12)]
		public uint dwReserved4;

		[FieldOffset(16)]
		public uint dwReserved5;

		[FieldOffset(20)]
		public uint dwReserved6;
	}
}