using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace WaveReaderDLL
{
	public class WaveReader
	{
		private readonly static int BitsPerByte;

		private readonly static int MaxBits;

		public int BitsPerSample
		{
			get;
			private set;
		}

		public int BlockAlign
		{
			get;
			private set;
		}

		public int BytesPerSecond
		{
			get;
			private set;
		}

		public int CompressionCode
		{
			get;
			private set;
		}

		public int[][] Data
		{
			get;
			private set;
		}

		public int Frames
		{
			get;
			private set;
		}

		public int NumberOfChannels
		{
			get;
			private set;
		}

		public int SampleRate
		{
			get;
			private set;
		}

		public double TimeLength
		{
			get;
			private set;
		}

		static WaveReader()
		{
			WaveReader.BitsPerByte = 8;
			WaveReader.MaxBits = 8;
		}

		public WaveReader(Stream stream)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				binaryReader.ReadChars(4);
				binaryReader.ReadInt32();
				binaryReader.ReadChars(4);
				string str = new string(binaryReader.ReadChars(4));
				int num = binaryReader.ReadInt32();
				this.CompressionCode = binaryReader.ReadInt16();
				this.NumberOfChannels = binaryReader.ReadInt16();
				this.SampleRate = binaryReader.ReadInt32();
				this.BytesPerSecond = binaryReader.ReadInt32();
				this.BlockAlign = binaryReader.ReadInt16();
				this.BitsPerSample = binaryReader.ReadInt16();
				binaryReader.ReadChars(num - 16);
				str = new string(binaryReader.ReadChars(4));
				try
				{
					while (str.ToLower() != "data")
					{
						binaryReader.ReadChars(binaryReader.ReadInt32());
						str = new string(binaryReader.ReadChars(4));
					}
				}
				catch
				{
					throw new Exception("Input stream misses the data chunk");
				}
				num = binaryReader.ReadInt32();
				this.Frames = 8 * num / this.BitsPerSample / this.NumberOfChannels;
				this.TimeLength = (double)this.Frames / (double)this.SampleRate;
			}
		}
	}
}