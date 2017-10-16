using System;
using System.Runtime.InteropServices;

namespace WaveLib.AudioMixer
{
	[StructLayout(LayoutKind.Explicit)]
	public struct MIXERCONTROLDETAILS
	{
		[FieldOffset(0)]
		public uint cbStruct;

		[FieldOffset(4)]
		public uint dwControlID;

		[FieldOffset(8)]
		public uint cChannels;

		[FieldOffset(12)]
		public IntPtr hwndOwner;

		[FieldOffset(12)]
		public uint cMultipleItems;

		[FieldOffset(16)]
		public uint cbDetails;

		[FieldOffset(20)]
		public IntPtr paDetails;
	}
}