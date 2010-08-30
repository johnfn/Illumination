using Microsoft.Xna.Framework;
using Illumination.Logic;
using Illumination.WorldObjects;
using System;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Illumination.Graphics.AnimationHandler;
using SpriteSheetRuntime;
using Illumination.Utility;
using Illumination.Components;
using Illumination.Components.Panels;

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
        static Point viewportShift;
        static Dimension tileSize;
        static Dimension viewportDimension;
        static double scale;

        static Panel nightOverlay;
        public static void NightOverlay(bool isOn)
        {
            if (isOn)
            {
                nightOverlay.Activate();
            }
            else
            {
                nightOverlay.Deactivate();
            }
        }

        public static Dimension TileSize 
        {
            get { return tileSize; }
            set { tileSize = value; }
        }

        public static Point ViewportShift {
            get { return viewportShift; }
            set { viewportShift = value; }
        }

        public static Dimension ViewportDimension {
            get { return viewportDimension; }
            set { viewportDimension = value; }
        }

        public static void TranslateViewport(int x, int y) {
            viewportShift.X += x;
            viewportShift.Y += y;
        }

        public static double Scale {
            get { return scale; }
            set { scale = value; }
        }

        public static void InitializeDisplay(Dimension tileSize, Point gridOrigin, Dimension viewportDimension) 
        {
            Display.tileSize = tileSize;
            Display.gridOrigin = gridOrigin;
            Display.viewportDimension = viewportDimension;

            nightOverlay = new Panel(Geometry.ConstructRectangle(gridOrigin, viewportDimension), new Color(0, 0, 0, 50));
            nightOverlay.Deactivate();

            Display.scale = 1.0;
        }

        public static void DrawWorld(SpriteBatchRelative spriteBatch, GameTime gameTime) 
        {
            foreach (Tile tile in World.Grid) {
                tile.Draw(spriteBatch);
            }

            foreach (Building building in World.BuildingSet) {
                if (!building.Hidden) {
                    building.Draw(spriteBatch);
                }
            } 
            
            foreach (Item item in World.ItemSet)
            {
                if (!item.Hidden)
                {
                    item.Draw(spriteBatch);
                }
            }

            if (World.SelectedEntities.Count > 0) {
                foreach (Entity e in World.SelectedEntities) {
                    spriteBatch.Draw(MediaRepository.Textures["TileBorder"], Display.GridLocationToViewport(e.GridLocation), Color.White);
                }
            }

            animationController.Draw(spriteBatch, gameTime);
            nightOverlay.Draw(spriteBatch);
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

        public static Animation CreateAnimation(Component component, double durationInSec)
        {
            Animation animation = new Animation(component, durationInSec);
            animationController.AddAnimation(animation);

            return animation;
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
            p = Geometry.Difference(p, gridOrigin);
            double shift = (World.Grid.GetLength(1) - 1) / 2.0;
            
            return new Point((int)Math.Floor(p.Y / (double)tileSize.Height - p.X / (double)tileSize.Width + shift + 0.5),
                (int)Math.Floor(p.Y / (double)tileSize.Height + p.X / (double)tileSize.Width - shift - 0.5));
        }

        public static Point RelativeViewportToGridLocation(Point p) {
            return ViewportToGridLocation(Geometry.Scale(
                Geometry.Translate(p, viewportShift.X, viewportShift.Y), 1 / Display.Scale));
        }

        public static Point GridLocationToViewport(Point p) {
            return GridLocationToViewport(new Vector2(p.X, p.Y));
        }

        public static Point GridLocationToViewport(Vector2 v) {
            return new Point((int)((World.Grid.GetLength(1) + v.Y - v.X - 1) * tileSize.Width / 2) + gridOrigin.X, 
                (int)((v.X + v.Y) * tileSize.Height / 2) + gridOrigin.Y);
        }

        public static Point GridCenterToViewport(Point p) {
            p = GridLocationToViewport(p);

            p.X += tileSize.Width / 2;
            p.Y += tileSize.Height / 2;

            return p;
        }

        public static Rectangle GridLocationToViewport(Rectangle gridLocation) {
            Point anchorPoint = GridLocationToViewport(new Point(gridLocation.X, gridLocation.Y));
            anchorPoint.X -= (gridLocation.Width - 1) * tileSize.Width / 2;
            int gridWidth = (gridLocation.Width + gridLocation.Height) * tileSize.Width / 2;
            int gridHeight = (gridLocation.Width + gridLocation.Height) * tileSize.Height / 2;

            return new Rectangle(anchorPoint.X, anchorPoint.Y, gridWidth, gridHeight);
        }

        public static Rectangle GetTextureBoundingBox(Texture2D texture, Rectangle gridLocation, int elevation)
        {
            Rectangle boundingBox = GridLocationToViewport(gridLocation);
            int newHeight = (int)(texture.Height * boundingBox.Width / (double)texture.Width);
            return new Rectangle(boundingBox.X, boundingBox.Y - newHeight + boundingBox.Height - elevation, boundingBox.Width, newHeight);
        }

        public static Rectangle GetTextureBoundingBox(Texture2D texture, Point gridLocation, int elevation)
        {
            return GetTextureBoundingBox(texture, new Rectangle(gridLocation.X, gridLocation.Y, 1, 1), elevation);
        }

        public static Rectangle GetTextureBoundingBox(Texture2D texture, Vector2 gridLocation, int elevation)
        {
            Rectangle boundingBox = GetTextureBoundingBox(texture, new Rectangle((int)Math.Floor(gridLocation.X), (int)Math.Floor(gridLocation.Y), 1, 1), elevation);
            Point anchorPoint = GridLocationToViewport(gridLocation);
            Point difference = Geometry.Difference(anchorPoint, new Point(boundingBox.X, boundingBox.Y));
            return Geometry.Translate(boundingBox, difference.X, difference.Y);
        }

        public static void ScaleView(double multiplier) {
            scale *= multiplier;

            viewportShift.X = (int) (viewportShift.X * multiplier);
            viewportShift.Y = (int) (viewportShift.Y * multiplier);
        }
    }
}