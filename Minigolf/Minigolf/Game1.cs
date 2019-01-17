using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minigolf.Sprites;
using Minigolf.Models;
using Microsoft.Xna.Framework.Audio;

namespace Minigolf
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        List<Sprite> sprites;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = C.MAINWINDOW.X;
            graphics.PreferredBackBufferHeight = C.MAINWINDOW.Y;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("scoreFont");

            #region Load Texture
            C.TEXTUREBACKGROUND = Content.Load<Texture2D>("Background");
            C.TEXTURESTARTGAME = Content.Load<Texture2D>("Start");
            C.TEXTURESCORE = Content.Load<Texture2D>("Tabellone");
            C.TEXTURETRACK = H.CreateTexture(GraphicsDevice, 15, 15, Color.Coral); //Content.Load<Texture2D>("Track");
            C.TEXTUREWALL = Content.Load<Texture2D>("Wall");
            C.TEXTURESAND = Content.Load<Texture2D>("Sand");
            C.TEXTURECLIMB = Content.Load<Texture2D>("Climb");
            C.TEXTUREARROW = Content.Load<Texture2D>("Arrow");
            C.TEXTUREBALL = Content.Load<Texture2D>("Ball");
            C.TEXTUREHOLE = Content.Load<Texture2D>("Hole");
            C.TEXTUREPLAY[0] = Content.Load<Texture2D>("Button/Play");
            C.TEXTUREPLAY[1] = Content.Load<Texture2D>("Button/PlayOver");
            C.TEXTUREPLAY[2] = Content.Load<Texture2D>("Button/PlayPress");
            C.TEXTURECONTINUE[0] = Content.Load<Texture2D>("Button/Continue");
            C.TEXTURECONTINUE[1] = Content.Load<Texture2D>("Button/ContinueOver");
            C.TEXTURECONTINUE[2] = Content.Load<Texture2D>("Button/ContinuePress");
            C.TEXTURERESTART[0] = Content.Load<Texture2D>("Button/Restart");
            C.TEXTURERESTART[1] = Content.Load<Texture2D>("Button/RestartOver");
            C.TEXTURERESTART[2] = Content.Load<Texture2D>("Button/RestartPress");
            C.TEXTUREBACKBAR = H.CreateBorderTexture(GraphicsDevice, C.DIMBAR.X, C.DIMBAR.Y, C.BORDERBAR, Color.Black);
            C.TEXTUREBAR = H.CreateTexture(GraphicsDevice, 1, 1, Color.Green);
            C.TEXTUREPLAYER1 = Content.Load<Texture2D>("player_Wario");
            C.TEXTUREPLAYER2 = Content.Load<Texture2D>("player_Waluigi");
            C.TEXTURESELECTEDRECT = Content.Load<Texture2D>("selectedRect");
            C.TEXTURENOTSELECTEDRECT = Content.Load<Texture2D>("selectedNotRect");
            C.TEXTURESELECTEDSQUARE = Content.Load<Texture2D>("selectedSquare");
            C.TEXTURENOTSELECTEDSQUARE = Content.Load<Texture2D>("selectedNotSquare");
            #endregion

            #region Load Animations

            var playerAnimations_Waluigi = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp_Waluigi"), 5) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown_Waluigi"), 5) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft_Waluigi"), 5) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight_Waluigi"), 5) },
                { "WalkDiagLeftDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftDown_Waluigi"), 5) },
                { "WalkDiagRightDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightDown_Waluigi"), 5) },
                { "WalkDiagLeftUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftUp_Waluigi"), 5) },
                { "WalkDiagRightUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightUp_Waluigi"), 5) }
            };

            var playerAnimations_Wario = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp_Wario"), 6) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown_Wario"), 6) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft_Wario"), 6) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight_Wario"), 6) },
                { "WalkDiagLeftDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftDown_Wario"), 6) },
                { "WalkDiagRightDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightDown_Wario"), 6) },
                { "WalkDiagLeftUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftUp_Wario"), 6) },
                { "WalkDiagRightUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightUp_Wario"), 6) }
            };
            // sarà da aggiungere ( e modificare, ovviamente) quando inseriremo l'animazione per la pallina
            //var ballAnimations = new Dictionary<string, Animation>()
            //{
            //    { "MotionLess", new Animation(Content.Load<Texture2D>("0"), 1) }
            //};
            #endregion


            #region Load Sprites
            List<Dictionary<string, Animation>> playerAnimations = new List<Dictionary<string, Animation>>();

            playerAnimations.Add(playerAnimations_Wario);
            playerAnimations.Add(playerAnimations_Waluigi);

            sprites = new List<Sprite>()
            {
                new Player(playerAnimations[0])
                {
                    Position = V.startPosition[V.level],
                    input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right
                    }
                },

                new Player(playerAnimations[1])
                {
                    Position = V.startPosition[V.level],
                    input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right
                    }
                },

                new Ball(C.TEXTUREBALL)
                {
                    Position = V.startPosition[V.level],

                    // --- da controllare, non ha molto senso ... ---
                    input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right
                    }
                    // ------------------------
                }
            };
            #endregion

            #region Load Sounds
            C.buttonSound = Content.Load<SoundEffect>(@"Sound/Arcade_S-wwwbeat-8526_hifi");
            C.golfShot = Content.Load<SoundEffect>(@"Sound/Golf_sho-Public_d-191_hifi");
            C.ballHitWall = Content.Load<SoundEffect>(@"Sound/Snooker_-public_d-274_hifi");
            C.ballInHole = Content.Load<SoundEffect>(@"Sound/Hole_in_-Public_d-189_hifi");
            C.applause = Content.Load<SoundEffect>(@"Sound/applause-5");
            C.boo = Content.Load<SoundEffect>(@"Sound/Boocrap-Tucker_J-7897_hifi");
            #endregion

            #region Button play e continue
            V.playButton = new Button(C.TEXTUREPLAY, C.MAINWINDOW.X / 2, 4 * C.MAINWINDOW.Y / 5);
            V.continueButton = new Button(C.TEXTURECONTINUE, C.MAINWINDOW.X / 2, 4 * C.MAINWINDOW.Y / 5);
            V.restartButton = new Button(C.TEXTURERESTART, C.MAINWINDOW.X / 2, 4 * C.MAINWINDOW.Y / 5 - C.MAINWINDOW.Y / 7);
            #endregion

            H.ReadFile();

            V.bar = new ProgressBar(C.TEXTUREBACKBAR, C.TEXTUREBAR, C.POSBAR);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //foreach (var sprite in sprites)
            //    sprite.Update(gameTime);
            for (int i = 0; i < 3; i++)
                if (i == V.selectedPlayer || i == 2 ) // per escludere il personaggio non selezionato
                    sprites[i].Update(gameTime);
            
            switch (V.gameState)
            {
                case GAMESTATE.STARTGAME:
                    if (V.playButton.Update())
                    {
                        V.gameState = GAMESTATE.START;
                        //sprites.RemoveAt(V.selectedPlayer + 1);
                    }
                    else
                    {
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.4f
                            || Keyboard.GetState().IsKeyDown(Keys.Right))                        
                            V.selectedPlayer = 1;                          
                        
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.4f
                        || Keyboard.GetState().IsKeyDown(Keys.Left))                        
                            V.selectedPlayer = 0;
                           
                    }
                    break;
                case GAMESTATE.START:
                    H.CreateOstacoli(V.level);
                    V.countHit = 0;
                    V.gameState = GAMESTATE.GOTOBALL;
                    break;
                case GAMESTATE.HITBALL:
                    if (Vector2.Distance(sprites[V.selectedPlayer].Position, sprites[2].Position) > C.GETBALLDISTANCE)
                        V.gameState = GAMESTATE.GOTOBALL;
                    if (sprites[2].selected)
                        V.gameState = GAMESTATE.PLAY;
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start)
                        || Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        V.previousGameState = V.gameState;
                        V.selectedButton = 'c';
                        V.gameState = GAMESTATE.PAUSE;
                    }
                    break;
                case GAMESTATE.PLAY:                    
                    if (H.Intersect(sprites[2].Position.ToPoint(), V.endPositionRect[V.level]))
                    {
                        float d = Vector2.Distance(sprites[2].Position, V.endPosition[V.level]);
                        if (d > 0.05f)
                        {
                            sprites[2].velocity = Vector2.Normalize(V.endPosition[V.level] - sprites[2].Position) * 0.1f;
                            if (!V.flagForSound)
                            {
                                C.ballInHole.Play();
                                V.flagForSound = true;
                            }
                        }
                        else
                        {
                            V.flagForSound = false;
                            if (V.countHit <= C.PAR[V.level])
                                C.applause.Play();
                            else
                                C.boo.Play();
                            V.hit[V.level] = V.countHit;                            
                            if (++V.level <= C.MAXLEVEL)
                                V.gameState = GAMESTATE.LEVELCOMPLETE;
                            else
                                V.gameState = GAMESTATE.ENDGAME;
                        }
                    }
                    else
                    {
                        if (sprites[2].velocity == Vector2.Zero && !sprites[2].selected)
                            V.gameState = GAMESTATE.GOTOBALL;
                    }
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start)
                        || Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        V.previousGameState = V.gameState;
                        V.selectedButton = 'c';
                        V.gameState = GAMESTATE.PAUSE;
                    }
                    break;
                case GAMESTATE.GOTOBALL:
                    if(Vector2.Distance(sprites[V.selectedPlayer].Position, sprites[2].Position) < C.GETBALLDISTANCE)
                        V.gameState = GAMESTATE.HITBALL;
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start)
                        || Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        V.previousGameState = V.gameState;
                        V.selectedButton = 'c';
                        V.gameState = GAMESTATE.PAUSE;
                    }
                    break;
                case GAMESTATE.LEVELCOMPLETE:
                    if (V.continueButton.Update())
                        V.gameState = GAMESTATE.START;
                    break;
                case GAMESTATE.PAUSE:
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.4f
                        || Keyboard.GetState().IsKeyDown(Keys.Up))                    
                        V.selectedButton = 'r';
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.4f
                        || Keyboard.GetState().IsKeyDown(Keys.Down))
                        V.selectedButton = 'c';

                    if(V.selectedButton == 'r')
                        if (V.restartButton.Update())
                                V.gameState = GAMESTATE.STARTGAME;
                    if(V.selectedButton == 'c')
                        if (V.continueButton.Update())
                            V.gameState = V.previousGameState;

                    break;
                case GAMESTATE.ENDGAME:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //(spriteBatch.DrawString(spriteFont, V.gameState.ToString(), new Vector2(0, 0), Color.White);

            H.Background(spriteBatch);
            
            if (V.gameState == GAMESTATE.STARTGAME)
            {
                spriteBatch.Draw(C.TEXTURESTARTGAME, new Rectangle(Point.Zero, C.MAINWINDOW), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                spriteBatch.Draw(C.TEXTUREPLAYER1, new Rectangle(new Point(C.MAINWINDOW.X * 9 / 30, C.MAINWINDOW.Y * 16 / 30), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 5)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                spriteBatch.Draw(C.TEXTUREPLAYER2, new Rectangle(new Point(C.MAINWINDOW.X * 16 / 30, C.MAINWINDOW.Y * 16 / 30), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 5)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                V.playButton.Draw(spriteBatch);

                if (V.selectedPlayer == 0)
                {
                    spriteBatch.Draw(C.TEXTURESELECTEDRECT, new Rectangle(new Point(C.MAINWINDOW.X * 9 / 30, C.MAINWINDOW.Y * 43 / 60), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 12)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    spriteBatch.Draw(C.TEXTURENOTSELECTEDRECT, new Rectangle(new Point(C.MAINWINDOW.X * 16 / 30, C.MAINWINDOW.Y * 43 / 60), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 12)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                }
                else if (V.selectedPlayer == 1)
                {
                    spriteBatch.Draw(C.TEXTURENOTSELECTEDRECT, new Rectangle(new Point(C.MAINWINDOW.X * 9 / 30, C.MAINWINDOW.Y * 43 / 60), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 12)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    spriteBatch.Draw(C.TEXTURESELECTEDRECT, new Rectangle(new Point(C.MAINWINDOW.X * 16 / 30, C.MAINWINDOW.Y * 43 / 60), new Point(C.MAINWINDOW.X / 6, C.MAINWINDOW.Y / 12)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                }
            }
            else if (V.gameState == GAMESTATE.LEVELCOMPLETE || V.gameState == GAMESTATE.ENDGAME)
            {
                string s;
                int dx = 69;
                int total = 0;
                spriteBatch.DrawString(spriteFont, "LEVEL COMPLETE", new Vector2(400, 150), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.Draw(C.TEXTURESCORE, new Rectangle(60, 200 ,900, 200), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                for (int c = 0; c <= C.MAXLEVEL; c++)
                {
                    spriteBatch.DrawString(spriteFont, C.PAR[c].ToString(), new Vector2(365 + c * dx, 255), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.6f);
                    s = V.hit[c] == 0 ? "-" : V.hit[c].ToString();
                    spriteBatch.DrawString(spriteFont, s, new Vector2(365 + c * dx, 305), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.6f);
                    s = V.hit[c] == 0 ? "" : (V.hit[c] - C.PAR[c]).ToString();
                    spriteBatch.DrawString(spriteFont, s, new Vector2(365 + c * dx, 355), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.6f);
                    total += V.hit[c] != 0 ? V.hit[c] - C.PAR[c] : 0;
                }
                spriteBatch.DrawString(spriteFont, total.ToString(), new Vector2(800, 355), Color.Red, 0 ,Vector2.Zero, 1, SpriteEffects.None, 0.6f);

                if (V.gameState == GAMESTATE.LEVELCOMPLETE)
                    V.continueButton.Draw(spriteBatch);
            }
            else if (V.gameState == GAMESTATE.PAUSE)
            {
                V.restartButton.Draw(spriteBatch);
                V.continueButton.Draw(spriteBatch);
                if (V.selectedButton == 'c')
                {
                    spriteBatch.Draw(C.TEXTURESELECTEDSQUARE, new Rectangle(new Point(C.MAINWINDOW.X / 2 - C.MAINWINDOW.X / 5, C.MAINWINDOW.Y * 81/100), new Point(C.MAINWINDOW.X / 9, C.MAINWINDOW.X / 9)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    spriteBatch.Draw(C.TEXTURENOTSELECTEDSQUARE, new Rectangle(new Point(C.MAINWINDOW.X / 2 - C.MAINWINDOW.X / 5, C.MAINWINDOW.Y * 81/100 - C.MAINWINDOW.Y / 7), new Point(C.MAINWINDOW.X / 9, C.MAINWINDOW.X / 9)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                }                
                else if (V.selectedButton == 'r')
                {
                    spriteBatch.Draw(C.TEXTURENOTSELECTEDSQUARE, new Rectangle(new Point(C.MAINWINDOW.X / 2 - C.MAINWINDOW.X / 5, C.MAINWINDOW.Y * 81/100), new Point(C.MAINWINDOW.X / 9, C.MAINWINDOW.X / 9)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                    spriteBatch.Draw(C.TEXTURESELECTEDSQUARE, new Rectangle(new Point(C.MAINWINDOW.X / 2 - C.MAINWINDOW.X / 5, C.MAINWINDOW.Y * 81/100 - C.MAINWINDOW.Y / 7), new Point(C.MAINWINDOW.X / 9, C.MAINWINDOW.X / 9)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                }
            }
            else
            {
                foreach (var rect in V.listTrack)
                {
                    spriteBatch.Draw(C.TEXTURETRACK, rect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
                }
                spriteBatch.DrawString(spriteFont, "Hits: " + V.countHit.ToString(), new Vector2(25, 25), Color.White);

                spriteBatch.Draw(C.TEXTUREHOLE, V.endPositionRect[V.level], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                foreach (var sprite in V.listSpriteLevel)
                    sprite.Draw(spriteBatch);
                for(int i=0; i<3; i++)
                    if (i == V.selectedPlayer || i == 2) // per escludere il personaggio non selezionato
                        sprites[i].Draw(spriteBatch);
            }

           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
