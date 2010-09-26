using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;
using Illumination.Logic;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects {
    public class Road : Item {
        private static Texture2D texture = MediaRepository.Textures["WaterTile"];

        public Road()
            : base(0, 0, 1, 1, texture) {
            Initialize();
        }

        private void Initialize() {
            blocksMovement = false;

            name = "Road";
            cost = 15;
        }

        public override Texture2D GetTexture() {
            return texture;
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public override Item CreateNewItem() {
            return new Road();
        }

        public override void ActionOnPlace(Point location) {
            Tile currentTile = World.Grid[location.X, location.Y];
            currentTile.MovementCost = currentTile.DefaultMovementCost / 2;
        }
    }

    public class Airport : Item {
        private static Texture2D texture = MediaRepository.Textures["WaterTile"];

        public Airport()
            : base(0, 0, 1, 1, texture) {
            Initialize();
        }

        private void Initialize() {
            blocksMovement = false;

            name = "Airport";
            cost = 10;
        }

        public override Texture2D GetTexture() {
            return texture;
        }

        public override void Draw(SpriteBatchRelative spriteBatch) {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public override Item CreateNewItem() {
            return new Airport();
        }

        public override void ActionOnPlace(Point location) {
            World.LocationsWithAirports.Add(location);
        }
    }
}
