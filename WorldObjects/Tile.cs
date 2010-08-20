#region Using Statements

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics;

#endregion

namespace Illumination.WorldObjects {
    public class Tile : WorldObject {
        public enum TileType {
            Water,
            Grass
        }

        static Dictionary <TileType, int> movementCosts;

        static Tile() {
            movementCosts = new Dictionary<TileType, int>();
            movementCosts[TileType.Water] = -1;
            movementCosts[TileType.Grass] = 1;
        }

        #region Properties

        TileType type;
        HashSet<Entity> entities;
        bool highlighted;

        public HashSet<Entity> Entities {
            get { return entities; }
        }

        public TileType Type {
            get { return type; }
            set { type = value; }
        }

        public bool Highlighted {
            get { return highlighted; }
            set { highlighted = value; }
        }

        #endregion

        #region Constructor

        public Tile(int gridX, int gridY, TileType type) {
            base.BoundingBox = Display.GridLocationToViewport(new Rectangle(gridX, gridY, 1, 1));
            this.type = type;
            this.highlighted = false;
            entities = new HashSet<Entity>();
        }

        #endregion

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch) {
            switch (type) {
                case TileType.Grass:
                    spriteBatch.Draw(MediaRepository.Textures["GrassTile"], BoundingBox, Color.White);
                    break;
                case TileType.Water:
                    spriteBatch.Draw(MediaRepository.Textures["WaterTile"], BoundingBox, Color.White);
                    break;
            }
            if (highlighted) {
                spriteBatch.Draw(MediaRepository.Textures["Blank"], BoundingBox, new Color(0, 255, 0, 150));
            }

            foreach (Entity entity in entities) {
                if (!entity.DeferDraw) {
                    entity.Draw(spriteBatch);
                }
            }
        }

        public int GetMovementCost() {
            foreach (Entity entity in entities) {
                if (entity.BlocksMovement) {
                    return -1;
                }
            }
            return movementCosts[type];
        }

        public void AddEntity(Entity entity) {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) {
            entities.Remove(entity);
        }

        public bool IsBlocked() {
            return entities.Count > 0;
        }

        #endregion
    }
}