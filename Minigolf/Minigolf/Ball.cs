using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf
{
    public class Ball
    {
        protected Texture2D texture;
        protected SpriteBatch sprite;
        protected Vector2 position;
        protected Vector2 speed;
        protected float radius;
        protected Rectangle rectangle;
        protected bool cueOn = false;
        protected Vector2 mousePosition;
        protected MouseState oldState;

        public Vector2 Position { get { return position; } }

        public Ball(Texture2D texture, SpriteBatch sprite)
        {
            this.position = Vector2.Zero;
            this.speed = Vector2.Zero;
            this.texture = texture;
            this.sprite = sprite;
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
                    speed.X = -speed.X;
                }
                else
                {
                    speed.Y = -speed.Y;
                }
            }
        }

        public void WindowCollision()
        {
            if (position.X - radius < 0)
                speed.X = -speed.X;
            if (position.Y - radius < 0)
                speed.Y = -speed.Y;
            if (position.X + radius > C.MAINWINDOW.X)
                speed.X = -speed.X;
            if (position.Y + radius > C.MAINWINDOW.Y)
                speed.Y = -speed.Y;
        }

        public Vector2 NextPosition()
        {
            speed = speed * C.FRICTION;
            if (H.Norme(speed) < 0.1f)
                speed = Vector2.Zero;
            return position + speed;
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
                this.speed.X = (this.position.X + -(mousePosition.X)) / C.CUEATTENUATION;
                this.speed.Y = (this.position.Y - (mousePosition.Y)) / C.CUEATTENUATION;
            }
            oldState = newState;
        }

        public void Update()
        {
            switch (V.gameState)
            {
                case GAMESTATE.START:
                    position = V.startPosition[V.level];
                    speed = Vector2.Zero;
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

        public void Draw()
        {
            sprite.Draw(texture, position, null, Color.White, 0, new Vector2(C.TEXTUREBALL.Width / 2, C.TEXTUREBALL.Height / 2), (2 * radius / C.TEXTUREBALL.Width), SpriteEffects.None, 1);
            if (cueOn)
                H.DrawLine(this.sprite, C.TEXTURELINE, mousePosition, position);
        }
    }
}
