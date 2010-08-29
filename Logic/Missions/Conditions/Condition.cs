using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Illumination.Logic.Missions.Conditions {
    [XmlInclude(typeof(ProfessionCondition))]
    [XmlInclude(typeof(PotentialProfessionCondition))]
    [XmlInclude(typeof(TurnCondition))]
    public abstract class Condition {
        private string description;
        public string Context {
            get { return description; }
            set { description = value; }
        }

        public Condition() : this("") { }

        public Condition(string description) {
            this.description = description;
        }

        public abstract bool CheckCondition();
    }
}
