﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumination.Data;
using Microsoft.Xna.Framework.Graphics;
using Illumination.Graphics;

namespace Illumination.WorldObjects
{
    public class Mirror : Item
    {
        public enum ReflectionType
        {
            NorthEast,
            NorthWest,
            SouthEast,
            SouthWest,
            SIZE
        }

        ReflectionType reflection;
        public ReflectionType Reflection
        {
            get { return reflection; }
            set { reflection = value; }
        }

        static Dictionary<ReflectionType, Texture2D> texturesMap;

        static Mirror()
        {
            texturesMap = new Dictionary<ReflectionType, Texture2D>();

            texturesMap[ReflectionType.NorthEast] = MediaRepository.Textures["Mirror_NE"];
            texturesMap[ReflectionType.NorthWest] = MediaRepository.Textures["Mirror_NW"];
            texturesMap[ReflectionType.SouthEast] = MediaRepository.Textures["Mirror_SE"];
            texturesMap[ReflectionType.SouthWest] = MediaRepository.Textures["Mirror_SW"];
        }

        public override Texture2D GetTexture() {
            return texturesMap[reflection];;
        }

        public Mirror() : base() {
            Initialize();
        }

        public Mirror(int x, int y) : base(x, y, 1, 1, MediaRepository.Textures["Mirror_NE"]) {
            reflection = ReflectionType.NorthEast;

            Initialize();
        }

        private void Initialize() {
            name = "Mirror";
            cost = 50;
        }

        public override void Draw(SpriteBatchRelative spriteBatch)
        {
            spriteBatch.DrawRelative(GetTexture(), BoundingBox, Color.White, Layer.GetWorldDepth(GridLocation));
        }

        public Entity.DirectionType Reflect(Entity.DirectionType lightIn)
        {
            Entity.DirectionType lightOut = DirectionType.NONE;
            switch (reflection)
            {
                case Mirror.ReflectionType.NorthEast:
                    if (lightIn == DirectionType.West) { lightOut = DirectionType.North; }
                    else if (lightIn == DirectionType.South) { lightOut = DirectionType.East; }
                    break;
                case Mirror.ReflectionType.NorthWest:
                    if (lightIn == DirectionType.East) { lightOut = DirectionType.North; }
                    else if (lightIn == DirectionType.South) { lightOut = DirectionType.West; }
                    break;
                case Mirror.ReflectionType.SouthEast:
                    if (lightIn == DirectionType.West) { lightOut = DirectionType.South; }
                    else if (lightIn == DirectionType.North) { lightOut = DirectionType.East; }
                    break;
                case Mirror.ReflectionType.SouthWest:
                    if (lightIn == DirectionType.East) { lightOut = DirectionType.South; }
                    else if (lightIn == DirectionType.North) { lightOut = DirectionType.West; }
                    break;
            }

            return lightOut;
        }

        public override Item CreateNewItem() {
            Mirror newMirror = new Mirror(0, 0);
            newMirror.reflection = reflection;

            return newMirror;
        }
    }
}
