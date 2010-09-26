using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Illumination.Graphics;
using Illumination.Logic.Missions;
using Illumination.Graphics.AnimationHandler;
using Illumination.Logic.Missions.Conditions;
using System.Xml.Serialization;
using System.IO;
using Illumination.Components.Panels;
using Illumination.Logic.Missions.Events;

namespace Illumination.Logic {
    public static class World {
        public enum EntityType {
            None,
            Person,
            Tree,
            Building,
            Item
        }

        public const double DAY_TIME_LIMIT = 60;

        /*
         * Enables various hacks that allow us to test
         * functionality faster.
         */

        public static bool cheater = true;

        static Tile[,] grid;
        static HashSet<Person> personSet;
        public static HashSet<Person> PersonSet {
            get { return personSet; }
        }

        static HashSet<Building> buildingSet;
        public static HashSet<Building> BuildingSet {
            get { return buildingSet; }
        }

        static HashSet<Tree> treeSet;
        public static HashSet<Tree> TreeSet {
            get { return treeSet; }
        }

        static HashSet<Item> itemSet;
        public static HashSet<Item> ItemSet {
            get { return itemSet; }
        }

        static Dictionary<ShopItem, int> inventory;
        public static Dictionary<ShopItem, int> Inventory {
            get { return inventory; }
            set { inventory = value; }
        }

        static LightLogic lightLogic = new LightLogic();

        static Mission currentMission = new Mission();
        public static Mission CurrentMission {
            get { return currentMission; }
        }

        static ResearchLogic researchLogic = new ResearchLogic();
        public static ResearchLogic ResearchLogic {
            get { return researchLogic; }
        }

        static ShopItem itemToPlace = null;
        public static ShopItem ItemToPlace {
            get { return itemToPlace; }
            set { itemToPlace = value; }
        }

        static bool isNight;
        public static bool IsNight {
            get { return isNight; }
            set {
                isNight = value;
                isDayAndNightToggled = true;
            }
        }

        static bool isDayAndNightToggled = false;

        static double timeLeft;
        public static double TimeLeft {
            get { return timeLeft; }
            set { timeLeft = value; }
        }

        static int dayCount;
        public static int DayCount {
            get { return dayCount; }
            set { dayCount = value; }
        }

        static int money;
        public static int Money {
            get { return money; }
            set { money = value; }
        }

        static HashSet <Entity> selectedEntities = new HashSet<Entity>();
        public static HashSet<Entity> SelectedEntities {
            get { return selectedEntities; }
        }

        static EntityType selectedEntityType = EntityType.None;
        public static EntityType SelectedEntityType {
            get { return selectedEntityType; }
        }

        static HashSet <Tile> highlightedTiles;

        public static double LightSpeed {
            get { return lightLogic.LightSpeed; }
        }

        public static Tile[,] Grid {
            get { return grid; }
        }

        public static Research GetResearch(int index) {
            return ResearchLogic.GetResearch(index);
        }

        public static HashSet<Light> LightSet {
            get { return lightLogic.LightSet; }
        }

        public static void SelectEntity(Entity entity) {
            EntityType entityType = GetEntityType(entity);
            if (selectedEntityType != entityType) {
                selectedEntities.Clear();

                RemoveAllHighlight();

                selectedEntityType = entityType;
            }
            selectedEntities.Add(entity);

            AddHighlight(entity.GetRange());
        }

        public static void DeselectEntity(Entity entity) {
            selectedEntities.Remove(entity);
            if (selectedEntities.Count == 0) {
                selectedEntityType = EntityType.None;
            }

            UpdateHighlight();
        }

        public static void ClearSelection() {
            selectedEntities.Clear();
            selectedEntityType = EntityType.None;

            RemoveAllHighlight();
        }

        private static EntityType GetEntityType(Entity entity) {
            if (entity is Person) {
                return EntityType.Person;
            } else if (entity is Building) {
                return EntityType.Building;
            } else if (entity is Tree) {
                return EntityType.Tree;
            } else if (entity is Item) { 
               return EntityType.Item; 
            } else {
                return EntityType.None;
            }
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
            itemSet = new HashSet<Item>();
            inventory = new Dictionary<ShopItem, int>();

            highlightedTiles = new HashSet<Tile>();

            isNight = false;
            timeLeft = DAY_TIME_LIMIT;
            dayCount = 0;
            money = 0;

            XmlSerializer serializer = new XmlSerializer(typeof(Mission));
            FileStream fs = new FileStream("test.xml", FileMode.Open);
            currentMission = (Mission) serializer.Deserialize(fs);
        }

