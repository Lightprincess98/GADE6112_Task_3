using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace RTSGame
{
    class ResourceBuilding : Building
    {

        private int availableResources;
        private const int RESOURCESTOMAKE = 5;

        public ResourceBuilding(int x, int y, int health, string faction, string symbol) : base(x,y,health,faction,symbol)
        {
        }

        public override bool isDead()
        {
            return (this.Health <= 0);
        }

        public override string toString()
        {
            string output = "X : " + X + Environment.NewLine
                            + "Y : " + Y + Environment.NewLine
                            + "Health: " + Health + Environment.NewLine
                            + "Faction: " + faction + Environment.NewLine
                            + "Symbol" + symbol + Environment.NewLine
                            + "Resources Available: " + availableResources + Environment.NewLine;

            return output;
        }

        public void generateResource()
        {
            availableResources += RESOURCESTOMAKE;
        }

        public void removeResource()
        {
            availableResources = availableResources - 1;
        }

        public int AvailableResources
        {
            get { return availableResources; }
            set { availableResources = value; }
        }

        public override void saveBuilding()
        {
            FileStream outFile = null;
            StreamWriter writer = null;
            try
            {
                // open the file
                outFile = new FileStream(@"SaveGame\ResourceBuilding.txt", FileMode.Append, FileAccess.Write);
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
    }
}
