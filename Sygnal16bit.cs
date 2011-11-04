using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    class Sygnal16bit : Sygnal2
    {
        private Int16[] kanal;

        public Sygnal16bit(HScrollBar scrollBar, TrackBar zoomTrackBar, PictureBox pictureBoxSygnal, Int16[] kanal)
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
