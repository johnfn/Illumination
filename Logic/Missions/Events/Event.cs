using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Illumination.Logic.Missions.Events {
    [XmlInclude(typeof(MoneyAdjustmentEvent))]
    public abstract class Event {
        public Event() { }

        public abstract void DoEvent(); 
    }
}
