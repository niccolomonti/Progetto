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
    public class Sand : Oggetto
    {
        public Sand(Point centerPosition)
        {
            texture = C.TEXTURESAND;
            rectangle = C.RECTSAND;
            rectangle.Location = centerPosition + new Point(-rectangle.Width / 2, -rectangle.Height / 2);
        }
    }
}
