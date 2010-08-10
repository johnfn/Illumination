using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.KeyHandler {
    public interface KeyListener {
        void KeysPressed(KeyEvent evt);
        void KeysReleased(KeyEvent evt);
    }
}
