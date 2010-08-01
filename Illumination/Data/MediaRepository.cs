using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace Illumination.Data
{
    public static class MediaRepository
    {
        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> SoundEffects;
        public static Dictionary<string, Song> Songs;
        public static Dictionary<string, SpriteFont> Fonts;

        private static bool loaded = false;

        public static void LoadAll(Game gameInstance)
        {
            Textures = new Dictionary<string, Texture2D>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Songs = new Dictionary<string, Song>();
            Fonts = new Dictionary<string, SpriteFont>();

            Textures.Add("GrassTile", gameInstance.Content.Load<Texture2D>("grasstile"));

            loaded = true;
        }

        public static bool IsLoaded()
        {
            return loaded;
        }
    }
}