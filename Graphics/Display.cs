using Microsoft.Xna.Framework;
using Illumination.Logic;
using Illumination.WorldObjects;
using System;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics.AnimationHandler;
using SpriteSheetRuntime;
using Illumination.Utility;

namespace Illumination.Graphics
{
    public struct Dimension
    {
        public int Width;
        public int Height;

        public Dimension(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    };

    public static class Display {
        static AnimationController animationController = new AnimationController();

        static Point gridOrigin;
        static Dimension tileSize;
        static Dimension gridViewportSize;

        public static Dimension TileSize 
        {
            get { return tileSize; }
        }

        public static Dimension GridViewportSize
        {
            get { return gridViewportSize; }
        }

        public static void InitializeDisplay(int numRows, int numCols, Rectangle displayWindow) 
        {
            tileSize = new Dimension(displayWindow.Width / numCols, displayWindow.Height / numRows);
            gridViewportSize = new Dimension(displayWindow.Width, displayWindow.Height);
            gridOrigin = new Point(displayWindow.X, displayWindow.Y);
        }

        public static void DrawWorld(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            foreach (Tile tile in World.Grid) {
                tile.Draw(spriteBatch);
            }

            foreach (Building building in World.BuildingSet) {
                if (!building.Hidden) {
                    building.Draw(spriteBatch);
                }
            }

            if (World.SelectedEntities.Count > 0) {
                foreach (Entity e in World.SelectedEntities) {
                    spriteBatch.Draw(MediaRepository.Textures["TileBorder"], e.BoundingBox, Color.White);
                }
            }

            animationController.Draw(spriteBatch, gameTime);

            if (World.IsNight) {
                /* Transparent Black Mask */
                spriteBatch.Draw(MediaRepository.Textures["Blank"], Geometry.ConstructRectangle(gridOrigin, gridViewportSize), new Color(0, 0, 0, 50));
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

        public static Animation CreateAnimation(Texture2D texture, Rectangle rect, double durationInSec) 
        {
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
            return new Point((p.Y - gridOrigin.Y) / tileSize.Height, (p.X - gridOrigin.X) / tileSize.Width);
        }

        public static Rectangle ViewportToGridLocation(Rectangle viewport) {
            Point anchorPoint = ViewportToGridLocation(new Point(viewport.X, viewport.Y));
            int gridWidth = (int) Math.Ceiling((double) viewport.Width / tileSize.Width);
            int gridHeight = (int) Math.Ceiling((double) viewport.Height / tileSize.Height);

            return new Rectangle(anchorPoint.X, anchorPoint.Y, gridWidth, gridHeight);
        }

        public static Point GridLocationToViewport(Point p) {
            return new Point(gridOrigin.X + p.Y * tileSize.Height, gridOrigin.Y + p.X * tileSize.Width);
        }

        public static Point GridCenterToViewport(Point p) {
            p = GridLocationToViewport(p);

            p.X += tileSize.Width / 2;
            p.Y += tileSize.Height / 2;

            return p;
        }

        public static Rectangle GridLocationToViewport(Rectangle gridLocation) {
            Point anchorPoint = GridLocationToViewport(new Point(gridLocation.X, gridLocation.Y));
            int gridWidth = gridLocation.Width * tileSize.Width;
            int gridHeight = gridLocation.Height * tileSize.Height;

            return new Rectangle(anchorPoint.X, anchorPoint.Y, gridWidth, gridHeight);
        }
    }
}