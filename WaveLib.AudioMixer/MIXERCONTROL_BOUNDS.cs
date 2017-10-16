using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MIXERCONTROL_BOUNDS
	{
		[FieldOffset(0)]
		public int lMinimum;

		[FieldOffset(4)]
		public int lMaximum;

		[FieldOffset(0)]
		public uint dwMinimum;

		[FieldOffset(4)]
		public uint dwMaximum;

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