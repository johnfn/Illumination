using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Illumination.Graphics;

namespace Illumination.Logic {
    public static class World {
        static Tile[,] grid;
        static HashSet<Person> personSet;
        static HashSet<Building> buildingSet;
        static HashSet<Tree> treeSet;

        static LightLogic lightLogic = new LightLogic();

        static bool isNight;
        static bool isDayAndNightToggled = false;

        public static bool IsNight {
            get { return isNight; }
            set {
                isNight = value;
                isDayAndNightToggled = true;
            }
        }

        public static HashSet<Tree> TreeSet {
            get { return treeSet; }
        }

        public static HashSet<Light> LightSet {
            get { return lightLogic.LightSet; }
        }

        public static HashSet<Person> PersonSet {
            get { return personSet; }
        }

        public static HashSet<Building> BuildingSet {
            get { return buildingSet; }
        }

        public static Tile[,] Grid {
            get { return grid; }
        }

        public static HashSet<Entity> GetEntities(int x, int y) {
            if (!InBound(x, y)) {
                return new HashSet<Entity>(); // Return empty set
            } else {
                return grid[x, y].Entities;
            }
        }

        public static void InitalizeWorld(int numRows, int numCols) {
            grid = new Tile[numRows, numCols];
            for (int row = 0; row < numRows; row++) {
                for (int col = 0; col < numCols; col++) {
                    grid[row, col] = new Tile(row, col, Tile.TileType.Grass);
                }
            }

            personSet = new HashSet<Person>();
            buildingSet = new HashSet<Building>();
            treeSet = new HashSet<Tree>();

            isNight = false;
        }

        public static bool InBound(int x, int y) {
            return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1);
        }

        public static bool InBound(Point p) {
            return InBound(p.X, p.Y);
        }

        public static bool IsClear(int x, int y, int width, int height) {
            if (width < 0 || height < 0 || !InBound(x, y) || !InBound(x + width - 1, y + height - 1)) {
                return false;
            }
            
            for (int i = x; i < x + width; i++) {
                for (int j = y; j < y + height; j++) {
                    if (grid[i, j].IsBlocked()) {
                        return false;
                    }
                }
            }
            
            return true;
        }

        public static int GetMovementCost(Point point) {
            if (!InBound(point)) {
                return -1;
            }
            return Grid[point.X, point.Y].GetMovementCost();
        }

        public static Person CreatePerson(int x, int y) {
            if (!IsClear(x, y, 1, 1)) {
                return null;
            }

            Person newPerson = new Person(x, y);
            Grid[x, y].AddEntity(newPerson);
            personSet.Add(newPerson);

            return newPerson;
        }

        public static void RemovePerson(Person person) {
            if (personSet.Contains(person)) {
                Grid[person.GridLocation.X, person.GridLocation.Y].RemoveEntity(person);
                personSet.Remove(person);
            }
        }

        public static Building CreateBuilding(int x, int y, string buildingClass) {
            Type buildingType = Type.GetType(buildingClass);

            Building newBuilding = (Building) Activator.CreateInstance(buildingType);
            newBuilding.Initialize(x, y);

            if (!IsClear(x, y, newBuilding.Width, newBuilding.Height)) {
                return null;
            }

            for (int i = x; i < x + newBuilding.Width; i++) {
                for (int j = y; j < y + newBuilding.Height; j++) {
                    Grid[i, j].AddEntity(newBuilding);
                }
            }
            buildingSet.Add(newBuilding);

            return newBuilding;
        }

        public static void RemoveBuilding(Building building) {
            if (buildingSet.Contains(building)) {
                Rectangle loc = building.GridLocation;
                for (int i = loc.X; i < loc.X + loc.Width; i++) {
                    for (int j = loc.Y; j < loc.Y + loc.Height; j++) {
                        Grid[i, j].RemoveEntity(building);
                    }
                }

                buildingSet.Remove(building);
            }
        }

        public static Tree CreateTree(int x, int y) {
            Tree newTree = new Tree(x, y);
            Grid[x, y].AddEntity(newTree);
            treeSet.Add(newTree);

            return newTree;
        }

        public static void RemoveTree(Tree tree) {
            throw new NotImplementedException();
        }

        public static Light CreateLight(int x, int y) {
            return lightLogic.CreateLight(x, y);
        }

        public static void RemoveLight(Light light) {
            lightLogic.RemoveLight(light);
        }

        public static void NextTimestep() {
            if (isDayAndNightToggled) {
                if (isNight)
                    BeginNight();
                else
                    BeginDay();

                isDayAndNightToggled = false;
            }

            lightLogic.NextTimestep();
        }

        public static void BeginNight() {
            lightLogic.ActivateTrees();
        }

        public static void BeginDay() {
            lightLogic.Clear();
        }
    }
}