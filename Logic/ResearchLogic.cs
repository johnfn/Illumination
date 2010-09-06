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

            string[] tasks = {"Y", "B", "Y"};
            researches.Add(new Research("Unlock School Effect #4", tasks, SchoolResearch));
        }

        /* Proof of concept */
        static void SchoolResearch() {
            School.UnlockEffect(3);
        }

        public static Research GetResearch(int index) {
            return researches[index];
        }
    }
}
