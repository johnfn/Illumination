﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Illumination.Utility;

namespace Illumination.Logic.MouseHandler {
    public static class MouseController {
        private const int CLICK_TOLERANCE = 5;

        private static bool mousePressed;
        private static bool isClick;
        private static Point startLocation;

        private static MouseState previousState;
        private static MouseState currentState;

        private static HashSet<MouseListener> mouseListeners;
        private static HashSet<MouseMotionListener> mouseMotionListeners;

        public static MouseState CurrentState {
            get { return currentState; }
        }

        public static void Initialize() {
            previousState = currentState = Mouse.GetState();

            mousePressed = false;
            mouseListeners = new HashSet<MouseListener>();
            mouseMotionListeners = new HashSet<MouseMotionListener>();
        }

        public static void AddMouseListener(MouseListener ml) {
            mouseListeners.Add(ml);
        }

        public static void RemoveMouseListener(MouseListener ml) {
            mouseListeners.Add(ml);
        }

        public static void AddMouseMotionListener(MouseMotionListener mml) {
            mouseMotionListeners.Add(mml);
        }

        public static void RemoveMouseMotionListener(MouseMotionListener mml) {
            mouseMotionListeners.Remove(mml);
        }

        public static void Update() {
            previousState = currentState;
            currentState = Mouse.GetState();

            int modifiers = 0;
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift)) {
                modifiers |= MouseEvent.SHIFT_DOWN;
            }
            if (keyState.IsKeyDown(Keys.LeftControl) || keyState.IsKeyDown(Keys.RightControl)) {
                modifiers |= MouseEvent.CTRL_DOWN;
            }
            if (keyState.IsKeyDown(Keys.LeftAlt) || keyState.IsKeyDown(Keys.RightAlt)) {
                modifiers |= MouseEvent.ALT_DOWN;
            }

            Point previousLocation = new Point(previousState.X, previousState.Y);
            Point currentLocation = new Point(currentState.X, currentState.Y);

            if ((previousState.X != currentState.X) || (previousState.Y != currentState.Y)) {
                FireMouseMoved(new MouseEvent(previousLocation, currentLocation, modifiers));
                if (mousePressed && currentState.LeftButton == ButtonState.Pressed) {
                    if (isClick && Geometry.Distance(startLocation, currentLocation) > CLICK_TOLERANCE) {
                        isClick = false;
                    }
                    FireMouseDragged(new MouseEvent(startLocation, currentLocation, modifiers));
                }
            }

            if (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released) {
                FireMouseReleased(new MouseEvent(currentLocation, modifiers));

                if (isClick) {
                    FireMouseClicked(new MouseEvent(currentLocation, modifiers));
                }
            }

            if (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed) {
                mousePressed = true;
                isClick = true;
                startLocation = new Point(currentState.X, currentState.Y);

                FireMousePressed(new MouseEvent(startLocation, modifiers));
            }

            if (previousState.RightButton == ButtonState.Pressed && currentState.RightButton == ButtonState.Released) {
                FireMouseReleased(new MouseEvent(currentLocation, MouseEvent.MouseButton.Right, modifiers));
            }

            if (previousState.RightButton == ButtonState.Released && currentState.RightButton == ButtonState.Pressed) {
                FireMousePressed(new MouseEvent(startLocation, MouseEvent.MouseButton.Right, modifiers));
            }
        }

        private static void FireMouseMoved(MouseEvent evt) {
            foreach (MouseMotionListener mml in mouseMotionListeners) {
                mml.MouseMoved(evt);
            }
        }

        private static void FireMousePressed(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MousePressed(evt);
            }
        }

        private static void FireMouseReleased(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseReleased(evt);
            }
        }

        private static void FireMouseDragged(MouseEvent evt) {
            foreach (MouseMotionListener mml in mouseMotionListeners) {
                mml.MouseDragged(evt);
            }
        }

        private static void FireMouseClicked(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseClicked(evt);
            }
        }
    }
}
