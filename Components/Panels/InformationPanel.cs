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
using Illumination.Logic.ActionHandler;

namespace Illumination.Components.Panels {
    public class InformationPanel : Panel, ActionListener {
        Panel directionPanel;
        Panel detailPanel;
        Panel mapPanel;

        Panel arrowPanel;
        Panel mirrorArrowPanel;
        Panel professionPanel;
        Panel personStatusPanel;
        Panel buildingStatusPanel;
        Panel researchPanel;
        TextBox missionResultBox;

        Button shopButton;
        Button itemsButton;
        Panel shopPanel;
        Panel itemsPanel;

        public InformationPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, new Color(255, 255, 255, 50)) {

            directionPanel = new Panel(new Rectangle(5, 5, 240, 140), new Color(255, 255, 255, 100));

            detailPanel = new Panel(new Rectangle(255, 5, 480, 140), new Color(0, 0, 0, 50));
            mapPanel = new Panel(new Rectangle(745, 5, 140, 140), new Color(255, 255, 255, 100));

            arrowPanel = new DirectionPanel(new Rectangle(20, 20, 200, 100), World.ChangeDirection);
            mirrorArrowPanel = new DirectionPanel(new Rectangle(20, 20, 200, 100), World.ChangeDirection, true);

            professionPanel = new ProfessionPanel(new Rectangle(285, -52, 320, 52));
            missionResultBox = new TextBox(new Rectangle(0, -40, 230, 40), "", Color.White, TextBox.AlignType.Center);
            
            personStatusPanel = new PersonPanel(new Rectangle(0, 0, 200, 140));
            buildingStatusPanel = new BuildingPanel(new Rectangle(0, 0, 200, 140));
            researchPanel = new ResearchPanel(new Rectangle(0, 0, 200, 140));

            shopButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(800, -30, 90, 30), new Color(0, 0, 150, 100),
                "Shop", MediaRepository.Fonts["DefaultFont"], Color.White);
            shopButton.AddActionListener(this);

            itemsButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(700, -30, 90, 30), new Color(0, 0, 150, 100),
                "Items", MediaRepository.Fonts["DefaultFont"], Color.White);
            itemsButton.AddActionListener(this);

            shopPanel = new ShopPanel(new Rectangle(5, 5, 880, 140), new Color(255, 255, 255, 100));
            shopPanel.Deactivate();

            itemsPanel = new ItemsPanel(new Rectangle(5, 5, 880, 140), new Color(255, 255, 255, 100));
            itemsPanel.Deactivate();

            AddComponent(directionPanel);
            AddComponent(detailPanel);
            AddComponent(mapPanel);

            AddComponent(missionResultBox);
            AddComponent(professionPanel);
            AddComponent(shopButton);
            AddComponent(itemsButton);
            AddComponent(shopPanel);
            AddComponent(itemsPanel);

            directionPanel.AddComponent(arrowPanel);
            directionPanel.AddComponent(mirrorArrowPanel);

            detailPanel.AddComponent(personStatusPanel);
            detailPanel.AddComponent(buildingStatusPanel);
            detailPanel.AddComponent(researchPanel);

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

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == shopButton) {
                if (shopPanel.IsActive) {
                    CloseShopPanel();
                }
                else {
                    CloseItemsPanel();
                    shopPanel.ActivatePanel(true);
                    directionPanel.DeactivatePanel(true);
                    detailPanel.DeactivatePanel(true);
                    mapPanel.DeactivatePanel(true);

                    shopButton.Text = "Back";
                    shopButton.Color = new Color(150, 0, 0, 100);
                }
            } else if (evt.InvokingComponent == itemsButton) {
                if (itemsPanel.IsActive) {
                    CloseItemsPanel();
                } else {
                    CloseShopPanel();
                    itemsPanel.ActivatePanel(true);
                    directionPanel.DeactivatePanel(true);
                    detailPanel.DeactivatePanel(true);
                    mapPanel.DeactivatePanel(true);

                    itemsButton.Text = "Back";
                    itemsButton.Color = new Color(150, 0, 0, 100);
                }
            }
        }

        private void CloseShopPanel() {
            shopPanel.DeactivatePanel(true);
            directionPanel.ActivatePanel(false);
            detailPanel.ActivatePanel(false);
            mapPanel.ActivatePanel(false);

            shopButton.Text = "Shop";
            shopButton.Color = new Color(0, 0, 150, 100);
        }

        private void CloseItemsPanel() {
            itemsPanel.DeactivatePanel(true);
            directionPanel.ActivatePanel(false);
            detailPanel.ActivatePanel(false);
            mapPanel.ActivatePanel(false);

            itemsButton.Text = "Items";
            itemsButton.Color = new Color(0, 0, 150, 100);
        }

        /*
         * Decides which panels to show.
         */
        public void UpdateInformationPanel() {
            CloseShopPanel();
            CloseItemsPanel();

            arrowPanel.DeactivatePanel(true);
            mirrorArrowPanel.DeactivatePanel(true);
            professionPanel.DeactivatePanel(true);
            personStatusPanel.DeactivatePanel(true);
            buildingStatusPanel.DeactivatePanel(true);
            researchPanel.DeactivatePanel(true);

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
                        personStatusPanel.ActivatePanel(true);
                    }
                    arrowPanel.ActivatePanel(true);
                    if (activateProfessionPanel) {
                        professionPanel.ActivatePanel(true);
                    }
                } else if (entityType == World.EntityType.Tree) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                        arrowPanel.ActivatePanel(true);
                    }
                } else if (entityType == World.EntityType.Building) {
                    foreach (Entity e in World.SelectedEntities) {
                        if (!e.Selectable) {
                            continue;
                        }
                    }
                    if (World.SelectedEntities.Count == 1) {
                        if (World.SelectedEntities.First() is ResearchCenter) {
                            researchPanel.ActivatePanel(true);
                        }
                        else {
                            buildingStatusPanel.ActivatePanel(true);
                        }
                    }
                } else if (entityType == World.EntityType.Item) { 
                    mirrorArrowPanel.ActivatePanel(true);
                }
            }
        }
    }
}
