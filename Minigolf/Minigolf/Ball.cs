﻿using System;
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
    public class Ball : Sprite
    {        
        protected float radius;        
        protected bool cueOn = false;
        protected Vector2 mousePosition;
        protected MouseState oldState;        
      

        public Ball(Texture2D theTexture): base(theTexture)
        {
            this.radius = C.BALLRADIUS;
            base.position = Vector2.Zero;
            base.rectangle = new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(2 * radius), (int)(2 * radius));
            base.velocity = Vector2.Zero;
            base.selected = false;
        }

        public Ball(Dictionary<string, Animation> theAnimaitions) : base(theAnimaitions)
        {
            this.radius = C.BALLRADIUS;
            base.position = Vector2.Zero;
            base.rectangle = new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(2 * radius), (int)(2 * radius));
            base.velocity = Vector2.Zero;
            base.selected = false;
        }

        public void WindowCollision()
        {
            if (position.X - radius < 0)
                velocity.X = -velocity.X;
            if (position.Y - radius < 0)
                velocity.Y = -velocity.Y;
            if (position.X + radius > C.MAINWINDOW.X)
                velocity.X = -velocity.X;
            if (position.Y + radius > C.MAINWINDOW.Y)
                velocity.Y = -velocity.Y;
        }

        public void CheckCollision()
        {
            Rectangle nextPositionRect = this.rectangle;
            nextPositionRect.Location = new Point((int)(NextPosition().X - radius), (int)(NextPosition().Y - radius));

            float distanza = float.MaxValue;
            Rectangle rectCollision = new Rectangle();

            foreach (var o in V.listSpriteLevel)
            {
                if (o is Wall)
                {
                    if (o.rectangle.Intersects(nextPositionRect))
                    {
                        float d = Vector2.Distance(o.rectangle.Center.ToVector2(), rectangle.Center.ToVector2());
                        if (d < distanza)
                        {
                            distanza = d;
                            rectCollision = o.rectangle;
                        }
                    }
                }
                if (o is Sand)
                {
                    if (H.Intersect(position.ToPoint(), o.rectangle))
                    {
                        velocity *= C.SANDFRICTION;
                    }
                }
                if (o is Climb)
                {
                    if (H.Intersect(position.ToPoint(), o.rectangle))
                    {
                        velocity += o.velocity;
                    }
                }
            }
            if (!rectCollision.IsEmpty)
            {
                int x = rectCollision.Center.X - rectangle.Center.X;
                int y = rectCollision.Center.Y - rectangle.Center.Y;
                if (Math.Abs(x) > Math.Abs(y))
                {
                    velocity.X = -velocity.X;
                }
                else
                {
                    velocity.Y = -velocity.Y;
                }
            }
        }

        public Vector2 NextPosition()
        {
            velocity *= C.FRICTION;
            if (H.Norme(velocity) < C.MINSPEED)
                velocity = Vector2.Zero;
            return position + velocity;
        }

        public void ManageMouse()
        {
            MouseState newState = Mouse.GetState();
            mousePosition = new Vector2(newState.X, newState.Y);
            if ((newState.LeftButton == ButtonState.Pressed) && (oldState.LeftButton == ButtonState.Released))
            {
                cueOn = true;
                selected = true; 
            }
            if (newState.LeftButton == ButtonState.Pressed)
                cueOn = true;
            if (newState.LeftButton == ButtonState.Released)            
                cueOn = false;            
            if ((newState.LeftButton == ButtonState.Released) && (oldState.LeftButton == ButtonState.Pressed))
            {
                Vector2 newVelocity = new Vector2(this.position.X - mousePosition.X, this.position.Y - mousePosition.Y) / C.CUEATTENUATION;
                if (H.Norme(newVelocity) >= C.MAXSPEED)
                    velocity = Vector2.Normalize(newVelocity) * C.MAXSPEED;
                else
                    velocity = newVelocity;
                selected = false;
            }
            oldState = newState;
        }

        override public void Update(GameTime gameTime)
        {
            //position = NextPosition();
            switch (V.gameState)
            {
                case GAMESTATE.START:
                    position = V.startPosition[V.level];
                    velocity = Vector2.Zero;                    
                    break;
                case GAMESTATE.HITBALL:
                    ManageMouse();
                    break;
                case GAMESTATE.GOTOBALL:                    
                    break;
                case GAMESTATE.PLAY:
                    CheckCollision();
                    WindowCollision();
                    position = NextPosition();
                    rectangle.Location = new Point((int)(position.X - radius), (int)(position.Y - radius));

                    if(velocity == Vector2.Zero)
                        ManageMouse();
                    break;
                case GAMESTATE.ENDGAME:
                    break;
            }
            
        }

        override protected void SetAnimation()
        {
            // da modificare
            animationManager.Play(animations["MotionLess"]);
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(C.TEXTUREBALL.Width / 2, C.TEXTUREBALL.Height / 2), (2 * radius / C.TEXTUREBALL.Width), SpriteEffects.None, 0.8f);
            if (cueOn)
                H.DrawLine(spriteBatch, C.TEXTUREARROW, mousePosition, position);
        }
    }
}
