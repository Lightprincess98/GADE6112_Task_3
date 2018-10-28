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
        #region Variables
        private string resourceType;
        private int resourceTicks;
        private int resourcesRemaining;
        private int availableResources = 0;
        #endregion

        #region Constructors

        public ResourceBuilding()
        {

        }

        public ResourceBuilding(int x, int y, int health, string faction, string symbol, string resourceType, int resourceTicks, int resourcesRemaining, int availableResources) : base(x,y,health,faction,symbol)
        {
            this.resourceType = resourceType;
            this.resourceTicks = resourceTicks;
            this.resourcesRemaining = resourcesRemaining;
            this.availableResources = availableResources;
        }

        #endregion

        #region Destructor

        ~ResourceBuilding()
        {

        }

        #endregion

        #region Accessors

        public int AvailableResources
        {
            get { return availableResources; }
            set { availableResources = value; }
        }

        #endregion

        #region Methods

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
                            + "Resource Type: " + resourceType + Environment.NewLine
                            + "Resources Available: " + availableResources + Environment.NewLine
                            + "Resources Remaining: " + resourcesRemaining + Environment.NewLine;

            return output;
        }

        public override void save()
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
                writer.WriteLine(resourceType);
                writer.WriteLine(resourceTicks);
                writer.WriteLine(resourcesRemaining);
                writer.WriteLine(availableResources);

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

        public void generateResource()
        {
            if(resourcesRemaining >= 0)
            {
                resourcesRemaining -= resourceTicks;
                availableResources += resourceTicks;
            }
        }

        public void removeResource(int amount)
        {
            availableResources = availableResources - amount;
        }

#endregion
    }
}
