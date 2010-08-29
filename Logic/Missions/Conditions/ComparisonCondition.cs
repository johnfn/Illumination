using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions.Conditions {
    public abstract class ComparisonCondition : Condition {
        public const int GREATER = 1 << 0;
        public const int EQUAL = 1 << 1;
        public const int LESS = 1 << 2;

        protected int comparisonValue;
        protected int comparisonType;

        public int ComparisonValue {
            get { return comparisonValue; }
            set { comparisonValue = value; }
        }

        public int ComparisonType {
            get { return comparisonType; }
            set { comparisonType = value; }
        }

        public ComparisonCondition() : this(0, EQUAL) { }

        public ComparisonCondition(int comparisonValue, int comparisonType) {
            this.comparisonValue = comparisonValue;
            this.comparisonType = comparisonType;
        }

        protected bool PerformComparison(int value, int comparisonValue, int comparisonType) {
            if (((comparisonType & GREATER) > 0) && value > comparisonValue) {
                return true;
            } else if (((comparisonType & EQUAL) > 0) && value == comparisonValue) {
                return true;
            }

            return (((comparisonType & LESS) > 0) && value < comparisonValue);
        }


    }
}
