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
        private readonly int szerokoscSkali = 40;
        private readonly int szerokoscRysunku;
        private readonly int miejsceNaTxtPonadRys = 10;
        private readonly int iloscEtykietLin = 6;
        private readonly int dyEtykietLin = (int)(100 / (6.0 - 1.0));

        private int dySkalaLin;
        private int dySkalaLog;

        private Panel pnLegenda;
        private PictureBox pbRysunek;
        private PictureBox pbSkala;
        private StringFormat formatDoPrawej;

        private Graphics gPbSkala;

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
            pbRysunek.Location = new Point(szerokoscSkali, (miejsceNaTxtPonadRys / 2) + 2);
            pbRysunek.Size = new Size(szerokoscRysunku, pnLegenda.Height - miejsceNaTxtPonadRys);

            formatDoPrawej = new StringFormat();
            formatDoPrawej.Alignment = StringAlignment.Far;

            pbSkala.Image = new Bitmap(pbSkala.Width, pbSkala.Height);
            gPbSkala = Graphics.FromImage(pbSkala.Image);

            UtworzRysunek();
            UtworzSkale();
        }

        private void UtworzRysunek()
        {
            pbRysunek.Image = new Bitmap(pbRysunek.Width, pbRysunek.Height);            
            Graphics g = Graphics.FromImage(pbRysunek.Image); 
            Pen pen = new Pen(new SolidBrush(Color.Black));  

            double dc = spektrogram.PaletaKolorow.Paleta.Length / (double)pbRysunek.Height;
            for (int y = 0, c = pbRysunek.Height - 1; y < pbRysunek.Height; y++, c--)
            {
                pen.Color = spektrogram.PaletaKolorow.Paleta[(int)(c * dc)];
                g.DrawLine(pen, 0, y, szerokoscRysunku, y);               
            }
        }

        private void OliczDYSkali()
        {
            dySkalaLin = (int)(pbRysunek.Height / (double)(iloscEtykietLin - 1));
     //       dySkalaLog = 
        }

        private void UtworzSkale()
        {
            OliczDYSkali();
            gPbSkala.Clear(pbSkala.BackColor);
            for (int y = 0, etykieta = 100; y < pbSkala.Height; y += dySkalaLin, etykieta -= dyEtykietLin)
                gPbSkala.DrawString(etykieta.ToString() + " % -", new Font(new FontFamily("Times New Roman"), 8), new SolidBrush(Color.Black),
                    pbSkala.Width, y, formatDoPrawej);
        }
    }
}
