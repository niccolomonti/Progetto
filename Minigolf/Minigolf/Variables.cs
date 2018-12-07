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
    public enum GAMESTATE { START, PLAY, END };

    public static class V //Variables
    { 
        public static Random random = new Random();

        //livello di partenza
        public static int level = 0;

        //Lista di matrici contenenti le mappe dei livelli
        public static List<char[,]> gridMaps = new List<char[,]>();
        //array contenenti le posizioni di inzio e fine di ogni livello
        public static Vector2[] startPosition = new Vector2[C.MAXLEVEL + 1];
        public static Vector2[] endPosition = new Vector2[C.MAXLEVEL + 1];

        //public static List<Rectangle> listRectWall = new List<Rectangle>();

        //public static Rectangle[] endPositionRect = new Rectangle[C.MAXLEVEL + 1];

        //public static List<Rectangle> listRectSand = new List<Rectangle>();
        //public static List<Rectangle> listClimbRight = new List<Rectangle>();
        //public static List<Rectangle> listClimbLeft = new List<Rectangle>();
        //public static List<Rectangle> listClimbTop = new List<Rectangle>();
        //public static List<Rectangle> listClimbBottom = new List<Rectangle>();

        public static GAMESTATE gameState = GAMESTATE.START;

        public static List<Oggetto> ostacoli = new List<Oggetto>();
    }
}
