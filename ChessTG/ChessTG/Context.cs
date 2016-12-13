using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTG
{
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
    public class Context
    {
        public int naPotezu; 
        public Tabla Stanje;
       
        public Context()
        {
            Stanje = new Tabla();
            naPotezu = 1;
        }
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
                        if (p.DaLiJeValidan(tip,trenutneKoordinate))
                            listaZaVracanje.Add(p);
            }
            return listaZaVracanje;
        }
        public void UradiPotez(Potez p1,Potez p2)
        {
            int tmp;
            tmp = Stanje.matrica[p1.x, p1.y];
            Stanje.matrica[p1.x, p1.y] = Stanje.matrica[p2.x, p2.y];
            Stanje.matrica[p2.x, p2.y] = tmp;
            naPotezu = naPotezu ^ 3;
        }
    }
}
