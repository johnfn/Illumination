using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Illumination.Logic.KeyHandler {
    public class KeyEvent {
        private IEnumerable <Keys> changedKeys;

        public IEnumerable <Keys> ChangedKeys {
            get { return changedKeys; }
        }

        public KeyEvent(IEnumerable<Keys> changedKeys) {
            this.changedKeys = changedKeys;
        }
    }
}
