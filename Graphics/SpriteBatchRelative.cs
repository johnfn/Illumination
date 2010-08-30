using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Utility;

namespace Illumination.Graphics {
    public class SpriteBatchRelative : SpriteBatch {
        public SpriteBatchRelative(GraphicsDevice device) : base(device) { }

        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Color color) {
            base.Draw(texture, transform(destinationRectangle), color);
        }

        public new void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin, SpriteEffects effects, float layerDepth) {
            base.Draw(texture, transform(destinationRectangle), sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        private Rectangle transform(Rectangle rectangle) {
            return Geometry.Translate(Geometry.Scale(rectangle, Display.Scale),
                -Display.ViewportShift.X, -Display.ViewportShift.Y);
        }
    }
}
