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
    public class Climb : Sprite
    {
        protected int orientation;

        public Climb(Texture2D texture, Vector2 center, int orientation) : base(texture)
        {
            base.rectangle = C.RECTCLIMB;
            base.center = center;
            base.rectangle.Location = center.ToPoint() + new Point(-rectangle.Width / 2, -rectangle.Height / 2);
            this.orientation = orientation;
            if (orientation == 0)
                velocity = new Vector2(0, -C.VELCLIMB);
            else if (orientation == 1)
            {
                velocity = new Vector2(C.VELCLIMB, 0);
                rectangle = H.RotateRectangle(rectangle);
            }
            else if (orientation == 2)
                velocity = new Vector2(0, C.VELCLIMB);
            else if (orientation == 3)
            {
                velocity = new Vector2(-C.VELCLIMB, 0);
                rectangle = H.RotateRectangle(rectangle);
            }
        }

        public override void Draw(SpriteBatch sprite)
        {
            Vector2 origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            if (orientation == 1 || orientation == 3)
                origin = H.Invert(origin);

            sprite.Draw(texture, center, null, Color.White, orientation * (float)Math.PI / 2, origin, C.PIXELSXPOINT.X / 15, SpriteEffects.None, 0.8f);
        }
    }
}
