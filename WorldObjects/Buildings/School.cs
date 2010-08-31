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

namespace Illumination.WorldObjects {
    public class School : Building {
        const int WIDTH = 2;
        const int HEIGHT = 2;

        const int EFFECT_RANGE = 2;

        static Dictionary<LightSequence, DoEffect> effects;

        static School() {
            effects = new Dictionary<LightSequence, DoEffect>();

            effects.Add(new LightSequence("Y"), StandardEffect);
        }

        public School() { /* Default constructor */ }

        public School(int x, int y) {
            Initialize(x, y);

            Name = "School";
        }

        public Texture2D Texture {
            get { return MediaRepository.Textures["School"]; }
        }

        public override void Initialize(int x, int y) {
            base.Initialize(x, y, WIDTH, HEIGHT, Texture);
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.DrawRelative(Texture, BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        protected override Dictionary<LightSequence, Building.DoEffect> GetEffects() {
            return School.effects;
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
        
        private static bool StandardEffect(Building building)
        {
            Rectangle boundingBox = new Rectangle(building.GridLocation.Left - EFFECT_RANGE + 1, building.GridLocation.Top - EFFECT_RANGE + 1,
                building.GridLocation.Width + EFFECT_RANGE * 2 - 2, building.GridLocation.Height + EFFECT_RANGE * 2 - 2);
            Animation effect = Display.CreateAnimation(MediaRepository.Sheets["Glow"], Display.GridLocationToViewport(boundingBox), 2, 0.1);
            effect.AddColorFrame(Color.TransparentWhite, 0);
            effect.AddColorFrame(Color.Yellow, 0.5);
            effect.AddColorFrame(Color.TransparentWhite, 2);

            foreach (Point p in building.GetEffectRange())
            {
                if (!World.InBound(p))
                {
                    continue;
                }

                foreach (Entity entity in World.GetEntities(p.X, p.Y))
                {
                    if (entity.Hidden)
                    {
                        continue;
                    }

                    if (entity is Person)
                    {
                        Person thisPerson = (Person)entity;
                        if (!thisPerson.IsEducated)
                        {
                            thisPerson.Educate(1);
                        }
                    }
                }
            }

            return true;
        }

        public override Texture2D GetTexture() {
            return MediaRepository.Textures["School"];
        }
    }
}
