using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Microsoft.Xna.Framework;
using Illumination.Graphics;

namespace Illumination.Logic {
    public class LightLogic {
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

        public void NextTimestep() {
            HashSet<Light> oldLightSet = new HashSet<Light> (lightSet);
            foreach (Light light in oldLightSet) {
                Rectangle newBoundingBox = light.BoundingBox;

                switch (light.Direction) {
                    case Entity.DirectionType.North:
                        newBoundingBox.Y--;
                        break;
                    case Entity.DirectionType.East:
                        newBoundingBox.X++;
                        break;
                    case Entity.DirectionType.South:
                        newBoundingBox.Y++;
                        break;
                    case Entity.DirectionType.West:
                        newBoundingBox.X--;
                        break;
                }

                light.BoundingBox = newBoundingBox;
                CollisionLogic(light);
            }
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
                if (entity is Person) {
                    Person thisPerson = (Person) entity;
                    if (Utility.Geometry.Distance(centerPosition, gridCenter) > 1 || light.LastCollisionLocation.Equals(gridLocation)) {
                        continue;
                    }
                    if (light.LightColor == Light.LightType.White) {
                        Light newLight = CreateLight(gridLocation.X, gridLocation.Y);
                        newLight.LightColor = thisPerson.ReflectedLightColor;
                        newLight.Direction = thisPerson.Direction;

                        light.LastCollisionLocation = gridLocation;
                    }
                } else if (entity is Building) {
                    Building building = (Building) entity;
                    building.Illuminate(light.LightColor);
                }

                if (light.LightColor != Light.LightType.White && !(entity is Tree)) {
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
