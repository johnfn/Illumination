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

        public InformationPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.Green) {
            detailPanel = new Panel(new Rectangle(200, 0, 400, 175), Color.White);
            directionPanel = new DirectionPanel(new Rectangle(0, 0, 200, 200), World.ChangeDirection);
            professionPanel = new ProfessionPanel(new Rectangle(0, 60, 250, 50));

            AddComponent(detailPanel);

            detailPanel.AddComponent(directionPanel);
            detailPanel.AddComponent(professionPanel);
        }

        public void DisplayPerson(Person p) {

        }

        public void UpdateDetailPanel() {
            directionPanel.Deactivate();
            professionPanel.Deactivate();

            if (World.SelectedEntities.Count > 0) {
                World.EntityType entityType = World.SelectedEntityType;
                if (entityType == World.EntityType.Person) {
                    bool activateProfessionPanel = true;
                    foreach (Entity e in World.SelectedEntities) {
                        Person thisPerson = (Person) e;
                        if (!thisPerson.IsEducated || thisPerson.Profession != Person.ProfessionType.Worker) {
                            activateProfessionPanel = false;
                        }
                    }
                    directionPanel.Activate();
                    if (activateProfessionPanel) {
                        professionPanel.Activate();
                    }
                } else if (entityType == World.EntityType.Tree) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                        directionPanel.Activate();
                    }
                } else if (entityType == World.EntityType.Building) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                    }
                    /* Something */
                }
            }
        }
    }
}
