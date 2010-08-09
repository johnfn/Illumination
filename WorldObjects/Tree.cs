using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;

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

        public Tree(int x, int y) : base(x, y, 1, 1) {
            direction = DirectionType.South;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MediaRepository.Textures["TreeOfLight"], BoundingBox, Color.White);

            switch (direction)
            {
                case DirectionType.North:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_N"], BoundingBox, Color.White);
                    break;
                case DirectionType.East:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_E"], BoundingBox, Color.White);
                    break;
                case DirectionType.South:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_S"], BoundingBox, Color.White);
                    break;
                case DirectionType.West:
                    spriteBatch.Draw(MediaRepository.Textures["Arrow_W"], BoundingBox, Color.White);
                    break;
            }
        }
    }
}
