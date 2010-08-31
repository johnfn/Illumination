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
            Depth["Background"] = 1;

            Depth["Tile"] = 0.7f;
            Depth["Highlight"] = 0.699f;
            Depth["Border"] = 0.698f;
            Depth["Arrow"] = 0.697f;

            Depth["WorldBottom"] = 0.6f;
            Depth["WorldTop"] = 0.4f;

            Depth["Notice"] = 0.3f;
            
            Depth["Panel"] = 0.1f;

            Depth["Foreground"] = 0;
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
