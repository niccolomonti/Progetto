using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Ostacoli;

namespace Minigolf
{
    public static class C //Costants
    { 
        #region Finestra gioco
        public static Point MAINGRID = new Point(68, 53);
        public static Point PIXELSXPOINT = new Point(15);
        public static Point MAINWINDOW = MAINGRID * PIXELSXPOINT;
        #endregion

        #region Nome files contenenti i layout 
        public static string[] FILENAME =
        {
            @"..\..\..\..\Layout\Level0.txt",
            @"..\..\..\..\Layout\Level1.txt",
            @"..\..\..\..\Layout\Level2.txt"
        };
        public static int MAXLEVEL = 1;
        #endregion

        #region Texture Start e End
        public static Texture2D TEXTURESTAR, TEXTUREEND;
        #endregion

        #region Dati Wall
        public static Texture2D TEXTUREWALL;
        public static Point DIMWALL = new Point(15, 15);
        public static Rectangle RECTWALL = new Rectangle(Point.Zero, DIMWALL);
        #endregion
        
        #region Dati Climb
        public static Texture2D TEXTURECLIMB;
        public static Point DIMCLIMB = new Point(30, 75);
        public static Rectangle RECTCLIMB = new Rectangle(Point.Zero, DIMCLIMB);
        public static float VELCLIMB = 0.05f;
        #endregion

        #region Dati Sand
        public static Texture2D TEXTURESAND;
        public static Point DIMSAND = new Point(30, 30);
        public static Rectangle RECTSAND = new Rectangle(Point.Zero, DIMSAND);
        public static float SANDFRICTION = 0.9f;
        #endregion

        #region Dati Ball
        public static Texture2D TEXTUREBALL;
        public static int BALLRADIUS = 5;
        public static float MAXSPEED = 13f;
        public static float MINSPEED = 0.05f;
        public static float FRICTION = 0.997f;
        #endregion

        public static int CUEATTENUATION = 50;

        public static Texture2D TEXTURESILVER,TEXTURELINE;
    }
}
