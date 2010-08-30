using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.MouseHandler {
    public interface MouseScrollListener {
        void MouseScrolled(MouseScrollEvent evt);
    }
}
