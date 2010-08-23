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

namespace Illumination.Components {
    public class Button : Component, MouseListener {
        SpriteFont font;
        string text;
        Vector2 textLocation;
        Color textColor;
        
        HashSet<ActionListener> actionListeners;

        public string Text
        {
            get { return text; }
            set { 
                text = value;
                textLocation = Geometry.CenterText(text, font, BoundingBox);
            }
        }

        public Button(Texture2D background, Rectangle boundingBox, Color color, MouseController mouseController) 
            : this(background, boundingBox, color, "", MediaRepository.Fonts["DefaultFont"], Color.White, mouseController) { }

        public Button(Rectangle boundingBox, string text, SpriteFont font, Color textColor, MouseController mouseController)
            : this(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite, text, font, textColor, mouseController) { }

        public Button(Texture2D background, Rectangle boundingBox, Color color, string text, SpriteFont font, Color textColor, MouseController mouseController)
            : base(background, boundingBox, color)
        {
            this.text = text;
            this.font = font;
            this.textColor = textColor;

            textLocation = Geometry.CenterText(text, font, boundingBox);

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
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, text, textLocation, textColor);
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
