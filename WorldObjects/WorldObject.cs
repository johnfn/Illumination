using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Utility;
using Illumination.Graphics;

namespace Illumination.WorldObjects
{
    public abstract class WorldObject
    {
        protected Rectangle boundingBox;
        public Rectangle BoundingBox 
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public abstract void Draw(SpriteBatchRelative spriteBatch);
    }
}