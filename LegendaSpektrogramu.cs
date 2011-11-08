using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class LegendaSpektrogramu
    {
        private readonly int szerokoscSkali = 10;
        private readonly int szerokoscRysunku;

        private Panel pnLegenda;
        private PictureBox pbRysunek;
        private PictureBox pbSkala;

        private Spektrogram spektrogram;

        public LegendaSpektrogramu(Panel pnLegenda, Spektrogram spektrogram)
        {
            this.pnLegenda = pnLegenda;
            this.spektrogram = spektrogram;

            szerokoscRysunku = pnLegenda.Width - szerokoscSkali;

            pbRysunek = new PictureBox();
            pbSkala = new PictureBox();
            pnLegenda.Controls.Add(pbRysunek);
            pnLegenda.Controls.Add(pbSkala);            
            pbSkala.Location = new Point(0, 0);
            pbSkala.Size = new Size(szerokoscSkali, pnLegenda.Height);            
            pbRysunek.Location = new Point(szerokoscSkali, 0);
            pbRysunek.Size = new Size(szerokoscRysunku, pnLegenda.Height);
            

            UtworzRysunek();
        }

        private void UtworzRysunek()
        {
            pbRysunek.Image = new Bitmap(pbRysunek.Width, pbRysunek.Height);            
            Graphics g = Graphics.FromImage(pbRysunek.Image); 
            Pen pen = new Pen(new SolidBrush(Color.Black));  

            double dy = spektrogram.PaletaKolorow.Paleta.Length / (double)pbRysunek.Height;
            for (int y = 0; y < pbRysunek.Height; y++)
            {
                pen.Color = spektrogram.PaletaKolorow.Paleta[(int)(y * dy)];
                g.DrawLine(pen, 0, y, szerokoscRysunku, y);               
            }
        }
    }
}
