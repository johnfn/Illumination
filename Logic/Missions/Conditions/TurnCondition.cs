using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions.Conditions {
    public class TurnCondition : ComparisonCondition {
        public TurnCondition() : base() { }
        
        public TurnCondition(int comparisonValue, int comparisonType) : base(comparisonValue, comparisonType) { }

        public override bool CheckCondition() {
            return base.PerformComparison(World.DayCount, comparisonValue, comparisonType);
        }
    }
}
