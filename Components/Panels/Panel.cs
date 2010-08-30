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
    public class Panel : Component, MouseListener {
        protected HashSet <Component> components;
        protected Point relativePosition;
        protected bool consumesMouseEvent = true;

        public Panel(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.White) { }

        public Panel(Rectangle boundingBox, Color backgroundColor) : this(MediaRepository.Textures["Blank"], boundingBox, backgroundColor) { }

        public Panel(Texture2D background, Rectangle boundingBox, Color color) : base(background, boundingBox, color) {
            components = new HashSet<Component>();
            relativePosition = new Point(boundingBox.X, boundingBox.Y);
            Origin = new Point(boundingBox.X, boundingBox.Y);

            MouseController.AddMouseListener(this);
        }

        public bool ConsumesMouseEvent {
            get { return consumesMouseEvent; }
            set { consumesMouseEvent = value; }
        }

        public Point RelativePosition {
            get { return relativePosition; }
            set { relativePosition = value; }
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            base.Draw(spriteBatch, isRelative);

            foreach (Component c in components) {
                if (c is Panel)
                {
                    if (((Panel)c).IsActive)
                    {
                        c.Draw(spriteBatch, isRelative);
                    }
                }
                else
                {
                    c.Draw(spriteBatch, isRelative);
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

        public override void Update() {
            foreach (Component c in components) {
                UpdateComponent(c);
            }
        }

        public void UpdateComponent(Component c) {
            c.BoundingBox = Geometry.Translate(c.BoundingBox, this.Origin.X, this.Origin.Y);
            c.Origin = this.Origin;

            if (c is Panel)
            {
                ((Panel)c).Update();
            }
            else if (c is TextBox)
            {
                ((TextBox)c).Update();
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

        public void MouseClicked(MouseEvent evt) {
            ConsumeMouseEvent(evt);
        }

        public void MousePressed(MouseEvent evt) {
            ConsumeMouseEvent(evt);
        }

        public void MouseReleased(MouseEvent evt) {
            ConsumeMouseEvent(evt);
        }

        private void ConsumeMouseEvent(MouseEvent evt) {
            if (consumesMouseEvent && isActive && boundingBox.Contains(evt.CurrentLocation)) {
                evt.Consumed = true;
            }
        }
    }
}
