using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;

namespace Illumination.Logic {
    public class ResearchLogic {
        public delegate void DoEffect();

        static List<Research> researches;

        static ResearchLogic() {
            researches = new List<Research>();

            string[] tasks1 = { "B", "Y", "Y" };
            researches.Add(new Research(0, "Unlock School Effect #3", tasks1, SchoolResearch1));

            string[] tasks2 = { "B", "BB", "YY", "YB" };
            researches.Add(new Research(1, "Unlock School Effect #4", tasks2, SchoolResearch2));
        }

        /* Proof of concept */
        static void SchoolResearch1() {
            School.UnlockEffect(2);
        }
        static void SchoolResearch2() {
            School.UnlockEffect(3);
        }

        public Research GetResearch(int index) {
            return researches[index];
        }
    }
}
