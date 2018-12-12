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
            spriteFont = Content.Load<SpriteFont>("font");

            #region Load Texture
            C.TEXTUREWALL = Content.Load<Texture2D>("Wall");
            C.TEXTURESAND = Content.Load<Texture2D>("Sand");
            C.TEXTURECLIMB = Content.Load<Texture2D>("Climb");
            C.TEXTUREARROW = H.CreateTexture(GraphicsDevice, 1, 1, Color.Black);
            C.TEXTUREBALL = Content.Load<Texture2D>("Ball");
            C.TEXTURESTART = Content.Load<Texture2D>("Start");
            C.TEXTUREHOLE = Content.Load<Texture2D>("Hole");
            C.TEXTUREINHOLE = Content.Load<Texture2D>("Hole");
            C.TEXTUREPLAY[0] = Content.Load<Texture2D>("Button/Play");
            C.TEXTUREPLAY[1] = Content.Load<Texture2D>("Button/PlayOver");
            C.TEXTUREPLAY[2] = Content.Load<Texture2D>("Button/PlayPress");
            C.TEXTURECONTINUE[0] = Content.Load<Texture2D>("Button/Continue");
            C.TEXTURECONTINUE[1] = Content.Load<Texture2D>("Button/ContinueOver");
            C.TEXTURECONTINUE[2] = Content.Load<Texture2D>("Button/ContinuePress");
            #endregion

            #region Load Animations
            var playerAnimations = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 5) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 5) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 5) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 5) },
                { "WalkDiagLeftDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftDown"), 5) },
                { "WalkDiagRightDown", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightDown"), 5) },
                { "WalkDiagLeftUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalLeftUp"), 5) },
                { "WalkDiagRightUp", new Animation(Content.Load<Texture2D>("Player/WalkingDiagonalRightUp"), 5) }
            };

            // sarà da aggiungere ( e modificare, ovviamente) quando inseriremo l'animazione per la pallina
            //var ballAnimations = new Dictionary<string, Animation>()
            //{
            //    { "MotionLess", new Animation(Content.Load<Texture2D>("0"), 1) }
            //};
            #endregion

            #region Load Sprites
            sprites = new List<Sprite>()
            {
                new Player(playerAnimations)
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

            #region Button play e continue
            V.playButton = new Button(C.TEXTUREPLAY);
            V.continueButton = new Button(C.TEXTURECONTINUE);
            #endregion

            H.ReadFile();
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

            foreach (var sprite in sprites)
                sprite.Update(gameTime);

            switch (V.gameState)
            {
                case GAMESTATE.STARTGAME:
                    if (V.playButton.Update())
                        V.gameState = GAMESTATE.START;
                    break;
                case GAMESTATE.START:
                    H.CreateOstacoli(V.level);
                    V.gameState = GAMESTATE.GOTOBALL;
                    break;
                case GAMESTATE.HITBALL:
                    if (Vector2.Distance(sprites[0].Position, sprites[1].Position) > C.GETBALLDISTANCE)
                        V.gameState = GAMESTATE.GOTOBALL;
                    if (sprites[1].selected)
                        V.gameState = GAMESTATE.PLAY;
                    break;
                case GAMESTATE.PLAY:
                    if (H.Intersect(sprites[1].Position.ToPoint(), V.endPositionRect[V.level]))
                    {
                        float d = Vector2.Distance(sprites[1].Position, V.endPosition[V.level]);
                        if (d > 0.05f)
                        {
                            sprites[1].velocity = Vector2.Normalize(V.endPosition[V.level] - sprites[1].Position) * 0.1f;
                        }
                        else
                        {
                            if (++V.level <= C.MAXLEVEL)
                                V.gameState = GAMESTATE.START;
                            else
                                V.gameState = GAMESTATE.ENDGAME;
                        }
                    }
                    else
                    {
                        if (sprites[1].velocity == Vector2.Zero && !sprites[1].selected)                        
                            V.gameState = GAMESTATE.GOTOBALL;
                    }
                    break;
                case GAMESTATE.GOTOBALL:
                    if(Vector2.Distance(sprites[0].Position, sprites[1].Position) < C.GETBALLDISTANCE)
                        V.gameState = GAMESTATE.HITBALL;
                    break;
                case GAMESTATE.LEVELCOMPLETE:
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
            spriteBatch.DrawString(spriteFont, V.gameState.ToString(), new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(spriteFont, Vector2.Distance(sprites[0].Position, sprites[1].Position).ToString(), new Vector2(0, 20), Color.White);
            //spriteBatch.DrawString(spriteFont, (sprites[0].Position).ToString(), new Vector2(0, 40), Color.White);
            //spriteBatch.DrawString(spriteFont, (sprites[1].Position).ToString(), new Vector2(0, 60), Color.White);

            ////controllo start program
            ////----------
            //if (V.level <= C.MAXLEVEL)
            //{
            //    H.DrawMap(spriteBatch, V.level);

            //    foreach (var sprite in sprites)
            //        sprite.Draw(spriteBatch);               
            //}
            //else
            //{
            //    //end program
            //}
            if (V.gameState == GAMESTATE.STARTGAME)
            {
                spriteBatch.Draw(C.TEXTURESTART, new Rectangle(0, 0, C.MAINWINDOW.X, C.MAINWINDOW.Y), Color.White);
                V.playButton.Draw(spriteBatch);
            }
            else if (V.gameState == GAMESTATE.LEVELCOMPLETE)
            {

            }
            else if (V.gameState == GAMESTATE.ENDGAME)
            {

            }
            else
            {
                spriteBatch.Draw(C.TEXTUREHOLE, V.endPositionRect[V.level], null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                foreach (var sprite in V.listSpriteLevel)
                    sprite.Draw(spriteBatch);
                foreach (var sprite in sprites)
                    sprite.Draw(spriteBatch);
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
