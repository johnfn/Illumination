using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Illumination.Graphics;

namespace Illumination.Logic {
    public static class World {
        static Tile[,] grid;
        static HashSet <Person> personSet;
        static HashSet <Building> buildingSet;

        public static Tile[,] Grid {
            get { return grid; }
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
        }

        public static Person CreatePerson(int x, int y) {
            Person newPerson = new Person(x, y);
            Grid[x, y].AddEntity(newPerson);
            personSet.Add(newPerson);

            return newPerson;
        }

        public static void RemovePerson(Person person) {

        }

        public static Building CreateBuilding(int x, int y) {
            return null;
        }

        public static void RemoveBuilding(Building building) {

        }
    }
}