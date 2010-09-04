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
using Illumination.Logic.Missions;
using Illumination.Graphics;

namespace Illumination.Components.Panels {
    public class InformationPanel : Panel {
        Panel directionPanel;
        Panel detailPanel;
        Panel mapPanel;

        Panel arrowPanel;
        Panel professionPanel;
        Panel personStatusPanel;
        Panel buildingStatusPanel;
        TextBox missionResultBox;

        public InformationPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, new Color(255, 255, 255, 50)) {
            directionPanel = new Panel(new Rectangle(5, 5, 240, 140), new Color(255, 255, 255, 100));
            detailPanel = new Panel(new Rectangle(255, 5, 480, 140), new Color(0, 0, 0, 50));
            mapPanel = new Panel(new Rectangle(745, 5, 140, 140), new Color(255, 255, 255, 100));

            arrowPanel = new DirectionPanel(new Rectangle(20, 20, 200, 100), World.ChangeDirection);
            professionPanel = new ProfessionPanel(new Rectangle(285, -52, 320, 52));
            missionResultBox = new TextBox(new Rectangle(0, -40, 230, 40), "", Color.White, TextBox.AlignType.Center);
            
            personStatusPanel = new PersonPanel(new Rectangle(0, 0, 200, 140));
            buildingStatusPanel = new BuildingPanel(new Rectangle(0, 0, 200, 140));

            AddComponent(directionPanel);
            AddComponent(detailPanel);
            AddComponent(mapPanel);

            AddComponent(missionResultBox);
            AddComponent(professionPanel);

            directionPanel.AddComponent(arrowPanel);

            detailPanel.AddComponent(personStatusPanel);
            detailPanel.AddComponent(buildingStatusPanel);

            this.consumesMouseEvent = false;
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative)
        {
            if (World.CurrentMission.GetMissionStatus() == Objective.StatusType.Failure)
            {
                missionResultBox.Color = new Color(255, 0, 0, 100);
                missionResultBox.Text = "Mission Fail!";
            }
            else if (World.CurrentMission.GetMissionStatus() == Objective.StatusType.Success)
            {
                missionResultBox.Color = new Color(0, 0, 255, 100);
                missionResultBox.Text = "Mission Complete!";
            }

            base.Draw(spriteBatch, isRelative);
        }

        public void DisplayPerson(Person p) {

        }

        public void UpdateInformationPanel() {
            arrowPanel.Deactivate();
            professionPanel.Deactivate();
            personStatusPanel.Deactivate();
            buildingStatusPanel.Deactivate();

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
                    if (World.SelectedEntities.Count == 1) {
                        personStatusPanel.Activate();
                    }
                    arrowPanel.Activate();
                    if (activateProfessionPanel) {
                        professionPanel.Activate();
                    }
                } else if (entityType == World.EntityType.Tree) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                        arrowPanel.Activate();
                    }
                } else if (entityType == World.EntityType.Building) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                    }
                    if (World.SelectedEntities.Count == 1) {
                        buildingStatusPanel.Activate();
                    }
                }
            }
        }
    }
}
