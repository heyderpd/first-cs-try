using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MIXERLINECONTROLS
	{
		[FieldOffset(0)]
		public uint cbStruct;

		[FieldOffset(4)]
		public uint dwLineID;

		[FieldOffset(8)]
		public uint dwControlID;

		[FieldOffset(8)]
		public MIXERCONTROL_CONTROLTYPE dwControlType;

		[FieldOffset(12)]
		public uint cControls;

		[FieldOffset(16)]
		public uint cbmxctrl;

		[FieldOffset(20)]
		public IntPtr pamxctrl;
	}
}