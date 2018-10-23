using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSGame
{
    abstract class Building
    {
        #region Instance Variables

        protected int x, y;
        protected int health;
        protected string faction;
        protected string symbol;

        #endregion

        #region Methods and Constrcutor

        public Building(int x, int y, int health, string faction, string symbol)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            this.faction = faction;
            this.symbol = symbol;
        }

        ~Building()
        {

        }

        abstract public bool isDead();

        abstract public string toString();

        abstract public void saveBuilding();
        #endregion

        #region Acessors

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public string Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        #endregion
    }
}
