using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Illumination.WorldObjects {
    public abstract class Entity : WorldObject {
        public enum DirectionType {
            North,
            South,
            East,
            West
        }

        Rectangle gridLocation = new Rectangle();

        public Rectangle GridLocation {
            get { return gridLocation; }
            set { gridLocation = value; }
        }

        public Entity(int x, int y, int width, int height) {
            gridLocation.X = x;
            gridLocation.Y = y;
            gridLocation.Width = width;
            gridLocation.Height = height;

            base.BoundingBox = gridLocation;
        }
    }
}