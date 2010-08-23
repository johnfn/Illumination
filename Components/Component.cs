using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;

namespace Illumination.Components {
    public abstract class Component {
        Rectangle boundingBox;
        Texture2D background;
        Color color;

        public Rectangle BoundingBox {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public Texture2D Background {
            get { return background; }
            set { background = value; }
        }

        public Color Color {
            get { return color; }
            set { color = value; }
        }

        public Component() { /* Default constructor */ }

        public Component(Texture2D background, Rectangle boundingBox, Color color) {
            this.background = background;
            this.boundingBox = boundingBox;
            this.color = color;
        }

        public Component(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) { }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, boundingBox, color);
        }
    }
}