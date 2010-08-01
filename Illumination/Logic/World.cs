using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Illumination.Logic
{
    public class World
    {
        #region Properties

        Tile[,] grid;
        int tileWidth;
        int tileHeight;

        public int TileWidth 
        {
            get { return tileWidth; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
        }

        public Tile[,] Grid
        {
            get { return grid; }
        }

        #endregion

        #region Constructor

        public World(int numRows, int numCols, int displayWidth, int displayHeight)
        {
            tileWidth = displayWidth / numCols;
            tileHeight = displayHeight / numRows;

            grid = new Tile[numRows, numCols];
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    grid[row, col] = new Tile(new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight), Tile.TileType.Grass);
                }
            }
        }

        #endregion

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in grid)
            {
                tile.Draw(spriteBatch);
            }
        }

        #endregion
    }
}