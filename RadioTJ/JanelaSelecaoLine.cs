using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RadioTJ
{
	public class JanelaSelecaoLine : Form
	{
		private IContainer components;

		private Button button1;

		private Button button2;

		private ComboBox comboBox1;

		private Label label1;

		public JanelaSelecaoLine()
		{
			this.InitializeComponent();
			this.fillLineNames();
		}

		public void button_Cancel(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button_OK(object sender, EventArgs e)
		{
			object selectedItem = this.comboBox1.SelectedItem;
			if (selectedItem != null)
			{
				Data.IN.Line = selectedItem.ToString();
			}
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void fillLineNames()
		{
			string[] lineNames = AudioApiInterface.getLineNames();
			this.comboBox1.Items.AddRange(lineNames);
		}

		private void InitializeComponent()
		{
			this.button1 = new Button();
			this.button2 = new Button();
			this.comboBox1 = new ComboBox();
			this.label1 = new Label();
			base.SuspendLayout();
			this.button1.Location = new Point(12, 52);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Ok";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button_OK);
			this.button2.Location = new Point(110, 52);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(91, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button_Cancel);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(12, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(189, 21);
			this.comboBox1.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Selecione a linha de entrada de áudio.";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(214, 90);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Name = "JanelaSelecaoLine";
			this.Text = "Audio Line";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}