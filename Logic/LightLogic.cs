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
        const double NORMAL_SPEED = 3;
        const double FAST_SPEED = 8;

        double lightSpeed;
        public double LightSpeed
        {
            get { return lightSpeed; }
            set { lightSpeed = value; }
        }

        public enum SpeedType
        {
            Fast,
            Normal
        }

        static HashSet<Light> lightSet;

        public HashSet<Light> LightSet {
            get { return lightSet; }
        }

        public LightLogic() {
            lightSet = new HashSet<Light>();
            lightSpeed = NORMAL_SPEED;
        }

        public void ActivateTrees() {
            foreach (Tree tree in World.TreeSet) {
                CreateLight(new Point(tree.GridLocation.X, tree.GridLocation.Y), 
                    Light.LightType.White, tree.Direction);
            }
        }

        public bool NextTimestep() {
            return lightSet.Count != 0;
        }

        public Animation CreateLight(Point location, Light.LightType lightColor, Entity.DirectionType direction) {
            Light newLight = new Light(location.X, location.Y);
            newLight.Type = lightColor;
            newLight.Direction = direction;

            lightSet.Add(newLight);
            return LightAnimation.CreateMovementAnimation(newLight);
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

        public void SetLightSpeed(SpeedType speed)
        {
            if (speed == SpeedType.Fast)
            {
                lightSpeed = FAST_SPEED;
            }
            else
            {
                lightSpeed = NORMAL_SPEED;
            }
        }
    }
}
