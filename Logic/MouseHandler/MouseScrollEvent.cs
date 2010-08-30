using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.MouseHandler {
    public class MouseScrollEvent {
        private int change;
        public int Change {
            get { return change; }
        }

        private bool consumed;
        public bool Consumed {
            get { return consumed; }
            set { consumed = value; }
        }

        public MouseScrollEvent(int change) {
            this.change = change;
        }
    }
}
