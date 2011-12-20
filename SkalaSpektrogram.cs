using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class SkalaSpektrogram : Skala
    {        
        private Spektrogram spektrogram;
        private Oscylogram oscylogram;
        private double probkaToSekunda;

        private const int MIN_ODLEGL_ETYKIET_Y = 14;        

        /// <summary>
        /// Maksymalna ilość etykiet wartosci Y (a), ograniczenie spowodawne możliowościami wyświetlenia.
        /// Może ich mniej niż obliczonych a.
        /// </summary>
        private int maxIloscEtY;
        private int iloscEty;
        private int dzielnikSkal;
        private int deltaWysokoscSkalaY;
        private double dYs;

        StringFormat formatDoPrawej;

        public SkalaSpektrogram(Spektrogram spektrogram, Oscylogram oscylogram,
            PictureBox pbSpektrogram, PictureBox pbSkalaX, PictureBox pbSkalaY)
            : base(pbSpektrogram, pbSkalaX, pbSkalaY)
        {            
            this.spektrogram = spektrogram;
            this.oscylogram = oscylogram;

            probkaToSekunda = ObliczProbkaToSekunda();
            maxIloscEtY = ObliczMaxEtykietY();
            iloscEty = ObliczIloscEtykiet(out dzielnikSkal);
            dYs = ObliczDY();
            dX = 70;
            deltaWysokoscSkalaY = pbSkalaY.Height - pbSpektrogram.Height;

            formatDoPrawej = new StringFormat();
            formatDoPrawej.Alignment = StringAlignment.Far;
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
                / (double)oscylogram.CzestotliwoscProbkowania;
        }

        public override void Rysuj()
        {
            RysujDefX();
            RysujDefY();
        }
        private void RysujDefX()
        {
            gDefSkalaX.Clear(pbSkalaX.BackColor);
            int stopX = pbSkalaX.Width - ROZNICA_PB_DEF_X;
            for (int x = startX, probka = startX + spektrogram.Przesuniecie; x < stopX; x += dX, probka += dX)
                gDefSkalaX.DrawString((probka * probkaToSekunda).ToString("0.0000"), czcionkaX, pedzelX, x, 0);
        }
        private void RysujDefY()
        {
            gDefSkalaY.Clear(pbSkalaY.BackColor);

            int srodekCzcionki = (int)czcionkaY.Size / 2 + 2;
            for (double y = pbSkalaY.Height - 1 - (czcionkaY.Size + 1); y >= 0; y -= dYs)
                gDefSkalaY.DrawString(PikselToA((int)y - deltaWysokoscSkalaY + srodekCzcionki).ToString("0.00") + " -", czcionkaY, pedzelY, pbSkalaY.Width, (int)y, formatDoPrawej);
        }       
        /// <summary>
        /// Oblicza maksymalną ilość etykiet Y
        /// </summary>
        /// <returns></returns>
        private int ObliczMaxEtykietY()
        {
            return pbRysunek.Height / MIN_ODLEGL_ETYKIET_Y;
        }       
        private int ObliczIloscEtykiet(out int dzielnikSkal)
        {
            dzielnikSkal = 1;

            int iloscSkal = spektrogram.IloscA;
            while (iloscSkal > maxIloscEtY)
            {
                iloscSkal = DzielenieWGore(iloscSkal, 2);
                dzielnikSkal *= 2;
            }
            return iloscSkal;                     
        }
        /// <summary>
        /// Oblicza odleglość pomiędzy etykietami/skalami Y (a)
        /// </summary>
        /// <returns></returns>
        private double ObliczDY()
        {

     //       if (pbRysunek.Height > spektrogram.IloscA)
                return (pbRysunek.Height / (double)spektrogram.IloscA) * dzielnikSkal;

   //         return dzielnikSkal;
        }       
        private double PikselToA(int y)
        {
            y = pbRysunek.Height - 1 - y;       // odwrotnosc; jak to bywa, zabawa zwiazana z tym, ze y=0 jest u gory...
            return spektrogram.StartA + (spektrogram.KrokA * (int)(y * spektrogram.Dy));
        }
        /// <summary>
        /// Dzielenie całkowite z zaokragleniem w gore, np 7/3 = 3
        /// </summary>        
        private int DzielenieWGore(int dzielna, int dzielnik)
        {
            return dzielna % dzielnik > 0 ? (dzielna / dzielnik) + 1 : dzielna / dzielnik;
        }

        /// <summary>
        /// Stosowany przy zmianie rozmiaru spektrogramu. Oblicza ponownie parametry zależne od wielkośco pictureBoxa
        /// i ustala Graphics do nowych rozmiarów oraz czyści graphics dla def. skali Y  
        /// </summary>
        public override void Odswiez()
        {
            probkaToSekunda = ObliczProbkaToSekunda();
            maxIloscEtY = ObliczMaxEtykietY();
            dYs = ObliczDY();
            iloscEty = ObliczIloscEtykiet(out dzielnikSkal);

            gDefSkalaX = Graphics.FromHwnd(pbSkalaX.Handle);
            gDefSkalaY = Graphics.FromHwnd(pbSkalaY.Handle);
        }
    }
}
