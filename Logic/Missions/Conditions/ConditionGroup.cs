using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions.Conditions {
    public class ConditionGroup : Condition {
        public HashSet <Condition> Conditions;

        public override bool CheckCondition() {
            foreach (Condition c in Conditions) {
                if (!c.CheckCondition())
                    return false;
            }
            return true;
        }
    }
}
