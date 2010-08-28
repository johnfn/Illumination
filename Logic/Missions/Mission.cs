using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions
{
    public abstract class Mission
    {
        protected string instruction;
        public string Instruction
        {
            get { return instruction; }
        }

        protected bool isComplete;
        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        protected bool isFail;
        public bool IsFail
        {
            get { return isFail; }
            set { isFail = value; }
        }

        public Mission(string instruction)
        {
            this.instruction = instruction;
            isComplete = false;
            isFail = false;
        }

        public abstract void Update();
    }
}
