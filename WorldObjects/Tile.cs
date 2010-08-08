#region Using Statements

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;

#endregion

namespace Illumination.WorldObjects
{
    public class Tile : WorldObject
    {
        public enum TileType
        {
            Water,
            Grass
        }

        #region Properties

        TileType type;
        HashSet<Entity> entities;

        public HashSet<Entity> Entities
        {
            get { return entities; }
        }

        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }

        #endregion

        #region Constructor

        public Tile(Rectangle rectangle, TileType type)
        {
            BoundingBox = rectangle;
            this.type = type;
            entities = new HashSet<Entity>();
        }

        #endregion

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (type)
            {
                case TileType.Grass:
                    spriteBatch.Draw(MediaRepository.Textures["GrassTile"], BoundingBox, Color.White);
                    break;
                case TileType.Water:
                    spriteBatch.Draw(MediaRepository.Textures["WaterTile"], BoundingBox, Color.White);
                    break;
            }

            foreach (Entity entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        #endregion
    }
}