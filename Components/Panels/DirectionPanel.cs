using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Logic.ActionHandler;
using Illumination.Logic;
using Illumination.WorldObjects;
using Illumination.Graphics;

namespace Illumination.Components.Panels {
    public class DirectionPanel : Panel, ActionListener {
        Button[] buttons;
        Texture2D[] textures;
        Rectangle[] boundingBoxes;

        public delegate void DirectionEvent(Entity.DirectionType direction);

        DirectionEvent directionEventHandler;

        private void init(Rectangle boundingBox, DirectionEvent directionEventHandler, bool isMirror) {
            int buttonWidth = boundingBox.Width / 2;
            int buttonHeight = boundingBox.Height / 2;

            buttons = new Button[(int) Entity.DirectionType.SIZE];
            textures = new Texture2D[(int) Entity.DirectionType.SIZE];
            boundingBoxes = new Rectangle[(int) Entity.DirectionType.SIZE];

            /* 0:North, 1:South, 2:East, 3:West */
            if (isMirror) {
                textures[0] = MediaRepository.Textures["Mirror_NE"];
                textures[1] = MediaRepository.Textures["Mirror_SW"];
                textures[2] = MediaRepository.Textures["Mirror_SE"];
                textures[3] = MediaRepository.Textures["Mirror_NW"];


                boundingBoxes[3] = new Rectangle(buttonWidth / 2, 0, buttonWidth, buttonHeight);
                boundingBoxes[1] = new Rectangle(0, buttonHeight / 2, buttonWidth, buttonHeight);
                boundingBoxes[0] = new Rectangle(buttonWidth, buttonHeight / 2, buttonWidth, buttonHeight);
                boundingBoxes[2] = new Rectangle(buttonWidth / 2, buttonHeight, buttonWidth, buttonHeight);
            } else {
                textures[0] = MediaRepository.Textures["Arrow_N"];
                textures[1] = MediaRepository.Textures["Arrow_S"];
                textures[2] = MediaRepository.Textures["Arrow_E"];
                textures[3] = MediaRepository.Textures["Arrow_W"];


                boundingBoxes[0] = new Rectangle(buttonWidth, 0, buttonWidth, buttonHeight);
                boundingBoxes[1] = new Rectangle(0, buttonHeight, buttonWidth, buttonHeight);
                boundingBoxes[2] = new Rectangle(buttonWidth, buttonHeight, buttonWidth, buttonHeight);
                boundingBoxes[3] = new Rectangle(0, 0, buttonWidth, buttonHeight);
            }


            Initialize();

            this.directionEventHandler = directionEventHandler;

            base.Deactivate();
        }

        public DirectionPanel(Rectangle boundingBox, DirectionEvent directionEventHandler, bool isMirror)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            init(boundingBox, directionEventHandler, isMirror);
        }

        public DirectionPanel(Rectangle boundingBox, DirectionEvent directionEventHandler)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            init(boundingBox, directionEventHandler, false);
        }

        public void Initialize() {
            for (int n = 0; n < (int) Entity.DirectionType.SIZE; n++) {
                buttons[n] = new Button(textures[n], boundingBoxes[n], Color.White);
                AddComponent(buttons[n]);
                buttons[n].AddActionListener(this);
            }
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                foreach (Button button in buttons) {
                    spriteBatch.DrawAbsolute(MediaRepository.Textures["BlankTile"], button.BoundingBox, new Color(0, 0, 0, 50));
                }
                base.Draw(spriteBatch, isRelative);
            }
        }

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == buttons[0]) {
                directionEventHandler(Entity.DirectionType.North);
            } else if (evt.InvokingComponent == buttons[1]) {
                directionEventHandler(Entity.DirectionType.South);
            } else if (evt.InvokingComponent == buttons[2]) {
                directionEventHandler(Entity.DirectionType.East);
            } else {
                directionEventHandler(Entity.DirectionType.West);
            }
        }
    }
}
