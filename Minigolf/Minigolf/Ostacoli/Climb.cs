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
    public class Climb : Oggetto
    {
        protected int orientation;
        protected Point center;

        public Climb(int orientation, Point centerPosition)
        {
            texture = C.TEXTURECLIMB;
            rectangle = C.RECTCLIMB;
            center = centerPosition;
            rectangle.Location = centerPosition + new Point(-rectangle.Width / 2, -rectangle.Height / 2);
            this.orientation = orientation;
            if (orientation == 0)
                deltaSpeed = new Vector2(0, -C.VELCLIMB);
            else if (orientation == 1)
            {
                deltaSpeed = new Vector2(C.VELCLIMB, 0);
                rectangle = H.RotateRectangle(rectangle);
            }
            else if (orientation == 2)
                deltaSpeed = new Vector2(0, C.VELCLIMB);
            else if (orientation == 3)
            {
                deltaSpeed = new Vector2(-C.VELCLIMB, 0);
                rectangle = H.RotateRectangle(rectangle);
            }
        }

        public override void Draw(SpriteBatch sprite)
        {
            Vector2 origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            if (orientation == 1 || orientation == 3)
                origin = H.Invert(origin);

#pragma warning disable CS0618 // Il tipo o il membro è obsoleto
            sprite.Draw(texture,
                position: center.ToVector2(),
                color: Color.White,
                origin: origin,
                rotation: orientation * (float)Math.PI / 2
                );
#pragma warning restore CS0618 // Il tipo o il membro è obsoleto
        }
    }
}
