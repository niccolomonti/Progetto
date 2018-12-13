using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf.Sprites
{
    public class Sand : Sprite
    {
        public Sand(Texture2D theTexture, Vector2 center) : base(theTexture)
        {
            base.rectangle = C.RECTSAND;
            base.Center = center;
            base.rectangle.Location = new Point(center.ToPoint().X - rectangle.Width / 2, center.ToPoint().Y - rectangle.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
        }
    }
}
