﻿using System;
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

        const int EFFECT_RANGE = 2;

        static Dictionary<LightSequence, DoEffect> effects;

        static ResearchCenter()
        {
            effects = new Dictionary<LightSequence, DoEffect>();
            effects.Add(new LightSequence("*"), StandardEffect);
        }

        public ResearchCenter() { /* Default constructor */ }

        public ResearchCenter(int x, int y)
        {
            Initialize(x, y);

            Name = "ResearchCenter";
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
            return ResearchCenter.effects;
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

        public override Texture2D GetTexture()
        {
            return MediaRepository.Textures["ResearchCenter"];
        }

        private static bool StandardEffect(Building building)
        {
            return false;
        }
    }
}