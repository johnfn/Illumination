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
using Illumination.WorldObjects;

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
        Button schoolResearchButton_1;
        Button schoolResearchButton_2;

        Button activateButton;
        Button abortButton;

        Research thisResearch;
        ResearchCenter thisCenter;

        const int Indent = 10;
        const int DescriptionY = 40;
        const int SequenceBarX = 25;
        const int LineHeight = 15;

        const int MaxTasksPerResearch = 5;

        public ResearchPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {
            title = new TextBox(new Rectangle(25, 10, 50, 25), "Research Center", Color.White, TextBox.AlignType.Left);

            activateButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(370, 10, 100, 25), new Color(100, 100, 255, 200), "Activate", MediaRepository.Fonts["Arial10"], Color.Black);
            abortButton = new Button(MediaRepository.Textures["Blank"], new Rectangle(370, 10, 100, 25), new Color(255, 100, 100, 200), "Abort", MediaRepository.Fonts["Arial10"], Color.Black);
            activateButton.AddActionListener(this);
            abortButton.AddActionListener(this);

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

            schoolResearchButton_1 = new Button(MediaRepository.Textures["Blank"], new Rectangle(300, 50, 120, 30), new Color(255, 255, 255, 100), "School Research 1", MediaRepository.Fonts["Arial10"], Color.Black);
            schoolResearchButton_1.AddActionListener(this);
            AddComponent(schoolResearchButton_1);

            schoolResearchButton_2 = new Button(MediaRepository.Textures["Blank"], new Rectangle(300, 90, 120, 30), new Color(255, 255, 255, 100), "School Research 2", MediaRepository.Fonts["Arial10"], Color.Black);
            schoolResearchButton_2.AddActionListener(this);
            AddComponent(schoolResearchButton_2);

            AddComponent(title);
            AddComponent(descriptionHeader);
            AddComponent(descriptionText);
            AddComponent(statusHeader);
            AddComponent(statusText);
            AddComponent(tasksHeader);
            AddComponent(activateButton);
            AddComponent(abortButton);

            DeactivatePanel(true);
        }

        public override void ActivatePanel(bool isRecursive) {
            thisCenter = (ResearchCenter)World.SelectedEntities.First();
            base.ActivatePanel(isRecursive);
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                activateButton.Deactivate();
                abortButton.Deactivate();

                if (thisCenter.IsActive) {
                    abortButton.Activate();
                }

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
                        activateButton.Activate();
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
        }

        public void ActionPerformed(ActionEvent evt) {
            if (evt.InvokingComponent == schoolResearchButton_1) {
                thisResearch = World.GetResearch(0);
            }
            else if (evt.InvokingComponent == schoolResearchButton_2) {
                thisResearch = World.GetResearch(1);
            }
            else if (evt.InvokingComponent == activateButton) {
                thisCenter.InitiateResearch(thisResearch.Index);
            }
            else if (evt.InvokingComponent == abortButton) {
                thisCenter.AbortResearch();
            }
        }
    }
}
