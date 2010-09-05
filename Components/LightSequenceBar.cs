using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Logic;
using Microsoft.Xna.Framework;
using Illumination.Graphics;
using Illumination.Utility;
using Illumination.WorldObjects;
using Illumination.Data;

namespace Illumination.Components {
    public class LightSequenceBar : Component {
        LightSequence sequence;
        public LightSequence Sequence {
            get { return sequence; }
            set { sequence = value; }
        }

        Dimension tileSize;
        int indent;

        public LightSequenceBar(LightSequence sequence, Point pivot, Dimension tileSize, int indent)
            : base(Geometry.ConstructRectangle(pivot, tileSize)) {
            this.sequence = sequence;
            this.tileSize = tileSize;
            this.indent = indent;
        }

        public override void Draw(SpriteBatchRelative spriteBatch, bool isRelative) {
            if (IsActive) {
                base.Draw(spriteBatch, isRelative);

                Point curPivot = new Point(BoundingBox.X, BoundingBox.Y);
                Point increment = new Point(tileSize.Width + indent, 0);

                for (Light.LightType type = 0; type < Light.LightType.SIZE; type++) {
                    for (int count = 0; count < sequence.Frequencies[type]; count++) {
                        if (isRelative) {
                            spriteBatch.DrawRelative(MediaRepository.Textures["Blank"], Geometry.ConstructRectangle(curPivot, tileSize),
                                Light.GetLightColor(type), layerDepth);
                        }
                        else {
                            spriteBatch.DrawAbsolute(MediaRepository.Textures["Blank"], Geometry.ConstructRectangle(curPivot, tileSize),
                                Light.GetLightColor(type));
                        }

                        curPivot = Geometry.Sum(curPivot, increment);
                    }
                }
            }
        }
    }
}
