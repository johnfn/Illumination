using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects
{
    public class Tree : Entity
    {
        DirectionType direction;

        public DirectionType Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Texture2D Texture
        {
            get { return MediaRepository.Textures["TreeOfLight"]; }
        }

        public Tree() : base() { /* Default Constructor */ }

        public Tree(int x, int y) {
            base.Initialize(x, y, 1, 1, Texture);

            direction = DirectionType.South;

            Name = "Tree";
        }

        public override void Draw(SpriteBatchRelative spriteBatch)
        {
            Rectangle arrowBox = Display.GridLocationToViewport(GridLocation);
            Color arrowColor = new Color(255, 255, 255, 200);
            switch (direction)
            {
                case DirectionType.North:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_N"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.East:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_E"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.South:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_S"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
                case DirectionType.West:
                    spriteBatch.DrawRelative(MediaRepository.Textures["Arrow_W"], arrowBox, arrowColor, Layer.Depth["Arrow"]);
                    break;
            }

            spriteBatch.DrawRelative(Texture, BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public override Texture2D GetTexture() {
            return MediaRepository.Textures["TreeOfLight"];
        }
    }
}
