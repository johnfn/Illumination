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

namespace Illumination.Components {
    public class TextBox : Component {
        SpriteFont font;
        string text;
        Vector2 textLocation;
        Color textColor;
        AlignType align;

        public enum AlignType {
            Left,
            Center,
            Right
        }

        public string Text {
            get { return text; }
            set {
                text = value;
                Update();
            }
        }

        public AlignType Align {
            get { return align; }
            set { align = value; }
        }


        public TextBox(Rectangle boundingBox, string text, Color textColor, AlignType alignType)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, MediaRepository.Fonts["DefaultFont"], textColor, alignType) { }

        public TextBox(Rectangle boundingBox, string text, Color textColor, SpriteFont font, AlignType alignType)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, font, textColor, alignType) { }

        public TextBox(Texture2D background, Rectangle boundingBox, Color color, string text, SpriteFont font, Color textColor, AlignType alignType)
            : base(background, boundingBox, color) {
            this.text = text;
            this.font = font;
            this.textColor = textColor;
            this.align = alignType;

            textLocation = Geometry.AlignText(text, font, boundingBox, align);
        }

        public override void Update() {
            base.Update();
            textLocation = Geometry.AlignText(text, font, BoundingBox, align);
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            base.Draw(spriteBatch, isRelative);

            if (!isRelative) {
                spriteBatch.DrawStringAbsolute(font, text, textLocation, textColor);
            } else {
                spriteBatch.DrawString(font, text, textLocation, textColor);
            }
        }
    }
}
