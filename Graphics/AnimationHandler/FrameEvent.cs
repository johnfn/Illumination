using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Graphics.AnimationHandler {
    public abstract class FrameEvent {
        protected bool isTriggered = false;

        public abstract void DoEvent(Animation animation);

        public virtual bool IsTriggered() {
            return isTriggered;
        }

        public virtual void MarkTriggered() {
            isTriggered = true;
        }
    }
}
