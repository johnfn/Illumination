using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Logic.Missions
{
    public class Demo3Mission : Mission
    {
        public Demo3Mission() : base("Complete all missions in 5 days.") { }

        public override void Update()
        {
            isFail = isComplete ? false : World.DayCount > 5;

            foreach (Mission mission in World.MissionSet)
            {
                if (mission == this)
                {
                    continue;
                }

                if (!mission.IsComplete)
                {
                    isComplete = false;
                    return;
                }
            }
            isComplete = true;
        }
    }
}
