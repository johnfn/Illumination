using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Illumination.Logic;

namespace Illumination.WorldObjects
{
    public abstract class Building : Entity
    {
        LightSequence lightBeams;
        Dictionary<LightSequence, DoEffect> effects;
        Dictionary<LightSequence, bool> isTriggered;

        delegate void DoEffect();

        public Building(int x, int y, int width, int height) : base(x, y, width, height) {
            lightBeams = new LightSequence();
            effects = new Dictionary<LightSequence, DoEffect>();
            isTriggered = new Dictionary<LightSequence, bool>();
        }

        public void Illuminate(Light.LightType color)
        {
            lightBeams.Frequencies[color]++;
        }

        public void Activate()
        {
            foreach (LightSequence sequence in effects.Keys)
            {
                if (lightBeams.IsSubsequence(sequence) && !isTriggered[sequence])
                {
                    effects[sequence]();
                    isTriggered[sequence] = true;
                }
            }
        }
    }
}