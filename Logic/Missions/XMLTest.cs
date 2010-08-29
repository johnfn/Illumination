using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic.Missions.Conditions;
using System.Xml.Serialization;
using System.IO;
using Illumination.WorldObjects;

namespace Illumination.Logic.Missions {
    public class XMLTest {
        public static void Test() {
            ProfessionCondition professionCondition1 = new ProfessionCondition(Person.ProfessionType.Educator, 3, ComparisonCondition.GREATER | ComparisonCondition.EQUAL);
            ProfessionCondition professionCondition2 = new ProfessionCondition(Person.ProfessionType.Worker, 0, ComparisonCondition.EQUAL);
            ProfessionCondition professionCondition3 = new ProfessionCondition(Person.ProfessionType.Educator, 3, ComparisonCondition.LESS);
            PotentialProfessionCondition professionCondition4 = new PotentialProfessionCondition(Person.ProfessionType.Educator, 3, ComparisonCondition.LESS);

            TurnCondition turnCondition = new TurnCondition(5, ComparisonCondition.LESS | ComparisonCondition.EQUAL);
            TurnCondition turnCondition2 = new TurnCondition(5, ComparisonCondition.GREATER);

            Objective objective1 = new Objective("Have at least 3 educators.");
            objective1.SuccessConditions.Add(professionCondition1);
            objective1.FailureConditions.Add(professionCondition4);

            Objective objective2 = new Objective("Give every worker a profession.");
            objective2.SuccessConditions.Add(professionCondition2);

            Objective objective3 = new Objective("No more than five days may elapse");
            objective3.FailureConditions.Add(turnCondition2);

            Mission currentMission = new Mission();

            currentMission.PrimaryObjectives.Add(objective1);
            currentMission.PrimaryObjectives.Add(objective2);
            currentMission.PrimaryObjectives.Add(objective3);

            XmlSerializer serializer = new XmlSerializer(typeof(Mission));
            TextWriter writer = new StreamWriter("test.xml");
            serializer.Serialize(writer, currentMission);
            writer.Close();
        }
    }
}
