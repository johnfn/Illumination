using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Logic.ActionHandler;
using Illumination.Graphics;
using Illumination.Utility;

namespace Illumination.Components.Panels {
    public class ResearchPanel : Panel, ActionListener {
        TextBox title;

        TextBox descriptionHeader;
        TextBox descriptionText;
        TextBox statusHeader;
        TextBox statusText;
        TextBox tasksHeader;
        LightSequenceBar[] tasksDisplay;

        /* Proof of concept */
        Button schoolResearchButton;

        Research thisResearch;

        const int Indent = 10;
        const int DescriptionY = 40;
        const int SequenceBarX = 25;
        const int LineHeight = 15;

        const int MaxTasksPerResearch = 5;

        public ResearchPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            title = new TextBox(new Rectangle(25, 10, 50, 25), "Research Center", Color.White, TextBox.AlignType.Left);
            
            descriptionHeader = new TextBox(new Rectangle(Indent, DescriptionY, 10, LineHeight), "Description:", Color.White, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);
            descriptionText = new TextBox(new Rectangle(90, DescriptionY, 10, LineHeight), "description...", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);
            statusHeader = new TextBox(new Rectangle(Indent, DescriptionY + LineHeight, 10, LineHeight), "Status:", Color.White, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);
            statusText = new TextBox(new Rectangle(60, DescriptionY + LineHeight, 10, LineHeight), "status...", Color.Black, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);
            tasksHeader = new TextBox(new Rectangle(Indent, DescriptionY + 2 * LineHeight, 10, LineHeight), "Tasks:", Color.White, MediaRepository.Fonts["Arial10"], TextBox.AlignType.Left);

            tasksDisplay = new LightSequenceBar[MaxTasksPerResearch];
            Point pivot = new Point(55, DescriptionY + 2 * LineHeight + 2);
            for (int index = 0; index < MaxTasksPerResearch; index++) {
                tasksDisplay[index] = new LightSequenceBar(new LightSequence(""), pivot, new Dimension(10, 10), 5);
                pivot = Geometry.Sum(pivot, new Point(0, LineHeight));
                AddComponent(tasksDisplay[index]);
            }

            schoolResearchButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(300, 40, 120, 40), new Color(255, 255, 255, 100), "School Research", MediaRepository.Fonts["Arial10"], Color.Black);
            schoolResearchButton.AddActionListener(this);
            AddComponent(schoolResearchButton);

            AddComponent(title);
            AddComponent(descriptionHeader);
            AddComponent(descriptionText);
            AddComponent(statusHeader);
            AddComponent(statusText);
            AddComponent(tasksHeader);

            DeactivatePanel(true);
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (thisResearch != null) {
                descriptionText.Text = thisResearch.Description;
                if (thisResearch.IsCompleted) {
                    statusText.Text = "Completed";
                }
                else if (thisResearch.IsInProgress) {
                    statusText.Text = "In Progress";
                }
                else {
                    statusText.Text = "Not Started";
                }

                for (int index = 0; index < MaxTasksPerResearch; index++) {
                    if (index < thisResearch.TaskCount) {
                        tasksDisplay[index].Sequence = thisResearch.GetTask(index);
                    }
                    else {
                        tasksDisplay[index].Sequence = new LightSequence();
                    }
                }
            }
            base.Draw(spriteBatch, isRelative);
        }

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == schoolResearchButton) {
                thisResearch = World.GetResearch(0);
            }
        }
    }
}
