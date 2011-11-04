using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalizatorFalkowy
{
    class FalkaMexicanHat : FalkaCiagla
    {
        public FalkaMexicanHat(double startA, double stopA, double krokA, int dokladnosc)
            : base(startA, stopA, krokA, dokladnosc)
        {

        }
        public FalkaMexicanHat() : this(0.1, 2.0, 0.01, 1000) { }

        protected override double[] ObliczFalke(double a)
        {
            //falka jest niezerowa w pewnym skonczonym przedziale. Obliczanie jej ma sens tylko w tym przedziale.
            //dlugosc w ktorej falka morleta jest niezerowa w zaleznosci od a, mozna wyliczyc korzystajac z funkcji logarytmicznej.
            double wspDlugosciFalki, x;
            if (a > 1)
            {
                x = -Math.Log(a, 1.25) - 5;       //0.8
                wspDlugosciFalki = ((Math.Log(a, 1.25) + 5) - x);        //1.25                
            }
            else
            {
                x = -Math.Log(a, 70.0) - 5;
                wspDlugosciFalki = ((Math.Log(a, 70.0) + 5) - x);
            }
            double dx = wspDlugosciFalki;
            wspDlugosciFalki *= 0.1;        // == wspDlFalki /= 10;  (1/10 = 0.1) 
            double[] wynik = new double[(int)(dokladnosc * wspDlugosciFalki)];
            double plusX = dx / wynik.Length;

            double f = 0.1;
            double c = 1.0;
            double sigma = a;

            for (int i = 0; i < wynik.Length; i++, x += plusX)
            {
                //wynik[i] = (1 / Math.Sqrt(a)) * (1 - 2 * Math.PI * Math.PI * f * f) * Math.Exp(-Math.PI * Math.PI * f * f * x * x);
                //wynik[i] = (1 / Math.Sqrt(a)) * c * (x * x - 1) * Math.Exp(-(x * x) * 0.5);
                wynik[i] = (2 / Math.Sqrt(3 * sigma) * Math.Pow(Math.PI, 0.25)) * (1 - (x * x) / (sigma * sigma)) *
                    Math.Exp(-(x * x) / (2 * sigma * sigma));
            }

            nazwaFalkiEnum = NazwaFalkiCiaglej.MexicanHat;
            return wynik;
        }

        protected override float[] ObliczFalkeF(double a)
        {
            throw new NotImplementedException();
        }
    }
}
