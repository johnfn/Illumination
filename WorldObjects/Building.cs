using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Illumination.Logic;

namespace Illumination.WorldObjects {
    public abstract class Building : Entity {
        LightSequence lightBeams;
        Dictionary<LightSequence, DoEffect> effects;
        Dictionary<LightSequence, bool> isTriggered;

        delegate void DoEffect();

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

        public override void Initialize(int x, int y, int width, int height) {
            base.Initialize(x, y, width, height);

            lightBeams = new LightSequence();
            effects = new Dictionary<LightSequence, DoEffect>();
            isTriggered = new Dictionary<LightSequence, bool>();

            base.DeferDraw = true;
        }

        public void Illuminate(Light.LightType color) {
            lightBeams.Frequencies[color]++;
        }

        public void Activate() {
            foreach (LightSequence sequence in effects.Keys) {
                if (lightBeams.IsSubsequence(sequence) && !isTriggered[sequence]) {
                    effects[sequence]();
                    isTriggered[sequence] = true;
                }
            }
        }
    }
}