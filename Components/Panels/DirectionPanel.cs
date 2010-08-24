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

namespace Illumination.Components.Panels {
    public class DirectionPanel : Panel, ActionListener {
        Button northButton, southButton, eastButton, westButton;
        
        public delegate void DirectionEvent(Entity.DirectionType direction);

        DirectionEvent directionEventHandler;

        public DirectionPanel(Rectangle boundingBox, DirectionEvent directionEventHandler)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            Initialize();

            this.directionEventHandler = directionEventHandler;

            base.Deactivate();
        }

        public void Initialize() {
            northButton = new Button(MediaRepository.Textures["Arrow_N"], new Rectangle(40, 0, 40, 40), Color.Black);
            southButton = new Button(MediaRepository.Textures["Arrow_S"], new Rectangle(40, 80, 40, 40), Color.Black);
            eastButton = new Button(MediaRepository.Textures["Arrow_E"], new Rectangle(80, 40, 40, 40), Color.Black);
            westButton = new Button(MediaRepository.Textures["Arrow_W"], new Rectangle(0, 40, 40, 40), Color.Black);

            AddComponent(northButton);
            AddComponent(southButton);
            AddComponent(eastButton);
            AddComponent(westButton);

            northButton.AddActionListener(this);
            southButton.AddActionListener(this);
            eastButton.AddActionListener(this);
            westButton.AddActionListener(this);
        }

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == northButton) {
                directionEventHandler(Entity.DirectionType.North);
            } else if (evt.InvokingComponent == southButton) {
                directionEventHandler(Entity.DirectionType.South);
            } else if (evt.InvokingComponent == eastButton) {
                directionEventHandler(Entity.DirectionType.East);
            } else {
                directionEventHandler(Entity.DirectionType.West);
            }
        }
    }
}
