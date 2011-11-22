namespace AnalizatorFalkowy
{
    partial class FrmMain
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
            this.trackBarOscylogram = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otworzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zakończToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cWTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liczToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skalaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liniowaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logarytmicznaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawMindBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dB40ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dB50ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dB60ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dB70ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.dB80ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogPlikWave = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxOscylogram = new System.Windows.Forms.PictureBox();
            this.hScrollOscylogram = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pbDefX = new System.Windows.Forms.PictureBox();
            this.pbDefY = new System.Windows.Forms.PictureBox();
            this.pbSpektrogram = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbSkalaLogarytmiczna = new System.Windows.Forms.CheckBox();
            this.pbSkalaSpektrX = new System.Windows.Forms.PictureBox();
            this.pbSkalaSpektrY = new System.Windows.Forms.PictureBox();
            this.pbFalka = new System.Windows.Forms.PictureBox();
            this.numA = new System.Windows.Forms.NumericUpDown();
            this.hScrollSpektrogram = new System.Windows.Forms.HScrollBar();
            this.pnLegendaSp = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOscylogram)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOscylogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpektrogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSkalaSpektrX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSkalaSpektrY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFalka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarOscylogram
            // 
            this.trackBarOscylogram.Enabled = false;
            this.trackBarOscylogram.Location = new System.Drawing.Point(213, 35);
            this.trackBarOscylogram.Maximum = 0;
            this.trackBarOscylogram.MaximumSize = new System.Drawing.Size(25, 200);
            this.trackBarOscylogram.Name = "trackBarOscylogram";
            this.trackBarOscylogram.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarOscylogram.Size = new System.Drawing.Size(45, 143);
            this.trackBarOscylogram.TabIndex = 2;
            this.trackBarOscylogram.Scroll += new System.EventHandler(this.trackBarOscylogram_Scroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.cWTToolStripMenuItem,
            this.skalaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(976, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otworzToolStripMenuItem,
            this.zakończToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // otworzToolStripMenuItem
            // 
            this.otworzToolStripMenuItem.Name = "otworzToolStripMenuItem";
            this.otworzToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.otworzToolStripMenuItem.Text = "Otwórz";
            this.otworzToolStripMenuItem.Click += new System.EventHandler(this.otworzToolStripMenuItem_Click);
            // 
            // zakończToolStripMenuItem
            // 
            this.zakończToolStripMenuItem.Name = "zakończToolStripMenuItem";
            this.zakończToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zakończToolStripMenuItem.Text = "Zakończ";
            this.zakończToolStripMenuItem.Click += new System.EventHandler(this.zakończToolStripMenuItem_Click);
            // 
            // cWTToolStripMenuItem
            // 
            this.cWTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liczToolStripMenuItem,
            this.ustawToolStripMenuItem});
            this.cWTToolStripMenuItem.Name = "cWTToolStripMenuItem";
            this.cWTToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.cWTToolStripMenuItem.Text = "CWT";
            // 
            // liczToolStripMenuItem
            // 
            this.liczToolStripMenuItem.Enabled = false;
            this.liczToolStripMenuItem.Name = "liczToolStripMenuItem";
            this.liczToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.liczToolStripMenuItem.Text = "Licz";
            this.liczToolStripMenuItem.Click += new System.EventHandler(this.liczToolStripMenuItem_Click);
            // 
            // ustawToolStripMenuItem
            // 
            this.ustawToolStripMenuItem.Name = "ustawToolStripMenuItem";
            this.ustawToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ustawToolStripMenuItem.Text = "Ustaw";
            this.ustawToolStripMenuItem.Click += new System.EventHandler(this.ustawToolStripMenuItem_Click);
            // 
            // skalaToolStripMenuItem
            // 
            this.skalaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liniowaToolStripMenuItem,
            this.logarytmicznaToolStripMenuItem});
            this.skalaToolStripMenuItem.Enabled = false;
            this.skalaToolStripMenuItem.Name = "skalaToolStripMenuItem";
            this.skalaToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.skalaToolStripMenuItem.Text = "Skala";
            // 
            // liniowaToolStripMenuItem
            // 
            this.liniowaToolStripMenuItem.Name = "liniowaToolStripMenuItem";
            this.liniowaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.liniowaToolStripMenuItem.Text = "Liniowa";
            this.liniowaToolStripMenuItem.Click += new System.EventHandler(this.liniowaToolStripMenuItem_Click);
            // 
            // logarytmicznaToolStripMenuItem
            // 
            this.logarytmicznaToolStripMenuItem.Checked = true;
            this.logarytmicznaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logarytmicznaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ustawMindBToolStripMenuItem});
            this.logarytmicznaToolStripMenuItem.Name = "logarytmicznaToolStripMenuItem";
            this.logarytmicznaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logarytmicznaToolStripMenuItem.Text = "Logarytmiczna";
            this.logarytmicznaToolStripMenuItem.Click += new System.EventHandler(this.logarytmicznaToolStripMenuItem_Click);
            // 
            // ustawMindBToolStripMenuItem
            // 
            this.ustawMindBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dB40ToolStripMenuItem,
            this.dB50ToolStripMenuItem1,
            this.dB60ToolStripMenuItem2,
            this.dB70ToolStripMenuItem3,
            this.dB80ToolStripMenuItem4});
            this.ustawMindBToolStripMenuItem.Name = "ustawMindBToolStripMenuItem";
            this.ustawMindBToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.ustawMindBToolStripMenuItem.Text = "Ustaw Minimum dB";
            this.ustawMindBToolStripMenuItem.Click += new System.EventHandler(this.ustawToolStripMenuItem1_Click);
            // 
            // dB40ToolStripMenuItem
            // 
            this.dB40ToolStripMenuItem.Name = "dB40ToolStripMenuItem";
            this.dB40ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dB40ToolStripMenuItem.Text = "-40 dB";
            this.dB40ToolStripMenuItem.Click += new System.EventHandler(this.dBToolStripMenuItem_Click);
            // 
            // dB50ToolStripMenuItem1
            // 
            this.dB50ToolStripMenuItem1.Name = "dB50ToolStripMenuItem1";
            this.dB50ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.dB50ToolStripMenuItem1.Text = "-50 dB";
            this.dB50ToolStripMenuItem1.Click += new System.EventHandler(this.dBToolStripMenuItem1_Click);
            // 
            // dB60ToolStripMenuItem2
            // 
            this.dB60ToolStripMenuItem2.Checked = true;
            this.dB60ToolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dB60ToolStripMenuItem2.Name = "dB60ToolStripMenuItem2";
            this.dB60ToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.dB60ToolStripMenuItem2.Text = "-60 dB";
            this.dB60ToolStripMenuItem2.Click += new System.EventHandler(this.dBToolStripMenuItem2_Click);
            // 
            // dB70ToolStripMenuItem3
            // 
            this.dB70ToolStripMenuItem3.Name = "dB70ToolStripMenuItem3";
            this.dB70ToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.dB70ToolStripMenuItem3.Text = "-70 dB";
            this.dB70ToolStripMenuItem3.Click += new System.EventHandler(this.dBToolStripMenuItem3_Click);
            // 
            // dB80ToolStripMenuItem4
            // 
            this.dB80ToolStripMenuItem4.Name = "dB80ToolStripMenuItem4";
            this.dB80ToolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.dB80ToolStripMenuItem4.Text = "-80 dB";
            this.dB80ToolStripMenuItem4.Click += new System.EventHandler(this.dBToolStripMenuItem4_Click);
            // 
            // openFileDialogPlikWave
            // 
            this.openFileDialogPlikWave.Filter = "Plik Wave|*wav;*.wave|Wszystkie plik|*.*";
            // 
            // pictureBoxOscylogram
            // 
            this.pictureBoxOscylogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxOscylogram.BackColor = System.Drawing.Color.White;
            this.pictureBoxOscylogram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxOscylogram.Location = new System.Drawing.Point(244, 35);
            this.pictureBoxOscylogram.Name = "pictureBoxOscylogram";
            this.pictureBoxOscylogram.Size = new System.Drawing.Size(682, 147);
            this.pictureBoxOscylogram.TabIndex = 0;
            this.pictureBoxOscylogram.TabStop = false;
            // 
            // hScrollOscylogram
            // 
            this.hScrollOscylogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollOscylogram.Enabled = false;
            this.hScrollOscylogram.Location = new System.Drawing.Point(244, 213);
            this.hScrollOscylogram.Name = "hScrollOscylogram";
            this.hScrollOscylogram.Size = new System.Drawing.Size(682, 17);
            this.hScrollOscylogram.TabIndex = 4;
            this.hScrollOscylogram.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // pbDefX
            // 
            this.pbDefX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDefX.Location = new System.Drawing.Point(224, 186);
            this.pbDefX.Name = "pbDefX";
            this.pbDefX.Size = new System.Drawing.Size(715, 24);
            this.pbDefX.TabIndex = 6;
            this.pbDefX.TabStop = false;
            // 
            // pbDefY
            // 
            this.pbDefY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDefY.Location = new System.Drawing.Point(932, 27);
            this.pbDefY.Name = "pbDefY";
            this.pbDefY.Size = new System.Drawing.Size(44, 161);
            this.pbDefY.TabIndex = 7;
            this.pbDefY.TabStop = false;
            // 
            // pbSpektrogram
            // 
            this.pbSpektrogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSpektrogram.BackColor = System.Drawing.Color.White;
            this.pbSpektrogram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbSpektrogram.Location = new System.Drawing.Point(244, 253);
            this.pbSpektrogram.Name = "pbSpektrogram";
            this.pbSpektrogram.Size = new System.Drawing.Size(682, 244);
            this.pbSpektrogram.TabIndex = 8;
            this.pbSpektrogram.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // chbSkalaLogarytmiczna
            // 
            this.chbSkalaLogarytmiczna.AutoSize = true;
            this.chbSkalaLogarytmiczna.Checked = true;
            this.chbSkalaLogarytmiczna.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSkalaLogarytmiczna.Enabled = false;
            this.chbSkalaLogarytmiczna.Location = new System.Drawing.Point(250, 233);
            this.chbSkalaLogarytmiczna.Name = "chbSkalaLogarytmiczna";
            this.chbSkalaLogarytmiczna.Size = new System.Drawing.Size(120, 17);
            this.chbSkalaLogarytmiczna.TabIndex = 10;
            this.chbSkalaLogarytmiczna.Text = "Skala logarytmiczna";
            this.chbSkalaLogarytmiczna.UseVisualStyleBackColor = true;
            this.chbSkalaLogarytmiczna.CheckedChanged += new System.EventHandler(this.chbSkalaLogarytmiczna_CheckedChanged);
            // 
            // pbSkalaSpektrX
            // 
            this.pbSkalaSpektrX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSkalaSpektrX.Location = new System.Drawing.Point(224, 503);
            this.pbSkalaSpektrX.Name = "pbSkalaSpektrX";
            this.pbSkalaSpektrX.Size = new System.Drawing.Size(715, 24);
            this.pbSkalaSpektrX.TabIndex = 11;
            this.pbSkalaSpektrX.TabStop = false;
            // 
            // pbSkalaSpektrY
            // 
            this.pbSkalaSpektrY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbSkalaSpektrY.Location = new System.Drawing.Point(200, 244);
            this.pbSkalaSpektrY.Name = "pbSkalaSpektrY";
            this.pbSkalaSpektrY.Size = new System.Drawing.Size(44, 253);
            this.pbSkalaSpektrY.TabIndex = 12;
            this.pbSkalaSpektrY.TabStop = false;
            // 
            // pbFalka
            // 
            this.pbFalka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFalka.BackColor = System.Drawing.Color.White;
            this.pbFalka.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbFalka.Location = new System.Drawing.Point(0, 35);
            this.pbFalka.Name = "pbFalka";
            this.pbFalka.Size = new System.Drawing.Size(207, 147);
            this.pbFalka.TabIndex = 13;
            this.pbFalka.TabStop = false;
            // 
            // numA
            // 
            this.numA.DecimalPlaces = 2;
            this.numA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numA.Location = new System.Drawing.Point(12, 190);
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(120, 20);
            this.numA.TabIndex = 14;
            this.numA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numA.ValueChanged += new System.EventHandler(this.numA_ValueChanged);
            // 
            // hScrollSpektrogram
            // 
            this.hScrollSpektrogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollSpektrogram.Enabled = false;
            this.hScrollSpektrogram.Location = new System.Drawing.Point(244, 530);
            this.hScrollSpektrogram.Name = "hScrollSpektrogram";
            this.hScrollSpektrogram.Size = new System.Drawing.Size(682, 17);
            this.hScrollSpektrogram.TabIndex = 15;
            this.hScrollSpektrogram.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollSpektrogram_Scroll);
            // 
            // pnLegendaSp
            // 
            this.pnLegendaSp.Location = new System.Drawing.Point(100, 244);
            this.pnLegendaSp.Name = "pnLegendaSp";
            this.pnLegendaSp.Size = new System.Drawing.Size(85, 253);
            this.pnLegendaSp.TabIndex = 17;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 566);
            this.Controls.Add(this.pnLegendaSp);
            this.Controls.Add(this.hScrollSpektrogram);
            this.Controls.Add(this.numA);
            this.Controls.Add(this.pbFalka);
            this.Controls.Add(this.pbSkalaSpektrY);
            this.Controls.Add(this.pbSkalaSpektrX);
            this.Controls.Add(this.chbSkalaLogarytmiczna);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbSpektrogram);
            this.Controls.Add(this.pictureBoxOscylogram);
            this.Controls.Add(this.pbDefY);
            this.Controls.Add(this.pbDefX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollOscylogram);
            this.Controls.Add(this.trackBarOscylogram);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Analizator falkowy";
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOscylogram)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOscylogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpektrogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSkalaSpektrX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSkalaSpektrY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFalka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarOscylogram;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otworzToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogPlikWave;
        private System.Windows.Forms.PictureBox pictureBoxOscylogram;
        private System.Windows.Forms.HScrollBar hScrollOscylogram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbDefX;
        private System.Windows.Forms.PictureBox pbDefY;
        private System.Windows.Forms.ToolStripMenuItem cWTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liczToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbSpektrogram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbSkalaLogarytmiczna;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbSkalaSpektrX;
        private System.Windows.Forms.PictureBox pbSkalaSpektrY;
        private System.Windows.Forms.PictureBox pbFalka;
        private System.Windows.Forms.NumericUpDown numA;
        private System.Windows.Forms.HScrollBar hScrollSpektrogram;
        private System.Windows.Forms.Panel pnLegendaSp;
        private System.Windows.Forms.ToolStripMenuItem skalaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liniowaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logarytmicznaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawMindBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dB40ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dB50ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dB60ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem dB70ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem dB80ToolStripMenuItem4;
    }
}

