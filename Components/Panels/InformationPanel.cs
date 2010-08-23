using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Microsoft.Xna.Framework;
using Illumination.Utility;
using Illumination.WorldObjects;

namespace Illumination.Components.Panels {
    public class InformationPanel : Panel {
        Panel detailPanel;

        public InformationPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, Color.Green) { 
            detailPanel = new Panel(new Rectangle(175, 0, 250, 175), Color.White);

            AddComponent(detailPanel);

            detailPanel.AddComponent(new DirectionPanel(new Rectangle(0, 0, 200, 200)));
        }

        public void DisplayPerson(Person p) {

        }
    }
}
