using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Illumination.Logic.KeyHandler {
    public class KeyController {
        private KeyboardState previousState;
        private KeyboardState currentState;

        private HashSet <Keys> previousKeySet;

        private HashSet <KeyListener> keyListeners;

        public KeyController() {
            previousState = currentState = Keyboard.GetState();

            previousKeySet = new HashSet<Keys>(currentState.GetPressedKeys());
            keyListeners = new HashSet<KeyListener>();
        }

        public void Update() {
            currentState = Keyboard.GetState();

            HashSet <Keys> currentKeySet = new HashSet <Keys> (currentState.GetPressedKeys());
            IEnumerable <Keys> pressedKeys = currentKeySet.Except(previousKeySet);
            IEnumerable <Keys> releasedKeys = previousKeySet.Except(currentKeySet);

            if (pressedKeys.Count() > 0) {
                FireKeyPressed(new KeyEvent(pressedKeys));
            }

            if (releasedKeys.Count() > 0) {
                FireKeyReleased(new KeyEvent(releasedKeys));
            }

            previousKeySet = currentKeySet;
            previousState = currentState;
        }

        public void AddKeyListener(KeyListener keyListener) {
            keyListeners.Add(keyListener);
        }

        public void RemoveKeyListener(KeyListener keyListener) {
            keyListeners.Remove(keyListener);
        }

        private void FireKeyPressed(KeyEvent evt) {
            foreach (KeyListener kl in keyListeners) {
                kl.KeysPressed(evt);
            }
        }

        private void FireKeyReleased(KeyEvent evt) {
            foreach (KeyListener kl in keyListeners) {
                kl.KeysReleased(evt);
            }
        }
    }
}
