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
    public class Wall : Sprite
    {
        public Wall(Texture2D theTexture, Vector2 position): base(theTexture)
        {
            base.rectangle = new Rectangle(Point.Zero, C.DIMWALL);
            base.rectangle.Location = position.ToPoint();
            base.position = position;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            #pragma warning disable CS0618 // Il tipo o il membro è obsoleto
            spriteBatch.Draw(texture, destinationRectangle: rectangle, color: Color.White, layerDepth: 1);
            #pragma warning restore CS0618 // Il tipo o il membro è obsoleto
        }
    }
}
