using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Illumination.Utility;

namespace Illumination.Logic.MouseHandler {
    public class MouseController {
        private const int CLICK_TOLERANCE = 5;

        private bool mousePressed;
        private bool isClick;
        private Point startLocation;

        private MouseState previousState;
        private MouseState currentState;

        private HashSet<MouseListener> mouseListeners;

        public MouseController() {
            previousState = currentState = Mouse.GetState();

            mousePressed = false;
            mouseListeners = new HashSet<MouseListener>();
        }

        public void AddMouseListener(MouseListener ml) {
            mouseListeners.Add(ml);
        }

        public void RemoveMouseListener(MouseListener ml) {
            mouseListeners.Add(ml);
        }

        public void Update() {
            previousState = currentState;
            currentState = Mouse.GetState();

            Point previousLocation = new Point(previousState.X, previousState.Y);
            Point currentLocation = new Point(currentState.X, currentState.Y);

            if ((previousState.X != currentState.X) || (previousState.Y != currentState.Y)) {
                FireMouseMoved(new MouseEvent(previousLocation, currentLocation));
                if (mousePressed && currentState.LeftButton == ButtonState.Pressed) {
                    if (isClick && Geometry.Distance(startLocation, currentLocation) > CLICK_TOLERANCE) {
                        isClick = false;
                    }
                    FireMouseDragged(new MouseEvent(startLocation, currentLocation));
                }
                
            }

            if (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released) {
                FireMouseReleased(new MouseEvent(currentLocation));

                if (isClick) {
                    FireMouseClicked(new MouseEvent(currentLocation));
                }
            }

            if (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed) {
                mousePressed = true;
                isClick = true;
                startLocation = new Point(currentState.X, currentState.Y);

                FireMousePressed(new MouseEvent(startLocation));
            }
        }

        private void FireMouseMoved(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseMoved(evt);
            }
        }

        private void FireMousePressed(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MousePressed(evt);
            }
        }
        
        private void FireMouseReleased(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseReleased(evt);
            }
        }

        private void FireMouseDragged(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseDragged(evt);
            }
        }

        private void FireMouseClicked(MouseEvent evt) {
            foreach (MouseListener ml in mouseListeners) {
                ml.MouseClicked(evt);
            }
        }
    }
}
