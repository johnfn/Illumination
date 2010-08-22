using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Illumination.Logic;

namespace Illumination.WorldObjects {
    public abstract class Building : Entity {
        LightSequence lightBeams;

        HashSet <LightSequence> triggeredSequences;

        protected delegate void DoEffect(Building triggeringBuilding);

        public Building() : base() { /* Default constructor */ }

        public Building(int x, int y, int width, int height) {
            Initialize(x, y, width, height);
        }

        public int Width {
            get { return base.GridLocation.Width; }
        }

        public int Height {
            get { return base.GridLocation.Height; }
        }

        public abstract void Initialize(int x, int y);

        protected abstract Dictionary<LightSequence, DoEffect> GetEffects();

        public override void Initialize(int x, int y, int width, int height) {
            base.Initialize(x, y, width, height);

            lightBeams = new LightSequence();
            triggeredSequences = new HashSet<LightSequence>();

            base.DeferDraw = true;
        }

        public void ClearLightSequences() {
            triggeredSequences.Clear();
            lightBeams.ResetAllFrequencies();
        }

        public void Illuminate(Light.LightType color) {
            lightBeams.Frequencies[color]++;
            Activate();
        }

        public void Activate() {
            Dictionary<LightSequence, DoEffect> effects = GetEffects();
            foreach (LightSequence sequence in effects.Keys) {
                if (lightBeams.IsSubsequence(sequence) && !triggeredSequences.Contains(sequence)) {
                    effects[sequence](this);
                    triggeredSequences.Add(sequence);
                }
            }
        }
    }
}