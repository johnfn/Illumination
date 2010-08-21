using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Illumination.Graphics;

namespace Illumination.WorldObjects {
    public abstract class Entity : WorldObject {
        private bool spansMultipleTiles = false;
        private bool blocksMovement = true;
        private bool hidden = false;
        private bool selectable = true;

        public enum DirectionType {
            North,
            East,
            South,
            West,
            SIZE
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

        public Entity(int x, int y, int width, int height) {
            Initialize(x, y, width, height);
        }

        public virtual void Initialize(int x, int y, int width, int height) {
            gridLocation.X = x;
            gridLocation.Y = y;
            gridLocation.Width = width;
            gridLocation.Height = height;

            base.BoundingBox = Display.GridLocationToViewport(gridLocation);
        }
    }
}