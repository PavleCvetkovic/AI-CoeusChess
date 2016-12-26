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
        public static long i;
        public static int brojPotezaCrnog=0;
        public Context()
        {
            Stanje = new Tabla();
            naPotezu = 1;
            i = 0;
        }
        public Context(Context c)
        {
            Stanje = new Tabla(c.Stanje);
            naPotezu = c.naPotezu;
            
        }

        #region Methods

        #region ListePoteza
        /// <summary>
        /// Vraca listu mogucih poteza za figuru
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
                    if (p.DaLiJeValidan(tip, trenutneKoordinate))
                    {
                        p.tipFigure = tip;
                        listaZaVracanje.Add(p);
                    }
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
            Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, ctx);
            Koordinate beliTop = NadjiFiguru(Tip.BeliTop, ctx);
            Koordinate crniKraljPozicija = NadjiFiguru(Tip.CrniKralj, ctx);
            //kreira listu svih poteza za figuru
            listaZaVracanje = ctx.listaMogucihPoteza(figura, trenutneKoordinate);
            switch (figura)
            {
                case Tip.CrniKralj:     //treba da nadje polja koja napadaju beli kralj i top

                    List<Tip> beleFigure = new List<Tip>();
                    beleFigure.Add(Tip.BeliKralj);
                    beleFigure.Add(Tip.BeliTop);

                    //vraca listu koordinata svih nadjenih belih figura
                    List<Koordinate> listaBelihFigura = NadjiFigure(beleFigure, ctx);

                    //u listu nedozvoljenih poteza dodaje sve moguce poteze belih figura
                    for (int i = 1; i <= listaBelihFigura.Count; i++)
                    {
                        listaNedozvoljenihPoteza.AddRange(ctx.listaMogucihPoteza(((Tip)ctx.Stanje.matrica[listaBelihFigura[i - 1].x, listaBelihFigura[i - 1].y]), new Potez(listaBelihFigura[i - 1].x, listaBelihFigura[i - 1].y)));
                    }

                    break;

                case Tip.BeliKralj:     //treba da nadje polja koja napada crni kralj

                    
                    
                    //-----
                    
                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj,new Potez(crniKraljPozicija.x, crniKraljPozicija.y));

                    break;

                case Tip.BeliTop:       //treba da nadje polja koja napada crni kralj, a ne stiti beli kralj

                    
                    Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, ctx);

                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj, new Potez(crniKralj.x, crniKralj.y));
                    List<Potez> kretanjeBelogKralja = listaMogucihPoteza(Tip.BeliKralj, new Potez(beliKralj.x, beliKralj.y));

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

        //skoro gotova
        //prilikom rekurzivnog poziva treba da se poziva za naredno stanje
        //treba da se testira 
        public Potez AlphaBeta(Context ctx, int depth, int alpha, int beta)
        {
            Context.i++;
            Potez trenutnoMesto;
            Koordinate trenutneKoordinate;
            //-------------------------------
           // Context zaProsledjivanje = new Context(ctx);
            List<Potez> listaPoteza = new List<Potez>();
            List<Potez> listaKralja = new List<Potez>();
            List<Potez> listaTopa = new List<Potez>();
            Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, ctx);
            Koordinate beliTop = NadjiFiguru(Tip.BeliTop, ctx);
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, ctx);
            if (ctx.naPotezu == (int)Igra.Beli)
            {
                listaKralja = FinalnaListaMogucihPoteza(ctx, new Potez(beliKralj.x, beliKralj.y));
                
                listaTopa = FinalnaListaMogucihPoteza(ctx, new Potez(beliTop.x, beliTop.y));
                listaPoteza.AddRange(listaTopa);
                listaPoteza.AddRange(listaKralja);
            }
            else
                listaPoteza = FinalnaListaMogucihPoteza(ctx, new Potez(crniKralj.x, crniKralj.y));

            Potez najbolji = new Potez(); //ovaj potez vraca funkcija
            Potez pom = new Potez();


                if (depth == 0 || ctx.DaLiJeKraj())
                {
                    najbolji.Value = ctx.Evaluate();
                    return najbolji;
                }

            int v;

            if (ctx.naPotezu == (int)Igra.Beli)
            {
                v = int.MinValue;
                foreach (Potez pot in listaPoteza)
                {
                    trenutneKoordinate = NadjiFiguru(pot.tipFigure, ctx);
                    trenutnoMesto = new Potez(trenutneKoordinate.x, trenutneKoordinate.y);
                    Context zaProsledjivanje = new Context(ctx);
                    zaProsledjivanje.UradiPotez(trenutnoMesto,pot);
                    pom = AlphaBeta(zaProsledjivanje, depth - 1, alpha, beta);
                    if (v < pom.Value)
                    {
                        v = pom.Value;
                        najbolji = pot;
                        najbolji.Value = v;
                        alpha = Math.Max(alpha, v);
                    }
                    if (beta <= alpha)
                        break;
                }

            }
            else
            {
                v = int.MaxValue;
                foreach (Potez pot in listaPoteza)
                {
                    trenutneKoordinate = NadjiFiguru(Tip.CrniKralj,ctx);
                    trenutnoMesto = new Potez(trenutneKoordinate.x, trenutneKoordinate.y);
                    Context zaProsledjivanje = new Context(ctx);
                    zaProsledjivanje.UradiPotez(trenutnoMesto,pot);
                    pom = AlphaBeta(zaProsledjivanje, depth - 1, alpha, beta);
                    if (v > pom.Value)
                    {
                        v = pom.Value;
                        najbolji = pot;
                        najbolji.Value = v;
                        beta = Math.Min(beta, v);
                    }
                    if (beta >= alpha)
                    {
                        break;
                    }
                }
            }



            //--------------------------------
            //lista poteza za onog ko je na potezu
           /* List<Potez> listaPoteza = new List<Potez>(); 
            if (ctx.naPotezu == (int) Igra.Beli)
            {
                Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, ctx);
                Koordinate beliTop = NadjiFiguru(Tip.BeliTop, ctx);
                listaPoteza.AddRange(FinalnaListaMogucihPoteza(ctx,
                    new Potez(beliKralj.x,beliKralj.y)));
                listaPoteza.AddRange(FinalnaListaMogucihPoteza(ctx, new Potez(beliTop.x, beliTop.y))); 
            }
            else
            {
                Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, ctx);
                listaPoteza.AddRange(FinalnaListaMogucihPoteza(ctx, 
                    new Potez(crniKralj.x,crniKralj.y)));
            }

            Potez najbolji = new Potez(); //ovaj potez vraca funkcija
            Potez pom = new Potez();

            if (depth == 0 || ctx.DaLiJeKraj())
            {
                najbolji.Value = ctx.Evaluate();
                return najbolji;
            }
            
            int v;

            if (ctx.naPotezu == (int)Igra.Beli)
            {
                v = int.MinValue;
                foreach (Potez pot in listaPoteza)
                {
                    pom = AlphaBeta(pot.StanjeTable, depth - 1, alpha, beta);


            if (v < pom.Value)
                    {
                        v = pom.Value;
                        najbolji = pot;
                        najbolji.Value = v;
                        alpha = Math.Max(alpha, v);
                    }
                    if (beta <= alpha)
                        break;
                }
                
            }
            else
            {
                v = int.MaxValue;
                foreach (Potez pot in listaPoteza)
                {
                    pom = AlphaBeta(pot.StanjeTable, depth - 1, alpha, beta);
                    if (v > pom.Value)
                    {
                        v = pom.Value;
                        najbolji = pot;
                        najbolji.Value = v;
                        beta = Math.Min(beta,v);
                    }
                    if (beta >= alpha)
                    {
                        break;
                    }
                }
            }
            */

            return najbolji;
        }
        /*
         *  1) Get the king in opposition and give a check, 
         *  2) If the rook is attacked, swing it over. 
         *  No stalemates, no uncertainty regarding which piece to move. And the rectangle method is not totally different, you can see it as 2D extension of the 1D opposition method.
         */
        public int Evaluate()
        {
            if (DaLiJeKraj())
                return 100000;
            List<Potez> listaBelih = new List<Potez>();
            Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, this);
            Koordinate beliTop = NadjiFiguru(Tip.BeliTop, this);
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            if (Context.brojPotezaCrnog - FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count > 4)
                return 1000;
            else if (Context.brojPotezaCrnog - FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count > 3)
                return 900;
            else if (Context.brojPotezaCrnog - FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count > 2)
                return 500;
            else if (Context.brojPotezaCrnog - FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count > 1)
                return 300;
            else
                return -1000;
           
        }
        /// <summary>
        /// Proverava da li je kralj unesenog tipa cekiran
        /// </summary>
        /// <returns></returns>
        public bool DalijeCheck()
        {
            Koordinate koordinate = NadjiFiguru(Tip.CrniKralj, this);
            Koordinate koordinate1, koordinate2;
            koordinate1 = NadjiFiguru(Tip.BeliKralj, this);
            koordinate2 = NadjiFiguru(Tip.BeliTop, this);
            if (koordinate.x == koordinate2.x || koordinate.y == koordinate2.y)
                return true;
            int x = koordinate.x;int y = koordinate.y;
            if (x + 1 < 8)
            {
                if (Stanje.matrica[x + 1, y] == (int)Tip.BeliKralj)//dole
                    return true;
                if (y + 1 < 8)
                {
                    if (Stanje.matrica[x + 1, y + 1] == (int)Tip.BeliKralj)//gore-desno
                        return true;
                }
                if (y - 1 >= 0)
                {
                    if (Stanje.matrica[x+1, y - 1] == (int)Tip.BeliKralj)//gore-levo
                        return true;
                }
            }
            return false;
        }

        //samo proverava da li crni kralj ima potez koji moze da odigra
        //treba da se proveri i da li je napadnut
        public bool DaLiJeKraj()
        {
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            if (FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count==0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Vrsi potez
        /// </summary>
        /// <param name="p1">Destinacija</param>
        
        public void UradiPotez(Potez p1,Potez p2) //p1 mesto na koje ide
        {
            int tmp;
            tmp = Stanje.matrica[p1.x, p1.y];
            Stanje.matrica[p1.x, p1.y] = Stanje.matrica[p2.x, p2.y];
            Stanje.matrica[p2.x, p2.y] = tmp;
            /*
            Koordinate koor = NadjiFiguru(p1.tipFigure, this);
            Stanje.matrica[koor.x, koor.y] = 0;
            Stanje.matrica[p1.x, p1.y] = (int)p1.tipFigure;
            */
            naPotezu = naPotezu ^ 3;
        }
        #endregion
    }
}
