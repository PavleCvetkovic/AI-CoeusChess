using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTG
{
    #region Enums
    public enum Tip
    {
        CrniKralj = 1,
        BeliKralj = 2,
        BeliTop = 3
    }
    public enum Igra
    {
        Crni=1,
        Beli=2
    }
    #endregion

    public struct Koordinate
    {
        public int x;
        public int y;

        public Koordinate(int i, int j) : this()
        {
            this.x = i;
            this.y = j;
        }
    }

    public class Context
    {
        public int naPotezu; 
        public Tabla Stanje;
       
        public Context()
        {
            Stanje = new Tabla();
            naPotezu = 1;
        }

        #region Methods

        #region ListePoteza
        /// <summary>
        /// Vraca listu svih mogucih poteza za figuru(sa potencijalno napadnutim poljima, ne filtira je)
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public List<Potez> listaMogucihPoteza(Tip tip,Potez trenutneKoordinate)
        {
            List<Potez> lista = new List<Potez>();
            List<Potez> listaZaVracanje = new List<Potez>();
            lista = Stanje.listaSlobodnihPolja();
            foreach(Potez p in lista)
            {
                
                    if (p.UGranicama())
                        if (p.DaLiJeValidan(tip,trenutneKoordinate))
                            listaZaVracanje.Add(p);
            }
            return listaZaVracanje;
        }

        /// <summary>
        ///  Kreira konacnu listu mogucih poteza koje figura moze odigradi
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="trenutneKoordinate"></param>
        /// <returns></returns>
        public List<Potez> FinalnaListaMogucihPoteza(Context ctx, Potez trenutneKoordinate)
        {
            List<Potez> listaZaVracanje = new List<Potez>();
            List<Potez> listaNedozvoljenihPoteza = new List<Potez>();
            Tip figura = (Tip)ctx.Stanje.matrica[trenutneKoordinate.x, trenutneKoordinate.y];

            //kreira listu svih poteza za figuru
            listaZaVracanje = ctx.listaMogucihPoteza(figura, trenutneKoordinate);
            switch (figura)
            {
                case Tip.CrniKralj:     //treba da nadje polja koja napadaju beli kralji top

                    List<Tip> beleFigure = new List<Tip>();
                    beleFigure.Add(Tip.BeliKralj);
                    beleFigure.Add(Tip.BeliTop);

                    //vraca listu koordinata svih nadjenih belih figura
                    List<Koordinate> listaBelihFigura = NadjiFigure(beleFigure, ctx);

                    //u listu nedozvoljenih poteza dodaje sve moguce poteze belih figura
                    for (int i = 1; i <= listaBelihFigura.Count; i++)
                    {
                        listaNedozvoljenihPoteza.AddRange(ctx.listaMogucihPoteza(((Tip)ctx.Stanje.matrica[listaBelihFigura[i - 1].x, listaBelihFigura[i - 1].y]),
                            new Potez(listaBelihFigura[i - 1].x, listaBelihFigura[i - 1].y)));
                    }

                    break;

                case Tip.BeliKralj:     //treba da nadje polja koja napada crni kralj

                    Koordinate crniKraljPozicija = NadjiFiguru(Tip.CrniKralj, ctx);

                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj,
                        new Potez(crniKraljPozicija.x, crniKraljPozicija.y));

                    break;

                case Tip.BeliTop:       //treba da nadje polja koja napada crni kralj, a ne stiti beli kralj

                    Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, ctx);
                    Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, ctx);

                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj, new Potez(crniKralj.x, crniKralj.y));
                    List<Potez> kretanjeBelogKralja = listaMogucihPoteza(Tip.BeliKralj,
                        new Potez(beliKralj.x, beliKralj.y));

                    listaNedozvoljenihPoteza = listaNedozvoljenihPoteza.Except(kretanjeBelogKralja).ToList();

                    break;
            }

            //iz liste mogucih poteza brise sve nedozvoljene poteze
            //u klasi potez funkcije Equals i GetHashCode su override-ovane da bi Except mogao da poredi poteze
            listaZaVracanje = listaZaVracanje.Except(listaNedozvoljenihPoteza).ToList();

            return listaZaVracanje;
        }

        #endregion

        #region NadjiFiguru/e
        /// <summary>
        /// Vraca listu koordinata na tabli za trazene figure
        /// </summary>
        /// <param name="figureZaTrazenje"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public List<Koordinate> NadjiFigure(List<Tip> figureZaTrazenje, Context ctx)
        {
            List<Koordinate> lista = new List<Koordinate>();
            int brojFigura = figureZaTrazenje.Count();

            for (int k = 1; k <= brojFigura; k++)
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (ctx.Stanje.matrica[i, j] == (int)figureZaTrazenje[k-1])
                        {
                            lista.Add(new Koordinate(i, j));
                        }
                    }

            return lista;
        }

        /// <summary>
        /// Vraca koordinate jedne trazene figure
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public Koordinate NadjiFiguru(Tip tip, Context ctx)
        {
            Koordinate retVal = new Koordinate(-10, -10);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (ctx.Stanje.matrica[i, j] == (int)tip)
                    {
                        retVal = new Koordinate(i, j);
                    }
                }

            return retVal;
        }

        #endregion


        public void UradiPotez(Potez p1,Potez p2)
        {
            int tmp;
            tmp = Stanje.matrica[p1.x, p1.y];
            Stanje.matrica[p1.x, p1.y] = Stanje.matrica[p2.x, p2.y];
            Stanje.matrica[p2.x, p2.y] = tmp;
            naPotezu = naPotezu ^ 3;
        }
        #endregion
    }
}
