using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics;

namespace Illumination.WorldObjects {
    public abstract class Item : Entity
    {
        protected int cost;
        public int Cost {
            get { return cost; }
            set { cost = value; }
        }
        
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

        public void SetLocation(Point p) {
            GridLocation = new Rectangle(p.X, p.Y, GridLocation.Width, GridLocation.Height);
            BoundingBox = Display.GetTextureBoundingBox(GetTexture(), GridLocation, 0);
        }

        public abstract Item CreateNewItem();
    }
}
