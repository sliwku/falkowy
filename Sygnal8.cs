using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AnalizatorFalkowy
{
    class Sygnal8bit : Sygnal2
    {
        private Byte[] kanal;

        public Sygnal8bit(HScrollBar scrollBar, TrackBar zoomTrackBar, PictureBox pictureBoxSygnal, Byte[] kanal)
            : base(scrollBar, zoomTrackBar, pictureBoxSygnal, kanal.Length)
        {
            this.kanal = kanal;
        }

        public override void Rysuj()
        {
            ;
        }

        public override void Scroll()
        {
            ;
        }

        public override void Zoom()
        {
            ;
        }
    }
}
