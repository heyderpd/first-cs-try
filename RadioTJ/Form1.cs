using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RadioTJ
{
	public class Form1 : Form
	{
		private IContainer components;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem arquivoToolStripMenuItem;

		private ToolStripMenuItem sorteioToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem loadINIToolStripMenuItem;

		private ToolStripMenuItem saveINIToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem carregaNovosArquivosToolStripMenuItem;

		private ToolStripMenuItem selecionaLinhaDoFMToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem resetPadrãoToolStripMenuItem;

		private ToolStripMenuItem infoToolStripMenuItem;

		private ToolStripMenuItem ajudaToolStripMenuItem;

		private Label label_nextANDtime;

		private TrackBar trackBar_mixWAV;

		private Label label_mixWAV;

		private Label label_mixOUT;

		private TrackBar trackBar_mixOUT;

		private Label label_mixIN;

		private TrackBar trackBar_mixIN;

		public ProgressBar bar_PlayStatus;

		private TextBox text_Elapse;

		public ListView listView1;

		private System.Windows.Forms.Timer timer1;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem playToolStripMenuItem;

		private ToolStripMenuItem renomiarToolStripMenuItem;

		private ToolStripMenuItem deletarToolStripMenuItem;

		private ToolStripMenuItem prioridadeToolStripMenuItem;

		private Form1.VerticalProgressBar bar_mixIN;

		private Form1.VerticalProgressBar bar_mixWAV;

		private Form1.VerticalProgressBar bar_mixOUT;

		private bool altBarRunning;

		private int changeBarValue;

		private ListViewItem FocusedItem;
		private Label label2;
		private object thisLock = new object();

		public Form1()
		{
			this.InitializeComponent();
			this.verticalBARset(out this.bar_mixIN, this.trackBar_mixIN.Location);
			this.verticalBARset(out this.bar_mixWAV, this.trackBar_mixWAV.Location);
			this.verticalBARset(out this.bar_mixOUT, this.trackBar_mixOUT.Location);
			this.redrawForm(false);
		}

		public void altBarInPlay()
		{
			if (!Data.Playng || this.altBarRunning)
			{
				return;
			}
			(new Thread(new ThreadStart(this.REALaltBarInPlay))).Start();
		}

		private void carregaNovosArquivosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DiscWorker.LoadDisk();
		}

		private void changeBar(int Value)
		{
			if (!base.InvokeRequired)
			{
				this.bar_PlayStatus.Value = Value;
				return;
			}
			this.changeBarValue = Value;
			base.Invoke(new MethodInvoker(this.changeBarInvoke));
		}

		private void changeBarInvoke()
		{
			if (this.changeBarValue < 0)
			{
				this.changeBarValue = 0;
			}
			else if (this.changeBarValue > 100)
			{
				this.changeBarValue = 100;
			}
			this.bar_PlayStatus.Value = this.changeBarValue;
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (this.FocusedItem != null)
			{
				this.contextMenuStrip1.Close();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void FillListView()
		{
			if (ListManager.getList() == null)
			{
				return;
			}
			List<VoiceItem> list = ListManager.getList();
			this.listView1.Clear();
			this.listView1.View = View.Details;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			ListView.ColumnHeaderCollection columns = this.listView1.Columns;
			System.Drawing.Size size = this.listView1.Size;
			columns.Add("Nome", size.Width - 164, HorizontalAlignment.Left);
			this.listView1.Columns.Add("Prioridade", 80, HorizontalAlignment.Left);
			this.listView1.Columns.Add("Duração", 80, HorizontalAlignment.Left);
			ListViewItem listViewItem = null;
			int num = 0;
			Color color = Color.FromArgb(240, 240, 240);
			foreach (VoiceItem voiceItem in list)
			{
				listViewItem = new ListViewItem(voiceItem.getName());
				if (!voiceItem.getPriori())
				{
					listViewItem.SubItems.Add("Normal");
				}
				else
				{
					listViewItem.SubItems.Add("Prioridade");
				}
				ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
				int time = (int) voiceItem.getTime();
				int min = time / 60;
				int sec = time % 60;
				subItems.Add(min +":"+ sec);
				int num1 = num;
				num = num1 + 1;
				if (num1 % 2 == 1)
				{
					listViewItem.BackColor = color;
					listViewItem.UseItemStyleForSubItems = true;
				}
				this.listView1.Items.Add(listViewItem);
			}
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("RadioTJ - Win7\nHeyder Dias\n(heyderpd@gmail.com)\nVersão: 1.18.1");
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sorteioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadINIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveINIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPadrãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.carregaNovosArquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.selecionaLinhaDoFMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label_nextANDtime = new System.Windows.Forms.Label();
            this.bar_PlayStatus = new System.Windows.Forms.ProgressBar();
            this.trackBar_mixWAV = new System.Windows.Forms.TrackBar();
            this.label_mixWAV = new System.Windows.Forms.Label();
            this.label_mixOUT = new System.Windows.Forms.Label();
            this.trackBar_mixOUT = new System.Windows.Forms.TrackBar();
            this.label_mixIN = new System.Windows.Forms.Label();
            this.trackBar_mixIN = new System.Windows.Forms.TrackBar();
            this.text_Elapse = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renomiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prioridadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixWAV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixOUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixIN)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(653, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sorteioToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadINIToolStripMenuItem,
            this.saveINIToolStripMenuItem,
            this.resetPadrãoToolStripMenuItem,
            this.toolStripSeparator2,
            this.carregaNovosArquivosToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.arquivoToolStripMenuItem.Text = "Opções";
            // 
            // sorteioToolStripMenuItem
            // 
            this.sorteioToolStripMenuItem.Name = "sorteioToolStripMenuItem";
            this.sorteioToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sorteioToolStripMenuItem.Text = "Sortear ordem de áudios";
            this.sorteioToolStripMenuItem.Click += new System.EventHandler(this.sorteioToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // loadINIToolStripMenuItem
            // 
            this.loadINIToolStripMenuItem.Name = "loadINIToolStripMenuItem";
            this.loadINIToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.loadINIToolStripMenuItem.Text = "Carregar configurações";
            this.loadINIToolStripMenuItem.Click += new System.EventHandler(this.loadINIToolStripMenuItem_Click);
            // 
            // saveINIToolStripMenuItem
            // 
            this.saveINIToolStripMenuItem.Name = "saveINIToolStripMenuItem";
            this.saveINIToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.saveINIToolStripMenuItem.Text = "Salvar configurações";
            this.saveINIToolStripMenuItem.Click += new System.EventHandler(this.saveINIToolStripMenuItem_Click);
            // 
            // resetPadrãoToolStripMenuItem
            // 
            this.resetPadrãoToolStripMenuItem.Name = "resetPadrãoToolStripMenuItem";
            this.resetPadrãoToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.resetPadrãoToolStripMenuItem.Text = "Resetar configurações";
            this.resetPadrãoToolStripMenuItem.Click += new System.EventHandler(this.resetPadrãoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // carregaNovosArquivosToolStripMenuItem
            // 
            this.carregaNovosArquivosToolStripMenuItem.Name = "carregaNovosArquivosToolStripMenuItem";
            this.carregaNovosArquivosToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.carregaNovosArquivosToolStripMenuItem.Text = "Carrega todos de AUDIO/";
            this.carregaNovosArquivosToolStripMenuItem.Click += new System.EventHandler(this.carregaNovosArquivosToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // selecionaLinhaDoFMToolStripMenuItem
            // 
            this.selecionaLinhaDoFMToolStripMenuItem.Name = "selecionaLinhaDoFMToolStripMenuItem";
            this.selecionaLinhaDoFMToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.selecionaLinhaDoFMToolStripMenuItem.Text = "Seleciona linha do FM";
            this.selecionaLinhaDoFMToolStripMenuItem.Click += new System.EventHandler(this.selecionaEntradaToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.ajudaToolStripMenuItem.Text = "Sobre";
            this.ajudaToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(557, 410);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_MouseClick);
            // 
            // label_nextANDtime
            // 
            this.label_nextANDtime.AutoSize = true;
            this.label_nextANDtime.Location = new System.Drawing.Point(326, 6);
            this.label_nextANDtime.Name = "label_nextANDtime";
            this.label_nextANDtime.Size = new System.Drawing.Size(97, 13);
            this.label_nextANDtime.TabIndex = 2;
            this.label_nextANDtime.Text = "label_nextANDtime";
            // 
            // bar_PlayStatus
            // 
            this.bar_PlayStatus.Location = new System.Drawing.Point(107, 6);
            this.bar_PlayStatus.Name = "bar_PlayStatus";
            this.bar_PlayStatus.Size = new System.Drawing.Size(135, 16);
            this.bar_PlayStatus.TabIndex = 3;
            // 
            // trackBar_mixWAV
            // 
            this.trackBar_mixWAV.LargeChange = 10;
            this.trackBar_mixWAV.Location = new System.Drawing.Point(602, 187);
            this.trackBar_mixWAV.Maximum = 100;
            this.trackBar_mixWAV.Name = "trackBar_mixWAV";
            this.trackBar_mixWAV.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_mixWAV.Size = new System.Drawing.Size(45, 104);
            this.trackBar_mixWAV.TabIndex = 5;
            this.trackBar_mixWAV.ValueChanged += new System.EventHandler(this.trackBar_Changed);
            // 
            // label_mixWAV
            // 
            this.label_mixWAV.AutoSize = true;
            this.label_mixWAV.Location = new System.Drawing.Point(578, 171);
            this.label_mixWAV.Name = "label_mixWAV";
            this.label_mixWAV.Size = new System.Drawing.Size(50, 13);
            this.label_mixWAV.TabIndex = 6;
            this.label_mixWAV.Text = "WAV: 99";
            // 
            // label_mixOUT
            // 
            this.label_mixOUT.AutoSize = true;
            this.label_mixOUT.Location = new System.Drawing.Point(578, 315);
            this.label_mixOUT.Name = "label_mixOUT";
            this.label_mixOUT.Size = new System.Drawing.Size(48, 13);
            this.label_mixOUT.TabIndex = 8;
            this.label_mixOUT.Text = "OUT: 99";
            this.label_mixOUT.Click += new System.EventHandler(this.label_mixOUT_Click);
            // 
            // trackBar_mixOUT
            // 
            this.trackBar_mixOUT.LargeChange = 10;
            this.trackBar_mixOUT.Location = new System.Drawing.Point(602, 331);
            this.trackBar_mixOUT.Maximum = 100;
            this.trackBar_mixOUT.Name = "trackBar_mixOUT";
            this.trackBar_mixOUT.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_mixOUT.Size = new System.Drawing.Size(45, 104);
            this.trackBar_mixOUT.TabIndex = 7;
            this.trackBar_mixOUT.ValueChanged += new System.EventHandler(this.trackBar_Changed);
            // 
            // label_mixIN
            // 
            this.label_mixIN.AutoSize = true;
            this.label_mixIN.Location = new System.Drawing.Point(578, 31);
            this.label_mixIN.Name = "label_mixIN";
            this.label_mixIN.Size = new System.Drawing.Size(36, 13);
            this.label_mixIN.TabIndex = 10;
            this.label_mixIN.Text = "IN: 99";
            // 
            // trackBar_mixIN
            // 
            this.trackBar_mixIN.LargeChange = 10;
            this.trackBar_mixIN.Location = new System.Drawing.Point(602, 47);
            this.trackBar_mixIN.Maximum = 100;
            this.trackBar_mixIN.Name = "trackBar_mixIN";
            this.trackBar_mixIN.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_mixIN.Size = new System.Drawing.Size(45, 104);
            this.trackBar_mixIN.TabIndex = 9;
            this.trackBar_mixIN.ValueChanged += new System.EventHandler(this.trackBar_Changed);
            // 
            // text_Elapse
            // 
            this.text_Elapse.Location = new System.Drawing.Point(290, 4);
            this.text_Elapse.Name = "text_Elapse";
            this.text_Elapse.Size = new System.Drawing.Size(30, 20);
            this.text_Elapse.TabIndex = 11;
            this.text_Elapse.Text = "3";
            this.text_Elapse.Leave += new System.EventHandler(this.modificElapse);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.deletarToolStripMenuItem,
            this.renomiarToolStripMenuItem,
            this.prioridadeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.StripMenuItem_Play);
            // 
            // deletarToolStripMenuItem
            // 
            this.deletarToolStripMenuItem.Name = "deletarToolStripMenuItem";
            this.deletarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deletarToolStripMenuItem.Text = "Deletar";
            this.deletarToolStripMenuItem.Click += new System.EventHandler(this.StripMenuItem_Delete);
            // 
            // renomiarToolStripMenuItem
            // 
            this.renomiarToolStripMenuItem.Name = "renomiarToolStripMenuItem";
            this.renomiarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.renomiarToolStripMenuItem.Text = "Renomiar";
            this.renomiarToolStripMenuItem.Click += new System.EventHandler(this.StripMenuItem_Rename);
            // 
            // prioridadeToolStripMenuItem
            // 
            this.prioridadeToolStripMenuItem.Name = "prioridadeToolStripMenuItem";
            this.prioridadeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.prioridadeToolStripMenuItem.Text = "Prioridade";
            this.prioridadeToolStripMenuItem.Click += new System.EventHandler(this.StripMenuItem_Prioridade);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Timer:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 449);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bar_PlayStatus);
            this.Controls.Add(this.label_mixIN);
            this.Controls.Add(this.trackBar_mixIN);
            this.Controls.Add(this.label_mixOUT);
            this.Controls.Add(this.trackBar_mixOUT);
            this.Controls.Add(this.text_Elapse);
            this.Controls.Add(this.label_mixWAV);
            this.Controls.Add(this.trackBar_mixWAV);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label_nextANDtime);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Radio TJ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixWAV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixOUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mixIN)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void listView1_MouseClick(object sender, EventArgs e)
		{
			lock (this.thisLock)
			{
				this.FocusedItem = this.listView1.FocusedItem;
				this.contextMenuStrip1.Show(System.Windows.Forms.Cursor.Position);
			}
		}

		private void loadINIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DiscWorker.LoadIni();
			this.redrawForm(false);
		}

		private void modificElapse(object sender, EventArgs e)
		{
			int num = 3;
			try
			{
				num = int.Parse(this.text_Elapse.Text);
				if (num < 3)
				{
					num = 3;
				}
				else if (num > 60)
				{
					num = 60;
				}
				Data.TimeElapse = num;
			}
			catch
			{
			}
			this.redrawForm(false);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			Program.ShutdownProgram();
		}

		public void passDataToLabelMax()
		{
			this.label_mixIN.Text = string.Concat("  FM: ", Data.IN.Max);
			this.label_mixWAV.Text = string.Concat("MSG: ", Data.MSG.Max);
			this.label_mixOUT.Text = string.Concat("OUT: ", Data.OUT.Max);
		}

		public void passDataToTrackAndBar(bool setTrackValue = false)
		{
			Player.getAllVolMixer();
			if (!setTrackValue)
			{
				int num = this.retific(Data.IN.Max);
				int num1 = this.retific(Data.MSG.Max);
				int num2 = this.retific(Data.OUT.Max);
				this.trackBar_mixIN.Value = num;
				this.trackBar_mixWAV.Value = num1;
				this.trackBar_mixOUT.Value = num2;
			}
			this.bar_mixIN.Value = this.retific(Data.IN.Value);
			this.bar_mixWAV.Value = this.retific(Data.MSG.Value);
			this.bar_mixOUT.Value = this.retific(Data.OUT.Value);
		}

		public void passFormTrackToData()
		{
			try
			{
				Data.IN.Max = this.trackBar_mixIN.Value;
				Data.MSG.Max = this.trackBar_mixWAV.Value;
				Data.OUT.Max = this.trackBar_mixOUT.Value;
				Player.setAllVolMixer();
			}
			catch
			{
			}
		}

		public void REALaltBarInPlay()
		{
			this.altBarRunning = true;
			double time = ListManager.getActual().getTime();
			DateTime dateTime = Player.deslokTime(time);
			double num = 0;
			int num1 = 100;
			while (Data.Playng)
			{
				num = Player.elapseTime(dateTime);
				num1 = 100 + (int)(num / time * 100);
				this.changeBar(num1);
				Player.WaitMili(100);
			}
			this.altBarRunning = false;
		}

		public void redrawForm(bool CallByClock = false)
		{
			this.togleElapseMode(CallByClock);
			this.passDataToLabelMax();
			this.passDataToTrackAndBar(CallByClock);
			this.FillListView();
			this.togleBarmode();
			this.togleLabelNextMode();
		}

		private void resetPadrãoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DiscWorker.MakeDefaultIni();
			this.redrawForm(false);
		}

		public int retific(int vol)
		{
			if (vol < 0)
			{
				return 0;
			}
			if (vol > 100)
			{
				return 100;
			}
			return vol;
		}

		private void saveINIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.passFormTrackToData();
			DiscWorker.SaveIni();
		}

		private void selecionaEntradaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DiscWorker.openDialogToSelectLine();
		}

		public void setBarValue(out int Bar, int Value)
		{
			Bar = this.retific(Value);
		}

		public void setTrackValue(ref int Track, int Value)
		{
			Track = this.retific(Value);
		}

		private void sorteioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ListManager.GeraOrdem();
		}

		private void StripMenuItem_Delete(object sender, EventArgs e)
		{
			if (this.FocusedItem != null)
			{
				ListManager.removeItemBYname(this.FocusedItem.Text);
				this.redrawForm(false);
			}
		}

		private void StripMenuItem_Play(object sender, EventArgs e)
		{
			if (this.FocusedItem != null)
			{
				Player.tryPlayThis(this.FocusedItem.Text);
			}
		}

		private void StripMenuItem_Prioridade(object sender, EventArgs e)
		{
			if (this.FocusedItem != null)
			{
				ListManager.invertPrioriBYname(this.FocusedItem.Text);
				this.redrawForm(false);
			}
		}

		private void StripMenuItem_Rename(object sender, EventArgs e)
		{
			if (this.FocusedItem != null)
			{
				VoiceItem itemBYname = ListManager.getItemBYname(this.FocusedItem.Text);
				string fileName = itemBYname.getFileName();
				string text = this.FocusedItem.Text;
				string str = Interaction.InputBox(fileName, "Rename", text, -1, -1);
                if (str.Trim() != "")
				{
					ListManager.renameBYitem(itemBYname, str);
					this.redrawForm(false);
				}
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			ClockManager.seeTheTime();
			this.redrawForm(true);
			this.altBarInPlay();
		}

		public void togleBarmode()
		{
			if (!Data.Playng)
			{
				if (Data.Working)
				{
					this.changeBar(100);
					return;
				}
				this.changeBar(0);
			}
		}

		public void togleElapseMode(bool CallByClock)
		{
			if (!CallByClock)
			{
				this.text_Elapse.Text = Data.TimeElapse.ToString();
			}
		}

		public void togleLabelNextMode()
		{
			if (Data.Playng)
			{
				this.label_nextANDtime.Text = string.Concat("[Playing]: ", ListManager.getActual().getName());
				return;
			}
			if (Data.TimeElapseShow == "[DESLIGADO]") {
				this.label_nextANDtime.Text = Data.TimeElapseShow;
				return;
			}
			this.label_nextANDtime.Text = string.Concat(Data.TimeElapseShow, " próxima: ", ListManager.getActual().getName());
		}

		private void trackBar_Changed(object sender, EventArgs e)
		{
			this.passFormTrackToData();
		}

		public void verticalBARset(out Form1.VerticalProgressBar VerticalBar, Point P)
		{
			int num = 20;
			VerticalBar = new Form1.VerticalProgressBar()
			{
				Location = new Point(P.X - num, P.Y),
				Size = new System.Drawing.Size(20, 100),
				TabIndex = 4,
				Value = 50
			};
			base.Controls.Add(VerticalBar);
		}

		public class VerticalProgressBar : ProgressBar
		{
			protected override System.Windows.Forms.CreateParams CreateParams
			{
				get
				{
					System.Windows.Forms.CreateParams createParams = base.CreateParams;
					System.Windows.Forms.CreateParams style = createParams;
					style.Style = style.Style | 4;
					return createParams;
				}
			}

			public VerticalProgressBar()
			{
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

        private void label_mixOUT_Click(object sender, EventArgs e)
        {

        }
    }
}