using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Illumination.Graphics.AnimationHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.Graphics {
    public class PersonAnimation {
        private class MovementAnimation : FrameEvent {
            private Person person;

            private const double MOVEMENT_INTERVAL = 0.35;

            public MovementAnimation(Person person) {
                this.person = person;
            }

            public void Move(Person.SearchNode endNode) {
                person.Hidden = true;
                person.Selectable = false;
                Animation a = Display.CreateAnimation(person.PersonTexture, person.BoundingBox, Double.MaxValue);
                AddPathAnimation(a, Person.SearchNode.GetForwardPath(endNode));
            }

            private void AddPathAnimation(Animation animation, Person.SearchNode startNode) {
                double time = 0;
                animation.AddColorFrame(new Color(255, 255, 255, 100), 0.01);
                for (; startNode != null; time += MOVEMENT_INTERVAL, startNode = startNode.nextNode) {
                    Rectangle newLocation = Display.GetTextureBoundingBox(person.PersonTexture, startNode.point, 0);
                    animation.AddTranslationFrame(new Point(newLocation.X, newLocation.Y), time);
                }
                animation.AddEventFrame(this, time - MOVEMENT_INTERVAL);
            }
           
            public override void DoEvent(Animation a) {
                person.Hidden = false;
                person.Selectable = true;
                a.StopAnimation();
            }
        }

        public static void CreateMovementAnimation(Person p, Person.SearchNode endNode) {
            new MovementAnimation(p).Move(endNode);
        }
    }
}
