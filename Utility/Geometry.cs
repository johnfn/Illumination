using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Illumination.Utility {
    static class Geometry {
        public static double Distance(Point p1, Point p2) {
            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
