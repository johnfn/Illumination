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
        protected bool consumesMouseEvent = true;

        public Panel(Rectangle boundingBox) : this(MediaRepository.Textures["Blank"], boundingBox, Color.White) { }

        public Panel(Rectangle boundingBox, Color backgroundColor) : this(MediaRepository.Textures["Blank"], boundingBox, backgroundColor) { }

        public Panel(Texture2D background, Rectangle boundingBox, Color color) : base(background, boundingBox, color) {
            components = new HashSet<Component>();
            relativePosition = new Point(boundingBox.X, boundingBox.Y);

            MouseController.AddMouseListener(this);
        }

        public bool ConsumesMouseEvent {
            get { return consumesMouseEvent; }
            set { consumesMouseEvent = value; }
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (!IsActive) {
                return;
            }

            base.Draw(spriteBatch, isRelative);

            foreach (Component c in components) {
                c.Draw(spriteBatch, isRelative);
            }
        }

        public void AddComponent(Component c) {
            c.Parent = this;

            c.Update();
            components.Add(c);
        }

        public void RemoveComponent(Component newComponent) {
            components.Remove(newComponent);
        }

        public override void Update() {
            base.Update();
            foreach (Component c in components) {
                c.Update();
            }
        }

        public virtual void ActivatePanel(bool isRecursive) {
            base.Activate();

            if (isRecursive) {
                foreach (Component c in components) {
                    c.Activate();
                }
            }
        }

        public virtual void DeactivatePanel(bool isRecursive) {
            base.Deactivate();

            if (isRecursive) {
                foreach (Component c in components) {
                    c.Deactivate();
                }
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
