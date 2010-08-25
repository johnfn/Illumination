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

        public const int SHIFT_DOWN = 1 << 0;
        public const int CTRL_DOWN = 1 << 1;
        public const int ALT_DOWN = 1 << 2;

        private int modifiers;
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

        public bool IsCtrlDown {
            get { return (modifiers & CTRL_DOWN) > 0; }
        }

        public bool IsShiftDown {
            get { return (modifiers & SHIFT_DOWN) > 0; }
        }

        public bool IsAltDown {
            get { return (modifiers & ALT_DOWN) > 0; }
        }

        public MouseEvent(Point currentLocation, int modifiers) : this(currentLocation, MouseButton.Left, modifiers) { }

        public MouseEvent(Point currentLocation, MouseButton button, int modifiers) : this(currentLocation, currentLocation, button, modifiers) { }

        public MouseEvent(Point startLocation, Point currentLocation, int modifiers) : this(startLocation, currentLocation, MouseButton.Left, modifiers) { }

        public MouseEvent(Point startLocation, Point currentLocation, MouseButton button, int modifiers) {
            this.startLocation = startLocation;
            this.currentLocation = currentLocation;
            this.button = button;
            this.modifiers = modifiers;
        }
    }
}
