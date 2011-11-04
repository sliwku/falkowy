namespace AnalizatorFalkowy
{
    partial class FrmUstawieniaCWT
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
            this.labStartA = new System.Windows.Forms.Label();
            this.numStartA = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAnuluj = new System.Windows.Forms.Button();
            this.numStopA = new System.Windows.Forms.NumericUpDown();
            this.labStopA = new System.Windows.Forms.Label();
            this.numKrokA = new System.Windows.Forms.NumericUpDown();
            this.labKrokA = new System.Windows.Forms.Label();
            this.numDokladnosc = new System.Windows.Forms.NumericUpDown();
            this.labDokladnosc = new System.Windows.Forms.Label();
            this.numKrokB = new System.Windows.Forms.NumericUpDown();
            this.LabKrokB = new System.Windows.Forms.Label();
            this.DrpWyborFalki = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numStartA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStopA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDokladnosc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokB)).BeginInit();
            this.SuspendLayout();
            // 
            // labStartA
            // 
            this.labStartA.AutoSize = true;
            this.labStartA.Location = new System.Drawing.Point(12, 57);
            this.labStartA.Name = "labStartA";
            this.labStartA.Size = new System.Drawing.Size(79, 13);
            this.labStartA.TabIndex = 0;
            this.labStartA.Text = "Początkowe \'a\'";
            // 
            // numStartA
            // 
            this.numStartA.DecimalPlaces = 1;
            this.numStartA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numStartA.Location = new System.Drawing.Point(155, 55);
            this.numStartA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numStartA.Name = "numStartA";
            this.numStartA.Size = new System.Drawing.Size(120, 20);
            this.numStartA.TabIndex = 1;
            this.numStartA.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(208, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAnuluj
            // 
            this.btnAnuluj.Location = new System.Drawing.Point(15, 279);
            this.btnAnuluj.Name = "btnAnuluj";
            this.btnAnuluj.Size = new System.Drawing.Size(75, 23);
            this.btnAnuluj.TabIndex = 3;
            this.btnAnuluj.Text = "Anuluj";
            this.btnAnuluj.UseVisualStyleBackColor = true;
            this.btnAnuluj.Click += new System.EventHandler(this.btnAnuluj_Click);
            // 
            // numStopA
            // 
            this.numStopA.DecimalPlaces = 1;
            this.numStopA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numStopA.Location = new System.Drawing.Point(155, 88);
            this.numStopA.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numStopA.Name = "numStopA";
            this.numStopA.Size = new System.Drawing.Size(120, 20);
            this.numStopA.TabIndex = 5;
            this.numStopA.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // labStopA
            // 
            this.labStopA.AutoSize = true;
            this.labStopA.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.labStopA.Location = new System.Drawing.Point(12, 90);
            this.labStopA.Name = "labStopA";
            this.labStopA.Size = new System.Drawing.Size(65, 13);
            this.labStopA.TabIndex = 4;
            this.labStopA.Text = "Końcowe \'a\'";
            // 
            // numKrokA
            // 
            this.numKrokA.DecimalPlaces = 3;
            this.numKrokA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numKrokA.Location = new System.Drawing.Point(155, 125);
            this.numKrokA.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numKrokA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numKrokA.Name = "numKrokA";
            this.numKrokA.Size = new System.Drawing.Size(120, 20);
            this.numKrokA.TabIndex = 7;
            this.numKrokA.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // labKrokA
            // 
            this.labKrokA.AutoSize = true;
            this.labKrokA.Location = new System.Drawing.Point(12, 127);
            this.labKrokA.Name = "labKrokA";
            this.labKrokA.Size = new System.Drawing.Size(87, 13);
            this.labKrokA.TabIndex = 6;
            this.labKrokA.Text = "Inkrementacja \'a\'";
            // 
            // numDokladnosc
            // 
            this.numDokladnosc.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDokladnosc.Location = new System.Drawing.Point(155, 205);
            this.numDokladnosc.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDokladnosc.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDokladnosc.Name = "numDokladnosc";
            this.numDokladnosc.Size = new System.Drawing.Size(120, 20);
            this.numDokladnosc.TabIndex = 9;
            this.numDokladnosc.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // labDokladnosc
            // 
            this.labDokladnosc.AutoSize = true;
            this.labDokladnosc.Location = new System.Drawing.Point(12, 207);
            this.labDokladnosc.Name = "labDokladnosc";
            this.labDokladnosc.Size = new System.Drawing.Size(130, 13);
            this.labDokladnosc.TabIndex = 8;
            this.labDokladnosc.Text = "Dokładność/gęstość falki";
            // 
            // numKrokB
            // 
            this.numKrokB.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numKrokB.Location = new System.Drawing.Point(155, 165);
            this.numKrokB.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numKrokB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKrokB.Name = "numKrokB";
            this.numKrokB.Size = new System.Drawing.Size(120, 20);
            this.numKrokB.TabIndex = 11;
            this.numKrokB.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // LabKrokB
            // 
            this.LabKrokB.AutoSize = true;
            this.LabKrokB.Location = new System.Drawing.Point(12, 167);
            this.LabKrokB.Name = "LabKrokB";
            this.LabKrokB.Size = new System.Drawing.Size(87, 13);
            this.LabKrokB.TabIndex = 10;
            this.LabKrokB.Text = "Inkrementacja \'b\'";
            // 
            // DrpWyborFalki
            // 
            this.DrpWyborFalki.BackColor = System.Drawing.SystemColors.Window;
            this.DrpWyborFalki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrpWyborFalki.ForeColor = System.Drawing.SystemColors.WindowText;
            this.DrpWyborFalki.FormattingEnabled = true;
            this.DrpWyborFalki.Items.AddRange(new object[] {
            "Falka Morleta",
            "Falka Mexican Hat"});
            this.DrpWyborFalki.Location = new System.Drawing.Point(154, 12);
            this.DrpWyborFalki.Name = "DrpWyborFalki";
            this.DrpWyborFalki.Size = new System.Drawing.Size(121, 21);
            this.DrpWyborFalki.TabIndex = 12;
            // 
            // FrmUstawieniaCWT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 314);
            this.Controls.Add(this.DrpWyborFalki);
            this.Controls.Add(this.numKrokB);
            this.Controls.Add(this.LabKrokB);
            this.Controls.Add(this.numDokladnosc);
            this.Controls.Add(this.labDokladnosc);
            this.Controls.Add(this.numKrokA);
            this.Controls.Add(this.labKrokA);
            this.Controls.Add(this.numStopA);
            this.Controls.Add(this.labStopA);
            this.Controls.Add(this.btnAnuluj);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numStartA);
            this.Controls.Add(this.labStartA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUstawieniaCWT";
            this.Text = "Ustawienia CWT";
            ((System.ComponentModel.ISupportInitialize)(this.numStartA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStopA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDokladnosc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKrokB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labStartA;
        private System.Windows.Forms.NumericUpDown numStartA;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAnuluj;
        private System.Windows.Forms.NumericUpDown numStopA;
        private System.Windows.Forms.Label labStopA;
        private System.Windows.Forms.NumericUpDown numKrokA;
        private System.Windows.Forms.Label labKrokA;
        private System.Windows.Forms.NumericUpDown numDokladnosc;
        private System.Windows.Forms.Label labDokladnosc;
        private System.Windows.Forms.NumericUpDown numKrokB;
        private System.Windows.Forms.Label LabKrokB;
        private System.Windows.Forms.ComboBox DrpWyborFalki;
    }
}