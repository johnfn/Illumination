using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Data;
using Illumination.Logic;

namespace Illumination.WorldObjects {
    public abstract class Item : Entity
    {
        protected int cost;
        public int Cost {
            get { return cost; }
            set { cost = value; }
        }

        protected Dimension dimension;
        public Dimension ItemDimension {
            get { return dimension; }
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
            dimension = new Dimension(1, 1);
        }

        public void SetLocation(Point p) {
            GridLocation = new Rectangle(p.X, p.Y, GridLocation.Width, GridLocation.Height);
            BoundingBox = Display.GetTextureBoundingBox(GetTexture(), GridLocation, 0);
        }

        public abstract Item CreateNewItem();

        public virtual void ActionOnPlace(Point location) { }
    }

    public class Inspiration : Item {
        private static Texture2D texture = MediaRepository.Textures["WhiteLight"];

        public Inspiration()
            : base(0, 0, 1, 1, texture) {
            Initialize();
        }

        private void Initialize() {
            blocksMovement = false;

            Name = "Inspiration";
            Cost = 10;
        }

        public override Texture2D GetTexture() {
            return texture;
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public override Item CreateNewItem() {
            return new Inspiration();
        }
    }
}
