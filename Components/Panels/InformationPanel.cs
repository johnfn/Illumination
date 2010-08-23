using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;
using Microsoft.Xna.Framework;
using Illumination.Utility;

namespace Illumination.Components.Panels
{
    public class InformationPanel : Panel
    {
        Rectangle detailPanelRelLoc = new Rectangle(175, 0, 250, 175);

        public InformationPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.Green)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            Rectangle detailPanelAbsLoc = Geometry.Translate(detailPanelRelLoc, BoundingBox.X, BoundingBox.Y);
            spriteBatch.Draw(MediaRepository.Textures["Blank"], detailPanelAbsLoc, Color.AliceBlue);
        }
    }
}
