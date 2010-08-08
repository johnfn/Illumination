using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Illumination.Logic {
    public static class World {
        #region Properties

        static Tile[,] grid;
        static int tileWidth;
        static int tileHeight;

        public static int TileWidth {
            get { return tileWidth; }
        }

        public static int TileHeight {
            get { return tileHeight; }
        }

        public static Tile[,] Grid {
            get { return grid; }
        }

        #endregion

        public static void InitalizeWorld(int numRows, int numCols, int displayWidth, int displayHeight) {
            tileWidth = displayWidth / numCols;
            tileHeight = displayHeight / numRows;

            grid = new Tile[numRows, numCols];
            for (int row = 0; row < numRows; row++) {
                for (int col = 0; col < numCols; col++) {
                    grid[row, col] = new Tile(new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight), Tile.TileType.Grass);
                }
            }
            grid[0, 0].AddEntity(new Person(0, 0, 50, 50));
        }

        #region Public Methods

        public static void Draw(SpriteBatch spriteBatch) {
            foreach (Tile tile in grid) {
                tile.Draw(spriteBatch);
            }
        }

        public static void ViewportToGridLocation(int x, int y) {
            Console.WriteLine("{0}, {1}", x, y);
        }

        #endregion
    }
}