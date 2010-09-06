using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic {
    public class Research {
        string description;
        List<LightSequence> tasks;
        ResearchLogic.DoEffect effectHandle;
        bool isCompleted;
        bool isInProgress;
        int currentIndex;

        public Research(string description, string[] tasks, ResearchLogic.DoEffect effectHandle) {
            this.description = description;
            this.tasks = new List<LightSequence>();
            foreach (string task in tasks) {
                this.tasks.Add(new LightSequence(task));
            }
            this.effectHandle = effectHandle;
            isInProgress = false;
            isCompleted = false;
            currentIndex = 0;
        }

        public string Description { get { return description; } }
        
        public int TaskCount { get { return tasks.Count; } }
        
        public bool IsInProgress {
            get { return isInProgress; }
        }

        public bool IsCompleted {
            get { return isCompleted; }
        }

        public void Initiate() {
            currentIndex = 0;
            isInProgress = true;
        }

        public LightSequence CurrentTask {
            get { return tasks[currentIndex]; }
        }

        public LightSequence GetTask(int index) {
            return tasks[index];
        }

        public void Abort() {
            currentIndex = 0;
            isInProgress = false;
        }

        public void TaskAccomplished() {
            currentIndex++;

            /* If research completed */
            if (currentIndex == tasks.Count) {
                effectHandle();

                isCompleted = true;
                isInProgress = false;
            }
        }
    }
}