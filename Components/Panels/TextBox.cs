using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.MouseHandler;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Logic.ActionHandler;
using Illumination.Data;
using Illumination.Utility;
using Illumination.Graphics;

namespace Illumination.Components
{
    public class TextBox : Component
    {
        SpriteFont font;
        string text;
        Vector2 textLocation;
        Color textColor;

        public string Text
        {
            get { return text; }
            set {
                text = value;
                UpdateTextLocation();
            }
        }

        public TextBox(Rectangle boundingBox, string text, Color textColor)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, MediaRepository.Fonts["DefaultFont"], textColor) { }

        public TextBox(Rectangle boundingBox, string text, Color textColor, SpriteFont font)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, font, textColor) { }

        public TextBox(Texture2D background, Rectangle boundingBox, Color color, string text, SpriteFont font, Color textColor)
            : base(background, boundingBox, color)
        {
            this.text = text;
            this.font = font;
            this.textColor = textColor;

            textLocation = Geometry.CenterText(text, font, boundingBox);
        }

        public void UpdateTextLocation()
        {
            textLocation = Geometry.CenterText(text, font, BoundingBox);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, text, textLocation, textColor);
        }
    }
}
