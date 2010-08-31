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

        public void DrawRelative(Texture2D texture, Rectangle destinationRectangle, Color color, float layerDepth) {
            DrawRelative(texture, transform(destinationRectangle), null, color, 0, new Vector2(0, 0), SpriteEffects.None, layerDepth);
        }

        public void DrawAbsolute(Texture2D texture, Rectangle destinationRectangle, Color color, float layerDepth) {
            base.Draw(texture, destinationRectangle, color);
        }

        public void DrawRelative(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin, SpriteEffects effects, float layerDepth) {
            base.Draw(texture, transform(destinationRectangle), sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public void DrawAbsolute(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin, SpriteEffects effects, float layerDepth) {
            base.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public new void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color) {
            base.DrawString(spriteFont, text, transform(position), color);
        }

        public void DrawStringAbsolute(SpriteFont spriteFont, string text, Vector2 position, Color color) {
            base.DrawString(spriteFont, text, position, color);
        }  

        Rectangle transform(Rectangle rectangle) {
            return Geometry.Translate(Geometry.Scale(rectangle, Display.Scale),
                -Display.ViewportShift.X, -Display.ViewportShift.Y);
        }

        Vector2 transform(Vector2 vector) {
            return Geometry.Translate(Geometry.Scale(vector, Display.Scale),
                -Display.ViewportShift.X, -Display.ViewportShift.Y);
        }
    }
}
