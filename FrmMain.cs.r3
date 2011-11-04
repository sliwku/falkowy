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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            PlikWave plik = new PlikWave(@"C:\Users\sliwku\Desktop\probki\latwy.wav");
            plik.OtworzPlik();            
        }
    }
}
