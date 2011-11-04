using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    /// <summary>
    /// Klasa odpowidzialna za tworzenie rysunku (tablicy pktow) w danej skali. Przechowuje te wartosci w tablicy 2n dla 
    /// obliczonych juz skal. Moze obliczyc dla nowej skali poprzez przekazanie jej do 
    /// wlasciwosci Skala. Udostepnia takze fukncje oswiez.
    /// </summary>
    abstract class RysunekSygnalu
    {
        public const double PODZIELNIK_SKALI = 2;
        protected const int GRANICA_ZAGESZCZENIE_PIKSELI = 7;

        protected int dlugoscSygnaluOryginalnego;
             
        protected PictureBox pbOscylogram;

        protected int maxSkala;
        protected int skala;
        protected int dlugoscSygnaluZeskalowanego;        
        protected double iloscProbekNaPiksel;
        protected int iloscPunktowNaX;

        protected Point[][] rysunekDlaSkali;

        public int DlugoscSygnaluOryginalnego
        {
            get { return dlugoscSygnaluOryginalnego; }            
        }  

        public int IloscPunktowNaX
        {
            get { return iloscPunktowNaX; }            
        }

        public int MaxSkala
        {
            get { return maxSkala; }
        }
        public double IloscProbekNaPiksel
        {
            get { return iloscProbekNaPiksel; }
        }
        //nie podoba mi sie to. Trzeba to zrobic w jakiejs funkcji..
        public int Skala
        {
            get { return skala; }
            set { 
                skala = value;

                dlugoscSygnaluZeskalowanego = ObliczDlugoscSygnaluZeskalowanego();
                iloscProbekNaPiksel = ObliczIloscProbekNaPiksel();

                if (rysunekDlaSkali[skala] == null)
                    StworzRysunek();
                else
                    iloscPunktowNaX = ObliczIloscPunktowNaX();
            }
        }

        public int DlugoscSygnaluZeskalowanego
        {
            get { return dlugoscSygnaluZeskalowanego; }
        }

        public Point[] Rysunek
        {
            get { return rysunekDlaSkali[skala]; }
        }

        public RysunekSygnalu(int dlugoscSygnalu, PictureBox pbOscylogram)
        {
            this.dlugoscSygnaluOryginalnego = dlugoscSygnalu;
            this.pbOscylogram = pbOscylogram;            
            
            skala = 0;
            
            maxSkala = ObliczMaxSkale();            
            dlugoscSygnaluZeskalowanego = ObliczDlugoscSygnaluZeskalowanego();
            iloscProbekNaPiksel = ObliczIloscProbekNaPiksel();

            rysunekDlaSkali = new Point[maxSkala + 1][];           
        }

        private int ObliczMaxSkale()
        {
            int maxSkala;
            if (dlugoscSygnaluOryginalnego <= pbOscylogram.Width)
                maxSkala = 0;
            else
                maxSkala = (int)Math.Log(dlugoscSygnaluOryginalnego / pbOscylogram.Width, PODZIELNIK_SKALI) + 1;
            if (skala > maxSkala)
                skala = maxSkala;
            return maxSkala;

        }
        private int ObliczDlugoscSygnaluZeskalowanego()
        {
            if (dlugoscSygnaluOryginalnego < pbOscylogram.Width)
                return pbOscylogram.Width;

            if (skala == maxSkala)
                return dlugoscSygnaluOryginalnego;

            return pbOscylogram.Width * (int)Math.Pow(PODZIELNIK_SKALI, skala);
        }
        private double ObliczIloscProbekNaPiksel()
        {
            return (dlugoscSygnaluOryginalnego / (double)dlugoscSygnaluZeskalowanego);
        }
        private int ObliczIloscPunktowNaX()
        {
            if (iloscProbekNaPiksel > GRANICA_ZAGESZCZENIE_PIKSELI)
                return 2;
            else
                return dlugoscSygnaluOryginalnego / dlugoscSygnaluZeskalowanego;
        }

        public void Odswiez()
        {
            maxSkala = ObliczMaxSkale();
            dlugoscSygnaluZeskalowanego = ObliczDlugoscSygnaluZeskalowanego();
            iloscProbekNaPiksel = ObliczIloscProbekNaPiksel();

            rysunekDlaSkali = new Point[maxSkala + 1][];

            StworzRysunek();
        }


        protected abstract void StworzRysunek();        
    }
}