        public static bool InBound(int x, int y) {
            return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1);
        }

        public static bool InBound(Point p) {
            return InBound(p.X, p.Y);
        }

        public static bool IsClear(Point p) {
            return IsClear(new Rectangle(p.X, p.Y, 1, 1));
        }

        public static bool IsClear(Rectangle r) {
            if (r.Width < 0 || r.Height < 0 || !InBound(r.X, r.Y) || !InBound(r.X + r.Width - 1, r.Y + r.Height - 1)) {
                return false;
            }

            for (int i = r.X; i < r.X + r.Width; i++) {
                for (int j = r.Y; j < r.Y + r.Height; j++) {
                    if (grid[i, j].IsBlocked()) {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsClear(int x, int y) {
            return IsClear(new Rectangle(x, y, 1, 1));
        }

        public static int GetMovementCost(Point point) {
            if (!InBound(point)) {
                return -1;
            }
            return Grid[point.X, point.Y].GetMovementCost();
        }

        public static bool AddEntity(Entity entity) {
            if (!IsClear(entity.GridLocation)) {
                return false;
            }

            for (int i = entity.GridLocation.X; i < entity.GridLocation.X + entity.GridLocation.Width; i++) {
                for (int j = entity.GridLocation.Y; j < entity.GridLocation.Y + entity.GridLocation.Height; j++) {
                    Grid[i, j].AddEntity(entity);
                }
            }

            if (entity is Person) {
                personSet.Add((Person) entity);
            } else if (entity is Building) {
                buildingSet.Add((Building) entity);
            } else if (entity is Tree) {
                treeSet.Add((Tree) entity);
            } else if (entity is Item) {
                itemSet.Add((Item) entity);
            }

            return true;
        }

        public static void RemoveEntity(Entity entity) {
            if (entity is Person) {
                if (!personSet.Contains((Person) entity)) { return; }
                personSet.Remove((Person) entity);
            } else if (entity is Building) {
                if (!buildingSet.Contains((Building) entity)) { return; }
                buildingSet.Remove((Building) entity);
            } else if (entity is Tree) {
                if (!treeSet.Contains((Tree) entity)) { return; }
                treeSet.Remove((Tree) entity);
            } else if (entity is Item) {
                if (!itemSet.Contains((Item) entity)) { return; }
                itemSet.Remove((Item) entity);
            }

            Grid[entity.GridLocation.X, entity.GridLocation.Y].RemoveEntity(entity);
        }

        public static void AddItemToInventory(ShopItem item) {
            if (!inventory.ContainsKey(item)) {
                inventory[item] = 0;
            }
            inventory[item]++;
        }

        public static void RemoveItemFromInventory(ShopItem item) {
            if (inventory.ContainsKey(item) && --inventory[item] <= 0) {
                inventory.Remove(item);
            }
        }

        public static void PlaceItem(Point location) {
            if (itemToPlace != null && IsClear(location)) {
                Item newItem = itemToPlace.BaseItem.CreateNewItem();
                newItem.SetLocation(location);
                AddEntity(newItem);

                RemoveItemFromInventory(itemToPlace);
                itemToPlace = null;
            }
        }

        public static Person MovePerson(Person person, Point newLocation) {
            if (IsClear(newLocation.X, newLocation.Y)) {
                Grid[person.GridLocation.X, person.GridLocation.Y].RemoveEntity(person);
                Grid[newLocation.X, newLocation.Y].AddEntity(person);
                person.Move(newLocation.X, newLocation.Y);
            }

            return person;
        }

        public static Animation CreateLight(Point location, Light.LightType lightColor, Entity.DirectionType direction) {
            return lightLogic.CreateLight(location, lightColor, direction);
        }

        public static void RemoveLight(Light light) {
            lightLogic.RemoveLight(light);
        }

        public static void NextTimestep(GameTime gameTime) {
            currentMission.Update();

            /* Toggling is done only by user input. */
            if (isDayAndNightToggled) {
                if (isNight) {
                    BeginNight();
                } else {
                    lightLogic.SetLightSpeed(LightLogic.SpeedType.Fast);
                    isNight = true;
                }
                isDayAndNightToggled = false;
            }

            /* Natural transition by game flow. */
            if (!isNight) {
                double elapsedSec = gameTime.ElapsedGameTime.Milliseconds / (double) 1000;
                timeLeft -= elapsedSec;

                if (timeLeft <= 0) {
                    BeginNight();
                    isNight = true;
                }
            } else {
                if (!lightLogic.NextTimestep()) {
                    BeginDay();
                    isNight = false;
                }
            }
        }

        public static void BeginNight() {
            Display.NightOverlay(true);
            lightLogic.ActivateTrees();
            foreach (Person person in personSet) {
                person.RemainingMovement = (int) ((person.MovementRange * 2 / 3.0 + 0.5));
            }

            timeLeft = 0;

            UpdateHighlight();
        }

        public static void BeginDay() {
            Display.NightOverlay(false);
            lightLogic.Clear();
            lightLogic.SetLightSpeed(LightLogic.SpeedType.Normal);
            foreach (Building building in buildingSet) {
                building.ClearLightSequences();
            }
            foreach (Person person in personSet) {
                person.RemainingMovement = person.MovementRange;
            }
            timeLeft = DAY_TIME_LIMIT;
            dayCount++;

            UpdateHighlight();
        }

        public static void AddHighlight(int x, int y, Tile.TileHighlightColor highlightColor) {
            if (InBound(x, y)) {
                highlightedTiles.Add(grid[x, y]);
                grid[x, y].HighlightColor = highlightColor;
            }
        }

        public static void AddHighlight(int x, int y) {
            AddHighlight(x, y, Tile.TileHighlightColor.Green);
        }

        public static void AddHighlight(IEnumerable<Point> points, Tile.TileHighlightColor highlightColor) {
            foreach (Point p in points) {
                if (InBound(p)) {
                    AddHighlight(p.X, p.Y, highlightColor);
                }
            }
        }

        public static void AddHighlight(IEnumerable<Point> points) {
            AddHighlight(points, Tile.TileHighlightColor.Green);
        }

        public static void RemoveHighlight(int x, int y) {
            if (InBound(x, y)) {
                highlightedTiles.Remove(grid[x, y]);
                grid[x, y].HighlightColor = Tile.TileHighlightColor.None;
            }
        }

        public static void RemoveHighlight(IEnumerable<Point> points) {
            foreach (Point p in points) {
                if (InBound(p)) {
                    RemoveHighlight(p.X, p.Y);
                }
            }
        }

        public static void RemoveAllHighlight() {
            foreach (Tile t in highlightedTiles) {
                t.HighlightColor = Tile.TileHighlightColor.None;
            }
            highlightedTiles.Clear();
        }

        public static void UpdateHighlight() {
            RemoveAllHighlight();
            foreach (Entity e in selectedEntities) {
                AddHighlight(e.GetRange());
            }
        }

        public static void ChangeDirection(Entity.DirectionType direction) {
            if (selectedEntityType == EntityType.Person) {
                foreach (Entity e in selectedEntities) {
                    ((Person) e).Direction = direction;
                }
            } else if (selectedEntityType == EntityType.Tree) {
                foreach (Entity e in selectedEntities) {
                    ((Tree) e).Direction = direction;
                }
            } else if (selectedEntityType == EntityType.Item) {
                foreach (Entity e in selectedEntities) {
                    if (e is Mirror) {
                        Mirror.ReflectionType newReflectiontype = Mirror.ReflectionType.NorthWest;
                        if (direction == Entity.DirectionType.East)  newReflectiontype = Mirror.ReflectionType.SouthEast;
                        if (direction == Entity.DirectionType.South) newReflectiontype = Mirror.ReflectionType.SouthWest;
                        if (direction == Entity.DirectionType.West)  newReflectiontype = Mirror.ReflectionType.NorthWest;
                        if (direction == Entity.DirectionType.North) newReflectiontype = Mirror.ReflectionType.NorthEast;
                        
                        ((Mirror) e).Reflection = newReflectiontype;
                        
                    }
                }
            }

        }
    }
}
