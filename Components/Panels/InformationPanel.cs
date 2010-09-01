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
        Panel detailPanel;

        // Panels in the detailPanel
        Panel directionPanel;
        Panel professionPanel;
        Panel personStatusPanel;
        Panel buildingStatusPanel;
        TextBox missionResultBox;
        Panel missionPanel;

        public InformationPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            detailPanel = new Panel(new Rectangle(0, 0, 400, 150), new Color(255, 255, 255, 100));
            directionPanel = new DirectionPanel(new Rectangle(0, 0, 200, 150), World.ChangeDirection);
            professionPanel = new ProfessionPanel(new Rectangle(0, 110, 250, 50));
            missionResultBox = new TextBox(new Rectangle(0, 0, 400, 150), "", Color.White, TextBox.AlignType.Center);
            missionPanel = new MissionPanel(new Rectangle(575, 35, 375, 120));
            personStatusPanel = new PersonPanel(new Rectangle(200, 0, 200, 150));
            buildingStatusPanel = new BuildingPanel(new Rectangle(200, 0, 200, 150));

            AddComponent(detailPanel);
            AddComponent(missionResultBox);
            AddComponent(missionPanel);

            detailPanel.AddComponent(directionPanel);
            detailPanel.AddComponent(professionPanel);
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

        public void UpdateDetailPanel() {
            directionPanel.Deactivate();
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
                    if (World.SelectedEntities.Count == 1) {
                        buildingStatusPanel.Activate();
                    }
                }
            }
        }
    }
}
