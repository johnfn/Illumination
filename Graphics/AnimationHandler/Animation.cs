using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpriteSheetRuntime;

namespace Illumination.Graphics.AnimationHandler
{
    public class Animation
    {
        const int numFramesPerSec = 40;

        public enum ImageType
        {
            Single,
            Multiple
        }

        ImageType image = ImageType.Multiple;

        /* Image */
        Texture2D texture;
        SpriteSheet spriteSheet;

        /* Translation */
        Rectangle initialBox;
        Rectangle finalBox;

        /* Rotation */
        float initialAngle = 0;
        float finalAngle = 0;

        /* Fading */
        byte initialAlpha = 255;
        byte finalAlpha = 255;

        /* Time */
        double elapsedTotalSec = 0;
        double animationDuration;
        double spriteFrameDuration; // only for multiple images

        public Animation(Texture2D texture, Rectangle location, double duration)
        {
            image = ImageType.Single;

            this.texture = texture;
            initialBox = location;
            finalBox = location;
            animationDuration = duration;
        }

        public Animation(SpriteSheet spriteSheet, Rectangle location, double duration, double frameDuration)
        {
            image = ImageType.Multiple;

            this.spriteSheet = spriteSheet;
            initialBox = location;
            finalBox = location;
            animationDuration = duration;
            spriteFrameDuration = frameDuration;
        }

        public void AddTranslation(Rectangle destination)
        {
            finalBox = destination;
        }

        public void AddRotation(float initialAngle, float finalAngle)
        {
            this.initialAngle = initialAngle;
            this.finalAngle = finalAngle;
        }

        public void AddFading(byte initalAlpha, byte finalAlpha)
        {
            this.initialAlpha = initalAlpha;
            this.finalAlpha = finalAlpha;
        }

        public bool DrawNextFrame(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int totalNumFrames = (int)(numFramesPerSec * animationDuration);
            int currentFrame = (int)(numFramesPerSec * elapsedTotalSec);
            double fraction = currentFrame / (double)totalNumFrames;

            Rectangle currentBox = initialBox;
            currentBox.X += (int)(fraction * (finalBox.X - initialBox.X));
            currentBox.Y += (int)(fraction * (finalBox.Y - initialBox.Y));
            currentBox.Width += (int)(fraction * (finalBox.Width - initialBox.Width));
            currentBox.Height += (int)(fraction * (finalBox.Height - initialBox.Height));

            Vector2 origin = new Vector2(currentBox.Width / 2, currentBox.Height / 2);
            currentBox.X += (int)origin.X;
            currentBox.Y += (int)origin.Y;

            float currentAngle = initialAngle;
            currentAngle += (float)(fraction * (finalAngle - initialAngle));

            byte currentAlpha = initialAlpha;
            currentAlpha += (byte)(fraction * (finalAlpha - initialAlpha));

            if (image == ImageType.Single)
            {
                spriteBatch.Draw(texture, currentBox, null, new Color(255, 255, 255, currentAlpha), currentAngle, origin, SpriteEffects.None, 0);
            }
            else
            {
                int index = (int)(elapsedTotalSec / spriteFrameDuration) % spriteSheet.Count; 
                spriteBatch.Draw(spriteSheet.Texture, currentBox, spriteSheet.SourceRectangle(index), new Color(255, 255, 255, currentAlpha), currentAngle, origin, SpriteEffects.None, 0);
            }

            elapsedTotalSec += gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            return elapsedTotalSec <= animationDuration;
        }
    }
}
