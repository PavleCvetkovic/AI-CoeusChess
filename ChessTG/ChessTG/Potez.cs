﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTG
{
    [Serializable]
    public class Potez
    {
        #region Attributes

        public Tip tipFigure;

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
        public Potez(Potez p)
        {
            this.x = p.x;
            y = p.y;
            Value = p.Value;
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
        /// Provarava validnost poteza(da li moze da skoci na polje)(ne gleda da li je zauzeto, samo da li je potez validan u smislu povlacenja)
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
                    if (this.x != p.x)
                        return true;
                    return false;
                }
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
