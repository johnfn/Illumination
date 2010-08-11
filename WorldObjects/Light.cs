using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects {
    public class Light : Entity {
        public enum LightType {
            White,
            Gray,
            Yellow,
            Blue,
            Red,
            Green,
            SIZE
        }

        private Point lastCollisionLocation;

        static Dictionary<LightType, Color> colorTable;
        
        static Light () {
            colorTable = new Dictionary<LightType, Color>();

            colorTable.Add(LightType.Blue, Color.Blue);
            colorTable.Add(LightType.Gray, Color.Gray);
            colorTable.Add(LightType.Yellow, Color.Yellow);
            colorTable.Add(LightType.White, Color.White);
            colorTable.Add(LightType.Green, Color.Green);
            colorTable.Add(LightType.Red, Color.Red);
        }

        DirectionType direction;

        public DirectionType Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Point LastCollisionLocation {
            get { return lastCollisionLocation; }
            set { lastCollisionLocation = value; }
        }

        LightType lightColor;

        public LightType LightColor
        {
            get { return lightColor; }
            set { lightColor = value; }
        }

        public Light(int x, int y) : base(x, y, 1, 1) {
            lightColor = LightType.White;

            lastCollisionLocation = new Point(x, y);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(MediaRepository.Textures["Light"], BoundingBox, colorTable[lightColor]);
        }
    }
}