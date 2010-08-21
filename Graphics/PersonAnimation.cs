using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Illumination.Graphics.AnimationHandler;
using Microsoft.Xna.Framework;

namespace Illumination.Graphics {
    public class PersonAnimation {
        private const double MOVEMENT_INTERVAL = 0.35;

        public static void Move(Person p, Person.SearchNode endNode) {
            Animation a = Display.CreateAnimation(p.PersonTexture, p.BoundingBox, 5);
            AddPathAnimation(a, Person.SearchNode.GetForwardPath(endNode));
        }

        private static void AddPathAnimation(Animation animation, Person.SearchNode startNode) {
            double time = 0;
            while (startNode != null) {
                animation.AddTranslationFrame(Display.GridLocationToViewport(startNode.point), time);

                startNode = startNode.nextNode;
                time += MOVEMENT_INTERVAL;
            }
        }
    }
}
