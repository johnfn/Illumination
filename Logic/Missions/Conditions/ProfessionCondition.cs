using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;

namespace Illumination.Logic.Missions.Conditions {
    public class ProfessionCondition : ComparisonCondition {
        private Person.ProfessionType professionType;

        public Person.ProfessionType ProfessionType {
            get { return professionType; }
            set { professionType = value; }
        }

        public ProfessionCondition() : base() { } 

        public ProfessionCondition(Person.ProfessionType professionType, int comparisonValue, int comparisonType) :
            base(comparisonValue, comparisonType) {
            this.professionType = professionType;
        }

        public override bool CheckCondition() {
            int count = 0;
            foreach (Person person in World.PersonSet) {
                if (person.Profession == professionType) {
                    count++;
                }
            }
            return base.PerformComparison(count, comparisonValue, comparisonType);
        }
    }

    public class PotentialProfessionCondition : ComparisonCondition {
        private Person.ProfessionType professionType;

        public Person.ProfessionType ProfessionType {
            get { return professionType; }
            set { professionType = value; }
        }

        public PotentialProfessionCondition() : base() { } 

        public PotentialProfessionCondition(Person.ProfessionType professionType, int comparisonValue, int comparisonType) :
            base(comparisonValue, comparisonType) {
            this.professionType = professionType;
        }

        public override bool CheckCondition() {
            int count = 0;
            int numWorker = 0;
            foreach (Person person in World.PersonSet) {
                if (person.Profession == professionType) {
                    count++;
                } else if (person.Profession == Person.ProfessionType.Worker) {
                    numWorker++;
                }
            }
            return base.PerformComparison(count + numWorker, comparisonValue, comparisonType);
        }
    }
}
