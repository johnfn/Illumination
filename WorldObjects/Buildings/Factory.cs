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
    public class Factory : Building
    {
        const int WIDTH = 2;
        const int HEIGHT = 2;

        const int EFFECT_RANGE = 2;

        /*
         * You start out with a pollution score of 10 (best).
         *
         * For each factory, you subtract this number from
         * the pollution score.
         *
         * It can be decreased through research / light.
         */

        double pollutionFactor = 1;
        public double PollutionFactor {
            get { return pollutionFactor; }
        }

        static BuildingEffect[] effects;

        static Factory()
        {
            effects = new BuildingEffect[4];

            effects[0] = new BuildingEffect("Money +5", new LightSequence("*"), Add5, true);
            effects[1] = new BuildingEffect("Money +20", new LightSequence("***"), Add20, false);
            effects[2] = new BuildingEffect("Clean technology", new LightSequence("GG"), Clean, true);
            effects[3] = new BuildingEffect("Cleaner technology", new LightSequence("GGGG"), Cleaner, true);
        }

        public static void UnlockEffect(int effectIndex) {
            effects[effectIndex].isKnown = true;    
        }

        public Factory() { /* Default constructor */ }

        public Factory(int x, int y)
        {
            Initialize(x, y);

            Name = "Factory";
        }

        public override void Initialize(int x, int y)
        {
            base.Initialize(x, y, WIDTH, HEIGHT, GetTexture());
        }

        public override void Draw(SpriteBatchRelative spriteBatch)
        {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
            
            effectDisplay.Sequence = effects[ActivatedEffect].sequence;
            effectDisplay.Draw(spriteBatch, true);
        }

        public override BuildingEffect[] GetEffects()
        {
            return Factory.effects;
        }

        public override HashSet<Point> GetEffectRange(int effectIndex)
        {
            HashSet<Point> points = new HashSet<Point>();

            int startRow = GridLocation.Left - EFFECT_RANGE;
            int endRow = GridLocation.Right + EFFECT_RANGE;
            int startCol = GridLocation.Top - EFFECT_RANGE;
            int endCol = GridLocation.Bottom + EFFECT_RANGE;

            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    points.Add(new Point(row, col));
                }
            }

            return points;
        }

        public void EarnMoney(int increment)
        {
            World.Money += increment;
            Rectangle textRect = BoundingBox;
            textRect.Width /= 4;
            textRect.Height /= 4;
            textRect = Geometry.Translate(textRect, textRect.Width * 2, textRect.Height);
            
            TextBox textBox = new TextBox(textRect, "+$" + increment.ToString(), Color.White, TextBox.AlignType.Center);
            textBox.LayerDepth = Layer.Depth["TextNotice"];
            Animation effect = Display.CreateAnimation(textBox, 1);
            effect.AddTranslationFrame(new Point(textRect.X, textRect.Y - textRect.Height), 1, Animation.EaseType.In);
        }
        
        public override Texture2D GetTexture()
        {
            return MediaRepository.Textures["Factory"];
        }

        private static bool Add5(Building building) {
            ((Factory) building).EarnMoney(5);

            return false;
        }
        private static bool Add20(Building building) {
            ((Factory) building).EarnMoney(20);

            return false;
        }

        private static bool Clean(Building building) {
            //It's worth it to check to see if the polution factor isn't already 0, just in case.
            if (((Factory) building).pollutionFactor > .5) 
                ((Factory) building).pollutionFactor = .5;

            return true;
        }

        private static bool Cleaner(Building building) {
            ((Factory) building).pollutionFactor = 0.0;

            return true;
        }
    }
}
