using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Illumination.Logic;

namespace Illumination.Graphics
{
    public static class Layer
    {
        public const SpriteSortMode SortMode = SpriteSortMode.BackToFront;

        public static Dictionary<string, float> Depth = new Dictionary<string, float>();

        static float worldObjectsInterval;

        static Layer()
        {
            Depth["Background"]     = 1;

            Depth["Tile"]           = 0.700f;
            Depth["Highlight"]      = 0.699f;
            Depth["Border"]         = 0.698f;
            Depth["Arrow"]          = 0.697f;

            Depth["WorldBottom"]    = 0.600f;
            Depth["WorldTop"]       = 0.400f;
            Depth["NightOverlay"]   = 0.399f;

            Depth["LightSequence"]  = 0.300f;
            Depth["Notice"]         = 0.299f;
            Depth["TextNotice"]     = 0.298f;

            Depth["WhiteLight"]     = 0.250f;
            Depth["ColoredLight"]   = 0.249f;

            Depth["Foreground"]     = 0;
        }

        public static void Initialize()
        {
            worldObjectsInterval = (Depth["WorldBottom"] - Depth["WorldTop"]) / (World.Grid.GetLength(0) + World.Grid.GetLength(1) + 2);
        }

        public static float GetWorldDepth(Rectangle gridLocation)
        {
            Point highest = new Point(gridLocation.Right, gridLocation.Top);
            return Depth["WorldBottom"] - worldObjectsInterval * (highest.X + highest.Y);
        }
    }
}
