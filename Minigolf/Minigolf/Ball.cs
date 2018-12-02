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
    public class Ball : Sprite
    {        
        protected float radius;
        protected Rectangle rectangle;
        protected bool cueOn = false;
        protected Vector2 mousePosition;
        protected MouseState oldState;        

        public Ball(Texture2D theTexture): base(theTexture)
        {
            this.radius = C.BALLRADIUS;
            this.rectangle = new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(2 * radius), (int)(2 * radius));
        }

        public Ball(Dictionary<string, Animation> theAnimaitions) : base(theAnimaitions)
        {
            this.radius = C.BALLRADIUS;
            this.rectangle = new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(2 * radius), (int)(2 * radius));
        }

        public void FrameCollision()
        {
            Rectangle nextPositionRect = this.rectangle;
            nextPositionRect.Location = new Point((int)(NextPosition().X - radius), (int)(NextPosition().Y - radius));

            float distanza = float.MaxValue;
            Rectangle rectCollision = new Rectangle();

            foreach (Rectangle wallRectangle in V.listRectWall)
            {
                if (wallRectangle.Intersects(nextPositionRect))
                {
                    float d = Vector2.Distance(wallRectangle.Center.ToVector2(), rectangle.Center.ToVector2());
                    if (d < distanza)
                    {
                        distanza = d;
                        rectCollision = wallRectangle;
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

        public Vector2 NextPosition()
        {
            velocity = velocity * C.FRICTION;
            if (H.Norme(velocity) < 0.1f)
                velocity = Vector2.Zero;
            return position + velocity;
        }

        public void ManageMouse()
        {
            MouseState newState = Mouse.GetState();
            mousePosition = new Vector2(newState.X, newState.Y);
            if ((newState.LeftButton == ButtonState.Pressed) && (oldState.LeftButton == ButtonState.Released))
                cueOn = true;
            if (newState.LeftButton == ButtonState.Pressed)
                cueOn = true;
            if (newState.LeftButton == ButtonState.Released)
                cueOn = false;
            if ((newState.LeftButton == ButtonState.Released) && (oldState.LeftButton == ButtonState.Pressed))
            {
                velocity.X = (this.position.X + -(mousePosition.X)) / C.CUEATTENUATION;
                velocity.Y = (this.position.Y - (mousePosition.Y)) / C.CUEATTENUATION;
            }
            oldState = newState;
        }

        override public void Update(GameTime gameTime)
        {
            switch (V.gameState)
            {
                case GAMESTATE.START:
                    position = V.startPosition[V.level];
                    velocity = Vector2.Zero;
                    V.gameState = GAMESTATE.PLAY;
                    break;
                case GAMESTATE.PLAY:
                    FrameCollision();
                    WindowCollision();
                    position = NextPosition();
                    rectangle.Location = new Point((int)(position.X - radius), (int)(position.Y - radius));

                    ManageMouse();

                    if (rectangle.Intersects(V.endPositionRect[V.level]))
                    {
                        if (++V.level <= C.MAXLEVEL)
                        {                           
                            V.gameState = GAMESTATE.START;
                        }
                        else
                        {
                            V.gameState = GAMESTATE.END;
                        }
                    }
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
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(C.TEXTUREBALL.Width / 2, C.TEXTUREBALL.Height / 2), (2 * radius / C.TEXTUREBALL.Width), SpriteEffects.None, 1);
            if (cueOn)
                H.DrawLine(spriteBatch, C.TEXTURELINE, mousePosition, position);
        }
    }
}
