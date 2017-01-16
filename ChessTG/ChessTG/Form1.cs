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
                            b.BackColor = Color.LightGreen ;
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
                    if (kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 0)
                        i--;
                    else
                    {
                        //proverava da li je odgovarajuci igrac na potezu
                        if ((kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 2 || kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 3) && kontekst.naPotezu == 1)
                            i--;
                        if (kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y] == 1 && kontekst.naPotezu == 2)
                            i--;
                    }
                listaMogucihPoteza = kontekst.FinalnaListaMogucihPoteza(kontekst, pocetneKoordinate);
                if (listaMogucihPoteza.Count > 0)
                    foreach (Potez p in listaMogucihPoteza)
                        buttons[p.x, p.y].BackColor = Color.LightSkyBlue;

            }
                if (i % 2 == 0)//vec je kliknuto jedanput na figuru, pa moze da se izvrsi potez, ukoliko se klikne na validno polje
                {

                    bool sadrzi = false;
                    odredisneKoordinate = new Potez((int.Parse((int.Parse(b.Tag.ToString()) / 10).ToString())), (int.Parse((int.Parse(b.Tag.ToString()) % 10).ToString())));
                    odredisneKoordinate.tipFigure = (Tip)kontekst.Stanje.matrica[pocetneKoordinate.x, pocetneKoordinate.y];
                    foreach (Potez p in listaMogucihPoteza)
                    {
                        if (p.Equals(odredisneKoordinate))
                            sadrzi = true;
                    }
                   
                    if (sadrzi)
                    {
                        kontekst.UradiPotez(odredisneKoordinate,pocetneKoordinate);
                    }
                    Refresh();
                    label1.Text = kontekst.DalijeNapadnut(Tip.CrniKralj).ToString();
                    label3.Text = kontekst.DalijeNapadnut(Tip.BeliTop).ToString();
                    if (kontekst.DaLiJeKraj())
                    {
                        if (kontekst.DaLiJeMat())
                            MessageBox.Show("MAT!");
                        else
                            MessageBox.Show("PAT!");
                    }
             }
                i++;
        }

        public void  Refresh()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            buttons[i,j].BackColor = Color.White;
                        else
                            buttons[i,j].BackColor = Color.LightGreen;
                    }
                    else
                    {
                        if (j % 2 != 0)
                            buttons[i,j].BackColor = Color.White;
                        else
                            buttons[i,j].BackColor = Color.LightGreen;
                    }

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
                        tableLayoutPanel1.Controls[i * 8 + j].BackgroundImage = null;
                }
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

        private void btnIgraj_Click(object sender, EventArgs e) { 
            if (kontekst.naPotezu == (int)Igra.Beli)
            {
                Potez p = kontekst.AlphaBeta(kontekst, 4, int.MinValue, int.MaxValue);
                lblPotezi.Text = Context.i.ToString();
                Context.i = 0;
                Koordinate mestoFigureKojaIgra = kontekst.NadjiFiguru(p.tipFigure, kontekst);
                Koordinate beliTop = kontekst.NadjiFiguru(Tip.BeliTop, kontekst);
                Context context = new Context(kontekst);
                context.UradiPotez(new Potez(mestoFigureKojaIgra.x, mestoFigureKojaIgra.y), p);
                if (!context.DalijeNapadnut(Tip.BeliTop))
                    kontekst.UradiPotez(new Potez(mestoFigureKojaIgra.x, mestoFigureKojaIgra.y), p);
                else
                {
                    List<Potez> listaFigureKojaIgra = kontekst.FinalnaListaMogucihPoteza(kontekst, new Potez(beliTop.x, beliTop.y));
                    if (listaFigureKojaIgra.Contains(new Potez(beliTop.x, 0)))
                         kontekst.UradiPotez(new Potez(beliTop.x, beliTop.y), new Potez(beliTop.x, 0));
                    else
                        kontekst.UradiPotez(new Potez(beliTop.x, beliTop.y), new Potez(beliTop.x, 7));
                }
                Refresh();
                label1.Text = kontekst.DalijeNapadnut(Tip.CrniKralj).ToString();
                label3.Text = kontekst.DalijeNapadnut(Tip.BeliTop).ToString();
               
                if (kontekst.DaLiJeKraj())
                {
                    if (kontekst.DaLiJeMat())
                        MessageBox.Show("MAT!");
                    else if(kontekst.DaLiJePat())
                        MessageBox.Show("PAT!");
                    kontekst.Seralization(Context.transposTable);
                }
            }
            else
            {
                Context.i = 0;
                Potez p = kontekst.AlphaBeta(kontekst, 4, int.MinValue, int.MaxValue);
                lblPotezi.Text = Context.i.ToString();
                Koordinate mestoFigureKojaIgra = kontekst.NadjiFiguru(p.tipFigure, kontekst);
                kontekst.UradiPotez(new Potez(mestoFigureKojaIgra.x, mestoFigureKojaIgra.y), p);
                Refresh();
                label1.Text = kontekst.DalijeNapadnut(Tip.CrniKralj).ToString();
                label3.Text = kontekst.DalijeNapadnut(Tip.BeliTop).ToString();
                if (kontekst.DaLiJeKraj())
                {
                    if (kontekst.DaLiJeMat())
                        MessageBox.Show("MAT!");
                    else if(kontekst.DaLiJePat())
                        MessageBox.Show("PAT!");
                    kontekst.Seralization(Context.transposTable);
                }
            }
        }
    }
}
