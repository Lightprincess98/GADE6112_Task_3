using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace RTSGame
{
    class FactoryBuilding : Building
    {
        #region Variables
        private int unitsToProduce;
        private int spawnX;
        private int spawnY;
        #endregion

        #region Constructors

        public FactoryBuilding()
        {

        }

        public FactoryBuilding(int x, int y, int health, string faction, string symbol,int unitsToProduce, int spawnX, int spawnY) : base(x, y, health, faction, symbol)
        {
            this.unitsToProduce = unitsToProduce;
            this.spawnX = spawnX;
            this.spawnY = spawnY;
        }

        #endregion

        #region Destructor

        ~FactoryBuilding()
        {

        }

        #endregion

        #region Methods

        public override bool isDead()
        {
            return (this.Health <= 0);
        }

        public Unit ProduceUnits()
        {
            if(unitsToProduce > 0)
            {
                Random rnd = new Random();
                if(rnd.Next(0,2) == 0)
                {
                    //MeleeUnit
                    MeleeUnit mU = new MeleeUnit(spawnX, spawnY, 100, 1, true, 1, this.faction, "M", "Tank");
                    unitsToProduce--;
                    return mU;
                }
                else
                {
                    //RangedUnit
                    RangedUnit rU = new RangedUnit(spawnX, spawnY, 100, 1, true, 1, this.faction, "R", "Ranger");
                    unitsToProduce--;
                    return rU;
                }
            }
            else
            {
                return null;
            }
        }

        public override string toString()
        {
            string output = "X : " + X + Environment.NewLine
                            + "Y : " + Y + Environment.NewLine
                            + "Health: " + Health + Environment.NewLine
                            + "Faction: " + faction + Environment.NewLine
                            + "Symbol" + symbol + Environment.NewLine;

            return output;
        }

        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;
            try
            {
                // open the file
                outFile = new FileStream(@"SaveGame\FactoryBuilding.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);

                // write to the file
                writer.WriteLine(X);
                writer.WriteLine(Y);
                writer.WriteLine(Health);
                writer.WriteLine(Faction);
                writer.WriteLine(Symbol);

                // close the file
                writer.Close();
                outFile.Close();
            }
            catch (Exception fe)
            {
                Debug.WriteLine(fe.Message); // put using System.Diagnostics; at the top
            }
            finally
            {
                if (outFile != null)
                {
                    writer.Close();
                    outFile.Close();
                }
            }
        }

        #endregion
    }
}
