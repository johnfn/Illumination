using System;
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

        private static LinkedList<MouseListener> mouseListeners;
        private static LinkedList<MouseMotionListener> mouseMotionListeners;
        private static LinkedList<MouseScrollListener> mouseScrollListeners;

        private static LinkedList<MouseListener> mouseListenersToAdd = new LinkedList<MouseListener>();
        private static LinkedList<MouseListener> mouseListenersToRemove = new LinkedList<MouseListener>();
        private static LinkedList<MouseMotionListener> mouseMotionListenersToAdd = new LinkedList<MouseMotionListener>();
        private static LinkedList<MouseMotionListener> mouseMotionListenersToRemove = new LinkedList<MouseMotionListener>();
        private static LinkedList<MouseScrollListener> mouseScrollListenersToAdd = new LinkedList<MouseScrollListener>();
        private static LinkedList<MouseScrollListener> mouseScrollListenersToRemove = new LinkedList<MouseScrollListener>();

        public static MouseState CurrentState {
            get { return currentState; }
        }

        public static void Initialize() {
            previousState = currentState = Mouse.GetState();

            mousePressed = false;
            mouseListeners = new LinkedList<MouseListener>();
            mouseMotionListeners = new LinkedList<MouseMotionListener>();
            mouseScrollListeners = new LinkedList<MouseScrollListener>();
        }

        public static void AddMouseListener(MouseListener ml) {
            mouseListenersToAdd.AddLast(ml);
        }

        public static void RemoveMouseListener(MouseListener ml) {
            mouseListenersToRemove.AddLast(ml);
        }

        public static void AddMouseMotionListener(MouseMotionListener mml) {
            mouseMotionListenersToAdd.AddLast(mml);
        }

        public static void RemoveMouseMotionListener(MouseMotionListener mml) {
            mouseMotionListenersToRemove.AddLast(mml);
        }

        public static void AddMouseScrollListener(MouseScrollListener msl) {
            mouseScrollListenersToAdd.AddLast(msl);
        }

        public static void RemoveMouseScrollListener(MouseScrollListener msl) {
            mouseScrollListenersToRemove.AddLast(msl);
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

            if (previousState.ScrollWheelValue != currentState.ScrollWheelValue) {
                FireMouseScrolled(new MouseScrollEvent(currentState.ScrollWheelValue - previousState.ScrollWheelValue));
            }
        }

        private static void UpdateListeners<T>(LinkedList<T> listenerList, LinkedList<T> addList, LinkedList<T> removeList) {
            if (addList.Count != 0 || removeList.Count != 0) {
                foreach (T ml in addList) {
                    listenerList.AddFirst(ml);
                }
                foreach (T ml in mouseListenersToRemove) {
                    listenerList.Remove(ml);
                }
            }
        }

        private static void UpdateMouseListeners() {
            UpdateListeners<MouseListener>(mouseListeners, mouseListenersToAdd, mouseListenersToRemove);
        }

        private static void UpdateMouseMotionListeners() {
            UpdateListeners<MouseMotionListener>(mouseMotionListeners, mouseMotionListenersToAdd, mouseMotionListenersToRemove);
        }

        private static void UpdateMouseScrollListeners() {
            UpdateListeners<MouseScrollListener>(mouseScrollListeners, mouseScrollListenersToAdd, mouseScrollListenersToRemove);
        }
        
        private static void FireMouseMoved(MouseEvent evt) {
            UpdateMouseMotionListeners();
            foreach (MouseMotionListener mml in mouseMotionListeners) {
                if (evt.Consumed)
                    break;
                mml.MouseMoved(evt);
            }
        }

        private static void FireMousePressed(MouseEvent evt) {
            UpdateMouseListeners();
            foreach (MouseListener ml in mouseListeners) {
                if (evt.Consumed)
                    break;
                ml.MousePressed(evt);
            }
        }

        private static void FireMouseReleased(MouseEvent evt) {
            UpdateMouseListeners();
            foreach (MouseListener ml in mouseListeners) {
                if (evt.Consumed)
                    break;
                ml.MouseReleased(evt);
            }
        }

        private static void FireMouseDragged(MouseEvent evt) {
            UpdateMouseMotionListeners();
            foreach (MouseMotionListener mml in mouseMotionListeners) {
                if (evt.Consumed)
                    break;
                mml.MouseDragged(evt);
            }
        }

        private static void FireMouseClicked(MouseEvent evt) {
            UpdateMouseListeners();
            foreach (MouseListener ml in mouseListeners) {
                if (evt.Consumed)
                    break;
                ml.MouseClicked(evt);
            }
        }

        private static void FireMouseScrolled(MouseScrollEvent evt) {
            UpdateMouseScrollListeners();
            foreach (MouseScrollListener msl in mouseScrollListeners) {
                if (evt.Consumed)
                    break;
                msl.MouseScrolled(evt);
            }
        }
    }
}
