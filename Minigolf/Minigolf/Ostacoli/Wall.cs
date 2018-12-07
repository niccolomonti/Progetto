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
    public class Wall : Oggetto
    {
        public Wall(Point position)
        {
            texture = C.TEXTUREWALL;
            rectangle = C.RECTWALL;
            rectangle.Location = position;
        }
    }
}
