using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    public class Spektrogram
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
        private FrmMain mForm;

        private SkalaSpektrogram skala;
        LegendaSpektrogramu legenda;

        private double startA;
        private double stopA;
        private double krokA;
        private int iloscA;

        private double dx;
        private double dy;

        //dzieki temu minimum skali to -60dB
        private double minLogarytmowane = 0.000001;

        public event EventHandler Narysowano;

        #region Wlasciwosci

        public int MinDecybeli
        {
            get { return MinimumLogarytmowaneToDecybel(minLogarytmowane); }
            set
            {
                if (value < 0)
                {
                    minLogarytmowane = DecybelToMinimumLogarytmowane(value);
                    legenda.OdswiezSkale();
                }
            }
        }

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

        #endregion Property

        public Spektrogram(PictureBox pbSpektrogram, PictureBox pbSkalaA, PictureBox pbSkalaB, HScrollBar scrollSpektrogram, Panel legendaSpektrogramu, CWT cwt, Oscylogram oscylogram, FrmMain mForm)
            : this(pbSpektrogram, pbSkalaA, pbSkalaB,scrollSpektrogram, legendaSpektrogramu, cwt, oscylogram, mForm, new Paleta768Standard(), true)
        {
            
        }
        public Spektrogram(PictureBox pbSpektrogram, PictureBox pbSkalaA, PictureBox pbSkalaB, HScrollBar scrollSpektrogram, Panel legendaSpektrogramu,
            CWT cwt, Oscylogram oscylogram, FrmMain mForm, PaletaKolorow paletaKolorow, bool logarytmicznaSkala)
        {
            this.pbSpektrogram = pbSpektrogram;
            this.scrollSpektrogram = scrollSpektrogram;
            this.cwt = cwt;
            this.paletaKolorow = paletaKolorow;
            this.logarytmicznaSkala = logarytmicznaSkala;
            this.oscylogram = oscylogram;   
            this.mForm = mForm;

            if (cwt.Falka is FalkaCiagla)
            {
                startA = ((FalkaCiagla)cwt.Falka).StartA;
                stopA = ((FalkaCiagla)cwt.Falka).StopA;
                krokA = ((FalkaCiagla)cwt.Falka).KrokA;
                iloscA = cwt.IloscA;
            }           

            skala = new SkalaSpektrogram(this, oscylogram, pbSpektrogram, pbSkalaB, pbSkalaA);
            legenda = new LegendaSpektrogramu(legendaSpektrogramu, this);
            legenda.Rysuj();           
        }        

        public void Rysuj()
        {              
            UtworzSpektrogram();
            SkalujSpektrogram();
            bmpLin = new Bitmap(pbSpektrogram.Width, pbSpektrogram.Height);
            bmpLog = new Bitmap(pbSpektrogram.Width, pbSpektrogram.Height);            

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
            skala.Rysuj();
            mForm.Invoke(mForm.DelKoniecWatku, null);
        }
        private void UtworzSpektrogram()
        {
            koloryLinCalosc = new Color[cwt.WynikCWT.GetLength(0), cwt.WynikCWT.GetLength(1)];
            koloryLogCalosc = new Color[cwt.WynikCWT.GetLength(0), cwt.WynikCWT.GetLength(1)];

            double dcLin = (paletaKolorow.Paleta.Length - 1) / (double)cwt.MaxCWTDlaInt16;
            double dcLog = (paletaKolorow.Paleta.Length - 1) / -Math.Log10(minLogarytmowane);            

            double liczbaLogarytmowana = 0.0;
            for (int i = 0; i < koloryLinCalosc.GetLength(0); i++)
                for (int j = 0; j < koloryLinCalosc.GetLength(1); j++)
                {
                    koloryLinCalosc[i, j] = paletaKolorow.Paleta[(int)(cwt.WynikCWT[i, j] * dcLin)];

                    //P_B = log10(P / P_0)
                    //wartosc w belach (P_B) jest rowna tej wartosc w ujeciu liniowym podzielonej przez P_0 i zlogarytmowanej.
                    //P_0 to wartość odniesienia 0 B (0 dB). Tutaj to wartosc maks CWT
                    //skalowanie zalezne od ustawionego minimum, tutaj poczatkowo -6 B (-60 dB); wartosc mozna zmienic
                    //gdy liczba logarytmowana jest mniejsza od minimumLogarytmowanego przyjmuje sie, ze wartosc transformaty
                    //w tym punkcie wynosi 0; w poczatkowym przykladzie wiec rozciagamy skale od 0 do -60 dB a wyniki ponizej traktujemy
                    //jako 0
                    liczbaLogarytmowana = cwt.WynikCWT[i, j] / (double)cwt.MaxCWTDlaInt16;                    
                    if (liczbaLogarytmowana > minLogarytmowane)
                        koloryLogCalosc[i, j] = paletaKolorow.Paleta[(int)(Math.Log10(liczbaLogarytmowana) * dcLog) + paletaKolorow.Paleta.Length - 1];
                    else
                        koloryLogCalosc[i, j] = paletaKolorow.Paleta[0];
                }
        }
        public void SkalujSpektrogram()
        {
            SkalujSpektrogram(0);
        }
        public void SkalujSpektrogram(int skalaOscylogramu)
        {
            koloryLinSkala = new Color[pbSpektrogram.Width * (int)Math.Pow(RysunekSygnalu.PODZIELNIK_SKALI, skalaOscylogramu), pbSpektrogram.Height];
            koloryLogSkala = new Color[pbSpektrogram.Width * (int)Math.Pow(RysunekSygnalu.PODZIELNIK_SKALI, skalaOscylogramu), pbSpektrogram.Height];

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


        public void RysujPoZmianieSkali()
        {
            if (logarytmicznaSkala)
                pbSpektrogram.Image = bmpLog;
            else
                pbSpektrogram.Image = bmpLin;
            legenda.OdswiezSkale();
        }       

        private int MinimumLogarytmowaneToDecybel(double minLogatytmowane)
        {
            return 10 * (int)Math.Log10(minLogarytmowane);
        }
        private double DecybelToMinimumLogarytmowane(int decybel)
        {
            return Math.Pow(10, (decybel / 10.0));
        }

       
    }
}
