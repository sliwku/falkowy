using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalizatorFalkowy
{
    public class FalkaMorleta : FalkaCiagla 
    {

        public FalkaMorleta(double startA, double stopA, double krokA, int dokladnosc)
            : base(startA, stopA, krokA, dokladnosc)
        {
                    
        }
        public FalkaMorleta() : this(0.1, 2.0, 0.01, 1000) { }  
        
        protected override double[] ObliczFalke(double a)
        {
            //falka jest niezerowa w pewnym skonczonym przedziale. Obliczanie jej ma sens tylko w tym przedziale.
            //dlugosc w ktorej falka morleta jest niezerowa w zaleznosci od a, mozna wyliczyc korzystajac z funkcji logarytmicznej.
            double wspDlugosciFalki, x;
            if (a > 1)
            {
                x = - Math.Log(a, 1.25) - 4;       //0.8
                wspDlugosciFalki = ((Math.Log(a, 1.25) + 4) - x);        //1.25
            }
            else
            {
                x = - Math.Log(a, 10.0) - 4;
                wspDlugosciFalki = ((Math.Log(a, 10.0) + 4) - x);
            }
            double dx = wspDlugosciFalki;
            wspDlugosciFalki *= 0.125;        // == wspDlFalki /= 8;  (1/8 = 0.125) 
            double[] wynik = new double[(int)(dokladnosc * wspDlugosciFalki)];
            double plusX = dx / wynik.Length;


            for (int i = 0; i < wynik.Length; i++, x += plusX)
            {
                wynik[i] = (1 / Math.Sqrt(a)) * Math.Exp(-0.5 * (x * x) / a) * Math.Cos(5 * x / a);
            }

            nazwaFalkiEnum = NazwaFalkiCiaglej.Morlet; 
            return wynik;
        }       

        protected override float[] ObliczFalkeF(double a)
        {
            //falka jest niezerowa w pewnym skonczonym przedziale. Obliczanie jej ma sens tylko w tym przedziale.
            //dlugosc falki morleta w zaleznosci od a mozna wyliczyc korzystajac z funkcji logarytmicznej.
            double wspDlugosciFalki, x;
            if (a > 1)
            {
                x = Math.Log(a, 0.8) - 4;
                wspDlugosciFalki = ((Math.Log(a, 1.25) + 4) - x);
            }
            else
            {
                x = Math.Log(a, 0.1) - 4;
                wspDlugosciFalki = ((Math.Log(a, 10.0) + 4) - x);
            }
            double dx = wspDlugosciFalki;
            wspDlugosciFalki *= 0.125;        // == wspDlFalki /= 8;  (1/8 = 0.125) 
            float[] wynik = new float[(int)(Dokladnosc * wspDlugosciFalki)];
            double plusX = dx / wynik.Length;


            for (int i = 0; i < wynik.Length; i++, x += plusX)
            {
                wynik[i] = (float)((1 / Math.Sqrt(a)) * Math.Exp(-0.5 * (x * x) / a) * Math.Cos(5 * x / a));
            }

            nazwaFalkiEnum = NazwaFalkiCiaglej.Morlet; 
            return wynik;
        }

       
    }
}
