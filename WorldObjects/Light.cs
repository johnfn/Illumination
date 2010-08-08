using Microsoft.Xna.Framework.Graphics;

namespace Illumination.WorldObjects
{
    public class Light : Entity
    {
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

        public Light(int x, int y, int width, int height) : base(x, y, width, height) {}

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}