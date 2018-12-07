using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf.Ostacoli
{
    public class Oggetto
    {
        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Vector2 deltaSpeed;

        public Texture2D Texture { get { return texture; } }
        public Rectangle Bounds { get { return rectangle; } }
        public Point Center { get { return rectangle.Center; } }
        public Vector2 DeltaSpeed { get { return deltaSpeed; } }

        public virtual void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, rectangle, Color.White);
        }
    }
}
