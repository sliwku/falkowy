using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    /*
     * Zakladamy zgodnosc enum nazwaFalki z kolejnoscia w DropDwonList
     */

    public partial class FrmUstawieniaCWT : Form
    {
        CWT cwt;
        FalkaCiagla falkaCiagla;

        public FrmUstawieniaCWT(CWT cwt)
        {
            InitializeComponent();

            this.cwt = cwt;
            this.falkaCiagla = (FalkaCiagla)cwt.Falka;

            //pamietac o zgodnosci pomiedzy enum NazwaFalki a DropDownlList WyborFalki!!
            DrpWyborFalki.SelectedIndex = (int)cwt.Falka.NazwaFalkiEnum;
            numStartA.Value = Convert.ToDecimal(falkaCiagla.StartA);
            numStopA.Value = Convert.ToDecimal(falkaCiagla.StopA);
            numKrokA.Value = Convert.ToDecimal(falkaCiagla.KrokA);
            numKrokB.Value = Convert.ToDecimal(cwt.KrokB);
            numDokladnosc.Value = Convert.ToDecimal(falkaCiagla.Dokladnosc);
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                cwt.KrokB = (int)numKrokB.Value;
                cwt.Falka.NazwaFalkiEnum = (NazwaFalkiCiaglej)DrpWyborFalki.SelectedIndex;
                
                switch(cwt.Falka.NazwaFalkiEnum)
                {
                    case NazwaFalkiCiaglej.Morlet:
                        cwt.Falka = new FalkaMorleta((double)numStartA.Value, (double)numStopA.Value, 
                        (double)numKrokA.Value, (int)numDokladnosc.Value);
                        break;
                    case NazwaFalkiCiaglej.MexicanHat:
                        cwt.Falka = new FalkaMexicanHat((double)numStartA.Value, (double)numStopA.Value,
                        (double)numKrokA.Value, (int)numDokladnosc.Value);
                        break;

                    default:
                        cwt.Falka = new FalkaMorleta((double)numStartA.Value, (double)numStopA.Value, 
                        (double)numKrokA.Value, (int)numDokladnosc.Value);
                        break;
                }                

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
