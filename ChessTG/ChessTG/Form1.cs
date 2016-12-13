using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using ChessTG.Properties;

namespace ChessTG
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        int i = 1;
        public static Context kontekst;
        List<Potez> listaMogucihPoteza;
        public Potez trenutnaFiguraZaDodavanje;
        /// <summary>
        /// klikom na neko polje se cuva u pocetnim koordinatama pozicija polja
        /// </summary>
        Potez pocetneKoordinate;
        Potez odredisneKoordinate;
        public Form1()
        {
            InitializeComponent();
            novaIgraToolStripMenuItem_Click(null, null);
            listaMogucihPoteza = new List<Potez>();
        }

        private void novaIgraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kontekst = new Context();
            buttons = new Button[8, 8];
            lblNaPotezu.Text = ((Igra)kontekst.naPotezu).ToString();
            btnDodajBelog.Enabled = true;
            btnDodajCrnog.Enabled = true;
            btnDodajTopa.Enabled = true;
            tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Button b = new Button();
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            b.BackColor = Color.White;
                        else
                            b.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        if (j % 2 != 0)
                            b.BackColor = Color.White;
                        else
                            b.BackColor = Color.LightGreen;
                    }
                    b.Dock = System.Windows.Forms.DockStyle.Fill;
                    b.Font = new System.Drawing.Font("Stencil", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                       
                    b.Name = "button" + i.ToString() + j.ToString();
                    b.Size = new System.Drawing.Size(59, 59);
                    b.Text = "";
                    b.Margin = new System.Windows.Forms.Padding(0);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    b.Tag = i * 10 + j;
                    b.Click += b_Click;
                    tableLayoutPanel1.Controls.Add(b, j, i);
                    buttons[i, j] = b;
                }

        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (i % 2 != 0)//ako se klikne na prazno "i" ostaje 0
            {
                pocetneKoordinate = new Potez((int.Parse((int.Parse(b.Tag.ToString()) / 10).ToString())), (int.Parse((int.Parse(b.Tag.ToString()) % 10).ToString())));
                listaMogucihPoteza = kontekst.listaMogucihPoteza((Tip)kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y], pocetneKoordinate);
                if (kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 0)
                    i--;
                else
                {
                    if ((kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 2 || kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 3) && kontekst.naPotezu == 1)
                        i--;
                    if (kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 1 && kontekst.naPotezu == 2)
                        i--;
                }
            }
            if (i % 2 == 0)//vec je kliknuto jedanput na figuru, pa moze da se izvrsi potez, ukoliko se klikne na validno polje
            {
                bool sadrzi = false;
                odredisneKoordinate= new Potez((int.Parse((int.Parse(b.Tag.ToString()) / 10).ToString())), (int.Parse((int.Parse(b.Tag.ToString()) % 10).ToString())));
                foreach(Potez p in listaMogucihPoteza)
                {
                    if (p.x == odredisneKoordinate.x && p.y == odredisneKoordinate.y)
                        sadrzi = true;
                }
                if (kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] != 3)
                {
                    if (!odredisneKoordinate.DalijeNapadnut(kontekst, (Tip)kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y], odredisneKoordinate))
                        if (sadrzi)
                        {
                            kontekst.UradiPotez(pocetneKoordinate, odredisneKoordinate);
                           // kontekst.naPotezu = kontekst.naPotezu ^ 3;
                        }
                }
                else
                {
                    if (sadrzi)
                    {
                        kontekst.UradiPotez(pocetneKoordinate, odredisneKoordinate);
                       // kontekst.naPotezu = kontekst.naPotezu ^ 3;
                    }
                }
                Refresh();
            }
            i++;
        }

        public void  Refresh()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (kontekst.Stanje.matrica[i, j] == 1)
                    {
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImage = Resources.crnikralj;
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (kontekst.Stanje.matrica[i, j] == 3)
                    {
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImage = Resources.top;
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (kontekst.Stanje.matrica[i, j] == 2)
                    {
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImage = Resources.belikralj;
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                        tableLayoutPanel1.Controls[i*8+j].BackgroundImage = null;
            lblNaPotezu.Text = ((Igra)kontekst.naPotezu).ToString();


        }

        private void btnDodajCrnog_Click(object sender, EventArgs e)
        {
            Form f = new Dodaj(this);
            f.ShowDialog();
            kontekst.Stanje.matrica[trenutnaFiguraZaDodavanje.x-1, trenutnaFiguraZaDodavanje.y-1] = (int)Tip.CrniKralj;
            btnDodajCrnog.Enabled = false;
            Refresh();
        }

        private void btnDodajBelog_Click(object sender, EventArgs e)
        {
            Form f = new Dodaj(this);
            f.ShowDialog();
            kontekst.Stanje.matrica[trenutnaFiguraZaDodavanje.x - 1, trenutnaFiguraZaDodavanje.y - 1] = (int)Tip.BeliKralj;
            btnDodajBelog.Enabled = false;
            Refresh();
        }

        private void btnDodajTopa_Click(object sender, EventArgs e)
        {
            Form f = new Dodaj(this);
            f.ShowDialog();
            kontekst.Stanje.matrica[trenutnaFiguraZaDodavanje.x - 1, trenutnaFiguraZaDodavanje.y - 1] = (int)Tip.BeliTop;
            btnDodajTopa.Enabled = false;
            Refresh();
        }
    }
}
