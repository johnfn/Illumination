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
        static HashSet <Person> personSet;
        static HashSet <Building> buildingSet;

        static LightLogic lightLogic = new LightLogic();

        public static HashSet<Tree> TreeSet
        {
            get { return treeSet; }
        }

        public static HashSet<Light> LightSet
        {
            get { return lightLogic.LightSet; }
        }

        public static HashSet<Person> PersonSet {
            get { return personSet; }
        }

        public static HashSet <Building> BuildingSet {
            get { return buildingSet; }
        }

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
            treeSet = new HashSet<Tree>();
        }

        public static Person CreatePerson(int x, int y) {
            Person newPerson = new Person(x, y);
            Grid[x,y].AddEntity(newPerson);
            personSet.Add(newPerson);

            return newPerson;
        }

        public static void RemovePerson(Person person) {
            throw new NotImplementedException();
        }

        public static Building CreateBuilding(int x, int y) {
            School newSchool = new School(x, y);
            Grid[x, y].AddEntity(newSchool);
            buildingSet.Add(newSchool);

            return newSchool;
        }

        public static void RemoveBuilding(Building building) {
            throw new NotImplementedException();
        }

        public static Tree CreateTree(int x, int y)
        {
            Tree newTree = new Tree(x, y);
            Grid[x, y].AddEntity(newTree);
            treeSet.Add(newTree);

            return newTree;
        }

        public static void RemoveTree(Tree tree)
        {

        }

        public static Light CreateLight(int x, int y)
        {
            return lightLogic.CreateLight(x, y);
        }

        public static void RemoveLight(Light light)
        {
            lightLogic.RemoveLight(light);
        }

        public static void NextTimestep()
        {
            lightLogic.NextTimestep();
        }

        public static void BeginNight()
        {
            lightLogic.ActivateTrees();
        }
    }
}