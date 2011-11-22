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
    public partial class FrmUstawMINdB : Form
    {
        private Spektrogram spektrogram;
        private ToolStripMenuItem menuUstaw;

        public FrmUstawMINdB(Spektrogram spektrogram, ToolStripMenuItem menuUstaw)
        {
            this.spektrogram = spektrogram;
            this.menuUstaw = menuUstaw;

            InitializeComponent();

            numMindB.Value = Convert.ToDecimal(spektrogram.MinDecybeli);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            spektrogram.MinDecybeli = (int)numMindB.Value;
            UstawZaznaczenieMenu();
            this.Close();
        }
        private void UstawZaznaczenieMenu()
        {
            foreach (ToolStripMenuItem menu in menuUstaw.DropDownItems)
                menu.Checked = false;
            switch (spektrogram.MinDecybeli)
            {
                case -40:
                    ((ToolStripMenuItem)(menuUstaw.DropDownItems[0])).Checked = true;
                    break;
                case -50:
                    ((ToolStripMenuItem)(menuUstaw.DropDownItems[1])).Checked = true;
                    break;
                case -60:
                    ((ToolStripMenuItem)(menuUstaw.DropDownItems[2])).Checked = true;
                    break;
                case -70:
                    ((ToolStripMenuItem)(menuUstaw.DropDownItems[3])).Checked = true;
                    break;
                case -80:
                    ((ToolStripMenuItem)(menuUstaw.DropDownItems[4])).Checked = true;
                    break;
            }
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
