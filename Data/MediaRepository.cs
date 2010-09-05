using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using SpriteSheetRuntime;

namespace Illumination.Data {
    public static class MediaRepository {
        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> SoundEffects;
        public static Dictionary<string, Song> Songs;
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, SpriteSheet> Sheets;

        private static bool loaded = false;

        public static void LoadAll(Game gameInstance) {
            Textures = new Dictionary<string, Texture2D>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Songs = new Dictionary<string, Song>();
            Fonts = new Dictionary<string, SpriteFont>();
            Sheets = new Dictionary<string, SpriteSheet>();

            Textures.Add("Blank", gameInstance.Content.Load<Texture2D>("Blank"));

            Textures.Add("BlankTile", gameInstance.Content.Load<Texture2D>("BlankTile"));
            Textures.Add("GrassTile", gameInstance.Content.Load<Texture2D>("GrassTile"));
            Textures.Add("WaterTile", gameInstance.Content.Load<Texture2D>("WaterTile"));

            Textures.Add("TileBorder", gameInstance.Content.Load<Texture2D>("BorderTile"));
            Textures.Add("Border", gameInstance.Content.Load<Texture2D>("Border"));

            Textures.Add("Worker", gameInstance.Content.Load<Texture2D>("WorkerTile"));
            Textures.Add("Educator", gameInstance.Content.Load<Texture2D>("EducatorTile"));
            Textures.Add("Scientist", gameInstance.Content.Load<Texture2D>("DeveloperTile"));
            Textures.Add("Doctor", gameInstance.Content.Load<Texture2D>("DoctorTile"));
            Textures.Add("Environmentalist", gameInstance.Content.Load<Texture2D>("EnvironmentalistTile"));

            Textures.Add("BaseLight", gameInstance.Content.Load<Texture2D>("BaseLight"));
            Textures.Add("WhiteLight", gameInstance.Content.Load<Texture2D>("WhiteLight"));

            Textures.Add("Mirror_NE", gameInstance.Content.Load<Texture2D>("MirrorTile_NE"));
            Textures.Add("Mirror_NW", gameInstance.Content.Load<Texture2D>("MirrorTile_NW"));
            Textures.Add("Mirror_SE", gameInstance.Content.Load<Texture2D>("MirrorTile_SE"));
            Textures.Add("Mirror_SW", gameInstance.Content.Load<Texture2D>("MirrorTile_SW"));

            Textures.Add("Arrow_N", gameInstance.Content.Load<Texture2D>("ArrowTile_N"));
            Textures.Add("Arrow_E", gameInstance.Content.Load<Texture2D>("ArrowTile_E"));
            Textures.Add("Arrow_S", gameInstance.Content.Load<Texture2D>("ArrowTile_S"));
            Textures.Add("Arrow_W", gameInstance.Content.Load<Texture2D>("ArrowTile_W"));

            Textures.Add("Notice", gameInstance.Content.Load<Texture2D>("notice"));

            Textures.Add("TreeOfLight", gameInstance.Content.Load<Texture2D>("TreeOfLight"));

            Textures.Add("School", gameInstance.Content.Load<Texture2D>("School"));
            Textures.Add("Factory", gameInstance.Content.Load<Texture2D>("Factory"));
            Textures.Add("ResearchCenter", gameInstance.Content.Load<Texture2D>("ResearchCenter"));

            Fonts.Add("DefaultFont", gameInstance.Content.Load<SpriteFont>("DefaultFont"));
            Fonts.Add("Arial10", gameInstance.Content.Load<SpriteFont>("Arial10"));

            Sheets.Add("Glow", gameInstance.Content.Load<SpriteSheet>("Glow"));

            loaded = true;
        }

        public static bool IsLoaded() {
            return loaded;
        }
    }
}