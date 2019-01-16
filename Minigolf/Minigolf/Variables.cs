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
        public static GAMESTATE gameState = GAMESTATE.STARTGAME;
        public static ProgressBar bar;

        #region Mappe livelli
        public static List<char[,]> gridMaps = new List<char[,]>();
        public static Vector2[] startPosition = new Vector2[C.MAXLEVEL + 1];
        public static Vector2[] endPosition = new Vector2[C.MAXLEVEL + 1];
        public static Rectangle[] endPositionRect = new Rectangle[C.MAXLEVEL + 1];
        public static List<Sprite> listSpriteLevel = new List<Sprite>();
        public static int level = 0;
        public static List<Rectangle> listTrack = new List<Rectangle>();
        #endregion

        #region Button
        public static Button playButton, continueButton, restartButton;
        #endregion

        public static int countHit = 0;
        public static int[] hit = new int[C.MAXLEVEL + 1];

        public static bool flagForSound = false;

        public static int selectedPlayer = 0; // 0 for Wario e -1 for Waluigi
    }
}
