using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace Illumination.WorldObjects
{
    public abstract class Entity : WorldObject
    {
        Rectangle location = new Rectangle ();

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }

        public void Initialize(int x, int y, int width, int height)
        {
            location.X = x;
            location.Y = y;
            location.Width = width;
            location.Height = height;
        }
    }
}