using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Sprites;
using Minigolf.Models;


namespace Minigolf
{
    public class Player : Sprite
    {
        public Player(Dictionary<string, Animation> theAnimaitions) : base(theAnimaitions)
        {
            
        }

        override protected void SetAnimation()
        {
            if (velocity.X > 0)
                animationManager.Play(animations["WalkRight"]);
            else if (velocity.X < 0)
                animationManager.Play(animations["WalkLeft"]);
            else if (velocity.Y > 0)
                animationManager.Play(animations["WalkDown"]);
            else if (velocity.Y < 0)
                animationManager.Play(animations["WalkUp"]);
            else
                animationManager.Stop();
        }

        override protected void Move()
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

        override public void Update(GameTime gameTime)
        {
            switch (V.gameState)
            {
                case GAMESTATE.START:
                    position = V.startPosition[V.level];                    
                    velocity = Vector2.Zero;
                    SetAnimation();
                    animationManager.Update(gameTime);                    
                    break;
                case GAMESTATE.PLAY:
                    Move();
                    SetAnimation();
                    animationManager.Update(gameTime);
                    Position += velocity;
                    velocity = Vector2.Zero;                    
                    break;

            }

        }
    }
}
