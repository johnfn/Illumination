using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Illumination.Logic.KeyHandler {
    public static class KeyController {
        private static KeyboardState previousState;
        private static KeyboardState currentState;

        private static HashSet <Keys> previousKeySet;

        private static HashSet <KeyListener> keyListeners;

        public static void Initialize() {
            previousState = currentState = Keyboard.GetState();

            previousKeySet = new HashSet<Keys>(currentState.GetPressedKeys());
            keyListeners = new HashSet<KeyListener>();
        }

        public static void Update() {
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

        public static void AddKeyListener(KeyListener keyListener) {
            keyListeners.Add(keyListener);
        }

        public static void RemoveKeyListener(KeyListener keyListener) {
            keyListeners.Remove(keyListener);
        }

        private static void FireKeyPressed(KeyEvent evt) {
            foreach (KeyListener kl in keyListeners) {
                kl.KeysPressed(evt);
            }
        }

        private static void FireKeyReleased(KeyEvent evt) {
            foreach (KeyListener kl in keyListeners) {
                kl.KeysReleased(evt);
            }
        }
    }
}
