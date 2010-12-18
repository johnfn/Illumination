using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Illumination.Utility {
    public class Polygon {
        private List<Point> points;
        public List<Point> Points {
            get { return points; }
            set { points = value; }
        }

        public Polygon() : this(new List<Point>()) { }

        public Polygon(List<Point> points) {
            this.points = points;
        }

        public Polygon(Point[] pointsArr) {
            points = new List<Point>();
            foreach (Point p in pointsArr) {
                points.Add(p);
            }
        }

        public Polygon(Polygon other) : this() {
            foreach (Point p in other.Points) {
                points.Add(new Point(p.X, p.Y));
            }
        }

        public void AddPoint(Point p) {
            points.Add(p);
        }

        public void RemovePoint(Point p) {
            points.Remove(p);
        }

        public void RemovePoint(int index) {
            points.RemoveAt(index);
        }

        public void Translate(int x, int y) {
            List <Point> pointsTranslated = new List<Point>();
            foreach (Point p in points) {
                pointsTranslated.Add(Geometry.Translate(p, x, y));
            }
            points = pointsTranslated;
        }

        public bool Contains(Point p) {
            bool oddCrossing = false;

            for (int i = 0, j = points.Count - 1; i < points.Count; j = i++) {
                Point curr = points.ElementAt(i);
                Point prev = points.ElementAt(j);
                if ((prev.Y < p.Y && curr.Y >= p.Y) || (curr.Y < p.Y && prev.Y >= p.Y)) {
                    if (p.X < (prev.X - curr.X) * (p.Y - curr.Y) / (prev.Y - curr.Y) + curr.X) {
                        oddCrossing = !oddCrossing;
                    }
                }
            }
            return oddCrossing;
        }

        public override string ToString() {
            string s = "";
            foreach (Point p in points) {
                s += p.ToString() + ", ";
            }
            return s.Substring(0, s.Length - 2);
        }
    }
}
