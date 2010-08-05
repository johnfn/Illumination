using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.MouseHandler {
    public interface MouseListener {
        void MouseClicked(MouseEvent evt);
        void MousePressed(MouseEvent evt);
        void MouseReleased(MouseEvent evt);

        void MouseMoved(MouseEvent evt);
        void MouseDragged(MouseEvent evt);
    }
}
