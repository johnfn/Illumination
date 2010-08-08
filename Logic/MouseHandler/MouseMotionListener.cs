using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.MouseHandler {
    public interface MouseMotionListener {
        void MouseMoved(MouseEvent evt);
        void MouseDragged(MouseEvent evt);
    }
}
