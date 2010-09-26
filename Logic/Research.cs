using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic {
    public class Research {
        int researchIndex;
        string description;
        List<LightSequence> tasks;
        ResearchLogic.DoEffect effectHandle;
        bool isCompleted;
        bool isInProgress;
        int currentIndex;

        public Research(int index, string description, string[] tasks, ResearchLogic.DoEffect effectHandle) {
            this.researchIndex = index;
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

        public int Index { get { return researchIndex; } }

        public string Description { get { return description; } }
        
        public int TaskCount { get { return tasks.Count; } }

        public bool IsInProgress { get { return isInProgress; } }

        public bool IsCompleted { get { return isCompleted; } }

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

        /// <summary> Returns if the entire research is complete or not. </summary>
        public bool TaskComplete() {
            currentIndex++;

            /* If research completed */
            if (currentIndex == tasks.Count) {
                effectHandle();

                isCompleted = true;
                isInProgress = false;

                return true;
            }

            return false;
        }
    }
}