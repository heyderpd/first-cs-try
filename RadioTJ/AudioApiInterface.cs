using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WaveLib.AudioMixer;

namespace RadioTJ
{
	public static class AudioApiInterface
	{
		public static Data.Volume Get(Data.Volume MixLine)
		{
			return AudioApiInterface.MixerInterface(MixLine, true);
		}

		public static string[] getLineNames()
		{
			string[] array;
			try
			{
				List<string> strs = new List<string>();
				int num = 0;
				IntPtr zero = IntPtr.Zero;
				MIXERLINE mIXERLINE = new MIXERLINE();
				MixerNative.mixerOpen(out zero, 0, IntPtr.Zero, IntPtr.Zero, 0);
				mIXERLINE.cbStruct = (uint)Marshal.SizeOf(mIXERLINE);
				mIXERLINE.dwDestination = 0;
				MixerNative.mixerGetLineInfo(zero, ref mIXERLINE, MIXER_GETLINEINFOF.DESTINATION);
				num = (int)mIXERLINE.cConnections;
				for (int i = 0; i < num; i++)
				{
					mIXERLINE.dwSource = (uint)i;
					MixerNative.mixerGetLineInfo(zero, ref mIXERLINE, MIXER_GETLINEINFOF.SOURCE);
					strs.Add(mIXERLINE.szName);
				}
				array = strs.ToArray();
			}
			catch
			{
				return null;
			}
			return array;
		}

		public static int lineToDetails(uint dwLineID, int Value, bool ifGetMode)
		{
			try
			{
				IntPtr zero = IntPtr.Zero;
				MIXERCONTROLDETAILS mIXERCONTROLDETAIL = new MIXERCONTROLDETAILS();
				MIXERLINECONTROLS mIXERLINECONTROL = new MIXERLINECONTROLS()
				{
					cbmxctrl = (uint)Marshal.SizeOf(typeof(MIXERCONTROL)),
					cbStruct = (uint)Marshal.SizeOf(typeof(MIXERLINECONTROLS)),
					dwControlType = MIXERCONTROL_CONTROLTYPE.VOLUME,
					dwLineID = dwLineID,
					pamxctrl = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(MIXERCONTROL)))
				};
				MixerNative.mixerGetLineControls(zero, ref mIXERLINECONTROL, MIXER_GETLINECONTROLSFLAG.ONEBYTYPE);
				MIXERCONTROL structure = (MIXERCONTROL)Marshal.PtrToStructure(mIXERLINECONTROL.pamxctrl, typeof(MIXERCONTROL));
				int num = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS_UNSIGNED));
				mIXERCONTROLDETAIL.cbStruct = (uint)Marshal.SizeOf(typeof(MIXERCONTROLDETAILS));
				mIXERCONTROLDETAIL.dwControlID = structure.dwControlID;
				mIXERCONTROLDETAIL.paDetails = Marshal.AllocCoTaskMem(num);
				mIXERCONTROLDETAIL.cChannels = 1;
				mIXERCONTROLDETAIL.cbDetails = (uint)num;
				MixerNative.mixerGetControlDetails(zero, ref mIXERCONTROLDETAIL, MIXER_GETCONTROLDETAILSFLAG.VALUE);
				MIXERCONTROLDETAILS_SIGNED mIXERCONTROLDETAILSSIGNED = (MIXERCONTROLDETAILS_SIGNED)Marshal.PtrToStructure(mIXERCONTROLDETAIL.paDetails, typeof(MIXERCONTROLDETAILS_SIGNED));
				if (!ifGetMode)
				{
					mIXERCONTROLDETAILSSIGNED.@value = Convert.ToUInt16(65535 * ((double)Value / 100));
					Marshal.StructureToPtr(mIXERCONTROLDETAILSSIGNED, mIXERCONTROLDETAIL.paDetails, false);
					MixerNative.mixerSetControlDetails(zero, ref mIXERCONTROLDETAIL, MIXER_SETCONTROLDETAILSFLAG.VALUE);
				}
				else
				{
					Value = Convert.ToInt32(100 * (Convert.ToDouble(mIXERCONTROLDETAILSSIGNED.@value) / 65535));
				}
			}
			catch
			{
			}
			return Value;
		}

		public static Data.Volume MixerInterface(Data.Volume MixLine, bool ifGetMode)
		{
			bool flag = true;
			if (MixLine.ID != 777)
			{
				flag = false;
			}
			else
			{
				try
				{
					int num = 0;
					IntPtr zero = IntPtr.Zero;
					MIXERLINE mIXERLINE = new MIXERLINE();
					MixerNative.mixerOpen(out zero, 0, IntPtr.Zero, IntPtr.Zero, 0);
					mIXERLINE.cbStruct = (uint)Marshal.SizeOf(mIXERLINE);
					mIXERLINE.dwDestination = 0;
					MixerNative.mixerGetLineInfo(zero, ref mIXERLINE, MIXER_GETLINEINFOF.DESTINATION);
					if (MixLine.Line != mIXERLINE.szName)
					{
						num = (int)mIXERLINE.cConnections;
						int num1 = 0;
						while (num1 < num)
						{
							mIXERLINE.dwSource = (uint)num1;
							MixerNative.mixerGetLineInfo(zero, ref mIXERLINE, MIXER_GETLINEINFOF.SOURCE);
							if (MixLine.Line != mIXERLINE.szName)
							{
								num1++;
							}
							else
							{
								flag = false;
								MixLine.ID = mIXERLINE.dwLineID;
								break;
							}
						}
					}
					else
					{
						flag = false;
						MixLine.ID = mIXERLINE.dwLineID;
					}
				}
				catch
				{
				}
			}
			MixLine.Value = AudioApiInterface.lineToDetails(MixLine.ID, MixLine.Value, ifGetMode);
			if (flag)
			{
				MixLine.Line = null;
			}
			return MixLine;
		}

		public static Data.Volume Set(Data.Volume MixLine)
		{
			return AudioApiInterface.MixerInterface(MixLine, false);
		}
	}
}