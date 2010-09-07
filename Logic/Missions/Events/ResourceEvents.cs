using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions.Events {
    public class MoneyAdjustmentEvent : Event {
        private int adjustmentQuantity;
        public int AdjustmentQuantity {
            get { return adjustmentQuantity; }
            set { adjustmentQuantity = value; }
        }

        public MoneyAdjustmentEvent() { }

        public MoneyAdjustmentEvent(int adjustmentQuantity) {
            this.adjustmentQuantity = adjustmentQuantity;
        }

        public override void DoEvent() {
            World.Money += adjustmentQuantity;
        }
    }
}
