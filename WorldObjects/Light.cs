using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Illumination.Graphics;

namespace Illumination.WorldObjects {
    public class Light : Entity {
        public enum LightType
        {
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

        public static Texture2D Texture {
            get { return MediaRepository.Textures["Light"]; }
        }

        public override Texture2D GetTexture() {
            return MediaRepository.Textures["Light"];
        }

        DirectionType direction;
        public DirectionType Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        LightType type;
        public LightType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Light(int x, int y) : base(x, y, 1, 1, Texture) {
            type = LightType.White;
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.Draw(Texture, BoundingBox, colorTable[type]);
        }
    }
}