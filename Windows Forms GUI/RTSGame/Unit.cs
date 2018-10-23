/* Unit.cs
 * Creates the base Unit class for the gameplay
 * Ruan Stahnke (18049161)
 * Date: 17 August 2018 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTSGame
{
    abstract class Unit
    {
        #region Instance Variables
        protected string name;
        protected int x, y;
        protected int health;
        protected int speed;
        protected bool attack;
        protected int attackRange;
        protected string faction;
        protected string symbol;

        #endregion

        #region Methods and Constructor

        //Constructor
        public Unit(string name,int x, int y, int health,int speed, bool attack, int attackRange, string faction, string Symbol)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.health = health;
            this.speed = speed;
            this.attack = attack;
            this.AttackRange = AttackRange;
            this.faction = faction;
            this.symbol = Symbol;
        }
        
        //Destructor
        ~Unit()
        {

        }

        abstract public void Move(int xPosition, int yPosition);

        abstract public void combat(Unit enemy);

        abstract public bool InRange(Unit enemy);

        abstract public Unit nearestUnit(List<Unit> list);

        abstract public bool IsDead();

        abstract public string toString();

        abstract public void saveUnit();
#endregion

        #region Accessors

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

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

        public bool Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
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
