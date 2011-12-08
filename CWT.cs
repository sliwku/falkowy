using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ParallelArrays;
using FPA = Microsoft.ParallelArrays.FloatParallelArray;
using PA = Microsoft.ParallelArrays.ParallelArrays;

namespace AnalizatorFalkowy
{ 
    public class CWT
    {
        #region Pola

        private int dokladnoscB;        

        private Plik plikSygnal;        
        private Falka falka;        

        private Byte[] przygotowanySygnal8bit;
        private Int16[] przygotowanySygnal16bit;

        private int[,] wynikCWT;
        private int postep;      

        #endregion

        public int Postep
        {
            get { return postep; }            
        }

        public int KrokB
        {
            get { return dokladnoscB; }
            set { dokladnoscB = value; }
        }

        internal Falka Falka
        {
            get { return falka; }
            set { falka = value; }
        }
        internal Plik PlikSygnal
        {
            get { return plikSygnal; }
            set { plikSygnal = value; }
        }

        /// <summary>
        /// CWT[a,b] indeksowane od 0
        /// </summary>
        public int[,] WynikCWT
        {
            get { return wynikCWT; }            
        }

        public int IloscA
        {
            get { return falka.FalkaDlaA.Length; }
        }

        /// <summary>
        /// Zwraca maksymalny maozliwy CWT dla danej falki i sygnalu o wartosciach z zakresu Int16 (-3200, 3200)
        /// </summary>
        public int MaxCWTDlaInt16
        {
            get { return falka.MaxCWTDlaInt16; }
        }

        public CWT(Falka falka, Plik plikSygnal)
            : this(falka, plikSygnal, 100)
        { }
        
        public CWT(Falka falka, Plik plikSygnal, int dokladnoscB)
        {
            this.falka = falka;
            this.plikSygnal = plikSygnal;
            this.dokladnoscB = dokladnoscB;
        }

        public void ObliczCWT()
        {
            if (plikSygnal.Dane16bit != null)
            {
                PrzygotujSygnal(plikSygnal.Dane16bit, ref przygotowanySygnal16bit);
                ObliczCWT(plikSygnal.Dane16bit);
            }
        }
        private void ObliczCWT(Int16[] sygnal)
        {
            double cwt = 0;

            wynikCWT = new int[falka.FalkaDlaA.Length, (sygnal.Length / dokladnoscB) + 1];             
            
            for (postep = 0; postep < falka.FalkaDlaA.Length; postep++)
            {
                for (int b = 0, iB = 0; b < sygnal.Length; b += dokladnoscB, iB++)
                {
                    cwt = 0;
                    for (int i = b, j = 0; j < falka.FalkaDlaA[postep].Length; i++, j++)
                        cwt += przygotowanySygnal16bit[i] * falka.FalkaDlaA[postep][j];

                    wynikCWT[postep, iB] = Math.Abs((int)cwt);
                }
            }
        }

        private void ObliczCWTParallelFX(Int16[] sygnal)
        {
            double cwt = 0;

            wynikCWT = new int[falka.FalkaDlaA.Length, (sygnal.Length / dokladnoscB) + 1];

            int size = falka.FalkaDlaA.Length;
            System.Threading.Parallel.For(0, size, delegate (int iA)           
            {
                for (int b = 0, iB = 0; b < sygnal.Length; b += dokladnoscB, iB++)
                {
                    cwt = 0;
                    for (int i = b, j = 0; j < falka.FalkaDlaA[iA].Length; i++, j++)
                        cwt += przygotowanySygnal16bit[i] * falka.FalkaDlaA[iA][j];

                    wynikCWT[iA, iB] = Math.Abs((int)cwt);
                }
            });
        }

        

        public void ObliczCWTprzezGPU()
        {
            if (plikSygnal.Dane16bit != null)
            {
                PrzygotujSygnal(plikSygnal.Dane16bit, ref przygotowanySygnal16bit);
                ObliczCWTprzezGPU(plikSygnal.Dane16bit);
            }
        }

        private void ObliczCWTprzezGPU(Int16[] sygnal)
        {
            float[] sygnalFloat = new float[sygnal.Length];
            for(int i=0;i<sygnal.Length;i++)
                sygnalFloat[i] = sygnal[i];

            wynikCWT = new int[falka.FalkaDlaAF.Length, (sygnal.Length / dokladnoscB) + 1]; 

            DX9Target evalTarget = new DX9Target();

            FloatParallelArrayParam fpaSygnal = new FloatParallelArrayParam();
            FloatParallelArrayParam fpaFalka = new FloatParallelArrayParam();
            FPA fpCalySygnal = new FPA(sygnalFloat);

            FPA wynik;
            wynik = PA.Abs(PA.Sum(PA.Multiply(fpaSygnal, fpaFalka)));

            for (int iA = 0; iA < falka.FalkaDlaAF.Length; iA++)
            {
                fpaFalka.Bind(new FPA(falka.FalkaDlaAF[iA]));
                for (int b = 0, iB = 0; b < sygnal.Length; b += dokladnoscB, iB++)
                {
                    fpaSygnal.Bind(PA.Section(fpCalySygnal, new SectionSpecifier(b, fpaFalka.GetLength(0))));
                    wynikCWT[iA, iB] = (int)evalTarget.ToArray1D(wynik)[0];
                }
            }
        }

        private void PrzygotujSygnal(Int16[] sygnal, ref Int16[] przygotowanySygnal)
        {
            przygotowanySygnal = new Int16[sygnal.Length + falka.FalkaDlaA[falka.FalkaDlaA.Length - 1].Length];
            int roznicaDlugosci = przygotowanySygnal.Length - sygnal.Length;
            //int polowaRoznicyDlugosci = roznicaDlugosci / 2;
            //for (int i = 0; i < polowaRoznicyDlugosci; i++)
            //    przygotowanySygnal[i] = 0;
            //int j = polowaRoznicyDlugosci;
            //for (int i = 0; i < sygnal.Length; i++, j++)
            //    przygotowanySygnal[j] = sygnal[i];
            //for (int i = j; i < przygotowanySygnal.Length; i++)
            //    przygotowanySygnal[i] = 0;

            for (int i = 0; i < sygnal.Length; i++)
                przygotowanySygnal[i] = sygnal[i];
            for (int i = sygnal.Length; i < przygotowanySygnal.Length; i++)
                przygotowanySygnal[i] = 0;
        }
        private void PrzygotujSygnal(Byte[] sygnal, ref Byte[] przygotowanySygnal)
        {
            przygotowanySygnal = new Byte[sygnal.Length + falka.FalkaDlaA[falka.FalkaDlaA.Length - 1].Length];
            int roznicaDlugosci = przygotowanySygnal.Length - sygnal.Length;
            int polowaRoznicyDlugosci = roznicaDlugosci / 2;
            for (int i = 0; i < polowaRoznicyDlugosci; i++)
                przygotowanySygnal[i] = 0;
            int j = polowaRoznicyDlugosci;
            for (int i = 0; i < sygnal.Length; i++, j++)
                przygotowanySygnal[j] = sygnal[i];
            for (int i = j; i < przygotowanySygnal.Length; i++)
                przygotowanySygnal[i] = 0;
        }       


        public int MaxCWT()
        {
            int max = 0;
            for (int i = 0; i < wynikCWT.GetLength(0); i++)
                for (int j = 0; j < wynikCWT.GetLength(1); j++)
                    if (wynikCWT[i, j] > max)
                        max = wynikCWT[i, j];
            return max;
        }        
        
    }
}
