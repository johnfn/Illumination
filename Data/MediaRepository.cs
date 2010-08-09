using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace Illumination.Data {
    public static class MediaRepository {
        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> SoundEffects;
        public static Dictionary<string, Song> Songs;
        public static Dictionary<string, SpriteFont> Fonts;

        private static bool loaded = false;

        public static void LoadAll(Game gameInstance) {
            Textures = new Dictionary<string, Texture2D>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Songs = new Dictionary<string, Song>();
            Fonts = new Dictionary<string, SpriteFont>();

            Textures.Add("Blank", gameInstance.Content.Load<Texture2D>("whitetile"));

            Textures.Add("GrassTile", gameInstance.Content.Load<Texture2D>("grasstile"));
            Textures.Add("WaterTile", gameInstance.Content.Load<Texture2D>("watertile"));

            Textures.Add("Worker", gameInstance.Content.Load<Texture2D>("worker"));
            Textures.Add("Educator", gameInstance.Content.Load<Texture2D>("educator"));
            Textures.Add("Scientist", gameInstance.Content.Load<Texture2D>("developer"));
            Textures.Add("Doctor", gameInstance.Content.Load<Texture2D>("doctor"));
            Textures.Add("Environmentalist", gameInstance.Content.Load<Texture2D>("environmentalist"));

            Textures.Add("Light", gameInstance.Content.Load<Texture2D>("whitelight"));

            Textures.Add("Mirror_NE", gameInstance.Content.Load<Texture2D>("mirror_NE"));
            Textures.Add("Mirror_NW", gameInstance.Content.Load<Texture2D>("mirror_NW"));
            Textures.Add("Mirror_SE", gameInstance.Content.Load<Texture2D>("mirror_SE"));
            Textures.Add("Mirror_SW", gameInstance.Content.Load<Texture2D>("mirror_SW"));

            Textures.Add("Arrow_N", gameInstance.Content.Load<Texture2D>("arrow_N"));
            Textures.Add("Arrow_E", gameInstance.Content.Load<Texture2D>("arrow_E"));
            Textures.Add("Arrow_S", gameInstance.Content.Load<Texture2D>("arrow_S"));
            Textures.Add("Arrow_W", gameInstance.Content.Load<Texture2D>("arrow_W"));

            Textures.Add("TreeOfLight", gameInstance.Content.Load<Texture2D>("treeoflight"));

            Textures.Add("School", gameInstance.Content.Load<Texture2D>("school"));

            Fonts.Add("DefaultFont", gameInstance.Content.Load<SpriteFont>("DefaultFont"));

            loaded = true;
        }

        public static bool IsLoaded() {
            return loaded;
        }
    }
}