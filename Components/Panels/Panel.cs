using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics;

namespace Illumination.Components.Panels
{
    public abstract class Panel : Component
    {
        public Panel(Texture2D background, Rectangle boundingBox, Color color) : base(background, boundingBox, color) {}

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
