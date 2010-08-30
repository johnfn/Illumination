using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.Utility {
    static class Geometry {
        public static double Distance(Point p1, Point p2) {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Rectangle Sum(Rectangle r1, Rectangle r2)
        {
            return new Rectangle(r1.X + r2.X, r1.Y + r2.Y, r1.Width + r2.Width, r1.Height + r2.Height);
        }

        public static Point Sum(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point Difference(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Dimension Sum(Dimension d1, Dimension d2)
        {
            return new Dimension(d1.Width + d2.Width, d1.Height + d2.Height);
        }

        public static Rectangle Translate(Rectangle r, int x, int y)
        {
            return new Rectangle(r.X + x, r.Y + y, r.Width, r.Height);
        }

        public static Point Translate(Point p, int x, int y) {
            return new Point(p.X + x, p.Y + y);
        }

        public static Rectangle ConstructRectangle(Point p, Dimension d)
        {
            return new Rectangle(p.X, p.Y, d.Width, d.Height);
        }

        public static Rectangle Scale(Rectangle rect, double scale) {
            return new Rectangle((int) (rect.X * scale), (int) (rect.Y * scale),
                (int) (rect.Width * scale),
                (int) (rect.Height * scale));
        }

        public static Point Scale(Point p, double scale) {
            return new Point((int) (p.X * scale), (int) (p.Y * scale));
        }

        public static Vector2 CenterText(string text, SpriteFont font, Rectangle boundingBox)
        {
            Vector2 textSize = font.MeasureString(text);
            int x = (int)(boundingBox.X + boundingBox.Width / 2 - textSize.X / 2);
            int y = (int)(boundingBox.Y + boundingBox.Height / 2 - textSize.Y / 2);
            return new Vector2(x, y);
        }
    }
}
