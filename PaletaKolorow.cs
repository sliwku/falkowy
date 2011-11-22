using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AnalizatorFalkowy
{
    public abstract class PaletaKolorow
    {
        protected Color[] paleta;

        public Color[] Paleta
        {
            get { return paleta; }            
        }
    }
}
