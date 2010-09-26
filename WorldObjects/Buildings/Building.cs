using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Illumination.Logic;
using Microsoft.Xna.Framework;
using Illumination.Components;
using Illumination.Graphics;

namespace Illumination.WorldObjects {
    public abstract class Building : Entity {
        public class BuildingEffect {
            public string description;
            public LightSequence sequence;
            public DoEffect effectHandle;
            public bool isKnown;

            public BuildingEffect(string description, LightSequence sequence, DoEffect effectHandle, bool isKnown) {
                this.description = description;
                this.sequence = sequence;
                this.effectHandle = effectHandle;
                this.isKnown = isKnown;
            }
        }

        LightSequence lightBeams;

        HashSet <LightSequence> triggeredSequences;

        protected LightSequenceBar effectDisplay;
        int activatedEffect;

        // Returns whether to mark effect as 1 time use
        public delegate bool DoEffect(Building triggeringBuilding);

        public Building() : base() { /* Default constructor */ }

        public Building(int x, int y, int width, int height, Texture2D texture) {
            Initialize(x, y, width, height, texture);

            Name = "Building";
        }

        public int ActivatedEffect {
            get { return activatedEffect; }
            set { activatedEffect = value; }
        }

        public int Width {
            get { return base.GridLocation.Width; }
        }

        public int Height {
            get { return base.GridLocation.Height; }
        }

        public abstract void Initialize(int x, int y);

        public abstract BuildingEffect[] GetEffects();

        public override void Initialize(int x, int y, int width, int height, Texture2D texture) {
            base.Initialize(x, y, width, height, texture);

            lightBeams = new LightSequence();
            triggeredSequences = new HashSet<LightSequence>();
            activatedEffect = 0;
            effectDisplay = new LightSequenceBar(new LightSequence(), new Point(BoundingBox.X + 30, BoundingBox.Y + 30), new Dimension(10, 10), 5);
            effectDisplay.LayerDepth = Layer.Depth["LightSequence"];

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

        /// <summary>
        /// Returns true if it is a 1 time effect.
        /// </summary>
        public void Activate() {
            BuildingEffect thisEffect = GetEffects()[activatedEffect];

            if (lightBeams.IsSubsequence(thisEffect.sequence) && !triggeredSequences.Contains(thisEffect.sequence)) {
                if (thisEffect.effectHandle(this)) { 
                    triggeredSequences.Add(thisEffect.sequence);
                }
            }
        }

        /*
         * Forces an effect to trigger, but only if you're a cheater.
         */
        public void ForceActivate(){
            if (World.cheater){
                BuildingEffect thisEffect = GetEffects()[activatedEffect];

                thisEffect.effectHandle(this);
            }
        }

        public virtual HashSet<Point> GetEffectRange(int effectIndex) {
            HashSet<Point> points = new HashSet<Point>();
            for (int row = GridLocation.Left; row < GridLocation.Right; row++) {
                for (int col = GridLocation.Top; col < GridLocation.Bottom; col++) {
                    points.Add(new Point(row, col));
                }
            }

            return points;
        }

        public override IEnumerable<Point> GetRange() {
            return GetEffectRange(activatedEffect);
        }

        public void ActivateEffect(int clickedEffect) {
            activatedEffect = clickedEffect;
        }
    }
}
