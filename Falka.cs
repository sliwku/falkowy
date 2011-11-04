using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AnalizatorFalkowy
{
    public abstract class Falka
    {
        protected double[][] falkaDlaA;
        protected float[][] falkaDlaAF;
        protected NazwaFalkiCiaglej nazwaFalkiEnum;

        public NazwaFalkiCiaglej NazwaFalkiEnum
        {
            get { return nazwaFalkiEnum; }
            set { nazwaFalkiEnum = value; }
        }

        public double[][] FalkaDlaA
        {
            get { return falkaDlaA; }
        }        

        public float[][] FalkaDlaAF
        {
            get { return falkaDlaAF; }
        }

        abstract public int MaxCWTDlaInt16
        {
            get;
        }

        public Falka()
        {

        }

        abstract public void Rysuj(System.Windows.Forms.PictureBox pbFalka, double a);       
    }
}
