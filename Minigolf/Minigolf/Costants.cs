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
    public static class C //Costants
    {
        public static Point MAINGRID = new Point(68, 53);
        public static Point PIXELSXPOINT = new Point(15);
        public static Point MAINWINDOW = MAINGRID * PIXELSXPOINT;

        public static Texture2D TEXTURESILVER, TEXTURESTART, TEXTUREEND, TEXTUREHOLE, TEXTURELINE, TEXTURESAND, TEXTURECLIMB;

        public static int MAXLEVEL = 2;
        public static string[] FILENAME =
        {
            @"..\..\..\..\Layout\Level0.txt",
            @"..\..\..\..\Layout\Level1.txt",
            @"..\..\..\..\Layout\Level2.txt"
        };

        public static Texture2D TEXTUREBALL;
        public static float BALLRADIUS = 5f;
        public static float FRICTION = 0.997f;

        public static int CUEATTENUATION = 50;

        public static Point PIXELSXSAND = new Point(30, 30);
        public static float SANDFRICTION = 0.9f;

        public static Point PIXELSXCLIMB = new Point(30, 75);
        public static float VELCLIMB = 0.05f;
    }
}
