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

        public Ball(Vector2 position, Texture2D texture, SpriteBatch sprite)
        {
            this.position = position;
            //this.speed = new Vector2(3, 3);
            this.speed = new Vector2(V.random.Next(-5, 5), V.random.Next(-5, 5));
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
            return position + speed;
        }

        public void Update()
        {
            FrameCollision();
            WindowCollision();
            position = NextPosition();
            rectangle.Location = new Point((int)(position.X - radius), (int)(position.Y - radius));
        }

        public void Draw()
        {
            sprite.Draw(texture, position, null, Color.White, 0, new Vector2(C.TEXTUREBALL.Width / 2, C.TEXTUREBALL.Height / 2), (2 * radius / C.TEXTUREBALL.Width), SpriteEffects.None, 1);
        }
    }
}
