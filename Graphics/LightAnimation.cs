using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Illumination.Graphics.AnimationHandler;
using Illumination.Data;
using Illumination.Logic;
using Microsoft.Xna.Framework;

namespace Illumination.Graphics
{
    public class LightAnimation
    {
        class Reflection : FrameEvent
        {
            Point location;
            Light.LightType lightColor;
            Entity.DirectionType direction;

            double targetTime;

            public Reflection(Point location, Light.LightType lightColor, 
                Entity.DirectionType direction, double targetTime)
            {
                this.location = location;
                this.lightColor = lightColor;
                this.direction = direction;
                this.targetTime = targetTime;
            }

            public override void DoEvent(Animation animation)
            {
                double timeGap = animation.ElapsedTotalSec - targetTime + 0.0160003;

                Animation newAnimation = World.CreateLight(location, lightColor, direction);
                newAnimation.ElapsedTotalSec = timeGap;
            }
        }

        class Stop : FrameEvent
        {
            Light light;

            public Stop(Light light)
            {
                this.light = light;
            }

            public override void DoEvent(Animation animation)
            {
                animation.StopAnimation();

                World.RemoveLight(light);
            }
        }

        class Movement : FrameEvent
        {
            Light light;
            Animation animation;
            double lastFrameTime;

            double Interval
            {
                get { return 1 / World.LightSpeed; }
            }

            public Movement(Light light, Animation animation, double lastFrameTime) 
            {
                this.light = light;
                this.animation = animation;
                this.lastFrameTime = lastFrameTime;
            }

            public void Play()
            {
                double targetTime = lastFrameTime + Interval;
                lastFrameTime = targetTime;

                Point newLocation = new Point(light.GridLocation.X, light.GridLocation.Y);
                switch (light.Direction) {
                    case Entity.DirectionType.North:
                        newLocation.X--;
                        break;
                    case Entity.DirectionType.East:
                        newLocation.Y++;
                        break;
                    case Entity.DirectionType.South:
                        newLocation.X++;
                        break;
                    case Entity.DirectionType.West:
                        newLocation.Y--;
                        break;
                }

                light.GridLocation = new Rectangle(newLocation.X, newLocation.Y, 1, 1);
                Rectangle newBoundingBox = Display.GetTextureBoundingBox(Light.LightTexture, newLocation, 0);
                light.BoundingBox = newBoundingBox;

                animation.AddTranslationFrame(new Point(newBoundingBox.X, newBoundingBox.Y), targetTime);
                animation.AddEventFrame(new Movement(light, animation, lastFrameTime), targetTime - Interval / 2);
            }

            public override void DoEvent(Animation animation)
            {
                /* Collision Test */
                if (!World.InBound(light.GridLocation.X, light.GridLocation.Y))
                {
                    animation.StopAnimation();
                    World.RemoveLight(light);
                    return;
                }

                HashSet<Entity> entities = World.GetEntities(light.GridLocation.X, light.GridLocation.Y);
                foreach (Entity entity in entities)
                {
                    if (entity.Hidden) { continue; }

                    if (entity is Person)
                    {
                        Person thisPerson = (Person)entity;

                        if ((light.Type == Light.LightType.Yellow) &&
                            (thisPerson.Profession == Person.ProfessionType.Worker) && !thisPerson.IsEducated)
                        {
                            thisPerson.Educate(1);
                        }

                        if (light.Type == Light.LightType.White)
                        {
                            animation.AddEventFrame(new Reflection(new Point(light.GridLocation.X, light.GridLocation.Y),
                                thisPerson.ReflectedLightColor, thisPerson.Direction, lastFrameTime), lastFrameTime);
                        }
                        else
                        {
                            animation.AddEventFrame(new Stop(light), lastFrameTime);
                            continue;
                        }
                        
                    }
                    else if (entity is Building)
                    {
                        Building building = (Building)entity;
                        building.Illuminate(light.Type);
                    }

                    if (light.Type != Light.LightType.White && !(entity is Tree))
                    {
                        animation.StopAnimation();
                        World.RemoveLight(light);
                    }
                }

                Play();
            }
        }

        public static Animation CreateMovementAnimation(Light light) 
        {
            Animation animation = Display.CreateAnimation(Light.LightTexture, light.BoundingBox, double.MaxValue);
            animation.AddColorFrame(Light.GetLightColor(light.Type), 0);
            new Movement(light, animation, 0).Play();

            return animation;
        }
    }
}
