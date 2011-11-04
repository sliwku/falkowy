using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class Paleta768Standard : PaletaKolorow
    {
        public Paleta768Standard()
        {
            UtworzPalete();
        }

        private void UtworzPalete()
        {
            paleta = new Color[765];

            int j = 0;            

            for (int i = 0; i < 255; i++)
                paleta[j++] = Color.FromArgb(255 - i , 255 - i, 255);
            for (int i = 0; i < 255; i++)
                paleta[j++] = Color.FromArgb(0, i, 255 - i);
            for (int i = 0; i < 255; i++)
                paleta[j++] = Color.FromArgb(i, 255 - i, 0);
        }
    }
}
