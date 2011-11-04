using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    /*
     * Założenia:
     * - pb def. dłuższe do rysunku; dzięki temu nie trzeba się bawić z przesuwaniem etykiet pod linie, załatwiamy to tą różnicą,
     */

    /// <summary>
    /// Klasa odpowiada za stworzenie def X i Y pod oscylogramem jak i rysowanie linii siatki skali na oscylogramie
    /// </summary>
    class SkalaOscylogram : Skala
    {        
        private Oscylogram oscylogram;
        private Plik plik;

        private Int32 czestotliwoscProbkowania;

        private double probkaToSekunda;     
        private double pikselToWartoscSygnalu;

        private int iloscLiniiY;

        public double ProbkaToSekunda
        {
            get { return probkaToSekunda; }            
        }
               
        public SkalaOscylogram(Oscylogram oscylogram, PlikWave plikWave,
            PictureBox pbOsyclogram, PictureBox pbSkalaX, PictureBox pbSkalaY)
            : base(pbOsyclogram, pbSkalaX, pbSkalaY)
        {
            this.oscylogram = oscylogram;
            this.plik = plikWave;
            this.czestotliwoscProbkowania = plikWave.CzestotliwoscProbkowania;

            startX = 0;
            dX = 70;

            iloscLiniiY = 5;
            startY = pbRysunek.Height / 2;
            dY = ObliczOdlegloscMiedzyLiniamiY();
            pikselToWartoscSygnalu = ObliczPikselToWartoscSygnalu();

            probkaToSekunda = ObliczProbkaToSekunda();
        }

        /// <summary>
        /// Zwraca stosunek próbki, rozumianej jako piksel na rysunku, do częstotliwości próbkowania sygnału.
        /// Otzrymujemy dzięki temu współczynnik, który przemnożony przez wartość x piksela na rysunku (pamiętajmy o tym, że w przypadku rysunków
        /// z przesunięciem suwaka należy to przesunięcie uwzględnić) daje w wyniku odpowidającą danemu x wartość sekund.
        /// </summary>
        /// <returns></returns>
        private double ObliczProbkaToSekunda()
        {
            return (oscylogram.RysunekSygnalu.DlugoscSygnaluOryginalnego / (double)oscylogram.RysunekSygnalu.DlugoscSygnaluZeskalowanego) 
                / (double)czestotliwoscProbkowania;    
        }
        /// <summary>
        /// Otrzymujemy współczynnik, który mnożąc przez wartość y rysunku daje nam w wyniku wartość sygnału odpowiadającą danemy y.
        /// </summary>      
        private double ObliczPikselToWartoscSygnalu()
        {
            if (pbRysunek.Height % 2 == 0)
                return (plik.MaksymalnaWartosc - plik.MinimalnaWartosc) / (double)pbRysunek.Height;
            else
                return (plik.MaksymalnaWartosc - plik.MinimalnaWartosc) / (double)(pbRysunek.Height - 1);

        }
        /// <summary>
        /// Dla ustalonego parametru ilość linii oblicza wartość w pikselach jaka musi dzielić linie by warunek ilośćLiniiY był spełniony.
        /// </summary>     
        private int ObliczOdlegloscMiedzyLiniamiY()
        {            
            return pbRysunek.Height / (iloscLiniiY - 1) - 1;
        }

        /// <summary>
        /// Stosowany przy zmianie rozmiaru spektrogramu. Oblicza ponownie parametry zależne od wielkośco pictureBoxa
        /// i ustala Graphics do nowych rozmiarów oraz czyści graphics dla def. skali Y        
        public override void Odswiez()
        {
            pikselToWartoscSygnalu = ObliczPikselToWartoscSygnalu();

            startY = pbRysunek.Height / 2;
            dY = ObliczOdlegloscMiedzyLiniamiY();

            probkaToSekunda = ObliczProbkaToSekunda();
            gDefSkalaX = Graphics.FromHwnd(pbSkalaX.Handle);
            gDefSkalaY = Graphics.FromHwnd(pbSkalaY.Handle);
            gDefSkalaY.Clear(pbSkalaY.BackColor);
        }

        /// <summary>
        /// Rysuje Definicje X i Y oraz linie na rysunku. Zależne od przesunięcia rysunku, można więc używać po scrollowaniu rysunku bez 
        /// użycia Odśwież().
        /// </summary>
        public override void Rysuj()
        {
            UstawStartX();
            RysujLinieYiDef();
            RysujLinieX();           
            RysujDefX();
        }
        private void RysujLinieX()
        {            
            for (int x = startX; x < pbRysunek.Width; x += dX)
                oscylogram.GraphicsRysunek.DrawLine(pioroX, x, 0, x, pbRysunek.Height);
        }
        private void RysujDefX()
        {
            gDefSkalaX.Clear(pbSkalaX.BackColor);
            int stopX = pbSkalaX.Width - ROZNICA_PB_DEF_X;
            for (int x = startX, probka = startX + oscylogram.Pzesuniecie; x < stopX; x+= dX, probka += dX)
                gDefSkalaX.DrawString((probka * probkaToSekunda).ToString("0.0000"), czcionkaX, pedzelX, x, 0);
        }
        /// <summary>
        /// Ustawia startX, czyli x pierwszej linii skali; zależna jest od tego też pozycja pierwszej etykiety defX
        /// </summary>
        private void UstawStartX()
        {
            startX = dX - (oscylogram.Pzesuniecie % dX);
        }

        private void RysujLinieYiDef()
        {
            for (int yWdol = startY, yWGore = startY; yWdol <= pbRysunek.Height; yWdol += dY, yWGore -= dY)
            {
                oscylogram.GraphicsRysunek.DrawLine(pioroY, 0, yWdol, pbRysunek.Width, yWdol);
                oscylogram.GraphicsRysunek.DrawLine(pioroY, 0, yWGore, pbRysunek.Width, yWGore);
                RysujDefY(yWdol);
                RysujDefY(yWGore);
            }
        }
        private void RysujDefY()
        {
            gDefSkalaY.Clear(pbSkalaY.BackColor);
            for (int y = startY; y < pbSkalaY.Height; y += dY)
            {
                gDefSkalaY.DrawString(ToWartoscSygnalu(y).ToString(), czcionkaY, pedzelY, 0, y);     
            }
        }
        private void RysujDefY(int y)
        {
            gDefSkalaY.DrawString(ToWartoscSygnalu(y).ToString(), czcionkaY, pedzelY, 0, y);            
        }
        private int ToWartoscSygnalu(int piksel)
        {
            return -((int)(piksel * pikselToWartoscSygnalu) + plik.MinimalnaWartosc + 1);         
        }
        
    }
}
