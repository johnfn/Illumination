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

        
        static Dictionary<char, LightType> shortNames;
        static Dictionary<LightType, Color> colorTable;
        
        Point lastCollisionLocation;
        Vector2 location;
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        
        static Light () {
            colorTable = new Dictionary<LightType, Color>();

            colorTable.Add(LightType.Blue, new Color(120, 120, 255));
            colorTable.Add(LightType.Gray, new Color(170, 150, 170));
            colorTable.Add(LightType.Yellow, new Color(255, 255, 120));
            colorTable.Add(LightType.White, Color.White);
            colorTable.Add(LightType.Green, new Color(120, 255, 120));
            colorTable.Add(LightType.Red, new Color(255, 120, 120));

            shortNames = new Dictionary<char, LightType>();

            shortNames['B'] = LightType.Blue;
            shortNames['g'] = LightType.Gray;
            shortNames['Y'] = LightType.Yellow;
            shortNames['W'] = LightType.White;
            shortNames['G'] = LightType.Green;
            shortNames['R'] = LightType.Red;
        }

        public static LightType GetLightType(char ch) {
            return shortNames.ContainsKey(ch) ? shortNames[ch] : LightType.Gray;
        }

        public static Color GetLightColor(LightType type) {
            return colorTable[type];
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

        LightType type;

        public LightType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Light(int x, int y) : base(x, y, 1, 1, MediaRepository.Textures["Light"]) {
            type = LightType.White;

            lastCollisionLocation = new Point(x, y);
            location = new Vector2(x, y);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(MediaRepository.Textures["Light"], BoundingBox, colorTable[type]);
        }
    }
}