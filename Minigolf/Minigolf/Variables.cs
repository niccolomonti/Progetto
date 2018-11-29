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
    public static class V //Variables
    {
        public static Random random = new Random();

        public static int level = 1;

        public static List<char[,]> gridMaps = new List<char[,]>();
        public static List<Rectangle> listRectWall = new List<Rectangle>();

        public static Ball ball;
    }
}
