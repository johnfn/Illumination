﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.Components.Panels {
    public class DirectionPanel : Panel {
        public DirectionPanel(Rectangle boundingBox)
            : base(MediaRepository.Textures["Blank"], boundingBox, Color.TransparentWhite) {

        }

        public void Initialize() {

        }
    }
}