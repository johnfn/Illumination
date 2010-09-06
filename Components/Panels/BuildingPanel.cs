using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic;
using Illumination.WorldObjects;
using Illumination.Logic.ActionHandler;
using Illumination.Graphics;

namespace Illumination.Components.Panels
{
    public class BuildingPanel : Panel, ActionListener
    {
        TextBox title;
        Panel[] effectPanels;
        Button[] effectButtons;
        LightSequenceBar[] sequenceBars;
        TextBox[] descriptions;
        TextBox[] unknownTexts;

        const int ButtonWidth = 230;
        const int ButtonHeight = 45;
        const int Indent = 5;
        const int FirstButtonY = 40;
        const int DescriptionY = 10;
        const int SequenceBarY = 25;
        const int InnerButtonIndent = 20;
        Dimension SequenceTileSize = new Dimension(10, 10);

        public BuildingPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite)
        {
            title = new TextBox(new Rectangle(25, 10, 50, 25), "Building", Color.White, TextBox.AlignType.Left);
            effectPanels = new Panel[4];
            effectButtons = new Button[4];
            sequenceBars = new LightSequenceBar[4];
            descriptions = new TextBox[4];
            unknownTexts = new TextBox[4];

            effectPanels[0] = new Panel(new Rectangle(Indent, FirstButtonY, ButtonWidth, ButtonHeight), new Color(255, 255, 255, 100));
            effectPanels[1] = new Panel(new Rectangle(Indent, FirstButtonY + ButtonHeight + Indent, ButtonWidth, ButtonHeight), new Color(255, 255, 255, 100));
            effectPanels[2] = new Panel(new Rectangle(ButtonWidth + 3 * Indent, FirstButtonY, ButtonWidth, ButtonHeight), new Color(255, 255, 255, 100));
            effectPanels[3] = new Panel(new Rectangle(ButtonWidth + 3 * Indent, FirstButtonY + ButtonHeight + Indent, ButtonWidth, ButtonHeight), new Color(255, 255, 255, 100));

            int index = 0;
            foreach (Panel p in effectPanels) {
                effectButtons[index] = new Button(MediaRepository.Textures["Blank"], new Rectangle(0, 0, ButtonWidth, ButtonHeight), Color.TransparentWhite);
                effectButtons[index].AddActionListener(this);

                sequenceBars[index] = new LightSequenceBar(new LightSequence(), new Point(InnerButtonIndent, SequenceBarY), SequenceTileSize, Indent);
                descriptions[index] = new TextBox(new Rectangle(InnerButtonIndent, DescriptionY, 10, 10), "", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);
                unknownTexts[index] = new TextBox(new Rectangle(0, 0, ButtonWidth, ButtonHeight), "Unknown", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Center);

                p.AddComponent(effectButtons[index]);
                p.AddComponent(sequenceBars[index]);
                p.AddComponent(descriptions[index]);
                p.AddComponent(unknownTexts[index]);
                
                AddComponent(p);
                index++;
            }

            AddComponent(title);

            Deactivate();
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                Building thisBuilding = (Building)World.SelectedEntities.First();
                for (int index = 0; index < 4; index++) {
                    if (thisBuilding.GetEffects()[index].isKnown) {
                        descriptions[index].Activate();
                        effectButtons[index].Activate();
                        sequenceBars[index].Activate();
                        unknownTexts[index].Deactivate();
                    }
                    else {
                        descriptions[index].Deactivate();
                        effectButtons[index].Deactivate();
                        sequenceBars[index].Deactivate();
                        unknownTexts[index].Activate();
                    }

                    if (World.IsNight) {
                        effectButtons[index].Deactivate();
                    }
                }

                base.Draw(spriteBatch, isRelative);

                spriteBatch.DrawAbsolute(MediaRepository.Textures["Border"], effectButtons[thisBuilding.ActivatedEffect].BoundingBox, World.IsNight ? Color.Black : Color.White);
            }
        }

        public override void ActivatePanel(bool isRecursive)
        {
            base.ActivatePanel(isRecursive);

            Update();

            /* It is gauranteed that selected entity is a single building. */
            Building thisBuilding = (Building)World.SelectedEntities.First();
            title.Text = thisBuilding.Name;

            for (int index = 0; index < 4; index++) {
                Building.BuildingEffect effect = thisBuilding.GetEffects()[index];
                descriptions[index].Text = effect.description;
                sequenceBars[index].Sequence = effect.sequence;
            }
        }

        /*
         * Triggered when a user clicks on a button.
         */

        public void ActionPerformed(ActionEvent evt) {
            int clickedEffect = 0;
            foreach (Button button in effectButtons) {
                if (evt.InvokingComponent == button) {
                    break;
                }
                clickedEffect++;
            }

            Building building = (Building)World.SelectedEntities.First();
            building.ActivateEffect(clickedEffect);

            if (World.cheater){
                building.ForceActivate();
            }
            World.UpdateHighlight();
        }
    }
}
