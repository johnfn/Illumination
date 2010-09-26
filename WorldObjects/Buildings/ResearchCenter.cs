using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic;
using Illumination.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics.AnimationHandler;
using Illumination.Utility;
using Illumination.Components;

namespace Illumination.WorldObjects
{
    public class ResearchCenter : Building
    {
        const int WIDTH = 2;
        const int HEIGHT = 2;
        const int NO_RESEARCH = -1;

        int activatedResearch;
        BuildingEffect[] effects;

        public bool IsActive { get { return activatedResearch != -1;  } }

        public ResearchCenter() { /* Default constructor */ }

        public ResearchCenter(int x, int y) {
            Initialize(x, y);

            Name = "Research Center";
            activatedResearch = NO_RESEARCH; 
            
            effects = new BuildingEffect[1];
            effects[0] = new BuildingEffect("Current Task", new LightSequence(), StandardEffect, true);
        }

        public override void Initialize(int x, int y) {
            base.Initialize(x, y, WIDTH, HEIGHT, GetTexture());
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));

            effectDisplay.Sequence = effects[ActivatedEffect].sequence;
            effectDisplay.Draw(spriteBatch, true);
        }

        public override BuildingEffect[] GetEffects() {
            return effects;
        }
        
        public override Texture2D GetTexture() {
            return MediaRepository.Textures["ResearchCenter"];
        }

        public void TaskComplete() {
            Rectangle textRect = BoundingBox;
            textRect.Width /= 4;
            textRect.Height /= 4;
            textRect = Geometry.Translate(textRect, textRect.Width * 2, textRect.Height);

            TextBox textBox = new TextBox(textRect, "", Color.White, TextBox.AlignType.Center);
            textBox.LayerDepth = Layer.Depth["TextNotice"];
            
            Research thisResearch = World.ResearchLogic.GetResearch(activatedResearch);
            if (thisResearch.TaskComplete()) {
                /* If the entire research is complete */
                activatedResearch = NO_RESEARCH;
                effects[0].sequence = new LightSequence();

                textBox.Text = thisResearch.Description + " Complete";
           }
            else {
                effects[0].sequence = thisResearch.CurrentTask;

                textBox.Text = "Task Complete";
            }

            Animation effect = Display.CreateAnimation(textBox, 1);
            effect.AddTranslationFrame(new Point(textRect.X, textRect.Y - textRect.Height), 1, Animation.EaseType.In);
        }

        public void InitiateResearch(int researchIndex) {
            World.ResearchLogic.GetResearch(researchIndex).Initiate();
            activatedResearch = researchIndex;
            effects[0].sequence = World.ResearchLogic.GetResearch(researchIndex).CurrentTask;
        }

        public void AbortResearch() {
            World.ResearchLogic.GetResearch(activatedResearch).Abort();
            activatedResearch = NO_RESEARCH;
            effects[0].sequence = new LightSequence();
        }

        private static bool StandardEffect(Building building) {
            ResearchCenter thisCenter = (ResearchCenter)building;
            if (thisCenter.IsActive) {
                thisCenter.TaskComplete();
            }

            return true;
        }
    }
}
