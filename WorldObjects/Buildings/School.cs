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

        //static Dictionary<LightSequence, DoEffect> effects;
        static BuildingEffect[] effects;

        static School() {
            effects = new BuildingEffect[4];

            effects[0] = new BuildingEffect("Education +1 (Range 2)", new LightSequence("Y"), Effect0, true);
            effects[1] = new BuildingEffect("Education +1 (Range 3)", new LightSequence("YB"), Effect1, true);
            effects[2] = new BuildingEffect("Education +2 (Range 2)", new LightSequence("YYY"), Effect2, true);
            effects[3] = new BuildingEffect("Education +2 (Range 3)", new LightSequence("YYYB"), Effect3, true);
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
        
        public override HashSet<Point> GetEffectRange(int effectIndex)
        {
            HashSet<Point> points = new HashSet<Point>();

            int effectRange = 0;
            if (effectIndex == 0 || effectIndex == 2) {
                effectRange = EffectRangeBasic;
            }
            else {
                effectRange = EffectRangeAdvanced;
            }

            int startRow = GridLocation.Left - effectRange;
            int endRow = GridLocation.Right + effectRange;
            int startCol = GridLocation.Top - effectRange;
            int endCol = GridLocation.Bottom + effectRange;

            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
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

        /* "Education +1 (Range 2)", Y */
        private static bool Effect0(Building building) {
            EducateAnimation(building.GridLocation, EffectRangeBasic);

            foreach (Point p in building.GetEffectRange(0))
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

        /* "Education +1 (Range 3)", YB */
        private static bool Effect1(Building building) {
            EducateAnimation(building.GridLocation, EffectRangeAdvanced);

            foreach (Point p in building.GetEffectRange(1)) {
                if (!World.InBound(p)) {
                    continue;
                }

                foreach (Entity entity in World.GetEntities(p.X, p.Y)) {
                    if (entity.Hidden) {
                        continue;
                    }

                    if (entity is Person) {
                        Person thisPerson = (Person)entity;
                        if (!thisPerson.IsEducated) {
                            thisPerson.Educate(1);
                        }
                    }
                }
            }

            return true;
        }

        /* "Education +2 (Range 2)", YYY */
        private static bool Effect2(Building building) {
            EducateAnimation(building.GridLocation, EffectRangeBasic);

            foreach (Point p in building.GetEffectRange(2)) {
                if (!World.InBound(p)) {
                    continue;
                }

                foreach (Entity entity in World.GetEntities(p.X, p.Y)) {
                    if (entity.Hidden) {
                        continue;
                    }

                    if (entity is Person) {
                        Person thisPerson = (Person)entity;
                        if (!thisPerson.IsEducated) {
                            thisPerson.Educate(2);
                        }
                    }
                }
            }

            return true;
        }

        /* "Education +2 (Range 3)", YYYB */
        private static bool Effect3(Building building) {
            EducateAnimation(building.GridLocation, EffectRangeAdvanced);

            foreach (Point p in building.GetEffectRange(3)) {
                if (!World.InBound(p)) {
                    continue;
                }

                foreach (Entity entity in World.GetEntities(p.X, p.Y)) {
                    if (entity.Hidden) {
                        continue;
                    }

                    if (entity is Person) {
                        Person thisPerson = (Person)entity;
                        if (!thisPerson.IsEducated) {
                            thisPerson.Educate(2);
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
