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
            //spriteBatch.Draw(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
            spriteBatch.Draw(texture: texture, destinationRectangle: rectangle, sourceRectangle: null, color: Color.White, rotation: 0, origin: Vector2.Zero, scale: new Vector2(C.PIXELSXPOINT.X / 15, C.PIXELSXPOINT.X / 15), effects: SpriteEffects.None, layerDepth: 0.8f);            
        }
    }
}
