using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.Missions.Conditions;
using System.Xml.Serialization;

namespace Illumination.Logic.Missions {
    public class Objective {
        public enum StatusType {
            Success,
            Failure,
            None
        }

        private string description;
        public string Description {
            get { return description; }
            set { description = value; }
        }

        private HashSet <Condition> successConditions;
        public HashSet<Condition> SuccessConditions {
            get { return successConditions; }
            set { successConditions = value; }
        }

        private HashSet <Condition> failureConditions;
        public HashSet<Condition> FailureConditions {
            get { return failureConditions; }
            set { failureConditions = value; }
        }

        public Objective() : this("") { }

        public Objective(string description) {
            this.description = description;

            successConditions = new HashSet<Condition>();
            failureConditions = new HashSet<Condition>();
        }

        public bool HasSuccessCondition() {
            return successConditions.Count > 0;
        }
        
        public StatusType GetStatus() {
            bool success = successConditions.Count > 0;
            foreach (Condition c in successConditions) {
                if (!c.CheckCondition()) {
                    success = false;
                    break;
                }
            }
            if (success) {
                return StatusType.Success;
            }
            
            foreach (Condition c in failureConditions) {
                if (c.CheckCondition()) {
                    return StatusType.Failure;
                }
            }

            return StatusType.None;
        }
    }
}
