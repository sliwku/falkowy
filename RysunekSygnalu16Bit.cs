using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class RysunekSygnalu16Bit : RysunekSygnalu
    {
        private Int16[] kanal;
        private UInt16[] kanalDodatniOdwrocony;

        public RysunekSygnalu16Bit(Int16[] kanal, System.Windows.Forms.PictureBox pbOscylogram)
            : base(kanal.Length, pbOscylogram)
        {
            this.kanal = kanal;
            StworzOdwroconyKanalDodatni();
            StworzRysunek();
        }
        private void StworzOdwroconyKanalDodatni()
        {
            kanalDodatniOdwrocony = new UInt16[kanal.Length];
            for (int i = 0; i < kanal.Length; i++)
            {
                //Ze wzgledu na to, ze picBox ma wsp dodatnie i wsp (0,0) jest w lewym gornym rogu                
                kanalDodatniOdwrocony[i] = (UInt16)(-kanal[i] - Int16.MinValue);
            }
        }

        protected override void StworzRysunek()
        {            
            if (skala == maxSkala)            
                StworzRysunekWSkali1Do1();            
            else if (iloscProbekNaPiksel <= GRANICA_ZAGESZCZENIE_PIKSELI)            
                StworzRysunekDlaZageszczeniaMalego();
            else
                StworzRysunekDlaZageszczeniaDuzego();
        }
        private void StworzRysunekDlaZageszczeniaMalego()
        {
            iloscPunktowNaX = kanal.Length / dlugoscSygnaluZeskalowanego;

            rysunekDlaSkali[skala] = new Point[kanal.Length];

            double dx = rysunekDlaSkali[skala].Length / (double)dlugoscSygnaluOryginalnego;
            double dy = pbOscylogram.Height / (double)UInt16.MaxValue;
            
            for (int i = 0; i < rysunekDlaSkali[skala].Length; i++)
            {                
                rysunekDlaSkali[skala][i] = new Point((int)(i / iloscProbekNaPiksel),           // X
                    (int)(kanalDodatniOdwrocony[(int)(i * dx)] * dy));                          // Y
            }
        }
        private void StworzRysunekDlaZageszczeniaDuzego()
        {
            iloscPunktowNaX = 2;
            rysunekDlaSkali[skala] = new Point[dlugoscSygnaluZeskalowanego * iloscPunktowNaX];

            double dx = rysunekDlaSkali[skala].Length / (double)dlugoscSygnaluOryginalnego;
            double dy = pbOscylogram.Height / (double)UInt16.MaxValue;

            int j = 0, x = 0;
            for (double i = 0; i < kanal.Length - 1; i += iloscProbekNaPiksel, x++)
            {
                rysunekDlaSkali[skala][j++] = new Point(x,                                                          // X
                    (int)(MaxWartosc(kanalDodatniOdwrocony, (int)i, (int)i + (int)iloscProbekNaPiksel) * dy));      // Y

                rysunekDlaSkali[skala][j++] = new Point(x,                                                          // X
                    (int)(MinWartosc(kanalDodatniOdwrocony, (int)i, (int)i + (int)iloscProbekNaPiksel) * dy));      // Y
            }            
        }
        private UInt16 MaxWartosc(UInt16[] tablica, int startIndex, int stopIndex)
        {
            if (stopIndex > tablica.Length)
                stopIndex = tablica.Length;

            UInt16 max = UInt16.MinValue;
            for (int i = startIndex; i < stopIndex; i++)
            {
                if (max < tablica[i])
                    max = tablica[i];
            }
            return max;
        }
        private UInt16 MinWartosc(UInt16[] tablica, int startIndex, int stopIndex)
        {
            if (stopIndex > tablica.Length)
                stopIndex = tablica.Length;

            UInt16 min = UInt16.MaxValue;
            for (int i = startIndex; i < stopIndex; i++)
            {
                if (min > tablica[i])
                    min = tablica[i];
            }
            return min;
        }
        private void StworzRysunekWSkali1Do1()
        {
            if (pbOscylogram.Width <= kanal.Length)
            {
                iloscPunktowNaX = 1;

                rysunekDlaSkali[skala] = new Point[kanal.Length];

                double dy = pbOscylogram.Height / (double)UInt16.MaxValue;

                for (int i = 0; i < rysunekDlaSkali[skala].Length; i++)
                    rysunekDlaSkali[skala][i] = new Point(i,                        // X
                        (int)(kanalDodatniOdwrocony[i] * dy));                      // Y
            }
            else
            {
                iloscPunktowNaX = 1;

                rysunekDlaSkali[skala] = new Point[pbOscylogram.Width];

                double dx = kanal.Length / (double)pbOscylogram.Width;
                double dy = pbOscylogram.Height / (double)UInt16.MaxValue;

                for (int i = 0; i < rysunekDlaSkali[skala].Length; i++)
                    rysunekDlaSkali[skala][i] = new Point(i,                        // X
                        (int)(kanalDodatniOdwrocony[(int)(i*dx)] * dy));            // Y
            }
        }
    }
}
