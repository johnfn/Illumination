using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Illumination.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.WorldObjects {
    public abstract class Entity : WorldObject {
        private static HashSet<Point> EMPTY_SET = new HashSet<Point>();

        private bool spansMultipleTiles = false;
        private bool blocksMovement = true;
        private bool hidden = false;
        private bool selectable = true;

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public enum DirectionType {
            North,
            East,
            South,
            West,
            SIZE,
            NONE
        }

        Rectangle gridLocation = new Rectangle();

        public bool BlocksMovement {
            get { return blocksMovement; }
            set { blocksMovement = value; }
        }

        public Rectangle GridLocation {
            get { return gridLocation; }
            set { gridLocation = value; }
        }

        public bool DeferDraw {
            get { return spansMultipleTiles; }
            set { spansMultipleTiles = value; }
        }

        public bool Hidden {
            get { return hidden; }
            set { hidden = value; }
        }

        public bool Selectable {
            get { return selectable; }
            set { selectable = value; }
        }

        public Entity() { /* Default constructor */ }

        public Entity(int x, int y, int width, int height, Texture2D texture) {
            Initialize(x, y, width, height, texture);

            name = "Entity";
        }

        public virtual void Initialize(int x, int y, int width, int height, Texture2D texture) {
            gridLocation.X = x;
            gridLocation.Y = y;
            gridLocation.Width = width;
            gridLocation.Height = height;

            base.BoundingBox = Display.GetTextureBoundingBox(texture, gridLocation, 0);
        }

        public abstract Texture2D GetTexture();

        public virtual void UpdateBoundingBox() {
            base.BoundingBox = Display.GetTextureBoundingBox(GetTexture(), gridLocation, 0);
        }

        public virtual IEnumerable<Point> GetRange() {
            return EMPTY_SET;
        }
    }
}