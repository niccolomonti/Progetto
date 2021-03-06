﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Models;

namespace Minigolf.Managers
{
    public class AnimationManager
    {
        public Animation animation;
        private float timer;         


        public AnimationManager(Animation theAnimation)
        {
            animation = theAnimation;
        }

        public void Play(Animation theAnimation)
        {
            if (animation == theAnimation)
                return;

            animation = theAnimation;
            animation.CurrentFrame = 0;
            timer = 0f;
        }

        public void Stop()
        {
            animation.CurrentFrame = 2; // dipende dagli sheet (potrebbe essere un problema se 
                                        // si hanno sheet con gli sprite di idle in posizioni
                                        // diverse
            timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;

                if (animation.CurrentFrame >= animation.FrameCount)
                    animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
           spriteBatch.Draw(animation.Texture, position - animation.Offset, new Rectangle(animation.CurrentFrame * animation.FrameWidth, 0, animation.FrameWidth, animation.FrameHeight), Color.White, 0 , Vector2.Zero, C.PIXELSXPOINT.X / 15, SpriteEffects.None, 0.4f);
        }
    }
}
