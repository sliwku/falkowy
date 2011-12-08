using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AnalizatorFalkowy
{
    public partial class FrmPostep : Form
    {
        Thread watekRoboczy;
        CWT cwt;
        public FrmPostep(Thread watek, CWT cwt)
        {
            this.watekRoboczy = watek;
            this.cwt = cwt;
            InitializeComponent();
            
            progressBar1.Maximum = cwt.IloscA;
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void FrmPostep_FormClosing(object sender, FormClosingEventArgs e)
        {
            watekRoboczy.Abort();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = cwt.Postep;
        }
    }
}
