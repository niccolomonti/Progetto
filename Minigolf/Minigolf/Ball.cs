using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Ostacoli;

namespace Minigolf
{
    public class Ball : Oggetto
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected int radius;
        protected bool cueOn = false;
        protected Vector2 mousePosition;
        protected MouseState oldState;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Speed { get { return speed; } }

        public Ball()
        {
            this.position = Vector2.Zero;
            this.speed = Vector2.Zero;
            this.texture = C.TEXTUREBALL;
            this.radius = C.BALLRADIUS;
            this.rectangle = new Rectangle((int)position.X - radius, (int)position.Y - radius, 2 * radius, 2 * radius);
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

        public void CheckCollision()
        {
            float distanza = float.MaxValue;
            Rectangle collisionRectangle = new Rectangle();
            Rectangle centerBallRectangle = new Rectangle(Center.X, Center.Y, 1, 1);

            foreach (Oggetto o in V.ostacoli)
            {
                if (o is Wall)
                {
                    if (o.Bounds.Intersects(NextRectangle()))
                    {
                        if (H.PerPixelCollision(this, o))
                        {
                            float d = Vector2.Distance(this.Center.ToVector2(), o.Center.ToVector2());
                            if (d < distanza)
                            {
                                distanza = d;
                                collisionRectangle = o.Bounds;
                            }
                        }
                    }
                }
                if (o is Sand)
                {
                    if (o.Bounds.Intersects(centerBallRectangle))
                    {
                        if (H.PerPixelCollision(this, o))
                            speed *= C.SANDFRICTION;
                    }
                }
                if (o is Climb)
                {
                    if (o.Bounds.Intersects(centerBallRectangle))
                    {
                        if (H.PerPixelCollision(this, o))
                            speed += o.DeltaSpeed;
                    }
                }
            }
            if (!collisionRectangle.IsEmpty)
            {
                int x = collisionRectangle.Center.X - Center.X;
                int y = collisionRectangle.Center.Y - Center.Y;
                if (Math.Abs(x) > Math.Abs(y))
                    speed.X = -speed.X;
                else
                    speed.Y = -speed.Y;
            }
        }

        public Vector2 NextPosition()
        {
            speed *= C.FRICTION;
            if (H.Norme(speed) < C.MINSPEED)
                speed = Vector2.Zero;
            return position + speed;
        }

        public Rectangle NextRectangle()
        {
            Rectangle r = this.Bounds;
            r.Location = new Point((int)(NextPosition().X - radius), (int)(NextPosition().Y - radius));
            return r;
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
                Vector2 newSpeed = new Vector2(this.position.X - mousePosition.X, this.position.Y - mousePosition.Y) / C.CUEATTENUATION;
                if (H.Norme(newSpeed) >= C.MAXSPEED)
                    speed = Vector2.Normalize(newSpeed) * C.MAXSPEED;
                else
                    speed = newSpeed;
            }
            oldState = newState;
        }

        public void Update()
        {
            WindowCollision();
            CheckCollision();

            position = NextPosition();
            rectangle.Location = new Point((int)position.X - radius, (int)position.Y - radius);

            ManageMouse();
        }

        public override void Draw(SpriteBatch sprite)
        {
            if (cueOn)
                   H.DrawLine(sprite, C.TEXTURELINE, mousePosition, position);
            base.Draw(sprite);
        }
    }
}
