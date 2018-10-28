using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RTSGame
{
    class Map 
    {
        #region Variables
        private const int MAX_RANDOM_UNITS = 50;
        public const string FIELD_SYMBOL = ".";
        private string[,] grid = new string[20,20];
        private List<Unit> unitsOnMap = new List<Unit>();
        private List<Building> buildingsOnMap = new List<Building>();
        private int unitsOnMapNum = 0;
        private int buildingsOnMapNum = 0;
        #endregion

        #region Accessors

        public string[,] Grid
        {
            get { return grid; }
        }

        public List<Unit> UnitsonMap
        {
            get { return unitsOnMap; }
        }

        public List<Building> BuildingsOnMap
        {
            get { return buildingsOnMap; }
        }

        public int UnitsOnMapNum
        {
            get { return unitsOnMapNum; }
        }

        public int BuildingsOnMapNum
        {
            get { return buildingsOnMapNum; }
        }

        #endregion

        #region Methods

        public void populate()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = FIELD_SYMBOL;
                }
            }
        }

        //Places all the units on the Map.
        public void SetUnits()
        {
            Random rnd = new Random();
            int tempx;
            int tempy;
            for (int i = 0; i < MAX_RANDOM_UNITS; i++)
            {
                do
                {
                    tempx = rnd.Next(0, 19);
                    tempy = rnd.Next(0, 19);
                }
                while (grid[tempx, tempy] != FIELD_SYMBOL);
                if (i < rnd.Next(0, 41))
                {
                    string name;
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    name = rnd.Next(0, 2) == 1 ? "Foot Soldier" : "Tank";
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 3);
                    team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
                    symbol = "M";
                    Unit tmp = new MeleeUnit(tempx, tempy, 100, 1, attackOption, 1, team, symbol,name);
                    unitsOnMap.Add(tmp);
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }
                else
                {
                    string name;
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    name = rnd.Next(0, 2) == 1 ? "Archer" : "Ranger";
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 5);
                    team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
                    symbol = "R";

                    unitsOnMap.Add(new RangedUnit(tempx, tempy, 100, 1, attackOption, 5, team,symbol,name));
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }

                if(i < rnd.Next(41, 51))
                {
                    string name;
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    name = rnd.Next(0, 2) == 1 ? "Foot Soldier" : "Tank";
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 3);
                    team = "Willie's Paladins";
                    symbol = "M";
                    Unit tmp = new MeleeUnit(tempx, tempy, 100, 1, attackOption, 1, team, symbol,name);
                    unitsOnMap.Add(tmp);
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }
                else
                {
                    string name;
                    bool attackOption;
                    int randomAttackRange;
                    string team;
                    string symbol;
                    name = rnd.Next(0, 2) == 1 ? "Archer" : "Ranger";
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 5);
                    team = "Willie's Paladins";
                    symbol = "R";

                    unitsOnMap.Add(new RangedUnit(tempx, tempy, 100, 1, attackOption, 5, team, symbol,name));
                    grid[unitsOnMap[unitsOnMapNum].X, unitsOnMap[unitsOnMapNum].Y] = unitsOnMap[unitsOnMapNum].Symbol;
                    unitsOnMapNum++;
                }
            }
        }

        public void setBuildings()
        {
            string team;
            string symbol;
            Random rnd = new Random();


            team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
            symbol = "$";
            buildingsOnMap.Add(new FactoryBuilding(0, 0, 100, team, symbol,20,1,1));
            grid[0, 0] = buildingsOnMap[buildingsOnMapNum].Symbol;
            buildingsOnMapNum++;

            team = rnd.Next(0, 2) == 1 ? "Aisha's Warriors" : "Kruben's Knights";
            symbol = "#";
            buildingsOnMap.Add(new ResourceBuilding(19, 19, 100, team,symbol,"Gold",5,500,0));
            grid[19, 19] = buildingsOnMap[buildingsOnMapNum].Symbol;
            buildingsOnMapNum++;
        }

        private void moveOnMap(Unit u,int newX, int newY)
        {
            grid[u.X, u.Y] = FIELD_SYMBOL;
            grid[newX, newY] = u.Symbol;

        }

        public void update(Unit u, int newX, int newY)
        {
            if ((newX >= 0 && newX < 20) && (newY >= 0 && newY < 20))
            {
                moveOnMap(u, newX, newY);
                u.move(newX, newY);
            }
        }

        //Checks the health of all the units and remove them from list if needed.
        public void checkHealth()
        {
            for (int i = 0; i < unitsOnMapNum; i++)
            {
                if(unitsOnMap[i].Health >= 10)
                {
                    if(unitsOnMap[i].Faction == "Kruben's Knights")
                    {
                        unitsOnMap[i].Faction = "Aisha's Warriors";
                    }
                    else
                    {
                        unitsOnMap[i].Faction = "Kruben's Knights";
                    }
                }

                if (unitsOnMap[i].isDead())
                {
                    grid[unitsOnMap[i].X, unitsOnMap[i].Y] = FIELD_SYMBOL;
                    unitsOnMap.RemoveAt(i);
                    unitsOnMapNum--;
                }
            }
        }

        public void loadMeleeUnits()
        {
            FileStream inFile = null;
            StreamReader reader = null;
            string input;
            string name;
            int x;
            int y;
            int health;
            int uSpeed;
            bool attackOption;
            int randomAttackRange;
            string team;
            string symbol;
            try
            {
                inFile = new FileStream(@"SaveGame\MeleeUnits.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(inFile);
                input = reader.ReadLine();      // priming read
                while (input != null)
                {
                    name = input;
                    x = int.Parse(reader.ReadLine());
                    y = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    uSpeed = int.Parse(reader.ReadLine());
                    attackOption = bool.Parse(reader.ReadLine());
                    randomAttackRange = int.Parse(reader.ReadLine());
                    team = reader.ReadLine();
                    symbol = reader.ReadLine();
                    MeleeUnit m = new MeleeUnit(x, y, health, uSpeed, attackOption, randomAttackRange, team, symbol,name);
                    unitsOnMap.Add(m);
                    grid[x, y] = symbol;
                    unitsOnMapNum++;
                    input = reader.ReadLine();      // secondary read
                }
                reader.Close();
                inFile.Close();
            }
            catch (Exception fe)
            {
                Debug.WriteLine(fe.Message);
            }
            finally
            {
                if (inFile != null)
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }

        public void loadRangedUnits()
        {
            FileStream inFile = null;
            StreamReader reader = null;
            string input;
            string name;
            int x;
            int y;
            int health;
            int uSpeed;
            bool attackOption;
            int randomAttackRange;
            string team;
            string symbol;
            try
            {
                inFile = new FileStream(@"SaveGame\RangedUnits.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(inFile);
                input = reader.ReadLine(); // priming read
                while (input != null)
                {
                    name = input;
                    x = int.Parse(reader.ReadLine());
                    y = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    uSpeed = int.Parse(reader.ReadLine());
                    attackOption = bool.Parse(reader.ReadLine());
                    randomAttackRange = int.Parse(reader.ReadLine());
                    team = reader.ReadLine();
                    symbol = reader.ReadLine();
                    RangedUnit r = new RangedUnit(x, y, health, uSpeed, attackOption, randomAttackRange, team, symbol,name);
                    unitsOnMap.Add(r);
                    grid[x, y] = symbol;
                    unitsOnMapNum++;
                    input = reader.ReadLine(); // secondary read
                }

                reader.Close();
                inFile.Close();
            }
            catch (Exception fe)
            {
                Debug.WriteLine(fe.Message);
            }
            finally
            {
                if (inFile != null)
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }

        public void loadFactory()
        {
            FileStream inFile = null;
            StreamReader reader = null;
            string input;
            int x;
            int y;
            int health;
            string team;
            string symbol;
            int unitsToproduce;
            int spawnX;
            int spawnY;

            try
            {
                inFile = new FileStream(@"SaveGame\FactoryBuilding.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(inFile);
                input = reader.ReadLine();      // priming read
                while (input != null)
                {
                    x = int.Parse(input);
                    y = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    team = reader.ReadLine();
                    symbol = reader.ReadLine();
                    unitsToproduce = int.Parse(reader.ReadLine());
                    spawnX = int.Parse(reader.ReadLine());
                    spawnY = int.Parse(reader.ReadLine());
                    FactoryBuilding f = new FactoryBuilding(x, y, health, team, symbol, unitsToproduce, spawnX, spawnY);
                    buildingsOnMap.Add(f);
                    grid[x, y] = symbol;
                    buildingsOnMapNum++;
                    input = reader.ReadLine();      // secondary read
                }
                reader.Close();
                inFile.Close();
            }
            catch (Exception fe)
            {
                Debug.WriteLine(fe.Message);
            }
            finally
            {
                if (inFile != null)
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }

        public void loadResource()
        {
            FileStream inFile = null;
            StreamReader reader = null;
            string input;
            int x;
            int y;
            int health;
            string team;
            string symbol;
            string resourceType;
            int resourceTicks;
            int resourcesRemaining;
            int availableResources;
            try
            {
                inFile = new FileStream(@"SaveGame\ResourceBuilding.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(inFile);
                input = reader.ReadLine();      // priming read
                while (input != null)
                {
                    x = int.Parse(input);
                    y = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    team = reader.ReadLine();
                    symbol = reader.ReadLine();
                    resourceType = reader.ReadLine();
                    resourceTicks = int.Parse(reader.ReadLine());
                    resourcesRemaining = int.Parse(reader.ReadLine());
                    availableResources = int.Parse(reader.ReadLine());
                    ResourceBuilding f = new ResourceBuilding(x, y, health, team, symbol,resourceType,resourceTicks,resourcesRemaining,availableResources);
                    buildingsOnMap.Add(f);
                    grid[x, y] = symbol;
                    buildingsOnMapNum++;
                    input = reader.ReadLine();      // secondary read
                }
                reader.Close();
                inFile.Close();
            }
            catch (Exception fe)
            {
                Debug.WriteLine(fe.Message);
            }
            finally
            {
                if (inFile != null)
                {
                    reader.Close();
                    inFile.Close();
                }
            }
        }
        #endregion
    }
}