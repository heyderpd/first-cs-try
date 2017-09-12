using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveLib.AudioMixer;
using System.Runtime.InteropServices;

using System.Windows.Forms;

namespace $safeprojectname$
{
    public static class AudioApiInterface
    {
        public static Data.Volume Get(Data.Volume MixLine)
        {
            return MixerInterface(MixLine, true);
        }

        public static Data.Volume Set(Data.Volume MixLine)
        {
            return MixerInterface(MixLine, false);
        }

        public static Data.Volume MixerInterface(Data.Volume MixLine, bool ifGetMode)
        {
            bool notFound = true;

            // VAR DECLARATION
            int ERROcode = -1;
            uint dwDestination = 0;
            int cConnections = 0;
            IntPtr hMixer = IntPtr.Zero;
            MIXERCONTROLDETAILS DETAILS = new MIXERCONTROLDETAILS();
            MIXERLINE LINE = new MIXERLINE();

            //  GET hMIXER
            MixerNative.mixerOpen(out hMixer, 0, IntPtr.Zero, IntPtr.Zero, 0);

            //  CONFIG MIXERLINE
            LINE.cbStruct = (uint)Marshal.SizeOf(LINE);
            LINE.dwDestination = 0;

            //  GET MIXERLINE BY DESTINATION
            ERROcode = MixerNative.mixerGetLineInfo(hMixer, ref LINE, MIXER_GETLINEINFOF.DESTINATION);
            ERROcode = MixerNative.mixerSetControlDetails(hMixer, ref DETAILS, MIXER_SETCONTROLDETAILSFLAG.VALUE);

            // FOR INITIALIZE
            dwDestination = LINE.dwDestination;
            if (MixLine.Line == LINE.szName)
            {
                MixLine.Value = lineToDetails(LINE.dwLineID, MixLine.Value, ifGetMode);
                return MixLine;
            }

            cConnections = (int)LINE.cConnections;
            for (int i = 0; i < cConnections; i++)
            {
                //  GET MIXERLINE BY SOURCE
                LINE.dwSource = (uint)i;
                ERROcode = MixerNative.mixerGetLineInfo(hMixer, ref LINE, MIXER_GETLINEINFOF.SOURCE);

                if (MixLine.Line == LINE.szName)
                {
                    notFound = false;
                    MixLine.Value = lineToDetails(LINE.dwLineID, MixLine.Value, ifGetMode);
                    break;  // to the "for"
                }
            }
            
            if (notFound)
                MixLine.Line = null;
            return MixLine;
        }

        public static int lineToDetails(uint dwLineID, int Value, bool ifGetMode)
        {
            // VAR DECLARATION
            int ERROcode = -1;
            IntPtr hMixer = IntPtr.Zero;
            MIXERCONTROLDETAILS DETAILS = new MIXERCONTROLDETAILS();
            MIXERLINECONTROLS LINE_CONTROLS = new MIXERLINECONTROLS();

            //  CONFIG MIXERLINECONTROLS    // help -> MIXERCONTROLDETAILS=24 MIXERLINECONTROLS=24 MIXERLINE)=280 MIXERCONTROL=228 MIXERCAPS=80 MIXERCONTROLDETAILS_SIGNED=4 MIXERCONTROLDETAILS_UNSIGNED=4
            LINE_CONTROLS.cbmxctrl = (uint)Marshal.SizeOf(typeof(MIXERCONTROL));
            LINE_CONTROLS.cbStruct = (uint)Marshal.SizeOf(typeof(MIXERLINECONTROLS));
            LINE_CONTROLS.dwControlType = MIXERCONTROL_CONTROLTYPE.VOLUME;
            LINE_CONTROLS.dwLineID = dwLineID;
            LINE_CONTROLS.pamxctrl = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(MIXERCONTROL))); //Marshal.AllocCoTaskMem(mixerControlSize);

            //  GET MIXERCONTROLDETAILS
            ERROcode = MixerNative.mixerGetLineControls(hMixer, ref LINE_CONTROLS, MIXER_GETLINECONTROLSFLAG.ONEBYTYPE);

            //  GET MIXERCONTROL
            MIXERCONTROL mixerControl = (MIXERCONTROL)Marshal.PtrToStructure(LINE_CONTROLS.pamxctrl, typeof(MIXERCONTROL));

            //  CONFIG MIXERCONTROLDETAILS
            int mcDetailsUnsigned = Marshal.SizeOf(typeof(MIXERCONTROLDETAILS_UNSIGNED));
            DETAILS.cbStruct = (uint)Marshal.SizeOf(typeof(MIXERCONTROLDETAILS));
            DETAILS.dwControlID = mixerControl.dwControlID;
            DETAILS.paDetails = Marshal.AllocCoTaskMem(mcDetailsUnsigned);
            DETAILS.cChannels = 1;
            DETAILS.cbDetails = (uint)mcDetailsUnsigned;
            //  GET MIXERCONTROLDETAILS
            ERROcode = MixerNative.mixerGetControlDetails(hMixer, ref DETAILS, MIXER_GETCONTROLDETAILSFLAG.VALUE);

            MIXERCONTROLDETAILS_SIGNED mixerControlDetail;
            mixerControlDetail = (MIXERCONTROLDETAILS_SIGNED)Marshal.PtrToStructure(DETAILS.paDetails, typeof(MIXERCONTROLDETAILS_SIGNED));

            if (ifGetMode)
            {
                Value = Convert.ToInt16(100 * (Convert.ToDouble(mixerControlDetail.value) / UInt16.MaxValue));
            }
            else
            {
                mixerControlDetail.value = Convert.ToUInt16(UInt16.MaxValue * (Value / 100.00)); // Convert.ToUInt16(50 / 100 * uint.MaxValue); //Convert.ToUInt16(MixLine.Value);
                Marshal.StructureToPtr(mixerControlDetail, DETAILS.paDetails, false);

                ERROcode = MixerNative.mixerSetControlDetails(hMixer, ref DETAILS, MIXER_SETCONTROLDETAILSFLAG.VALUE);
            }

            return Value;
        }
    }
}
