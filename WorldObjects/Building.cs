using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Illumination.Logic;
using Microsoft.Xna.Framework;

namespace Illumination.WorldObjects {
    public abstract class Building : Entity {
        LightSequence lightBeams;

        HashSet <LightSequence> triggeredSequences;

        // Returns whether to mark effect as completed or not
        protected delegate bool DoEffect(Building triggeringBuilding);

        public Building() : base() { /* Default constructor */ }

        public Building(int x, int y, int width, int height, Texture2D texture) {
            Initialize(x, y, width, height, texture);

            Name = "Building";
        }

        public int Width {
            get { return base.GridLocation.Width; }
        }

        public int Height {
            get { return base.GridLocation.Height; }
        }

        public abstract void Initialize(int x, int y);

        protected abstract Dictionary<LightSequence, DoEffect> GetEffects();

        public override void Initialize(int x, int y, int width, int height, Texture2D texture) {
            base.Initialize(x, y, width, height, texture);

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
                    if (effects[sequence](this)) {
                        triggeredSequences.Add(sequence);
                    }
                }
            }
        }

        public virtual HashSet<Point> GetEffectRange() {
            HashSet<Point> points = new HashSet<Point>();
            for (int row = GridLocation.Top; row < GridLocation.Bottom; row++) {
                for (int col = GridLocation.Left; col < GridLocation.Right; col++) {
                    points.Add(new Point(row, col));
                }
            }

            return points;
        }

        public override IEnumerable<Point> GetRange() {
            return GetEffectRange();
        }
    }
}