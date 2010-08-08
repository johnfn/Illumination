using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.Components {
    public abstract class Component {
        private Rectangle boundingBox;
        
        public Rectangle BoundingBox {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public Component() { /* Default constructor */ }

        public Component(Rectangle boundingBox) {
            this.boundingBox = boundingBox;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
