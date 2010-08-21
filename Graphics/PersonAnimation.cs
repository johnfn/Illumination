using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Illumination.Graphics.AnimationHandler;
using Microsoft.Xna.Framework;

namespace Illumination.Graphics {
    public class PersonAnimation {
        public static void Move(Person p, Person.SearchNode endNode) {
            Animation a = Display.CreateAnimation(p.GetTexture(), p.BoundingBox, 10);
            AddPathAnimation(a, Person.SearchNode.GetForwardPath(endNode));
        }

        private static void AddPathAnimation(Animation animation, Person.SearchNode startNode) {
            int time = 0;
            while (startNode != null) {
                animation.AddTranslationFrame(Display.GridLocationToViewport(startNode.point), time);

                startNode = startNode.nextNode;
                time += 2;
            }
        }
    }
}
