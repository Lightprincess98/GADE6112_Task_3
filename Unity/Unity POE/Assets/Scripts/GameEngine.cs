using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public class GameEngine : MonoBehaviour
    {
        [Header("Map Attributes")]
        [SerializeField]
        private int height;
        [SerializeField]
        private int width;
        [Space(5)]

        private Map map = new Map();

        // Use this for initialization
        void Start()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Instantiate(Resources.Load("Environment/Grass"), new Vector3(x * 1, y * 1, -2), Quaternion.identity);
                }
            }

            map.populate();
            map.SetUnits();
            map.setBuildings();
            Debug.Log("Number of units: " + map.UnitsOnMapNum.ToString());
            foreach(Unit temp in map.UnitsonMap)
            {
                var unitType = temp.GetType().ToString();
                var xPos = temp.X * 1;
                var yPos = temp.Y * 1;
                if(unitType.Contains("MeleeUnit"))
                {
                    if(temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f),Quaternion.identity);
                    }
                    else if(temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Full Health/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Full Health/RangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
            }

            Debug.Log("Number of buildings: " + map.BuildingsOnMapNum);
            foreach(Building temp in map.BuildingsOnMap)
            {
                var buildingType = temp.GetType().ToString();
                var xPos = temp.X * 1;
                var yPos = temp.Y * 1;

                if (buildingType.Contains("FactoryBuilding"))
                {
                    if(temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if(temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Full Health/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Full Health/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
            }

            StartCoroutine(Gameplay());
        }

        IEnumerator Gameplay()
        {
            yield return new WaitForSeconds(1);

            var units = GameObject.FindGameObjectsWithTag("Unit");
            var buildings = GameObject.FindGameObjectsWithTag("Building");
            Debug.Log("Number of units: " + map.UnitsOnMapNum.ToString());
            Debug.Log("Number of buildings: " + map.BuildingsOnMapNum.ToString());
            for (int i = 0; i < units.Length; i++)
            {
                Destroy(units[i]);
            }

            for (int i = 0; i < buildings.Length; i++)
            {
                Destroy(buildings[i]);
            }
            yield return new WaitForSeconds(1);

            ((FactoryBuilding)map.BuildingsOnMap[0]).ProduceUnits();
            ((ResourceBuilding)map.BuildingsOnMap[1]).generateResource();
            
            for (int i = 0; i < map.UnitsOnMapNum - 1; i++)
            {
                map.checkHealth();
                Unit closest = map.UnitsonMap[i].nearestUnit(map.UnitsonMap);

                if (map.UnitsonMap[i].Health < 25)
                {
                    map.update(map.UnitsonMap[i], map.UnitsonMap[i].X, map.UnitsonMap[i].Y);
                }

                if ((map.UnitsonMap[i].inRange(closest)))
                {

                    map.UnitsonMap[i].combat(closest);
                }

                if (map.UnitsonMap[i].attackRangeFactory((FactoryBuilding)map.BuildingsOnMap[0]))
                {
                    map.UnitsonMap[i].combatFactory((FactoryBuilding)map.BuildingsOnMap[0]);
                }

                if (map.UnitsonMap[i].attackRangeResource((ResourceBuilding)map.BuildingsOnMap[1]))
                {
                    map.UnitsonMap[i].combatResource((ResourceBuilding)map.BuildingsOnMap[1]);
                }

                if ((map.UnitsonMap[i].X < closest.X))
                {
                    if (map.Grid[map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X + 1, map.UnitsonMap[i].Y);
                    }
                }

                if ((map.UnitsonMap[i].X > closest.X))
                {
                    if (map.Grid[map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y] == ".")
                    {
                        map.update(map.UnitsonMap[i], map.UnitsonMap[i].X - 1, map.UnitsonMap[i].Y);
                    }
                }

                if ((map.UnitsonMap[i].Y < closest.Y))
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

            foreach (Unit temp in map.UnitsonMap)
            {
                var unitType = temp.GetType().ToString();
                var xPos = temp.X * 1;
                var yPos = temp.Y * 1;
                if (unitType.Contains("MeleeUnit") && temp.Health == 100)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Full Health/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if(unitType.Contains("MeleeUnit") && temp.Health <= 80)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 80%/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 80%/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 80%/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("MeleeUnit") && temp.Health <= 60)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 60%/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 60%/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 60%/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("MeleeUnit") && temp.Health <= 40)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 40%/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 40%/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 40%/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("MeleeUnit") && temp.Health <= 20)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 20%/MeleeUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 20%/MeleeUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 20%/MeleeUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("RangedUnit") && temp.Health == 100)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Full Health/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Full HealthRangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if(unitType.Contains("RangedUnit") && temp.Health <= 80)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 80%/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 80%/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 80%/RangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("RangedUnit") && temp.Health <= 60)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 60%/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 60%/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 60%/RangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("RangedUnit") && temp.Health <= 40)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 40%/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 40%/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 40%/RangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (unitType.Contains("RangedUnit") && temp.Health <= 20)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 20%/RangedUnitA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knights"))
                    {
                        Instantiate(Resources.Load("Health 20%/RangedUnitB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Resources.Load("Health 20%/RangedUnitC"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
            }

            foreach (Building temp in map.BuildingsOnMap)
            {
                var buildingType = temp.GetType().ToString();
                var xPos = temp.X * 1;
                var yPos = temp.Y * 1;

                if (buildingType.Contains("FactoryBuilding") && temp.Health == 100)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Full Health/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("FactoryBuilding") && temp.Health <= 80)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 80%/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 80%/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("FactoryBuilding") && temp.Health <= 60)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 60%/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 60%/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("FactoryBuilding") && temp.Health <= 40)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 40%/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 40%/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("FactoryBuilding") && temp.Health <= 20)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 20%/FactoryBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 20%/FactoryBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("ResourceBuilding") && temp.Health == 100)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Full Health/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Full Health/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("ResourceBuilding") && temp.Health <= 80)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 80%/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 80%/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("ResourceBuilding") && temp.Health <= 60)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 60%/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 60%/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("ResourceBuilding") && temp.Health <= 40)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 40%/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 40%/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
                else if (buildingType.Contains("ResourceBuilding") && temp.Health <= 20)
                {
                    if (temp.Faction.Equals("Aisha's Warriors"))
                    {
                        Instantiate(Resources.Load("Health 20%/ResourceBuildingA"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                    else if (temp.Faction.Equals("Kruben's Knigths"))
                    {
                        Instantiate(Resources.Load("Health 20%/ResourceBuildingB"), new Vector3(xPos, yPos, -2.1f), Quaternion.identity);
                    }
                }
            }

            StartCoroutine(Gameplay());
        }
    }
}
