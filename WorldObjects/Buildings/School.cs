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

        const int EffectRangeBasic = 2;
        const int EffectRangeAdvanced = 3;

        private class EducationEffect : Building.BuildingEffect {
            public int range;
            public int increment;

            public EducationEffect(string description, LightSequence sequence, bool isKnown, int range, int increment)
                : base(description, sequence,
                    delegate(Building building) {
                        return EducatePeople(building, range, increment);
                    }, isKnown) {

                this.range = range;
                this.increment = increment;
            }
        }

        static EducationEffect[] effects;

        static School() {
            effects = new EducationEffect[4];

            effects[0] = new EducationEffect("Education +1 (Range 2)", new LightSequence("Y"), true, 2, 1);
            effects[1] = new EducationEffect("Education +1 (Range 3)", new LightSequence("YB"), true, 3, 1);
            effects[2] = new EducationEffect("Education +2 (Range 2)", new LightSequence("YYY"), false, 2, 2);
            effects[3] = new EducationEffect("Education +2 (Range 3)", new LightSequence("YYYB"), false, 3, 2);
        }

        public static void UnlockEffect(int effectIndex) {
            effects[effectIndex].isKnown = true;    
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

            effectDisplay.Sequence = effects[ActivatedEffect].sequence;
            effectDisplay.Draw(spriteBatch, true);
        }

        public override BuildingEffect[] GetEffects() {
            return School.effects;
        }

        public override HashSet<Point> GetEffectRange(int activatedEffect) {
            int range = effects[activatedEffect].range;

            HashSet<Point> points = new HashSet<Point>();

            int startRow = GridLocation.Left - range;
            int endRow = GridLocation.Right + range;
            int startCol = GridLocation.Top - range;
            int endCol = GridLocation.Bottom + range;

            for (int row = startRow; row < endRow; row++) {
                for (int col = startCol; col < endCol; col++) {
                    points.Add(new Point(row, col));
                }
            }

            return points;
        }

        static void EducateAnimation(Rectangle gridLocation, int effectRange) {
            Rectangle boundingBox = new Rectangle(gridLocation.Left - effectRange + 1, gridLocation.Top - effectRange + 1,
                gridLocation.Width + effectRange * 2 - 2, gridLocation.Height + effectRange * 2 - 2);
            Animation effect = Display.CreateAnimation(MediaRepository.Sheets["Glow"], Display.GridLocationToViewport(boundingBox), 2, 0.1);
            effect.AddColorFrame(Color.TransparentWhite, 0);
            effect.AddColorFrame(Color.Yellow, 0.5);
            effect.AddColorFrame(Color.TransparentWhite, 2);
        }

        private static bool EducatePeople(Building building, int range, int educationIncrement) {
            EducateAnimation(building.GridLocation, range);
            
            foreach (Point p in building.GetRange()) {
                if (!World.InBound(p)) {
                    continue;
                }

                foreach (Entity entity in World.GetEntities(p.X, p.Y)) {
                    if (entity.Hidden) {
                        continue;
                    }
                    if (entity is Person) {
                        Person thisPerson = (Person) entity;
                        if (!thisPerson.IsEducated) {
                            thisPerson.Educate(educationIncrement);
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
