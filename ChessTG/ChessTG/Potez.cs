using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTG
{
    public class Potez
    {
        #region Attributes
        //stavio sam da bude property umesto atributa
        public Context StanjeTable { get; set;} 
        public int x, y;

        public int Value { get; set; }

        #endregion

        #region Constructors

        public Potez()
        {
            x = 0;
            y = 0;
        }
        public Potez(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Methods

        public bool UGranicama()
        {
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
                return true;
            return false;
        }
        /// <summary>
        /// Provarava validnost poteza
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="p">Polazne koordinate</param>
        /// <returns></returns>
        public bool DaLiJeValidan(Tip tip,Potez p)
        {
            if(tip==Tip.BeliKralj||tip==Tip.CrniKralj)
            {
                if (this.x == p.x - 1)
                {
                    if (this.y == p.y || this.y == p.y - 1 || this.y == p.y + 1)
                        return true;
                        
                }
                if (this.x == p.x + 1)
                {
                    if (this.y == p.y || this.y == p.y - 1 || this.y == p.y + 1)
                        return true;
                }
                if (this.x == p.x)
                {
                    if (this.y == p.y - 1 || this.y == p.y + 1)
                        return true;
                }
                return false;
            }
            if (tip == Tip.BeliTop)
            {
                if (this.x == p.x)
                {
                    if (this.y != p.y)
                        return true;
                    return false;
                }
                if (this.y == p.y)
                {
                    if (this.x != p.y)
                        return true;
                    return false;
                }
            }
            return false;
        }

        //nisam koristio
        public bool DalijeNapadnut(Context c,Tip t,Potez p)
        {
            List<Potez> lista = new List<Potez>();
            List<Potez> PomocnaLista = new List<Potez>();
            int g = 0;
            int g1 = 0;
            for(int i=0;i<8;i++)
                for(int j = 0; j < 8; j++)
                {
                    if (c.Stanje.matrica[i, j] !=0 && c.Stanje.matrica[i,j]!= (int)t)
                    {
                        if ((int)t == 1)
                        {
                            lista = c.listaMogucihPoteza((Tip)c.Stanje.matrica[i, j], new Potez(i, j)); 
                            foreach (Potez pot in lista)
                                 PomocnaLista.Add(pot);
                        }
                        else
                        {
                            for (int ii = 0; ii < 8; ii++)
                                for (int jj = 0; jj < 8; jj++)
                                    if (c.Stanje.matrica[ii, jj] == 1)
                                    {
                                        PomocnaLista = c.listaMogucihPoteza((Tip)c.Stanje.matrica[i, j], new Potez(ii, jj));
                                    }
                        }         
                    }
                }
            foreach(Potez pot in PomocnaLista)
            {
                if (pot.x == p.x && pot.y == p.y)
                    return true;
            }
            return false;
       }

        //potrebno zbog uporedjivanja poteza
        public override bool Equals(object obj)
        {
            var item = obj as Potez;

            if (item == null)
                return false;

            return this.x == item.x && this.y == item.y;
        }

        //takodje potrebno
        public override int GetHashCode()
        {
            int hash = 5;
            return hash * (x + y);
        }

        #endregion
    }
}
