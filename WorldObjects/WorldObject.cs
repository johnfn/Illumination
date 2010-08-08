using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects
{
    public abstract class WorldObject
    {
        Rectangle boundingBox;

        public Rectangle BoundingBox 
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}