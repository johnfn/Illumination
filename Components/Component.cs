using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;
using Illumination.Utility;

namespace Illumination.Components {
    public abstract class Component {
        protected Point relativePosition;
        public Point RelativePosition {
            get { return relativePosition; }
            set { relativePosition = value; }
        }

        protected Rectangle boundingBox;
        protected Texture2D background;
        protected Color color;
        protected bool isActive = true;
        protected Component parent = null;
        protected float layerDepth = 0;

        public Component Parent {
            get { return parent; }
            set { parent = value; }
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

        public float LayerDepth {
            get { return layerDepth; }
            set { layerDepth = value; }
        }

        public Component() { /* Default constructor */ }

        public Component(Texture2D background, Rectangle boundingBox, Color color) {
            this.background = background;
            this.boundingBox = boundingBox;
            this.color = color;

            relativePosition = new Point(boundingBox.X, boundingBox.Y);
        }

        public Component(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) { }

        /* Called when bounding box has changed */
        public virtual void Update() {
            if (parent != null) {
                Point absolutePosition = Geometry.Translate(relativePosition, parent.boundingBox.X, parent.boundingBox.Y);
                boundingBox = new Rectangle(absolutePosition.X, absolutePosition.Y, boundingBox.Width, boundingBox.Height);
            }
        }

        public virtual void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (!isRelative) {
                spriteBatch.DrawAbsolute(background, boundingBox, color);
            } else {
                spriteBatch.DrawRelative(background, boundingBox, color, layerDepth);
            }
        }

        public virtual void Activate() {
            isActive = true;
        }

        public virtual void Deactivate() {
            isActive = false;
        }

        public virtual void Destroy() { /* Default to do nothing */ }
    }
}