/* Unit.cs
 * Creates the base Unit class for the gameplay
 * Ruan Stahnke (18049161)
 * Date: 17 August 2018 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RTSGame
{
    abstract class Unit
    {
        #region Variables
        protected int x, y;
        protected int health;
        protected int speed;
        protected bool attack;
        protected int attackRange;
        protected string faction;
        protected string symbol;
        protected string name;

        #endregion

        #region Constructors

        public Unit()
        {

        }

        //Constructor
        public Unit(int x, int y, int health,int speed, bool attack, int attackRange, string faction, string symbol, string name)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.health = health;
            this.speed = speed;
            this.attack = attack;
            this.AttackRange = AttackRange;
            this.faction = faction;
            this.symbol = symbol;
        }

        #endregion

        #region Destructor

        //Destructor
        ~Unit()
        {

        }

        #endregion

        #region Accessors

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

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Methods

        abstract public void move(int x, int y);

        abstract public void combat(Unit enemy);

        abstract public bool inRange(Unit enemy);

        abstract public Unit nearestUnit(List<Unit> list);

        abstract public bool attackRangeFactory(FactoryBuilding enemy);

        abstract public void combatFactory(FactoryBuilding enemy);

        abstract public bool attackRangeResource(ResourceBuilding enemy);

        abstract public void combatResource(ResourceBuilding enemy);

        abstract public bool isDead();

        abstract public string toString();

        abstract public void save();

        #endregion
    }
}
