using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;

namespace Illumination.Components {
    public abstract class Component {
        protected Point origin;
        protected Rectangle boundingBox;
        protected Texture2D background;
        protected Color color;
        protected bool isActive = true;

        public Point Origin {
            get { return origin; }
            set { origin = value; }
        }

        public bool IsActive {
            get { return isActive; }
        }

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
            
            origin = new Point(0, 0);
        }

        public Component(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) { }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, boundingBox, color);
        }

        public virtual void Activate() {
            isActive = true;
        }

        public virtual void Deactivate() {
            isActive = false;
        }
    }
}