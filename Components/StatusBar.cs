using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;

namespace Illumination.Components
{
    public class StatusBar : Component
    {
        double fraction = 0;
        Color frontColor;

        public double Fraction
        {
            get { return fraction; }
            set { fraction = value; }
        }

        public StatusBar(Rectangle boundingBox, Color frontColor, Color backColor) : base(MediaRepository.Textures["Blank"], boundingBox, backColor)
        {
            this.frontColor = frontColor;
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative)
        {
            base.Draw(spriteBatch, isRelative);

            Rectangle frontBarBoundingBox = BoundingBox;
            frontBarBoundingBox.Width = (int)(frontBarBoundingBox.Width * fraction);

            if (isRelative) {
                spriteBatch.DrawRelative(MediaRepository.Textures["Blank"], frontBarBoundingBox, frontColor, layerDepth);
            } else {
                spriteBatch.DrawAbsolute(MediaRepository.Textures["Blank"], frontBarBoundingBox, frontColor, layerDepth);
            }
        }
    }
}
