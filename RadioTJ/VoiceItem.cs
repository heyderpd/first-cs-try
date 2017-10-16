using System;

namespace RadioTJ
{
	public class VoiceItem
	{
		private bool priori;

		public bool active;

		private double time;

		private int ID;

		private string name;

		private int posicao;

		private string fileName;

		public VoiceItem(string Name, string fileName, bool priori = false)
		{
			this.ID = this.GetHashCode();
			this.setPriori(priori);
			this.name = Name;
			this.fileName = fileName;
			this.setTime();
		}

		public string getFileName()
		{
			return this.fileName;
		}

		public string getName()
		{
			return this.name;
		}

		public int getPosicao()
		{
			return this.posicao;
		}

		public bool getPriori()
		{
			return this.priori;
		}

		public double getTime()
		{
			return this.time;
		}

		public void setName(string name)
		{
			this.name = name;
		}

		public void setPosicao(int posicao)
		{
			this.posicao = posicao;
		}

		public void setPriori(bool priori)
		{
			this.priori = priori;
		}

		public void setTime()
		{
			string str = string.Concat(Data.audioPath, this.fileName);
			this.time = Player.getAudioTime(str);
		}
	}
}