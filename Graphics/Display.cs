using Microsoft.Xna.Framework;
using Illumination.Logic;
using Illumination.WorldObjects;
using System;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics.AnimationHandler;
using SpriteSheetRuntime;

namespace Illumination.Graphics
{
    public struct Dimension
    {
        int width;
        int height;

        public int Width 
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Dimension(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    };

    public static class Display {
        static AnimationController animationController = new AnimationController();

        static int tileWidth;
        static int tileHeight;

        public static int TileWidth {
            get { return tileWidth; }
        }

        public static int TileHeight {
            get { return tileHeight; }
        }

        public static void InitializeDisplay(int numRows, int numCols, int displayWidth, int displayHeight) {
            tileWidth = displayWidth / numCols;
            tileHeight = displayHeight / numRows;
        }

        public static void DrawWorld(SpriteBatch spriteBatch, GameTime gameTime) {
            foreach (Tile tile in World.Grid) {
                tile.Draw(spriteBatch);
            }

            foreach (Building building in World.BuildingSet) {
                if (!building.Hidden) {
                    building.Draw(spriteBatch);
                }
            }

            animationController.Draw(spriteBatch, gameTime);

            if (World.IsNight) {
                /* Transparent Black Mask */
                spriteBatch.Draw(MediaRepository.Textures["Blank"], new Rectangle(0, 0, 500, 500), new Color(0, 0, 0, 50));
            }

            foreach (Light light in World.LightSet) {
                if (!light.Hidden) {
                    light.Draw(spriteBatch);
                }
            }
        }

        public static Animation CreateAnimation(Texture2D texture, Point position, Dimension size, double durationInSec)
        {
            Animation animation = new Animation(texture, position, size, durationInSec);
            animationController.AddAnimation(animation);

            return animation;
        }

        public static Animation CreateAnimation(Texture2D texture, Rectangle rect, double durationInSec) {
            return CreateAnimation(texture, new Point(rect.X, rect.Y), new Dimension(rect.Width, rect.Height), durationInSec);
        }

        public static Animation CreateAnimation(SpriteSheet spriteSheet, Point position, Dimension size, double durationInSec, double spriteFrameDurationInSec)
        {
            Animation animation = new Animation(spriteSheet, position, size, durationInSec, spriteFrameDurationInSec);
            animationController.AddAnimation(animation);

            return animation;
        }

        public static Animation CreateAnimation(SpriteSheet spriteSheet, Rectangle rectangle, double durationInSec, double spriteFrameDurationInSec) {
            return CreateAnimation(spriteSheet, new Point(rectangle.X, rectangle.Y), new Dimension(rectangle.Width, rectangle.Height), durationInSec, spriteFrameDurationInSec);
        }

        public static void RemoveAnimation(Animation animation)
        {
            animationController.RemoveAnimation(animation);
        }

        public static Point ViewportToGridLocation(Point p) {
            return new Point(p.Y / tileHeight, p.X / tileWidth);
        }

        public static Rectangle ViewportToGridLocation(Rectangle viewport) {
            Point anchorPoint = ViewportToGridLocation(new Point(viewport.X, viewport.Y));
            int gridWidth = (int) Math.Ceiling((double) viewport.Width / tileWidth);
            int gridHeight = (int) Math.Ceiling((double) viewport.Height / tileHeight);

            return new Rectangle(anchorPoint.X, anchorPoint.Y, gridWidth, gridHeight);
        }

        public static Point GridLocationToViewport(Point p) {
            return new Point(p.Y * tileHeight, p.X * tileWidth);
        }

        public static Point GridCenterToViewport(Point p) {
            p = GridLocationToViewport(p);

            p.X += tileWidth / 2;
            p.Y += tileHeight / 2;

            return p;
        }

        public static Rectangle GridLocationToViewport(Rectangle gridLocation) {
            Point anchorPoint = GridLocationToViewport(new Point(gridLocation.X, gridLocation.Y));
            int gridWidth = gridLocation.Width * TileWidth;
            int gridHeight = gridLocation.Height * TileHeight;

            return new Rectangle(anchorPoint.X, anchorPoint.Y, gridWidth, gridHeight);
        }
    }
}