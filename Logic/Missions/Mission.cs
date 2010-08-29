using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.Missions.Conditions;
using System.Xml.Serialization;

namespace Illumination.Logic.Missions {
    public class Mission {
        private HashSet <Objective> primaryObjectives;

        public HashSet<Objective> PrimaryObjectives {
            get { return primaryObjectives; }
            set { primaryObjectives = value; }
        }

        public Mission() {
            primaryObjectives = new HashSet<Objective>();
        }

        public int GetNumConditions() {
            return primaryObjectives.Count;
        }

        public Objective.StatusType GetMissionStatus() {
            bool success = true;
            foreach (Objective o in primaryObjectives) {
                Objective.StatusType status = o.GetStatus();
                if (status == Objective.StatusType.Failure) {
                    return Objective.StatusType.Failure;
                } else if (o.HasSuccessCondition() && status != Objective.StatusType.Success) {
                    success = false;
                }
            }

            return success ? Objective.StatusType.Success : Objective.StatusType.None;
        }
    }
}
