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
        /*The Center Manhattan-Distance is the Manhattan-Distance or number of orthogonal 
          King moves on the otherwise empty board from any square to the four 
          squares {d4, d5, e4, e5} in the center of the board.*/
        public static readonly int[,] CMD ={ 
          { 6, 5, 4, 3, 3, 4, 5, 6 },
          { 5, 4, 3, 2, 2, 3, 4, 5 },
          { 4, 3, 2, 1, 1, 2, 3, 4 },
          { 3, 2, 1, 0, 0, 1, 2, 3 },
          { 3, 2, 1, 0, 0, 1, 2, 3 },
          { 4, 3, 2, 1, 1, 2, 3, 4 },
          { 5, 4, 3, 2, 2, 3, 4, 5 },
          { 6, 5, 4, 3, 3, 4, 5, 6 }
        };
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
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, ctx);
            //kreira listu svih poteza za figuru
            listaZaVracanje = ctx.listaMogucihPoteza(figura, trenutneKoordinate);
            switch (figura)
            {
                case Tip.CrniKralj:     //treba da nadje polja koja napadaju beli kralj i top

                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.BeliKralj, new Potez(beliKralj.x, beliKralj.y));
                    listaNedozvoljenihPoteza.AddRange(listaMogucihPoteza(Tip.BeliTop,new Potez(beliTop.x,beliTop.y)));

                    if (beliKralj.x == beliTop.x || beliKralj.y == beliTop.y)
                    {
                        List<Potez> blokiraniPotezi = new List<Potez>();

                        if (beliKralj.x == beliTop.x)
                        {
                            if (beliKralj.y < beliTop.y)
                                for (int i = 0; i <= beliKralj.y; blokiraniPotezi.Add(new Potez(beliKralj.x, i++))) ;
                            else
                                for (int i = 7; i >= beliKralj.y; blokiraniPotezi.Add(new Potez(beliKralj.x, i--))) ;
                        }
                        else if (beliKralj.y == beliTop.y)
                        {
                            if (beliKralj.x < beliTop.x)
                                for (int i = 0; i <= beliKralj.x; blokiraniPotezi.Add(new Potez(i++, beliKralj.y))) ;
                            else
                                for (int i = 7; i >= beliKralj.x; blokiraniPotezi.Add(new Potez(i--, beliKralj.y))) ;
                        }

                        listaNedozvoljenihPoteza = listaNedozvoljenihPoteza.Except(blokiraniPotezi).ToList();
                    }

                    break;

                case Tip.BeliKralj:     //treba da nadje polja koja napada crni kralj
                    
                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj,new Potez(crniKralj.x, crniKralj.y));

                    break;

                case Tip.BeliTop:       //treba da nadje polja koja napada crni kralj, a ne stiti beli kralj
                                        //i da mu onemoguci da preskace belog kralja

                    listaNedozvoljenihPoteza = listaMogucihPoteza(Tip.CrniKralj, new Potez(crniKralj.x, crniKralj.y));
                    List<Potez> kretanjeBelogKralja = listaMogucihPoteza(Tip.BeliKralj, new Potez(beliKralj.x, beliKralj.y));

                    listaNedozvoljenihPoteza = listaNedozvoljenihPoteza.Except(kretanjeBelogKralja).ToList();

                    //proverava da li beli kralj blokira kretanje topa
                    if (beliKralj.x == beliTop.x)
                    {
                        if (beliKralj.y < beliTop.y)
                            for (int i = 0; i <= beliKralj.y; listaNedozvoljenihPoteza.Add(new Potez(beliKralj.x, i++))) ;
                        else
                            for (int i = 7; i >= beliKralj.y; listaNedozvoljenihPoteza.Add(new Potez(beliKralj.x, i--))) ;
                    }
                    else if (beliKralj.y == beliTop.y)
                    {
                        if(beliKralj.x < beliTop.x)
                            for (int i = 0; i <= beliKralj.x; listaNedozvoljenihPoteza.Add(new Potez(i++, beliKralj.y))) ;
                        else
                            for (int i = 7; i >= beliKralj.x; listaNedozvoljenihPoteza.Add(new Potez(i--, beliKralj.y))) ;
                    }

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

        public Potez AlphaBeta(Context ctx, int depth, int alpha, int beta)
        {

            Potez najbolji = new Potez(); //ovaj potez vraca funkcija
            Potez pom = new Potez();
            Context.i++;
            if (depth == 0 || ctx.DaLiJeKraj())
            {
                najbolji.Value = ctx.Evaluate(depth-4);

                return najbolji;
            }
            Potez trenutnoMesto;
            Koordinate trenutneKoordinate;
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
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }

            return najbolji;
        }
       
        public int Evaluate(int depth)
        {
            if (naPotezu == (int)Igra.Beli)
            {
                if (DalijeNapadnut(Tip.BeliTop))
                    return -1000000;
                return (heuristic_beli(depth));
            }
            else
            {
                return (heuristic_crni(depth));
            }
        }
        /// <summary>
        /// Proverava da li je unesen Tip figure napadnut
        /// </summary>
        /// <returns></returns>
        public bool DalijeNapadnut(Tip tip)
        {
            Koordinate koordinate = NadjiFiguru(tip, this);
            int x = koordinate.x;
            int y = koordinate.y;
            if (Stanje.matrica[x,y] == 1)//provera za crnog kralja
            {
                Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, this);
                Koordinate beliTop = NadjiFiguru(Tip.BeliTop, this);
                List<Potez> listaBelogTopa = FinalnaListaMogucihPoteza(this, new Potez(beliTop.x, beliTop.y));
                int dwkbk = ChebyshevDistance(beliKralj.x, beliKralj.y, koordinate.x, koordinate.y);
                int dwrbk = ChebyshevDistance(beliTop.x, beliTop.y, koordinate.x, koordinate.y);
                if (koordinate.x == beliTop.x)
                {
                    if (koordinate.x != beliKralj.x)
                        return true;
                    else
                    {
                        if ((beliKralj.y < koordinate.y && koordinate.y < beliTop.y) || (beliTop.y < koordinate.y && koordinate.y < beliKralj.y))
                            return true;
                        else
                        {
                            if (dwrbk < dwkbk)
                                return true;
                        }
                        return false;
                    }
                }
                if (koordinate.y == beliTop.y)
                {
                    if (koordinate.y != beliKralj.y)
                        return true;
                    else
                    {
                        if ((beliKralj.x < koordinate.x && koordinate.x < beliTop.x) || (beliTop.x < koordinate.x && koordinate.x < beliKralj.x))
                            return true;
                        else
                        {
                            if (dwrbk < dwkbk)
                                return true;
                        }
                        return false;
                    }
                }
                if (ChebyshevDistance(beliKralj.x, beliKralj.y, koordinate.x, koordinate.y) == 1) //ovo nikad nece da se bude true
                    return true;
            }
            else //provera  belog topa
            {
                Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
                Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, this);
                if (ChebyshevDistance(crniKralj.x, crniKralj.y, koordinate.x, koordinate.y) == 1)
                    if (ChebyshevDistance(beliKralj.x, beliKralj.y, koordinate.x, koordinate.y) != 1)
                        return true;
            }
            return false;
        }
        public bool DaLiJePat()
        {

            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            if (FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count == 0)
                return true;
            return false;
        }
        /// <summary>
        /// Da li je kraj igre
        /// </summary>
        /// <returns></returns>
        public bool DaLiJeKraj()
        {
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            if (FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count==0)
            {
                if(!DalijeNapadnut(Tip.BeliTop))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Da li je crni kralj matiran
        /// </summary>
        /// <returns></returns>
        public bool DaLiJeMat()
        {
            if (DaLiJeKraj())
                if (DalijeNapadnut(Tip.CrniKralj))
                        return true;
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
            naPotezu = naPotezu ^ 3;
        }
        public int ChebyshevDistance(int x,int y,int x1,int y1)
        {
            return Math.Max(Math.Abs(x1 - x), Math.Abs(y1 - y));
        }
        public int ManhattanDistance(int x,int y,int x1,int y1)
        {
            return ((Math.Abs(x1 - x)) + Math.Abs(y1 - y));
        }
        public int heuristic_beli(int dubina)
        {
            double bonus = 0, penalty = 0;
            double BrojPotezaCrnog = 0;
            Koordinate beliKralj, crniKralj, beliTop;
            beliKralj = NadjiFiguru(Tip.BeliKralj, this);
            crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            beliTop = NadjiFiguru(Tip.BeliTop, this);
            List<Potez> listaCrniKralj = FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y));
            BrojPotezaCrnog = listaCrniKralj.Count;
            double CK_CMD = Context.CMD[crniKralj.x, crniKralj.y]; //central manhattan distance
            double BT_CMD = CMD[beliTop.x, beliTop.y];             //CMD
            if (DaLiJeMat())
                bonus += 1000 / Math.Abs(dubina)+1;
            else if (DaLiJeKraj())
                penalty += 1000 / Math.Abs(dubina)+1;
            if(naPotezu==(int)Igra.Crni)
                if (DalijeNapadnut(Tip.BeliTop)) 
                     penalty += 99999999;
            if (ChebyshevDistance(beliKralj.x, beliKralj.y, crniKralj.x, crniKralj.y) == 2)
                bonus += 20;
            if (ChebyshevDistance(beliTop.x, beliTop.y, crniKralj.x, crniKralj.y) > 3)
                bonus += 50;
            //apsolutna razlika izmedju x i y BT i CK, sto vece to bolje, znaci da je kralj vise ranjiv od topa
            double BT_CK_ydiff = Math.Abs(crniKralj.y - beliTop.y);
            double BT_CK_xdiff = Math.Abs(crniKralj.x - beliTop.x);
            double BT_CK_test = (Math.Max(BT_CK_ydiff, BT_CK_xdiff) / (Math.Min(BT_CK_ydiff, BT_CK_xdiff))+1)-1;
            //da li je beliK izmedju topa i crnogK, ako jeste, kazni ga
            if(beliKralj.y==crniKralj.y&&beliKralj.y==beliTop.y)
                if ((beliTop.y < beliKralj.y && beliKralj.y < crniKralj.y) || (crniKralj.y < beliKralj.y && beliKralj.y < beliTop.y))
                    BT_CK_test = -2 * BT_CK_test;
            if(beliTop.x==beliKralj.x&&beliKralj.x==crniKralj.x)
                if ((beliTop.x < beliKralj.x && beliKralj.x < crniKralj.x) || (crniKralj.x < beliKralj.x && beliKralj.x < beliTop.x))
                    BT_CK_test = -2 * BT_CK_test;
            double BK_CK_man = ManhattanDistance(beliKralj.x, beliKralj.y, crniKralj.x, crniKralj.y);
            double vrati = ((9.7 * CK_CMD + 1.6 * (14 - BK_CK_man) + BT_CK_test - (10 * brojPotezaCrnog / (CK_CMD + 1)) + bonus - penalty)*200);
            return (int)vrati;

        }
        public int heuristic_crni(int dubina)
        {
            Koordinate beliKralj = NadjiFiguru(Tip.BeliKralj, this);
            Koordinate beliTop = NadjiFiguru(Tip.BeliTop, this);
            Koordinate crniKralj = NadjiFiguru(Tip.CrniKralj, this);
            double bonus=0, penalty=0;
            if (DaLiJeMat())
                penalty += 1000 / Math.Abs(dubina)+1;
            else if (DaLiJePat())
                bonus += 1000 / Math.Abs(dubina)+1;
            if (DalijeNapadnut(Tip.CrniKralj))
                penalty += 500;
            if (ChebyshevDistance(crniKralj.x, crniKralj.y, beliTop.x, beliTop.y) == 1) 
                bonus += 250;
            if (ChebyshevDistance(crniKralj.x, crniKralj.y, beliKralj.x, beliKralj.y) == 1)
                bonus += 10;
            int brojPoteza = FinalnaListaMogucihPoteza(this, new Potez(crniKralj.x, crniKralj.y)).Count;
            int CK_CMD = CMD[crniKralj.x, crniKralj.y];
            double BT_CK_diff = Math.Abs(Math.Abs(crniKralj.y - beliTop.y) - Math.Abs(crniKralj.x - beliTop.x));
            double vrati = (-9.3 * BT_CK_diff - 5.7 * CK_CMD + (10 *brojPoteza / (CK_CMD + 1)) + penalty - bonus)*200;
            return (int)vrati;
        }
        #endregion
    }
}
