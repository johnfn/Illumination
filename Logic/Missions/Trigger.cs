using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.Missions.Conditions;
using Illumination.Logic.Missions.Events;

namespace Illumination.Logic.Missions {
    public class Trigger {
        private Condition triggerCondition;
        public Condition TriggerCondition {
            get { return triggerCondition; }
            set { triggerCondition = value; }
        }

        private Event triggerEvent;
        public Event TriggerEvent {
            get { return triggerEvent; }
            set { triggerEvent = value; }
        }

        private bool hasActivated = false;

        public Trigger() { }

        public Trigger(Condition triggerCondition, Event triggerEvent) {
            this.triggerCondition = triggerCondition;
            this.triggerEvent = triggerEvent;
        }

        public void Update() {
            if (!hasActivated && triggerCondition.CheckCondition()) {
                triggerEvent.DoEvent();
                hasActivated = true;
            }
        }
    }
}
