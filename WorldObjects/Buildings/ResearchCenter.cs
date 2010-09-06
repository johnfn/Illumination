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

        public ResearchCenter() { /* Default constructor */ }

        public ResearchCenter(int x, int y)
        {
            Initialize(x, y);

            Name = "Research Center";
        }

        public override void Initialize(int x, int y)
        {
            base.Initialize(x, y, WIDTH, HEIGHT, GetTexture());
        }

        public override void Draw(SpriteBatchRelative spriteBatch)
        {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public override BuildingEffect[] GetEffects()
        {
            /* Exception for research center */
            return null;
        }
        
        public override Texture2D GetTexture()
        {
            return MediaRepository.Textures["ResearchCenter"];
        }

        private static bool StandardEffect(Building building)
        {
            /* Nothing Yet */
            return true;
        }
    }
}
