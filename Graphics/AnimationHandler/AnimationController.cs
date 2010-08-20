using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Illumination.Graphics.AnimationHandler
{
    public class AnimationController
    {
        HashSet<Animation> animations;

        public AnimationController()
        {
            animations = new HashSet<Animation>();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            HashSet<Animation> oldAnimations = new HashSet<Animation>(animations);
            foreach (Animation animation in oldAnimations)
            {
                if (!animation.Update(spriteBatch, gameTime))
                {
                    RemoveAnimation(animation);
                }
            }
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation);
        }

        public void RemoveAnimation(Animation animation)
        {
            animations.Remove(animation);
        }
    }
}
