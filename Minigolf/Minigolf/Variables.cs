using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Sprites;

namespace Minigolf
{
    public enum GAMESTATE { STARTGAME, START, HITBALL, GOTOBALL, PLAY, LEVELCOMPLETE, ENDGAME };
                                            
    public static class V //Variables
    { 
        public static Random random = new Random();

        public static int level = 0;

        public static List<char[,]> gridMaps = new List<char[,]>();
        public static List<Rectangle> listRectWall = new List<Rectangle>();

        public static Vector2[] startPosition = new Vector2[C.MAXLEVEL + 1];
        public static Vector2[] endPosition = new Vector2[C.MAXLEVEL + 1];
        public static Rectangle[] endPositionRect = new Rectangle[C.MAXLEVEL + 1];

        public static GAMESTATE gameState = GAMESTATE.STARTGAME;

        public static List<Sprite> listSpriteLevel = new List<Sprite>();

        public static Button playButton, continueButton;
    }
}
