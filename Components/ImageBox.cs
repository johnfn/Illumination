using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Data;

namespace Illumination.Components {
    public class ImageBox : Component {
        public ImageBox(Texture2D texture, Rectangle boundingBox, Color color)
            : base(texture, boundingBox, color) { }

        public ImageBox(Rectangle boundingBox, Color color)
            : base(MediaRepository.Textures["Blank"], boundingBox, color) { }
    }
}
