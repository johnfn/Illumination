using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Logic;
using Illumination.Graphics;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects {
    public class School : Building {
        private const int WIDTH = 2;
        private const int HEIGHT = 2;

        private static Dictionary<LightSequence, DoEffect> effects;

        static School() {
            effects = new Dictionary<LightSequence, DoEffect>();

            effects.Add(new LightSequence("Y"), StandardEffect);
        }

        public School() { /* Default constructor */ }

        public School(int x, int y) {
            Initialize(x, y);
        }

        public override void Initialize(int x, int y) {
            base.Initialize(x, y, WIDTH, HEIGHT);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(MediaRepository.Textures["School"], base.BoundingBox, Color.White);
        }

        protected override Dictionary<LightSequence, Building.DoEffect> GetEffects() {
            return School.effects;
        }

        public static void StandardEffect(Building building) {
            Display.CreateAnimation(MediaRepository.Sheets["Glow"], building.BoundingBox, 2, 0.1);
        }
    }
}
