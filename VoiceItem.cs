using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $safeprojectname$
{
    class VoiceItem
    {
        private int ID = 0;
        private String name = null;
        private int prioridade = 0;
        private int posicao = 0;
        private String fileName = null;
        public VoiceItem(String Name, String fileName)
        {
            this.ID = this.GetHashCode();
            this.name = Name;
            this.fileName = fileName;
        }
        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public String getFileName()
        {
            return fileName;
        }
        public void setPosicao(int posicao)
        {
            this.posicao = posicao;
        }
        public int getPosicao()
        {
            return this.posicao;
        }
    }
}
