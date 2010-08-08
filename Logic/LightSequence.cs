using System.Collections.Generic;
using Illumination.WorldObjects;

namespace Illumination.Logic
{
    public class LightSequence
    {
        Dictionary<Light.LightType, int> frequencies;

        public Dictionary<Light.LightType, int> Frequencies
        {
            get { return frequencies; }
            set { frequencies = value; }
        }

        public override int GetHashCode()
        {
            int hash = 1;
            int largestPossible = 12;
            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++)
            {
                hash += Frequencies[color];
                hash *= largestPossible;
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            LightSequence otherSequence = (LightSequence)obj;
            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++)
            {
                if (frequencies[color] != otherSequence.frequencies[color])
                    return false;
            }
            return true;
        }

        public bool IsSubsequence(LightSequence otherSequence)
        {
            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++)
            {
                if (frequencies[color] < otherSequence.frequencies[color])
                    return false;
            }
            return true;
        }
    }
}