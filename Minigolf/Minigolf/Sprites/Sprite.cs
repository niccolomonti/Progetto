using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Models;
using Minigolf.Managers;

namespace Minigolf.Sprites
{
    public class Sprite
    {
        public Texture2D texture;
        public Input input;
        protected Vector2 position;
        public float speed = 1f;
        public Vector2 velocity;        

        protected AnimationManager animationManager;
        protected Dictionary<string, Animation> animations;

        public Rectangle rectangle;
        public bool selected;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                if (animationManager != null)
                    animationManager.Position = position;
            }
        }

        public Sprite(Texture2D theTexture)
        {
            texture = theTexture;
        }

        public Sprite(Dictionary<string, Animation> theAnimaitions)
        {
            animations = theAnimaitions;
            animationManager = new AnimationManager(theAnimaitions.First().Value);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, Color.White);
            else if (animationManager != null)
                animationManager.Draw(spriteBatch);
            else throw new Exception("There is an error!");
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(input.Up))
                velocity.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(input.Down))
                velocity.Y += speed;
            if (Keyboard.GetState().IsKeyDown(input.Left))
                velocity.X -= speed;
            if (Keyboard.GetState().IsKeyDown(input.Right))
                velocity.X += speed;
            
        }

        protected virtual void SetAnimation()
        {
            
        }

        public virtual void Update(GameTime gameTime/*, List<Sprite> sprites*/)
        {
            Move();

            SetAnimation();

            animationManager.Update(gameTime);

            Position += velocity;
            velocity = Vector2.Zero;
        }        
    }
}
