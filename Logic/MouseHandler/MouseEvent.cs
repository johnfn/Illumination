using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Illumination.Logic.MouseHandler {
    public class MouseEvent {
        public enum MouseButton {
            Left,
            Right
        }

        private Point startLocation;
        private Point currentLocation;
        private MouseButton button;

        public Point StartLocation {
            get { return startLocation; }
        }

        public Point CurrentLocation {
            get { return currentLocation; }
        }

        public MouseButton Button {
            get { return button; }
        }

        public MouseEvent(Point currentLocation) : this(currentLocation, MouseButton.Left) { }

        public MouseEvent(Point currentLocation, MouseButton button) : this(currentLocation, currentLocation, button) { }

        public MouseEvent(Point startLocation, Point currentLocation) : this(startLocation, currentLocation, MouseButton.Left) { }

        public MouseEvent(Point startLocation, Point currentLocation, MouseButton button) {
            this.startLocation = startLocation;
            this.currentLocation = currentLocation;
            this.button = button;
        }
    }
}
