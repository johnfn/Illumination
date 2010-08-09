﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;

namespace Illumination.WorldObjects {
    public class School : Building {
        private const int WIDTH = 2;
        private const int HEIGHT = 2;

        public School(int x, int y) : base(x, y, WIDTH, HEIGHT) {

        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(MediaRepository.Textures["School"], base.BoundingBox, Color.White);
        }
    }
}