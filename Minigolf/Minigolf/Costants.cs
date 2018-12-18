﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Minigolf
{
    public static class C //Costants
    {
        #region Dimensioni Finestra e Griglia
        public static Point MAINGRID = new Point(68, 53);
        public static Point PIXELSXPOINT = new Point(15);
        public static Point MAINWINDOW = MAINGRID * PIXELSXPOINT;
        #endregion

        #region Background e pista
        public static Texture2D TEXTUREBACKGROUND;
        public static Texture2D TEXTURETRACK;
        #endregion

        #region Nome e numero files contenenti i layout
        public static int MAXLEVEL = 2;
        public static string[] FILENAME =
        {
            @"..\..\..\..\Layout\Level0.txt",
            @"..\..\..\..\Layout\Level1.txt",
            @"..\..\..\..\Layout\Level2.txt"
        };
        public static int[] PAR = { 2, 4, 10 };
        #endregion

        #region Texture di start game e Tabellone punteggi
        public static Texture2D TEXTURESTARTGAME, TEXTURESCORE;
        #endregion

        #region Texture Hole e start-ball
        public static Texture2D TEXTUREHOLE, TEXTURESTART;
        #endregion

        #region Dati Ball
        public static Texture2D TEXTUREBALL;
        public static float BALLRADIUS = 7.5f;
        public static float FRICTION = 0.997f;
        public static float MAXSPEED = 13f;
        public static float MINSPEED = 0.05f;
        #endregion

        #region Dati Wall
        public static Texture2D TEXTUREWALL;
        public static Point DIMWALL = C.PIXELSXPOINT;
        public static Rectangle RECTWALL = new Rectangle(Point.Zero, DIMWALL);
        #endregion

        #region Dati Climb
        public static Texture2D TEXTURECLIMB;
        public static Point DIMCLIMB = new Point(3, 5) * C.PIXELSXPOINT;
        public static Rectangle RECTCLIMB = new Rectangle(Point.Zero, DIMCLIMB);
        public static float VELCLIMB = 0.1f;
        #endregion

        #region Dati Sand
        public static Texture2D TEXTURESAND;
        public static Point DIMSAND = new Point(3, 3) * C.PIXELSXPOINT;
        public static Rectangle RECTSAND = new Rectangle(Point.Zero, DIMSAND);
        public static float SANDFRICTION = 0.9f;
        #endregion

        #region Dati Button Play e Continue
        public static Texture2D[] TEXTUREPLAY = new Texture2D[3];
        public static Texture2D[] TEXTURECONTINUE = new Texture2D[3];
        public static Point DIMBUTTON = new Point(295, 97);
        #endregion

        #region altre costanti

        public static Texture2D TEXTUREARROW;
        public static int CUEATTENUATION = 50;
        public static float GETBALLDISTANCE = 30f;
        #endregion

        #region Suoni
        public static SoundEffect buttonSound, golfShot, ballHitWall, ballInHole, applause, boo; 
        #endregion
    }
}
