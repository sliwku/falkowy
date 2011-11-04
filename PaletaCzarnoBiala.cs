using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class PaletaCzarnoBiala : PaletaKolorow
    {
        public PaletaCzarnoBiala()
        {
            UtworzPalete();
        }

        private void UtworzPalete()
        {
            paleta = new Color[256];

            for (int i = 0; i < 256; i++)
            {
                paleta[i] = Color.FromArgb(i, i, i);
            }
        }
    }
}
