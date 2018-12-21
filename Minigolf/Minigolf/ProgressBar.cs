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
    public class ProgressBar
    {
        protected Texture2D background;
        protected Texture2D bar;
        protected Vector2 position;
        protected float speedIncrement;
        protected float timer;
        protected int level;

        public int Level { get { return level; } }

        public ProgressBar(Texture2D background, Texture2D bar, Vector2 position)
        {
            this.background = background;
            this.bar = bar;
            this.position = position;
            this.level = 0;
            this.speedIncrement = 0.05f;
            this.timer = 0;
        }

        public void Increment(GameTime gameTime)
        {
            if (H.Timeout(gameTime, ref timer, speedIncrement))
                level = (level + 1) % 100;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Point b = new Point(level * (background.Bounds.Width - 4) / 100, (background.Bounds.Height - 4));
            spriteBatch.Draw(background, position, null, Color.White,0,new Vector2(0),1,SpriteEffects.None,1);
            spriteBatch.Draw(bar, new Rectangle(position.ToPoint() + new Point(2), b), Color.White);
        }
    }
}
