using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    abstract class Sygnal2
    {
        protected const int GRANICA_ZAGESZCZENIE_PIKSELI = 7;

        protected HScrollBar scrollBar;
        protected TrackBar zoomTrackBar;
        protected PictureBox pictureBoxSygnal;
        protected Graphics graphicsSygnal;

        protected int dlugoscSygnalu;
        protected int iloscPikseliNaProbke;

        private int dlugoscKanalu;

        public Sygnal2(HScrollBar scrollBar, TrackBar zoomTrackBar, PictureBox pictureBoxSygnal, int dlugoscKanalu)
        {
            this.scrollBar = scrollBar;
            this.zoomTrackBar = zoomTrackBar;
            this.pictureBoxSygnal = pictureBoxSygnal;
            this.dlugoscKanalu = dlugoscKanalu;
            graphicsSygnal = Graphics.FromHwnd(pictureBoxSygnal.Handle);

            UstawMaksZoom();
            UstawMaksINowaWartoscScroll();
            dlugoscSygnalu = ObliczDlugoscSygnalu();
            iloscPikseliNaProbke = ObliczIloscPikseliNaProbke();
        }

        private int ObliczIloscPikseliNaProbke()
        {
            return dlugoscKanalu / dlugoscSygnalu;
        }
        private void UstawMaksZoom()
        {
            zoomTrackBar.Minimum = 0;
            zoomTrackBar.Maximum = ObliczMaksZoom();
            zoomTrackBar.Value = zoomTrackBar.Maximum;
            if (zoomTrackBar.Maximum > 0)
                zoomTrackBar.Enabled = true;
        }
        private int ObliczMaksZoom()
        {
            return (int)Math.Log(dlugoscKanalu / pictureBoxSygnal.Width, 2) + 1;
        }

        private int ObliczDlugoscSygnalu()
        {
            if (zoomTrackBar.Value == zoomTrackBar.Maximum)
                return pictureBoxSygnal.Width;

            return dlugoscKanalu / (int)Math.Pow(2, zoomTrackBar.Value);
        }

        private void UstawMaksINowaWartoscScroll()
        {
            if (zoomTrackBar.Value == zoomTrackBar.Maximum)
            {
                scrollBar.Value = 0;
                scrollBar.Maximum = 0;
                scrollBar.Enabled = false;
            }
            else if (scrollBar.Maximum != 0)
            {
                scrollBar.Enabled = true;

                int staryMaks = scrollBar.Maximum;

                scrollBar.Maximum = dlugoscSygnalu - pictureBoxSygnal.Width;

                int dScroll = scrollBar.Maximum - staryMaks;
                //        if (scrollBar.Maximum > staryMaks)
                {
                    double ilorazScroll = scrollBar.Maximum / (double)staryMaks;
                    scrollBar.Value = (int)(scrollBar.Value * ilorazScroll);
                }
                //else
                //{
                //    double ilorazScroll = staryMaks / (double)scrollBar.Maximum;
                //    scrollBar.Value -= (int)(scrollBar.Value * ilorazScroll);
                //}

                //else if (nowaWartosc > scrollBar.Maximum)
                //    scrollBar.Value = scrollBar.Maximum;
                //else
                //    scrollBar.Value = nowaWartosc;
            }
            else
            {
                scrollBar.Enabled = true;
                scrollBar.Maximum = dlugoscSygnalu - pictureBoxSygnal.Width;
            }
        }

        protected void OdswiezPrzyZmianieZoom()
        {
            dlugoscSygnalu = ObliczDlugoscSygnalu();
            iloscPikseliNaProbke = ObliczIloscPikseliNaProbke();
            UstawMaksINowaWartoscScroll();
        }
        protected void OdswiezPrzyZmianieRozmiaruPictureBox()
        {
            zoomTrackBar.Maximum = ObliczMaksZoom();
            OdswiezPrzyZmianieZoom();
        }

        public abstract void Rysuj();

        public abstract void Zoom();

        public abstract void Scroll();
    }
}
