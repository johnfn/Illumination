using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;

namespace Illumination.Logic.Missions
{
    public class Demo1Mission : Mission
    {
        public Demo1Mission() : base("Have at least 3 educators.") { }

        public override void Update()
        {
            int educatorCount = 0;
            int workerCount = 0;
            foreach (Person person in World.PersonSet)
            {
                if (person.Profession == Person.ProfessionType.Educator)
                {
                    educatorCount++;
                }
                else if (person.Profession == Person.ProfessionType.Worker)
                {
                    workerCount++;
                }
            }

            isComplete = educatorCount >= 3;
            isFail = educatorCount + workerCount < 3;
        }
    }
}
