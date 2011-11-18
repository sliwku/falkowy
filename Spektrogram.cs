using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class Spektrogram
    {
        private PictureBox pbSpektrogram;
        private HScrollBar scrollSpektrogram;
        private CWT cwt;
        private PaletaKolorow paletaKolorow;        
        private Color[,] koloryLinCalosc, koloryLogCalosc, koloryLinSkala, koloryLogSkala;
        private bool logarytmicznaSkala;
        private Bitmap bmpLin;
        private Bitmap bmpLog;
        private Oscylogram oscylogram;

        private SkalaSpektrogram skalaSpektrogram;

        private double startA;
        private double stopA;
        private double krokA;
        private int iloscA;

        private double dx;
        private double dy;

        public double Dy
        {
            get { return dy; }
        }

        public double Dx
        {
            get { return dx; }         
        }

        public int IloscA
        {
            get { return iloscA; }         
        }

        public double KrokA
        {
            get { return krokA; }            
        }
        public double StopA
        {
            get { return stopA; }            
        }
        public double StartA
        {
            get { return startA; }            
        }

        /// <summary>
        /// do zrobienia
        /// </summary>
        private int przesuniecie;

        public int Przesuniecie
        {
            get { return przesuniecie; }
            set { przesuniecie = value; }
        }

        public bool LogarytmicznaSkala
        {            
            set { logarytmicznaSkala = value; }
            get { return logarytmicznaSkala; }
        }
        public PaletaKolorow PaletaKolorow
        {
            get { return paletaKolorow; }
            set { paletaKolorow = value; }
        }

        public Spektrogram(PictureBox pbSpektrogram, PictureBox pbSkalaA, PictureBox pbSkalaB, HScrollBar scrollSpektrogram, CWT cwt, Oscylogram oscylogram)
            : this(pbSpektrogram, pbSkalaA, pbSkalaB,scrollSpektrogram, cwt, oscylogram, new Paleta768Standard(), true)
        {
            
        }
        public Spektrogram(PictureBox pbSpektrogram, PictureBox pbSkalaA, PictureBox pbSkalaB, HScrollBar scrollSpektrogram,
            CWT cwt, Oscylogram oscylogram, PaletaKolorow paletaKolorow, bool logarytmicznaSkala)
        {
            this.pbSpektrogram = pbSpektrogram;
            this.scrollSpektrogram = scrollSpektrogram;
            this.cwt = cwt;
            this.paletaKolorow = paletaKolorow;
            this.logarytmicznaSkala = logarytmicznaSkala;
            this.oscylogram = oscylogram;            

            if (cwt.Falka is FalkaCiagla)
            {
                startA = ((FalkaCiagla)cwt.Falka).StartA;
                stopA = ((FalkaCiagla)cwt.Falka).StopA;
                krokA = ((FalkaCiagla)cwt.Falka).KrokA;
                iloscA = cwt.IloscA;
            }           

            skalaSpektrogram = new SkalaSpektrogram(this, oscylogram, pbSpektrogram, pbSkalaB, pbSkalaA);
        }

        public void Rysuj()
        {  
            {
                UtworzSpektrogram();
                SkalujSpektrogram();
                bmpLin = new Bitmap(pbSpektrogram.Width, pbSpektrogram.Height);
                bmpLog = new Bitmap(pbSpektrogram.Width, pbSpektrogram.Height);
            }

            for (int x1 = 0, x2 = scrollSpektrogram.Value; x1 < pbSpektrogram.Width; x1++, x2++)
                for (int y = 0; y < pbSpektrogram.Height; y++)
                {
                    bmpLin.SetPixel(x1, y, koloryLinSkala[x2, y]);
                    bmpLog.SetPixel(x1, y, koloryLogSkala[x2, y]);
                }

            if (logarytmicznaSkala)
                pbSpektrogram.Image = bmpLog;
            else
                pbSpektrogram.Image = bmpLin;
            skalaSpektrogram.Rysuj();
        }       
        public void SkalujSpektrogram()
        {
            koloryLinSkala = new Color[pbSpektrogram.Width * (int)Math.Pow(RysunekSygnalu.PODZIELNIK_SKALI, oscylogram.Skala), pbSpektrogram.Height];
            koloryLogSkala = new Color[pbSpektrogram.Width * (int)Math.Pow(RysunekSygnalu.PODZIELNIK_SKALI, oscylogram.Skala), pbSpektrogram.Height];

            dx = koloryLinCalosc.GetLength(1) / (double)koloryLinSkala.GetLength(0);
            dy = koloryLinCalosc.GetLength(0) / (double)pbSpektrogram.Height;

            //w kolory podobnie jak w wynik CWT jest tablica [a,b] a to skala utozsamiana z y, b przesuniecie utozsamiane z x
            //mozna sie pokusic o jakies lepsze skalowanie...
            for (int x = 0; x < koloryLinSkala.GetLength(0); x++)
                for (int a = 0, y = koloryLinSkala.GetLength(1) - 1; y >= 0; y--)
                {
                    koloryLinSkala[x, y] = koloryLinCalosc[(int)(a * dy), (int)(x * dx)];
                    koloryLogSkala[x, y] = koloryLogCalosc[(int)(a++ * dy), (int)(x * dx)];                   
                }
        }
        private void UtworzSpektrogram()
        {
            //dzieki temu minimum skali to -60dB
            const double minLogarytmowane = 0.000001;

            koloryLinCalosc = new Color[cwt.WynikCWT.GetLength(0), cwt.WynikCWT.GetLength(1)];
            koloryLogCalosc = new Color[cwt.WynikCWT.GetLength(0), cwt.WynikCWT.GetLength(1)];

            double dcLin = (paletaKolorow.Paleta.Length - 1) / (double)cwt.MaxCWTDlaInt16;
            double dcLog = (paletaKolorow.Paleta.Length - 1) / -Math.Log10(minLogarytmowane);

            double liczbaLogarytmowana = 0.0;
            for (int i = 0; i < koloryLinCalosc.GetLength(0); i++)
                for (int j = 0; j < koloryLinCalosc.GetLength(1); j++)
                {
                    koloryLinCalosc[i, j] = paletaKolorow.Paleta[(int)(cwt.WynikCWT[i, j] * dcLin)];

                    liczbaLogarytmowana = cwt.WynikCWT[i, j] / (double)cwt.MaxCWTDlaInt16;
                    if (liczbaLogarytmowana > minLogarytmowane)
                        koloryLogCalosc[i, j] = paletaKolorow.Paleta[(int)(Math.Log10(liczbaLogarytmowana) * dcLog) + paletaKolorow.Paleta.Length - 1];
                    else
                        koloryLogCalosc[i, j] = paletaKolorow.Paleta[0];
                }
        }


        public void RysujPoZmianieSkali()
        {
            if (logarytmicznaSkala)
                pbSpektrogram.Image = bmpLog;
            else
                pbSpektrogram.Image = bmpLin;
        }

       
    }
}
