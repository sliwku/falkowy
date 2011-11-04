using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    abstract class Skala
    {
        protected readonly int ROZNICA_PB_DEF_X;

        protected PictureBox pbRysunek;
        protected PictureBox pbSkalaX;
        protected PictureBox pbSkalaY;
        protected Graphics gRysunekSkalaX;
        protected Graphics gRysunekSkalaY;
        protected Graphics gDefSkalaX;
        protected Graphics gDefSkalaY;

        protected int startX;
        protected int dX;
        protected int startY;
        protected int dY;

        protected Color kolorX;
        protected Color kolorY;

        protected Pen pioroX;
        protected Pen pioroY;
        protected Font czcionkaX;
        protected Font czcionkaY;
        protected Brush pedzelX;
        protected Brush pedzelY;

        public Skala(PictureBox pbRysunek, PictureBox pbSkalaX, PictureBox pbSkalaY)
        {
            this.pbRysunek = pbRysunek;
            this.pbSkalaX = pbSkalaX;
            this.pbSkalaY = pbSkalaY;

            ROZNICA_PB_DEF_X = pbSkalaX.Width - pbRysunek.Width;

            gRysunekSkalaX = Graphics.FromHwnd(pbRysunek.Handle);
            gRysunekSkalaY = Graphics.FromHwnd(pbRysunek.Handle);
            gDefSkalaX = Graphics.FromHwnd(pbSkalaX.Handle);
            gDefSkalaY = Graphics.FromHwnd(pbSkalaY.Handle);

            kolorX = Color.Silver;
            kolorY = Color.Silver;

            pioroX = new Pen(new SolidBrush(kolorX));
            pioroY = new Pen(new SolidBrush(kolorY));
            pedzelX = new SolidBrush(Color.Black);
            pedzelY = new SolidBrush(Color.Black);            

            czcionkaX = new Font(new FontFamily("Microsoft Sans Serif"), 12, GraphicsUnit.Pixel);
            czcionkaY = new Font(new FontFamily("Microsoft Sans Serif"), 12, GraphicsUnit.Pixel);            
        }

        public abstract void Rysuj();

        public abstract void Odswiez();
    }
}
