using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;

namespace Illumination.Logic.Missions
{
    public class Demo2Mission : Mission
    {
        public Demo2Mission() : base("Give every worker a profession.") { }

        public override void Update()
        {
            foreach (Person person in World.PersonSet)
            {
                if (person.Profession == Person.ProfessionType.Worker)
                {
                    isComplete = false;
                    return;
                }
            }

            isComplete = true;
        }
    }
}
