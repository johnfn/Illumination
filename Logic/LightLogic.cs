using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Data;
using Illumination.Graphics.AnimationHandler;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.Logic {
    public class LightLogic {
        const float LIGHT_SPEED = 0.04f;

        static HashSet<Light> lightSet;

        public HashSet<Light> LightSet {
            get { return lightSet; }
        }

        public LightLogic() {
            lightSet = new HashSet<Light>();
        }

        public void ActivateTrees() {
            foreach (Tree tree in World.TreeSet) {
                Light light = CreateLight(tree.GridLocation.X, tree.GridLocation.Y);
                light.Direction = tree.Direction;
            }
        }

        public bool NextTimestep() {
            if (lightSet.Count == 0)
            {
                return false;
            }

            HashSet<Light> oldLightSet = new HashSet<Light> (lightSet);
            foreach (Light light in oldLightSet) {
                Vector2 newLocation = light.Location;

                /* Tracing effect */
                Animation trace = Display.CreateAnimation(MediaRepository.Textures["Light"], light.BoundingBox, 0.6);
                Color startColor = Light.GetLightColor(light.Type);
                startColor.A = 50;
                Color endColor = startColor;
                endColor.A = 0;

                trace.AddColorFrame(startColor, 0);
                trace.AddColorFrame(endColor, 0.6);

                switch (light.Direction) {
                    case Entity.DirectionType.North:
                        newLocation.X -= LIGHT_SPEED;
                        break;
                    case Entity.DirectionType.East:
                        newLocation.Y += LIGHT_SPEED;
                        break;
                    case Entity.DirectionType.South:
                        newLocation.X += LIGHT_SPEED;
                        break;
                    case Entity.DirectionType.West:
                        newLocation.Y -= LIGHT_SPEED;
                        break;
                }

                light.Location = newLocation;
                light.BoundingBox = Display.GetTextureBoundingBox(MediaRepository.Textures["Light"], newLocation, 0);
                CollisionLogic(light);
            }

            return true;
        }

        private void CollisionLogic(Light light) {
            Point centerPosition = new Point(light.BoundingBox.X + light.BoundingBox.Width / 2, light.BoundingBox.Y + light.BoundingBox.Height / 2);

            Point gridLocation = Display.ViewportToGridLocation(centerPosition);
            Point gridCenter = Display.GridCenterToViewport(gridLocation);

            if (!World.InBound(gridLocation.X, gridLocation.Y)) {
                RemoveLight(light);
                return;
            }

            HashSet <Entity> entities = World.GetEntities(gridLocation.X, gridLocation.Y);
            foreach (Entity entity in entities) {
                if (entity.Hidden) {
                    continue;
                }
                if (entity is Person) {
                    Person thisPerson = (Person) entity;
                    if (Utility.Geometry.Distance(centerPosition, gridCenter) > 1 || light.LastCollisionLocation.Equals(gridLocation)) {
                        continue;
                    }
                    if (light.Type == Light.LightType.White) {
                        Light newLight = CreateLight(gridLocation.X, gridLocation.Y);
                        newLight.Type = thisPerson.ReflectedLightColor;
                        newLight.Direction = thisPerson.Direction;

                        light.LastCollisionLocation = gridLocation;
                    }
                    else if ((light.Type == Light.LightType.Yellow) && 
                        (thisPerson.Profession == Person.ProfessionType.Worker) && !thisPerson.IsEducated) {
                        thisPerson.Educate(1);
                    }
                } else if (entity is Building) {
                    Building building = (Building) entity;
                    building.Illuminate(light.Type);
                }

                if (light.Type != Light.LightType.White && !(entity is Tree)) {
                    RemoveLight(light);
                }
            }
        }

        private Point GetGridLocation(Light light) {
            return new Point();
        }

        public Light CreateLight(int x, int y) {
            Light newLight = new Light(x, y);
            lightSet.Add(newLight);

            return newLight;
        }

        public void RemoveLight(Light light) {
            if (lightSet.Contains(light))
            {
                lightSet.Remove(light);
            }
        }

        internal void Clear() {
            lightSet.Clear();
        }
    }
}
