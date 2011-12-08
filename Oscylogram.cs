using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace AnalizatorFalkowy
{
    public class Oscylogram
    {
        private Graphics graphicsRysunek;
        
        private Pen pen;

        private RysunekSygnalu rysunekSygnalu;
        
        private PictureBox pictureBoxRysunek;
        private TrackBar zoomTrackBar;
        private HScrollBar scrollBar;
        private Skala skalaLiniowa;
        private int czestotliwoscProbkowania;

        public int CzestotliwoscProbkowania
        {
            get { return czestotliwoscProbkowania; }
        }

        public Graphics GraphicsRysunek
        {
            get { return graphicsRysunek; }            
        }        

        internal RysunekSygnalu RysunekSygnalu
        {
            get { return rysunekSygnalu; }           
        }

        public int Pzesuniecie
        {
            get { return scrollBar.Value; }
        }
        public int Skala
        {
            get { return zoomTrackBar.Value; }
        }

        public Oscylogram(RysunekSygnalu rysunekSygnalu, PlikWave plikWave, PictureBox pictureBoxRysunek, PictureBox pbSkalaDefX, PictureBox pbSkalaDefY, 
            TrackBar zoomTrackBar, HScrollBar scrollBar)
        {
            this.rysunekSygnalu = rysunekSygnalu;
            this.pictureBoxRysunek = pictureBoxRysunek;                                                                                 
            this.zoomTrackBar = zoomTrackBar;
            this.scrollBar = scrollBar;
            czestotliwoscProbkowania = plikWave.CzestotliwoscProbkowania;

            zoomTrackBar.Value = 0;
            UstawZoom();

            scrollBar.Value = 0;
            UstawMaxScroll();
            
            pen = new Pen(new SolidBrush(Color.Black));

            skalaLiniowa = new SkalaOscylogram(this, plikWave, pictureBoxRysunek, pbSkalaDefX, pbSkalaDefY);           
        }
        private void UstawZoom()
        {            
            zoomTrackBar.Maximum = rysunekSygnalu.MaxSkala;
            zoomTrackBar.Enabled = (zoomTrackBar.Maximum > 0);
        }
        private void UstawMaxScroll()
        {
            int scrollMax = rysunekSygnalu.DlugoscSygnaluZeskalowanego - pictureBoxRysunek.Width;
            if (scrollMax > 0)
                scrollBar.Maximum = scrollMax;
            else
                scrollBar.Maximum = 0;
            scrollBar.Enabled = scrollBar.Maximum > 0;
        }
        private void UstawMaxScroll(out int staryMaxScroll)
        {
            staryMaxScroll = scrollBar.Maximum;
            UstawMaxScroll();
        }

        public void Resize()
        {            
            rysunekSygnalu.Odswiez();
            skalaLiniowa.Odswiez();
            
            UstawZoom();
            UstawMaxINowaPozycjScroll();

            Rysuj();
        }

        public void Zoom()
        {
            rysunekSygnalu.Skala = zoomTrackBar.Value;
            skalaLiniowa.Odswiez();

            UstawMaxINowaPozycjScroll();

            Rysuj();
        }
       
        private void UstawMaxINowaPozycjScroll()
        {
            //zapisujemy wartosc scrolla ze wzgledu na to, ze przy zmianie wartosci scrolBar.Maximum na mniejsza niz scrollBar.Value
            //scrollBar.Value automatycznie przyjmuje wartosc scrollBar.Maximum
            int staryScroll = scrollBar.Value;
            int staryMaxScroll;            
            UstawMaxScroll(out staryMaxScroll);

            double dScroll = (scrollBar.Maximum + pictureBoxRysunek.Width) / (double)(staryMaxScroll + pictureBoxRysunek.Width);
            int nowyScroll = (int)(staryScroll * dScroll);

            if (nowyScroll > scrollBar.Maximum)
                scrollBar.Value = scrollBar.Maximum;
            else if (nowyScroll < scrollBar.Minimum)
                scrollBar.Value = scrollBar.Minimum;
            else
                scrollBar.Value = nowyScroll;
        }

        public void Rysuj()
        {
            pictureBoxRysunek.Image = new Bitmap(pictureBoxRysunek.Width, pictureBoxRysunek.Height);
            graphicsRysunek = Graphics.FromImage(pictureBoxRysunek.Image);
            
            skalaLiniowa.Rysuj();
            graphicsRysunek.DrawLines(pen, ZwrocTablicePunktowOd(scrollBar.Value));
            Application.DoEvents();            
        }
        private Point[] ZwrocTablicePunktowOd(int przesuniecie)
        {         
            int indexStart = przesuniecie * rysunekSygnalu.IloscPunktowNaX;
            while (rysunekSygnalu.Rysunek[indexStart].X < przesuniecie)
                indexStart++;

            int indexStop = indexStart + pictureBoxRysunek.Width * rysunekSygnalu.IloscPunktowNaX - 1;
            
            int pictureBoxWidthMinusJeden = pictureBoxRysunek.Width - 1;
            while (rysunekSygnalu.Rysunek[indexStop].X - przesuniecie < pictureBoxWidthMinusJeden)
                indexStop++;

            Point[] tabPoint = new Point[indexStop - indexStart];


            for (int i = 0, j = indexStart; i < tabPoint.Length; i++, j++)
            {
                tabPoint[i].X = rysunekSygnalu.Rysunek[j].X - przesuniecie;
                tabPoint[i].Y = rysunekSygnalu.Rysunek[j].Y;
            }
            return tabPoint;
        }
    }
}
