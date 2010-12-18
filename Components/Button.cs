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
    public class Button : TextBox, MouseListener {
        HashSet<ActionListener> actionListeners;

        public Polygon clickableRegionRel = null;
        public Polygon ClickableRegionRel {
            get { return clickableRegionRel; }
            set {
                clickableRegionRel = value;
                Update();
            }
        }
        private Polygon clickableRegion;

        public Button(Texture2D background, Rectangle boundingBox, Color color) 
            : this(background, boundingBox, color, "", MediaRepository.Fonts["DefaultFont"], Color.White) { }

        public Button(Rectangle boundingBox, string text, SpriteFont font, Color textColor)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, font, textColor) { }

        public Button(Texture2D background, Rectangle boundingBox, Color color, string text, SpriteFont font, Color textColor)
            : base(background, boundingBox, color, text, font, textColor, AlignType.Center)
        {
            MouseController.AddMouseListener(this);
            
            actionListeners = new HashSet<ActionListener>();
        }

        public void AddActionListener(ActionListener al) {
            actionListeners.Add(al);
        }

        public void RemoveActionListener(ActionListener al) {
            actionListeners.Remove(al);
        }

        public void MouseReleased(MouseEvent evt) { /* Ignore */ }
        public void MousePressed(MouseEvent evt) { /* Ignore */ }
        public void MouseClicked(MouseEvent evt) {
            if (IsActive && ((clickableRegion == null && BoundingBox.Contains(evt.CurrentLocation)) ||
                             (clickableRegion != null && clickableRegion.Contains(evt.CurrentLocation)))) {
                foreach (ActionListener al in actionListeners) {
                    al.ActionPerformed(new ActionEvent(this));
                }
                evt.Consumed = true;
            }
        }

        public override void Update() {
            base.Update();

            if (clickableRegionRel != null) {
                clickableRegion = new Polygon(clickableRegionRel);
                clickableRegion.Translate(boundingBox.X, boundingBox.Y);
            }
        }

        public override void Destroy() {
            base.Destroy();
            MouseController.RemoveMouseListener(this);
        }

        public override string ToString() {
            return String.Format("Button {0}: {1} - {2}", boundingBox, Text.Length > 0 ? Text : "<No Text>",
                isActive ? "Enabled" : "Disabled");
        }
    }
}
