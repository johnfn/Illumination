using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Microsoft.Xna.Framework;
using Illumination.Utility;
using Illumination.WorldObjects;
using Illumination.Logic;

namespace Illumination.Components.Panels {
    public class InformationPanel : Panel {
        Panel detailPanel;
        
        // Panels in the detailPanel
        Panel directionPanel;
        Panel professionPanel;
        //Panel personStatusPanel;
        //Panel buildingStatusPanel;

        public InformationPanel(Rectangle boundingBox) : base(MediaRepository.Textures["Blank"], boundingBox, Color.Green) { 
            detailPanel = new Panel(new Rectangle(175, 0, 250, 175), Color.White);
            directionPanel = new DirectionPanel(new Rectangle(0, 0, 200, 200));
            professionPanel = new ProfessionPanel(new Rectangle(0, 60, 250, 50));
 
            AddComponent(detailPanel);

            detailPanel.AddComponent(directionPanel);
            detailPanel.AddComponent(professionPanel);
        }

        public void DisplayPerson(Person p) {

        }

        public void UpdateDetailPanel()
        {
            directionPanel.Deactivate();
            professionPanel.Deactivate();

            Entity entity = World.SelectedEntity;
            if (entity is Person && entity.Selectable)
            {
                Person thisPerson = (Person)entity;
                if (thisPerson.IsEducated && thisPerson.Profession == Person.ProfessionType.Worker)
                {
                    professionPanel.Activate();
                }

                directionPanel.Activate();
            }
            else if (entity is Tree && entity.Selectable)
            {
                directionPanel.Activate();
            }
            else if (entity is Building && entity.Selectable)
            {
                /* Something */
            }
        }
    }
}
