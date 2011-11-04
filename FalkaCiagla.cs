using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    public abstract class FalkaCiagla : Falka
    {
        protected double startA;       
        protected double stopA;        
        protected double krokA;       
        protected int dokladnosc;
        
        protected int maxCWTDlaInt16;

        public override int MaxCWTDlaInt16
        {
            get { return maxCWTDlaInt16; }
        }

        public double StartA
        {
            get { return startA; }
        }
        public double StopA
        {
            get { return stopA; }
        }
        public double KrokA
        {
            get { return krokA; }
        }
        public int Dokladnosc
        {
            get { return dokladnosc; }
        }

        public FalkaCiagla(double startA, double stopA, double krokA, int dokladnosc)
        {
            this.startA = startA;
            this.stopA = stopA;
            this.krokA = krokA;
            this.dokladnosc = dokladnosc;            

            UtworzRodzineFalek();
   //         UtworzRodzineFalekF();
            maxCWTDlaInt16 = ObliczMaxCWTDlaInt16();
        }

        public FalkaCiagla() : this(0.1, 2.0, 0.01, 1000) { }      
        

        public void UtworzRodzineFalek()
        {
            falkaDlaA = new double[(int)Math.Round((StopA - StartA) / KrokA, 1) + 1][];

            int i = 0;
            for (double a = StartA; i < falkaDlaA.Length; a += KrokA)
                falkaDlaA[i++] = ObliczFalke(a);
        }
        protected abstract double[] ObliczFalke(double a);

        public void UtworzRodzineFalekF()
        {
            falkaDlaAF = new float[(int)Math.Round((StopA - StartA) / KrokA, 0) + 1][];

            int i = 0;
            for (double a = StartA; i < falkaDlaAF.Length; a += KrokA)
                falkaDlaAF[i++] = ObliczFalkeF(a);
        }

        protected abstract float[] ObliczFalkeF(double a);

        protected int ObliczMaxCWTDlaInt16()
        {
            if (falkaDlaA != null)
            {
                int max = 0;
                for (int i = 0; i < falkaDlaA[0].Length; i++)
                    max += Math.Abs((int)(falkaDlaA[0][i] * falkaDlaA[0][i] * Int16.MaxValue));

                return max;
            }
            else if (falkaDlaAF != null)
            {
                int max = 0;
                for (int i = 0; i < falkaDlaAF[0].Length; i++)
                    max += Math.Abs((int)(falkaDlaAF[0][i] * falkaDlaAF[0][i] * Int16.MaxValue));

                return max;
            }
            return -1;
        }

        public override void Rysuj(PictureBox pbFalka, double a)
        {
            pbFalka.Image = new Bitmap(pbFalka.Width, pbFalka.Height);
            Graphics g = Graphics.FromImage(pbFalka.Image);

            int iA = (int)Math.Round(((a - startA) / krokA),0);
            double dx = pbFalka.Width / (double)falkaDlaA[iA].Length;            
          //  double dx = pbFalka.Width / 700.0;                        
            double dy = pbFalka.Height / 2.0;

            for (int i = 0; i < falkaDlaA[iA].Length - 1; )
                g.DrawLine(new Pen(new SolidBrush(Color.Black)), (int)(i * dx), (int)((-falkaDlaA[iA][i++] + 1) * dy),
                    (int)(i * dx), (int)((-falkaDlaA[iA][i] + 1) * dy));
        }
    }
}
