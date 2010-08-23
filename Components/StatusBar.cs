using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;

namespace Illumination.Components
{
    public class StatusBar : Component
    {
        double fraction = 0;
        Color color;

        public double Fraction
        {
            get { return fraction; }
            set { fraction = value; }
        }

        public StatusBar(Rectangle boundingBox, Color frontColor, Color backColor) : base(MediaRepository.Textures["Blank"], boundingBox, backColor)
        {
            this.color = frontColor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Rectangle frontBarBoundingBox = BoundingBox;
            frontBarBoundingBox.Width = (int)(frontBarBoundingBox.Width * fraction);

            spriteBatch.Draw(MediaRepository.Textures["Blank"], frontBarBoundingBox, color);
        }
        
    }
}
