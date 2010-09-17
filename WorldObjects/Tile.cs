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

        public enum TileHighlightColor {
            None,
            Green,
            Red,
            Blue
        }

        static Dictionary <TileType, int> movementCosts;
        static Dictionary <TileHighlightColor, Color> tileHighlightColors;

        static Tile() {
            movementCosts = new Dictionary<TileType, int>();
            movementCosts[TileType.Water] = -1;
            movementCosts[TileType.Grass] = 1;

            tileHighlightColors = new Dictionary<TileHighlightColor, Color>();
            tileHighlightColors[TileHighlightColor.Green] = new Color(0, 255, 0, 100);
            tileHighlightColors[TileHighlightColor.Red] = new Color(255, 0, 0, 100);
            tileHighlightColors[TileHighlightColor.Blue] = new Color(0, 0, 255, 100);
        }

        #region Properties

        TileType type;
        public TileType Type {
            get { return type; }
            set { type = value; }
        }

        HashSet<Entity> entities;
        public HashSet<Entity> Entities {
            get { return entities; }
        }

        TileHighlightColor highlightColor;
        public TileHighlightColor HighlightColor {
            get { return highlightColor; }
            set { highlightColor = value; }
        }

        public Point gridLocation;
        public Point GridLocation {
            get { return gridLocation; }
            set { gridLocation = value; }
        }

        #endregion

        #region Constructor

        public Tile(int gridX, int gridY, TileType type) {
            base.BoundingBox = Display.GridLocationToViewport(new Rectangle(gridX, gridY, 1, 1));
            this.type = type;
            this.highlightColor = TileHighlightColor.None;
            this.gridLocation = new Point(gridX, gridY);
            entities = new HashSet<Entity>();
        }

        #endregion

        #region Public Methods

        public override void Draw(SpriteBatchRelative spriteBatch) {
            switch (type) {
                case TileType.Grass:
                    spriteBatch.DrawRelative(MediaRepository.Textures["GrassTile"], BoundingBox, new Color(255, 255, 255, 100), Layer.Depth["Tile"]);
                    break;
                case TileType.Water:
                    spriteBatch.DrawRelative(MediaRepository.Textures["WaterTile"], BoundingBox, new Color(255, 255, 255, 100), Layer.Depth["Tile"]);
                    break;
            }
            if (highlightColor != TileHighlightColor.None) {
                spriteBatch.DrawRelative(MediaRepository.Textures["BlankTile"], BoundingBox, tileHighlightColors[highlightColor], Layer.Depth["Highlight"]);
            }

            foreach (Entity entity in entities) {
                if (!entity.DeferDraw && !entity.Hidden) {
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

        public void UpdateBoundingBox() {
            base.BoundingBox = Display.GridLocationToViewport(new Rectangle(gridLocation.X, gridLocation.Y, 1, 1));
            foreach (Entity e in entities) {
                e.UpdateBoundingBox();
            }
        }
    }
}