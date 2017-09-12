using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ClockManager.seeTheTime();
            redrawFormValues(false);
        }
        
        public void FillListView(String[] list)
        {
            this.listView1.Clear();
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Columns.Add("Nome", -2, 0);
            foreach (String item in list)
            {
                this.listView1.Items.Add(new ListViewItem(item, 0));
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            redrawFormValues();
        }

        public void changeBar()
        {
            if (Data.Working)
                statusBar.Value = 100;
            else
                statusBar.Value = 0;
        }

        public void redrawFormValues(bool notClock = true)
        {
            if (notClock)
            {
                setForm_VolMax();
                this.boxElapse.Text = "" + Data.TimeElapse;
            }
            setForm_VolValue();
            FillListView(ListItemManager.getStringList());
            changeBar();
            this.labelElapse.Text = Data.TimeElapseShow;
            this.labelNext.Text = "Next: " + Data.NextName;
        }

        private ListViewItem FocusedItem = null;
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            FocusedItem = listView1.FocusedItem;
            this.contextMenuStrip1.Show(Cursor.Position);
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.tryPlayThis(FocusedItem.Text);
        }
        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListItemManager.Del(FocusedItem.Text);
            redrawFormValues();
        }

        public void setForm_VolValue()
        {
            Player.getAllVolMixer();
            this.labelVolIN.Text = "Vol: " + Data.IN.Value;
            this.labelVolWAV.Text = "Vol: " + Data.MSG.Value;
            this.labelVolOUT.Text = "Vol: " + Data.OUT.Value;
        }

        public void setForm_VolMax()
        {
            this.boxIN.Text = Data.IN.Max.ToString();
            this.boxWAV.Text = Data.MSG.Max.ToString();
            this.boxOUT.Text = Data.OUT.Max.ToString();
        }

        public void setData_VolMax()
        {
            try
            {
                Data.IN.Max = retA(int.Parse(this.boxIN.Text));
                Data.MSG.Max = retA(int.Parse(this.boxWAV.Text));
                Data.OUT.Max = retA(int.Parse(this.boxOUT.Text));
                Player.setAllVolMixer();
            }
            catch
            {
                setForm_VolMax();
            }
        }

        public void setElapseTime()
        {
            try
            {
                int time = Int16.Parse(this.boxElapse.Text);
                if (time < 3)
                    time = 3;
                else
                    if (time > 45)
                        time = 45;
                Data.TimeElapse = time;
            }
            catch { }
        }

        public int retA(int vol)  //retifica auio para o range correto
        {
            if (vol < 0)
                return 0;
            else
            {
                if (vol > 100)
                    return 100;
                else
                    return vol;
            }
        }

        private void boxAUDIOmodifc(object sender, EventArgs e)
        {
            setElapseTime();
            setData_VolMax();
            redrawFormValues();
        }

        private void button_default(object sender, EventArgs e)
        {
            DiscWorker.MakeDefaultIni();
            redrawFormValues();
        }

        private void button_load(object sender, EventArgs e)
        {
            DiscWorker.LoadIni();
            redrawFormValues();
        }

        private void button_save(object sender, EventArgs e)
        {
            setData_VolMax();
            DiscWorker.SaveIni();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VoiceItem Item = ListItemManager.Get(FocusedItem.Text);
            String fileName = Item.getFileName();
            String Name = FocusedItem.Text;
            String NewName = Microsoft.VisualBasic.Interaction.InputBox(fileName, "Rename", Name);
            ListItemManager.rename(Name, NewName);
            redrawFormValues();
        }

        private void statusBar_Click(object sender, EventArgs e)
        {
            Data.IN.Value = Convert.ToInt16(this.boxIN.Text);
            AudioApiInterface.Get(Data.IN);
        }
    }
}
