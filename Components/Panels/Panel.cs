using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Utility;
using Illumination.Data;

namespace Illumination.Components.Panels {
    public class Panel : Component {
        protected HashSet <Component> components;

        public Panel(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.White) { }

        public Panel(Rectangle boundingBox, Color backgroundColor) : this(MediaRepository.Textures["Blank"], boundingBox, backgroundColor) { }

        public Panel(Texture2D background, Rectangle boundingBox, Color color) : base(background, boundingBox, color) {
            components = new HashSet<Component>();
        }

        public Color BackgroundColor {
            get { return base.Color; }
            set { base.Color = value; }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            foreach (Component c in components) {
                c.Draw(spriteBatch);
            }
        }

        public void AddComponent(Component c) {
            c.BoundingBox = Geometry.Translate(c.BoundingBox, this.BoundingBox.X, this.BoundingBox.Y);
            components.Add(c);
        }

        public void RemoveComponent(Component newComponent) {
            components.Remove(newComponent);
        }
    }
}
