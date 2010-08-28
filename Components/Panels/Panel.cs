using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Utility;
using Illumination.Data;
using Illumination.Logic.MouseHandler;

namespace Illumination.Components.Panels {
    public class Panel : Component {
        protected HashSet <Component> components;
        protected Point relativePosition;

        public Panel(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.White) { }

        public Panel(Rectangle boundingBox, Color backgroundColor) : this(MediaRepository.Textures["Blank"], boundingBox, backgroundColor) { }

        public Panel(Texture2D background, Rectangle boundingBox, Color color) : base(background, boundingBox, color) {
            components = new HashSet<Component>();
            relativePosition = new Point(boundingBox.X, boundingBox.Y);
            Origin = new Point(boundingBox.X, boundingBox.Y);
        }

        public Point RelativePosition {
            get { return relativePosition; }
            set { relativePosition = value; }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            foreach (Component c in components) {
                if (c is Panel)
                {
                    if (((Panel)c).IsActive)
                    {
                        c.Draw(spriteBatch);
                    }
                }
                else
                {
                    c.Draw(spriteBatch);
                }
            }
        }

        public void AddComponent(Component c) {
            UpdateComponent(c);
            components.Add(c);
        }

        public void RemoveComponent(Component newComponent) {
            components.Remove(newComponent);
        }

        public void UpdateComponents() {
            foreach (Component c in components) {
                UpdateComponent(c);
            }
        }

        public void UpdateComponent(Component c) {
            c.BoundingBox = Geometry.Translate(c.BoundingBox, this.Origin.X, this.Origin.Y);
            c.Origin = this.Origin;

            if (c is Panel)
            {
                ((Panel)c).UpdateComponents();
            }
            else if (c is TextBox)
            {
                ((TextBox)c).UpdateTextLocation();
            }
        }

        public override void Activate() {
            base.Activate();

            foreach (Component c in components) {
                c.Activate();
            }
        }

        public override void Deactivate() {
            base.Deactivate();

            foreach (Component c in components) {
                c.Deactivate();
            }
        }
    }
}
