namespace $safeprojectname$
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxIN = new System.Windows.Forms.TextBox();
            this.labelIN = new System.Windows.Forms.Label();
            this.labelVolIN = new System.Windows.Forms.Label();
            this.labelVolWAV = new System.Windows.Forms.Label();
            this.labelWAV = new System.Windows.Forms.Label();
            this.boxWAV = new System.Windows.Forms.TextBox();
            this.labelVolOUT = new System.Windows.Forms.Label();
            this.labelOUT = new System.Windows.Forms.Label();
            this.boxOUT = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.ProgressBar();
            this.labelElapse = new System.Windows.Forms.Label();
            this.labelNext = new System.Windows.Forms.Label();
            this.boxElapse = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(12, 26);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(452, 224);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(473, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_save);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 70);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // boxIN
            // 
            this.boxIN.Location = new System.Drawing.Point(473, 65);
            this.boxIN.Name = "boxIN";
            this.boxIN.Size = new System.Drawing.Size(51, 20);
            this.boxIN.TabIndex = 2;
            this.boxIN.Leave += new System.EventHandler(this.boxAUDIOmodifc);
            // 
            // labelIN
            // 
            this.labelIN.AutoSize = true;
            this.labelIN.Location = new System.Drawing.Point(470, 49);
            this.labelIN.Name = "labelIN";
            this.labelIN.Size = new System.Drawing.Size(18, 13);
            this.labelIN.TabIndex = 3;
            this.labelIN.Text = "IN";
            // 
            // labelVolIN
            // 
            this.labelVolIN.AutoSize = true;
            this.labelVolIN.Location = new System.Drawing.Point(530, 68);
            this.labelVolIN.Name = "labelVolIN";
            this.labelVolIN.Size = new System.Drawing.Size(35, 13);
            this.labelVolIN.TabIndex = 4;
            this.labelVolIN.Text = "Vol: X";
            // 
            // labelVolWAV
            // 
            this.labelVolWAV.AutoSize = true;
            this.labelVolWAV.Location = new System.Drawing.Point(530, 107);
            this.labelVolWAV.Name = "labelVolWAV";
            this.labelVolWAV.Size = new System.Drawing.Size(35, 13);
            this.labelVolWAV.TabIndex = 7;
            this.labelVolWAV.Text = "Vol: X";
            // 
            // labelWAV
            // 
            this.labelWAV.AutoSize = true;
            this.labelWAV.Location = new System.Drawing.Point(470, 88);
            this.labelWAV.Name = "labelWAV";
            this.labelWAV.Size = new System.Drawing.Size(32, 13);
            this.labelWAV.TabIndex = 6;
            this.labelWAV.Text = "WAV";
            // 
            // boxWAV
            // 
            this.boxWAV.Location = new System.Drawing.Point(473, 104);
            this.boxWAV.Name = "boxWAV";
            this.boxWAV.Size = new System.Drawing.Size(51, 20);
            this.boxWAV.TabIndex = 5;
            this.boxWAV.Leave += new System.EventHandler(this.boxAUDIOmodifc);
            // 
            // labelVolOUT
            // 
            this.labelVolOUT.AutoSize = true;
            this.labelVolOUT.Location = new System.Drawing.Point(530, 146);
            this.labelVolOUT.Name = "labelVolOUT";
            this.labelVolOUT.Size = new System.Drawing.Size(35, 13);
            this.labelVolOUT.TabIndex = 10;
            this.labelVolOUT.Text = "Vol: X";
            // 
            // labelOUT
            // 
            this.labelOUT.AutoSize = true;
            this.labelOUT.Location = new System.Drawing.Point(470, 127);
            this.labelOUT.Name = "labelOUT";
            this.labelOUT.Size = new System.Drawing.Size(30, 13);
            this.labelOUT.TabIndex = 9;
            this.labelOUT.Text = "OUT";
            // 
            // boxOUT
            // 
            this.boxOUT.Location = new System.Drawing.Point(473, 143);
            this.boxOUT.Name = "boxOUT";
            this.boxOUT.Size = new System.Drawing.Size(51, 20);
            this.boxOUT.TabIndex = 8;
            this.boxOUT.Leave += new System.EventHandler(this.boxAUDIOmodifc);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(473, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Load Default";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_default);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(473, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Load";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button_load);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(473, 26);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(97, 20);
            this.statusBar.Step = 100;
            this.statusBar.TabIndex = 13;
            this.statusBar.Value = 100;
            this.statusBar.Click += new System.EventHandler(this.statusBar_Click);
            // 
            // labelElapse
            // 
            this.labelElapse.AutoSize = true;
            this.labelElapse.Location = new System.Drawing.Point(530, 3);
            this.labelElapse.Name = "labelElapse";
            this.labelElapse.Size = new System.Drawing.Size(33, 13);
            this.labelElapse.TabIndex = 14;
            this.labelElapse.Text = "X min";
            // 
            // labelNext
            // 
            this.labelNext.AutoSize = true;
            this.labelNext.Location = new System.Drawing.Point(12, 9);
            this.labelNext.Name = "labelNext";
            this.labelNext.Size = new System.Drawing.Size(42, 13);
            this.labelNext.TabIndex = 15;
            this.labelNext.Text = "Next: X";
            // 
            // boxElapse
            // 
            this.boxElapse.Location = new System.Drawing.Point(473, 0);
            this.boxElapse.Name = "boxElapse";
            this.boxElapse.Size = new System.Drawing.Size(51, 20);
            this.boxElapse.TabIndex = 16;
            this.boxElapse.Text = "time";
            this.boxElapse.Leave += new System.EventHandler(this.boxAUDIOmodifc);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 262);
            this.Controls.Add(this.boxElapse);
            this.Controls.Add(this.labelNext);
            this.Controls.Add(this.labelElapse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.boxWAV);
            this.Controls.Add(this.labelIN);
            this.Controls.Add(this.labelVolOUT);
            this.Controls.Add(this.labelVolIN);
            this.Controls.Add(this.labelOUT);
            this.Controls.Add(this.labelVolWAV);
            this.Controls.Add(this.boxOUT);
            this.Controls.Add(this.labelWAV);
            this.Controls.Add(this.boxIN);
            this.Name = "Form1";
            this.Text = "RADIO MSG                                                                        " +
                "               por: Heyder P. Dias";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TextBox boxIN;
        private System.Windows.Forms.Label labelIN;
        private System.Windows.Forms.Label labelVolIN;
        private System.Windows.Forms.Label labelVolWAV;
        private System.Windows.Forms.Label labelWAV;
        private System.Windows.Forms.TextBox boxWAV;
        private System.Windows.Forms.Label labelVolOUT;
        private System.Windows.Forms.Label labelOUT;
        private System.Windows.Forms.TextBox boxOUT;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar statusBar;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.Label labelElapse;
        private System.Windows.Forms.Label labelNext;
        private System.Windows.Forms.TextBox boxElapse;
    }
}

