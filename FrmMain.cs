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
    

    public partial class FrmMain : Form
    {
        PlikWave plikWave;
        RysunekSygnalu rysunekSygnalu;
        Oscylogram oscylogram;
        Spektrogram spektrogram;        
        Falka falka, falka2;
        CWT cwt;        
        Thread thrObliczCWT;

        public delegate void DelTypKoniecWatku();
        public DelTypKoniecWatku DelKoniecWatku;

        FrmPostep frmPostep;

        bool scrollePowiazane = false;

        public FrmMain()
        {
            InitializeComponent();
            DelKoniecWatku = new DelTypKoniecWatku(KoniecWatku);            

            scrollePowiazane = false;

            falka = new FalkaMorleta(0.1, 2.0, 0.01, 500);
            falka2 = new FalkaMexicanHat();
            cwt = new CWT(falka, plikWave);
            numA.Minimum = Convert.ToDecimal(((FalkaCiagla)falka).StartA);
            numA.Maximum = Convert.ToDecimal(((FalkaCiagla)falka).StopA);
            falka.Rysuj(pbFalka, (double)numA.Value);
            
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

        private void KoniecWatku()
        {
            if (frmPostep != null)
                frmPostep.Close();
        }

        private void ObliczIRysujCWT()
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

        /// <summary>
        /// Ustawia enabled podanych elementow zgodnie z wartoscia wlacz
        /// </summary>
        /// <param name="zalacz">Jesli true - wlacza wszystkie elementy, false - wylacza wszystkie elementy</param>
        /// <param name="lista">Elementy do wlaczenia/wylaczenia. Osluguje MenuItem, ToolStripMenuItem, CheckBox,
        /// TrackBar, ScrollBar</param>
        private void WylaczWlaczElementy(bool zalacz, params object[] lista)
        {
            foreach (object item in lista)
            {
                if (item is MenuItem)
                    ((MenuItem)item).Enabled = zalacz;
                else if (item is ToolStripMenuItem)
                    ((ToolStripMenuItem)item).Enabled = zalacz;
                else if (item is CheckBox)
                    ((CheckBox)item).Enabled = zalacz;
                else if (item is TrackBar)
                    ((TrackBar)item).Enabled = zalacz;
                else if (item is ScrollBar)
                    ((ScrollBar)item).Enabled = zalacz;
            }
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

                    spektrogram = new Spektrogram(pbSpektrogram, pbSkalaSpektrY, pbSkalaSpektrX, hScrollSpektrogram, pnLegendaSp, cwt, oscylogram, this);
                    spektrogram.LogarytmicznaSkala = chbSkalaLogarytmiczna.Checked;                    

                    liczToolStripMenuItem.Enabled = true;
                    chbSkalaLogarytmiczna.Enabled = true;
                    skalaToolStripMenuItem.Enabled = true;

                  //  thrRysujOscylogram = new Thread(new ThreadStart(oscylogram.Rysuj));
                 //   thrRysujSpektrogram = new Thread(new ThreadStart(spektrogram.Rysuj));
                    thrObliczCWT = new Thread(new ThreadStart(ObliczIRysujCWT));                    
                }
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {        
            oscylogram.Resize();
            spektrogram.Resize();
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
            thrObliczCWT = new Thread(new ThreadStart(ObliczIRysujCWT));
            thrObliczCWT.Priority = ThreadPriority.Highest;
            thrObliczCWT.Start();
            frmPostep = new FrmPostep(thrObliczCWT, cwt);            
            frmPostep.ShowDialog();
        }
        

        private void chbSkalaLogarytmiczna_CheckedChanged(object sender, EventArgs e)
        {   
            logarytmicznaToolStripMenuItem.Checked = chbSkalaLogarytmiczna.Checked;
            liniowaToolStripMenuItem.Checked = !chbSkalaLogarytmiczna.Checked;

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
            cwt.Falka.Rysuj(pbFalka, (double)numA.Value);           
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

        private void liniowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logarytmicznaToolStripMenuItem.Checked = false;
            liniowaToolStripMenuItem.Checked = true;
            chbSkalaLogarytmiczna.Checked = logarytmicznaToolStripMenuItem.Checked;
        }

        private void logarytmicznaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logarytmicznaToolStripMenuItem.Checked = true;
            liniowaToolStripMenuItem.Checked = false;
            chbSkalaLogarytmiczna.Checked = logarytmicznaToolStripMenuItem.Checked;
        }
        private void UstawSkalaLogZaznaczenie(object sender)
        {
            dB40ToolStripMenuItem.Checked = false;
            dB50ToolStripMenuItem1.Checked = false;
            dB60ToolStripMenuItem2.Checked = false;
            dB70ToolStripMenuItem3.Checked = false;
            dB70ToolStripMenuItem3.Checked = false;
            dB80ToolStripMenuItem4.Checked = false;

            ((ToolStripMenuItem)sender).Checked = true;
        }

        private void dBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UstawSkalaLogZaznaczenie(sender);
            spektrogram.MinDecybeli = -40;
        }

        private void dBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UstawSkalaLogZaznaczenie(sender);
            spektrogram.MinDecybeli = -50;
        }

        private void dBToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UstawSkalaLogZaznaczenie(sender);
            spektrogram.MinDecybeli = -60;
        }

        private void dBToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UstawSkalaLogZaznaczenie(sender);
            spektrogram.MinDecybeli = -70;
        }

        private void dBToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UstawSkalaLogZaznaczenie(sender);
            spektrogram.MinDecybeli = -80;
        }

        private void ustawToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmUstawMINdB frmUstawieniaMINdB = new FrmUstawMINdB(spektrogram, ustawMindBToolStripMenuItem);            
            frmUstawieniaMINdB.Show();
        }

      
    }
}
