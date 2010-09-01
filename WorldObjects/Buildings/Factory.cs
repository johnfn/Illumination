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

        static Dictionary<LightSequence, DoEffect> effects;

        static Factory()
        {
            effects = new Dictionary<LightSequence, DoEffect>();
            effects.Add(new LightSequence("*"), StandardEffect);
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
        }

        protected override Dictionary<LightSequence, Building.DoEffect> GetEffects()
        {
            return Factory.effects;
        }

        public override HashSet<Point> GetEffectRange()
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
            Animation effect = Display.CreateAnimation(textBox, 1);
            effect.AddTranslationFrame(new Point(textRect.X, textRect.Y - textRect.Height), 1, Animation.EaseType.In);
        }
        
        public override Texture2D GetTexture()
        {
            return MediaRepository.Textures["Factory"];
        }

        private static bool StandardEffect(Building building) {
            ((Factory) building).EarnMoney(5);

            return false;
        }
    }
}
