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
			this.listView1.Columns.Add("Tempo (s)", 80, HorizontalAlignment.Left);
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
				double time = voiceItem.getTime();
				subItems.Add(time.ToString());
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
			MessageBox.Show("Programa: RadioTJ\nCriado por: Heyder Pestana Dias\nContato: heyderpd@gmail.com\nVersão: 1.16.0\nUltima Atualização: 07/02/14");
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new MenuStrip();
			this.arquivoToolStripMenuItem = new ToolStripMenuItem();
			this.sorteioToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.loadINIToolStripMenuItem = new ToolStripMenuItem();
			this.saveINIToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.carregaNovosArquivosToolStripMenuItem = new ToolStripMenuItem();
			this.selecionaLinhaDoFMToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.resetPadrãoToolStripMenuItem = new ToolStripMenuItem();
			this.infoToolStripMenuItem = new ToolStripMenuItem();
			this.ajudaToolStripMenuItem = new ToolStripMenuItem();
			this.listView1 = new ListView();
			this.label_nextANDtime = new Label();
			this.bar_PlayStatus = new ProgressBar();
			this.trackBar_mixWAV = new TrackBar();
			this.label_mixWAV = new Label();
			this.label_mixOUT = new Label();
			this.trackBar_mixOUT = new TrackBar();
			this.label_mixIN = new Label();
			this.trackBar_mixIN = new TrackBar();
			this.text_Elapse = new TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.playToolStripMenuItem = new ToolStripMenuItem();
			this.deletarToolStripMenuItem = new ToolStripMenuItem();
			this.renomiarToolStripMenuItem = new ToolStripMenuItem();
			this.prioridadeToolStripMenuItem = new ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			((ISupportInitialize)this.trackBar_mixWAV).BeginInit();
			((ISupportInitialize)this.trackBar_mixOUT).BeginInit();
			((ISupportInitialize)this.trackBar_mixIN).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			// ToolStripItemCollection items = this.menuStrip1.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.arquivoToolStripMenuItem, this.infoToolStripMenuItem };
            this.menuStrip1.Items.AddRange(toolStripItemArray);
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(571, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// ToolStripItemCollection dropDownItems = this.arquivoToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.sorteioToolStripMenuItem, this.toolStripSeparator1, this.loadINIToolStripMenuItem, this.saveINIToolStripMenuItem, this.toolStripSeparator2, this.carregaNovosArquivosToolStripMenuItem, this.selecionaLinhaDoFMToolStripMenuItem, this.toolStripSeparator3, this.resetPadrãoToolStripMenuItem };
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(toolStripItemArray1);
			this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
			this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.arquivoToolStripMenuItem.Text = "Arquivo";
			this.sorteioToolStripMenuItem.Name = "sorteioToolStripMenuItem";
			this.sorteioToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.sorteioToolStripMenuItem.Text = "Sorteio";
			this.sorteioToolStripMenuItem.Click += new EventHandler(this.sorteioToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
			this.loadINIToolStripMenuItem.Name = "loadINIToolStripMenuItem";
			this.loadINIToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.loadINIToolStripMenuItem.Text = "Load INI";
			this.loadINIToolStripMenuItem.Click += new EventHandler(this.loadINIToolStripMenuItem_Click);
			this.saveINIToolStripMenuItem.Name = "saveINIToolStripMenuItem";
			this.saveINIToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.saveINIToolStripMenuItem.Text = "Save INI";
			this.saveINIToolStripMenuItem.Click += new EventHandler(this.saveINIToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
			this.carregaNovosArquivosToolStripMenuItem.Name = "carregaNovosArquivosToolStripMenuItem";
			this.carregaNovosArquivosToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.carregaNovosArquivosToolStripMenuItem.Text = "Carrega novos arquivos";
			this.carregaNovosArquivosToolStripMenuItem.Click += new EventHandler(this.carregaNovosArquivosToolStripMenuItem_Click);
			this.selecionaLinhaDoFMToolStripMenuItem.Name = "selecionaLinhaDoFMToolStripMenuItem";
			this.selecionaLinhaDoFMToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.selecionaLinhaDoFMToolStripMenuItem.Text = "Seleciona linha do FM";
			this.selecionaLinhaDoFMToolStripMenuItem.Click += new EventHandler(this.selecionaEntradaToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(195, 6);
			this.resetPadrãoToolStripMenuItem.Name = "resetPadrãoToolStripMenuItem";
			this.resetPadrãoToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.resetPadrãoToolStripMenuItem.Text = "Reset Padrão";
			this.resetPadrãoToolStripMenuItem.Click += new EventHandler(this.resetPadrãoToolStripMenuItem_Click);
			this.infoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.ajudaToolStripMenuItem });
			this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
			this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.infoToolStripMenuItem.Text = "Info";
			this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
			this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
			this.ajudaToolStripMenuItem.Text = "Sobre";
			this.ajudaToolStripMenuItem.Click += new EventHandler(this.helpToolStripMenuItem_Click);
			this.listView1.Location = new Point(12, 27);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(488, 363);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.Click += new EventHandler(this.listView1_MouseClick);
			this.label_nextANDtime.AutoSize = true;
			this.label_nextANDtime.Location = new Point(370, 9);
			this.label_nextANDtime.Name = "label_nextANDtime";
			this.label_nextANDtime.Size = new System.Drawing.Size(97, 13);
			this.label_nextANDtime.TabIndex = 2;
			this.label_nextANDtime.Text = "label_nextANDtime";
			this.bar_PlayStatus.Location = new Point(175, 3);
			this.bar_PlayStatus.Name = "bar_PlayStatus";
			this.bar_PlayStatus.Size = new System.Drawing.Size(153, 23);
			this.bar_PlayStatus.TabIndex = 3;
			this.trackBar_mixWAV.LargeChange = 10;
			this.trackBar_mixWAV.Location = new Point(526, 170);
			this.trackBar_mixWAV.Maximum = 100;
			this.trackBar_mixWAV.Name = "trackBar_mixWAV";
			this.trackBar_mixWAV.Orientation = Orientation.Vertical;
			this.trackBar_mixWAV.Size = new System.Drawing.Size(45, 104);
			this.trackBar_mixWAV.TabIndex = 5;
			this.trackBar_mixWAV.ValueChanged += new EventHandler(this.trackBar_Changed);
			this.label_mixWAV.AutoSize = true;
			this.label_mixWAV.Location = new Point(509, 154);
			this.label_mixWAV.Name = "label_mixWAV";
			this.label_mixWAV.Size = new System.Drawing.Size(50, 13);
			this.label_mixWAV.TabIndex = 6;
			this.label_mixWAV.Text = "WAV: 99";
			this.label_mixOUT.AutoSize = true;
			this.label_mixOUT.Location = new Point(509, 270);
			this.label_mixOUT.Name = "label_mixOUT";
			this.label_mixOUT.Size = new System.Drawing.Size(48, 13);
			this.label_mixOUT.TabIndex = 8;
			this.label_mixOUT.Text = "OUT: 99";
			this.trackBar_mixOUT.LargeChange = 10;
			this.trackBar_mixOUT.Location = new Point(526, 286);
			this.trackBar_mixOUT.Maximum = 100;
			this.trackBar_mixOUT.Name = "trackBar_mixOUT";
			this.trackBar_mixOUT.Orientation = Orientation.Vertical;
			this.trackBar_mixOUT.Size = new System.Drawing.Size(45, 104);
			this.trackBar_mixOUT.TabIndex = 7;
			this.trackBar_mixOUT.ValueChanged += new EventHandler(this.trackBar_Changed);
			this.label_mixIN.AutoSize = true;
			this.label_mixIN.Location = new Point(509, 31);
			this.label_mixIN.Name = "label_mixIN";
			this.label_mixIN.Size = new System.Drawing.Size(36, 13);
			this.label_mixIN.TabIndex = 10;
			this.label_mixIN.Text = "IN: 99";
			this.trackBar_mixIN.LargeChange = 10;
			this.trackBar_mixIN.Location = new Point(526, 47);
			this.trackBar_mixIN.Maximum = 100;
			this.trackBar_mixIN.Name = "trackBar_mixIN";
			this.trackBar_mixIN.Orientation = Orientation.Vertical;
			this.trackBar_mixIN.Size = new System.Drawing.Size(45, 104);
			this.trackBar_mixIN.TabIndex = 9;
			this.trackBar_mixIN.ValueChanged += new EventHandler(this.trackBar_Changed);
			this.text_Elapse.Location = new Point(334, 6);
			this.text_Elapse.Name = "text_Elapse";
			this.text_Elapse.Size = new System.Drawing.Size(30, 20);
			this.text_Elapse.TabIndex = 11;
			this.text_Elapse.Text = "3";
			this.text_Elapse.Leave += new EventHandler(this.modificElapse);
			this.timer1.Enabled = true;
			this.timer1.Interval = 300;
			this.timer1.Tick += new EventHandler(this.timer_Tick);
			// ToolStripItemCollection toolStripItemCollections = this.contextMenuStrip1.Items;
			ToolStripItem[] toolStripItemArray2 = new ToolStripItem[] { this.playToolStripMenuItem, this.deletarToolStripMenuItem, this.renomiarToolStripMenuItem, this.prioridadeToolStripMenuItem };
            this.contextMenuStrip1.Items.AddRange(toolStripItemArray2);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(129, 92);
			this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
			this.playToolStripMenuItem.Name = "playToolStripMenuItem";
			this.playToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.playToolStripMenuItem.Text = "Play";
			this.playToolStripMenuItem.Click += new EventHandler(this.StripMenuItem_Play);
			this.deletarToolStripMenuItem.Name = "deletarToolStripMenuItem";
			this.deletarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.deletarToolStripMenuItem.Text = "Deletar";
			this.deletarToolStripMenuItem.Click += new EventHandler(this.StripMenuItem_Delete);
			this.renomiarToolStripMenuItem.Name = "renomiarToolStripMenuItem";
			this.renomiarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.renomiarToolStripMenuItem.Text = "Renomiar";
			this.renomiarToolStripMenuItem.Click += new EventHandler(this.StripMenuItem_Rename);
			this.prioridadeToolStripMenuItem.Name = "prioridadeToolStripMenuItem";
			this.prioridadeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.prioridadeToolStripMenuItem.Text = "Prioridade";
			this.prioridadeToolStripMenuItem.Click += new EventHandler(this.StripMenuItem_Prioridade);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(571, 403);
			base.Controls.Add(this.bar_PlayStatus);
			base.Controls.Add(this.label_mixIN);
			base.Controls.Add(this.trackBar_mixIN);
			base.Controls.Add(this.label_mixOUT);
			base.Controls.Add(this.trackBar_mixOUT);
			base.Controls.Add(this.text_Elapse);
			base.Controls.Add(this.label_mixWAV);
			base.Controls.Add(this.trackBar_mixWAV);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.label_nextANDtime);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "Form1";
			this.Text = "Radio TJ por: Heyder P. Dias";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((ISupportInitialize)this.trackBar_mixWAV).EndInit();
			((ISupportInitialize)this.trackBar_mixOUT).EndInit();
			((ISupportInitialize)this.trackBar_mixIN).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
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
			this.label_mixIN.Text = string.Concat("FM: ", Data.IN.Max);
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
				ListManager.renameBYitem(itemBYname, str);
				this.redrawForm(false);
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
				this.label_nextANDtime.Text = string.Concat("Playing: ", ListManager.getActual().getName());
				return;
			}
			this.label_nextANDtime.Text = string.Concat(Data.TimeElapseShow, " : ", ListManager.getActual().getName());
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
	}
}