using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTG
{
    public class Tabla
    {
        #region Attributes
        public int[,] matrica;
        public int pobedio;
        #endregion

        #region Constructors
        public Tabla()
        {
            matrica = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    matrica[i, j] = 0;
        }
        public Tabla(Tabla t)
        {
            matrica = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    matrica[i, j] = t.matrica[i, j];
        }
        #endregion

        /// <summary>
        /// vraca polje iz matrice
        /// </summary>
        /// <returns></returns>
        public int  Polje(int x,int y) 
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
                return matrica[x, y];
            return 0;
        }

        /// <summary>
        /// Vraca listu svih slobodnih polja na tabli
        /// </summary>
        /// <returns></returns>
        public List<Potez>listaSlobodnihPolja()
        {
            List<Potez> lista = new List<Potez>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (matrica[i, j] == 0)
                        lista.Add(new Potez(i, j));
            return lista;
        }

    }
}
