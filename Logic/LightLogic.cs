using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.WorldObjects;
using Microsoft.Xna.Framework;

namespace Illumination.Logic
{
    public class LightLogic
    {
        static HashSet<Light> lightSet;

        public HashSet<Light> LightSet
        {
            get { return lightSet; }
        }

        public LightLogic()
        {
            lightSet = new HashSet<Light>();
        }

        public void ActivateTrees()
        {
            foreach (Tree tree in World.TreeSet)
            {
                Light light = CreateLight(tree.GridLocation.X, tree.GridLocation.Y);
                light.Direction = tree.Direction;
            }
        }

        public void NextTimestep()
        {
            foreach (Light light in lightSet)
            {
                Rectangle newBoundingBox = light.BoundingBox;

                switch (light.Direction)
                {
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
            }
        }
    
        public Light CreateLight(int x,int y)
        {
            Light newLight = new Light(x, y);
            lightSet.Add(newLight);

            return newLight;
        }

        public void RemoveLight(Light light)
        {

        }

        internal void Clear()
        {
            lightSet.Clear();
        }
    }
}
