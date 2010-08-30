using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.WorldObjects {
    public abstract class Item : Entity
    {
        public Item() : base() { }

        public Item(int x, int y, int width, int height, Texture2D texture) {
            Initialize(x, y, width, height, texture);

            Name = "Item";
        }

        public override void Initialize(int x, int y, int width, int height, Texture2D texture)
        {
            base.Initialize(x, y, width, height, texture);

            base.DeferDraw = true;
        }
    }
}
