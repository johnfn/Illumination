using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.MouseHandler;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Logic.ActionHandler;

namespace Illumination.Components {
    public class Button : Component, MouseListener {
        private string text;
        private SpriteFont font;
        private Vector2 location;

        private HashSet<ActionListener> actionListeners;

        public Button(string text, SpriteFont font, Vector2 location, MouseController mouseController) {
            this.text = text;
            this.font = font;
            this.location = location;

            Vector2 dimensions = font.MeasureString(text);
            base.BoundingBox = new Rectangle((int) location.X, (int) location.Y, (int) dimensions.X, (int) dimensions.Y);

            mouseController.AddMouseListener(this);

            actionListeners = new HashSet<ActionListener>();
        }

        public void AddActionListener(ActionListener al) {
            actionListeners.Add(al);
        }

        public void RemoveActionListener(ActionListener al) {
            actionListeners.Remove(al);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(font, text, location, Color.Black);
        }

        public void MouseReleased(MouseEvent evt) { /* Ignore */ }
        public void MousePressed(MouseEvent evt) { /* Ignore */ }
        public void MouseClicked(MouseEvent evt) {
            if (base.BoundingBox.Contains(evt.CurrentLocation)) {
                foreach (ActionListener al in actionListeners) {
                    al.ActionPerformed(new ActionEvent(this));
                }
            }
        }
    }
}
