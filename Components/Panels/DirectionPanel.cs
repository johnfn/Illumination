using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Illumination.Components.Panels {
    public class DirectionPanel : Panel {
        Button northButton, southButton, eastButton, westButton;

        public DirectionPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            Initialize();
        }

        public void Initialize() {
            northButton = new Button(MediaRepository.Textures["Arrow_N"], new Rectangle(0, 0, 50, 50), Color.Blue);
            southButton = new Button(MediaRepository.Textures["Arrow_S"], new Rectangle(0, 0, 50, 50), Color.White);
            eastButton = new Button(MediaRepository.Textures["Arrow_E"], new Rectangle(0, 0, 50, 50), Color.White);
            westButton = new Button(MediaRepository.Textures["Arrow_W"], new Rectangle(0, 0, 50, 50), Color.White);

            AddComponent(southButton);
        }
    }
}
