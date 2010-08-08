using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Illumination.Logic.MouseHandler {
    public class MouseEvent {
        private Point startLocation;
        private Point currentLocation;

        public Point StartLocation {
            get { return startLocation; }
        }

        public Point CurrentLocation {
            get { return currentLocation; }
        }

        public MouseEvent(Point startLocation, Point currentLocation) {
            this.startLocation = startLocation;
            this.currentLocation = currentLocation;
        }

        public MouseEvent(Point currentLocation) {
            this.startLocation = this.currentLocation = currentLocation;
        }
    }
}
