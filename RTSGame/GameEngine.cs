using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RTSGame
{
    class GameEngine
    {
        #region Variables
        Map map = new Map();
        #endregion

        #region Methods
        public void Initialize()
        {
            map.populate();
        }

        public void setGame()
        {
            map.SetUnits();
            map.setBuildings();
        }

        public void Combat()
        {
            Random rnd = new Random();

            for (int i = 0; i < map.UnitsOnMapNum - 1; i++)
            {
                map.checkHealth();
                Unit closest = map.UnitsonMap[i].nearestUnit(map.UnitsonMap);
                //map.update(map.UnitsonMap[i], map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y + 1);
                if (map.UnitsonMap[i].Health < 25)
                {
                    map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y);
                }

                if ((map.UnitsonMap[i].InRange(closest)))
                {
                    map.UnitsonMap[i].combat(closest);
                }

                if ((map.UnitsonMap[i].X < closest.X))
                {
                    if(map.Grid[map.UnitsonMap[i].X + 1,map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y);
                    }
                }

                if((map.UnitsonMap[i].X > closest.X))
                {
                    if (map.Grid[map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y);
                    }
                }

                if((map.UnitsonMap[i].Y < closest.Y))
                {
                    if (map.Grid[map.UnitsonMap[i].X, map.UnitsonMap[i].Y + 1] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y + 1);
                    }
                }

                if ((map.UnitsonMap[i].Y > closest.Y))
                {
                    if (map.Grid[map.UnitsonMap[i].X, map.UnitsonMap[i].Y - 1] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y - 1);
                    }
                }
            }
        }

        public void saveGame()
        {
            if (File.Exists(@"SaveGame\MeleeUnits.txt"))
            {
                File.Delete(@"SaveGame\MeleeUnits.txt");
            }

            if (File.Exists(@"SaveGame\RangedUnits.txt"))
            {
                File.Delete(@"SaveGame\RangedUnits.txt");
            }

            if (File.Exists(@"SaveGame\ResourceBuilding.txt"))
            {
                File.Delete(@"SaveGame\ResourceBuilding.txt");
            }

            if (File.Exists(@"SaveGame\FactoryBuilding.txt"))
            {
                File.Delete(@"SaveGame\FactoryBuilding.txt");
            }

            for (int i = 0; i < map.UnitsonMap.Count; i++)
            {
                map.UnitsonMap[i].saveUnit();
            }

            for (int j = 0; j < map.BuildingsOnMap.Count; j++)
            {
                map.BuildingsOnMap[j].saveBuilding();
            }
        }

        public void loadGame()
        {
            map.loadMeleeUnits();
            map.loadRangedUnits();
            map.loadFactory();
            map.loadResource();
        }
        #endregion

        #region Accessors
        public Map Map
        {
            get { return map; }
        }
        #endregion
    }
}
