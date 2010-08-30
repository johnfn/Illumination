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

        public Tree(int x, int y) : base(x, y, 1, 1, MediaRepository.Textures["TreeOfLight"]) {
            direction = DirectionType.South;
        }

        public override void Draw(SpriteBatchRelative spriteBatch)
        {
            Rectangle arrowBox = Display.GridLocationToViewport(GridLocation);
            Color arrowColor = new Color(255, 255, 255, 200);
            switch (direction)
            {
                case DirectionType.North:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_N"], arrowBox, arrowColor);
                    break;
                case DirectionType.East:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_E"], arrowBox, arrowColor);
                    break;
                case DirectionType.South:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_S"], arrowBox, arrowColor);
                    break;
                case DirectionType.West:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_W"], arrowBox, arrowColor);
                    break;
            }

            spriteBatch.Draw(MediaRepository.Textures["TreeOfLight"], BoundingBox, Color.White);
        }

        public override Texture2D GetTexture() {
            return MediaRepository.Textures["TreeOfLight"];
        }
    }
}
