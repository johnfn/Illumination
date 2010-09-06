using System.Collections.Generic;
using Illumination.WorldObjects;

namespace Illumination.Logic
{
    public class LightSequence
    {
        Dictionary<Light.LightType, int> frequencies;

        public LightSequence() {
            frequencies = new Dictionary<Light.LightType, int>();
            ResetAllFrequencies();
        }

        /*
         * Initialize a LightSequence 
         *
         * lights - a string representing the light sequence
         *
         * light is not in any special order.
         *
          B Blue;
          * Gray;
          Y Yellow;
          W White;
          G Green;
          R Red;
         *
         */
        public LightSequence(string lights) : this() {
            Light.LightType[] lightSequence = new Light.LightType[lights.Length];
            foreach (char ch in lights.ToCharArray()) {
                Light.LightType light = Light.GetLightType(ch);
                frequencies[light]++;
            }
        }

        public LightSequence(Dictionary<Light.LightType, int> frequencies) {
            this.frequencies = new Dictionary<Light.LightType, int>(frequencies);
        }

        public Dictionary<Light.LightType, int> Frequencies {
            get { return frequencies; }
        }

        public override int GetHashCode()
        {
            int hash = 1;
            int largestPossible = 12;
            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++)
            {
                hash += frequencies[color];
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
            int surplus = frequencies[Light.LightType.Gray];

            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++)
            {
                if (color == Light.LightType.Gray)
                    continue;
                if (frequencies[color] < otherSequence.frequencies[color])
                    return false;

                if (color != Light.LightType.White) {
                    surplus += frequencies[color] - otherSequence.frequencies[color];
                }
            }
            return surplus >= otherSequence.frequencies[Light.LightType.Gray];
        }

        public void ResetAllFrequencies() {
            for (Light.LightType color = 0; color < Light.LightType.SIZE; color++) {
                frequencies[color] = 0;
            }
        }
    }
}
