using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    public partial class FrmMain : Form
    {
        PlikWave plikWave;
        RysunekSygnalu rysunekSygnalu;
        Oscylogram oscylogram;
        Spektrogram spektrogram;
        Falka falka, falka2;
        CWT cwt;

        bool scrollePowiazane = false;

        public FrmMain()
        {
            InitializeComponent();

            scrollePowiazane = false;

            falka = new FalkaMorleta(0.1, 2.0, 0.1, 500);
            falka2 = new FalkaMexicanHat();
            cwt = new CWT(falka2, plikWave);
            numA.Minimum = Convert.ToDecimal(((FalkaCiagla)falka).StartA);
            numA.Maximum = Convert.ToDecimal(((FalkaCiagla)falka).StopA);
            falka2.Rysuj(pbFalka, (double)numA.Value);
        }

        /// <summary>
        /// Przypisuje wartosci scrolla1 do scrolla2
        /// </summary>
        /// <param name="scroll1"></param>
        /// <param name="scroll2"></param>
        private void PowiazScrolle(HScrollBar scroll1, HScrollBar scroll2)
        {            
            scroll2.Maximum = scroll1.Maximum;
            scroll2.Value = scroll1.Value;
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogPlikWave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                plikWave = new PlikWave(openFileDialogPlikWave.FileName);
                cwt.PlikSygnal = plikWave;
                if (plikWave.OtworzPlik())
                {
                    rysunekSygnalu = new RysunekSygnalu16Bit(plikWave.Kanal0Bit16, pictureBoxOscylogram);
                    oscylogram = new Oscylogram(rysunekSygnalu, plikWave, pictureBoxOscylogram, pbDefX, pbDefY,
                        trackBarOscylogram, hScrollOscylogram);                    
                    
                    oscylogram.Rysuj();

                    spektrogram = new Spektrogram(pbSpektrogram, pbSkalaSpektrY, pbSkalaSpektrX, hScrollSpektrogram, cwt, oscylogram);
                    spektrogram.LogarytmicznaSkala = chbSkalaLogarytmiczna.Checked;
                    LegendaSpektrogramu leg = new LegendaSpektrogramu(pnLegendaSp, spektrogram);


                    liczToolStripMenuItem.Enabled = true;
                    chbSkalaLogarytmiczna.Enabled = true; 
                }
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            oscylogram.Resize();
        }

        private void trackBarOscylogram_Scroll(object sender, EventArgs e)
        {            
            oscylogram.Zoom();

            if (scrollePowiazane)
            {
                PowiazScrolle(hScrollOscylogram, hScrollSpektrogram);
                if (cwt.WynikCWT != null)
                    spektrogram.SkalujSpektrogram();
            }
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            oscylogram.Rysuj();
            if (scrollePowiazane)
            {
                PowiazScrolle(hScrollOscylogram, hScrollSpektrogram);
      //          if (cwt.WynikCWT != null)
     //               spektrogram.Rysuj();
            }
            
        }

        private void liczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cwt.ObliczCWT();
            //  cwt.ObliczCWTprzezGPU();

            if (scrollePowiazane)
            {
                PowiazScrolle(hScrollOscylogram, hScrollSpektrogram);
                hScrollSpektrogram.Enabled = hScrollOscylogram.Enabled;
            }
            spektrogram.Rysuj();

            
        }

        private void chbSkalaLogarytmiczna_CheckedChanged(object sender, EventArgs e)
        {
            spektrogram.LogarytmicznaSkala = chbSkalaLogarytmiczna.Checked;
            spektrogram.RysujPoZmianieSkali();            
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ustawToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            FrmUstawieniaCWT frmUstawieniaCWT = new FrmUstawieniaCWT(cwt);
            frmUstawieniaCWT.Show();            
        }

        private void numA_ValueChanged(object sender, EventArgs e)
        {
            falka2.Rysuj(pbFalka, (double)numA.Value);
        }     

        private void hScrollSpektrogram_Scroll(object sender, ScrollEventArgs e)
        {
            if (scrollePowiazane)
            {
                PowiazScrolle(hScrollSpektrogram, hScrollOscylogram);
                oscylogram.Rysuj();
            }
            spektrogram.Rysuj();
        }       
       
    }
}
