using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTG
{
    public partial class Dodaj : Form
    {
        Form1 glavna;
        public Dodaj()
        {
            InitializeComponent();
        }
        public Dodaj(Form1 referenca)
            :this()
        {
            glavna = referenca;
        }

        private void btnProsledi_Click(object sender, EventArgs e)
        {
            Potez p = new Potez(int.Parse(numericUpDown1.Value.ToString()), int.Parse(numericUpDown2.Value.ToString()));
            glavna.trenutnaFiguraZaDodavanje = p;
            this.Close();
        }
    }
}
