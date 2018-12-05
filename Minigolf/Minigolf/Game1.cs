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
using Minigolf;

namespace Minigolf
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        SpriteFont font;

        
        /*private*/public List<Sprite> sprites;

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

            font = Content.Load<SpriteFont>("font");

            H.ReadFile();

            C.TEXTURESILVER = H.CreateTexture(GraphicsDevice, C.PIXELSXPOINT.X, C.PIXELSXPOINT.Y, Color.Silver);
            C.TEXTURELINE = H.CreateTexture(GraphicsDevice, 1, 1, Color.Black);
            C.TEXTUREBALL = Content.Load<Texture2D>("0");
            C.TEXTUREHOLE = Content.Load<Texture2D>("end");
            
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
                case GAMESTATE.START:
                    V.gameState = GAMESTATE.HITBALL;
                    break;
                case GAMESTATE.HITBALL:
                    if (sprites[1].selected)
                        V.gameState = GAMESTATE.PLAY;
                    break;
                case GAMESTATE.PLAY:
                    if (sprites[1].rectangle.Intersects(V.endPositionRect[V.level]))
                    {
                        if (++V.level <= C.MAXLEVEL)
                        {
                            V.gameState = GAMESTATE.START;
                        }
                        else
                        {
                            V.gameState = GAMESTATE.END;
                        }
                    }
                    else
                    {
                        if (sprites[1].velocity == Vector2.Zero && !sprites[1].selected)
                        {
                            //sprites[1].selected = false;
                            V.gameState = GAMESTATE.GOTOBALL;
                        }
                    }
                    break;
                case GAMESTATE.GOTOBALL:
                    //if (Vector2.Distance(new Vector2((sprites[0].position.X + sprites[0].texture.Width)/2, (sprites[0].position.Y + sprites[0].texture.Height) / 2),
                    //                     new Vector2((sprites[1].position.X + sprites[1].texture.Width) / 2, (sprites[1].position.Y + sprites[1].texture.Height) / 2)) < C.GETBALLDISTANCE)
                    if(Vector2.Distance(sprites[0].Position, sprites[1].Position) < C.GETBALLDISTANCE)
                        V.gameState = GAMESTATE.HITBALL;
                    break;
                case GAMESTATE.END:
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

            spriteBatch.Begin();
            spriteBatch.DrawString(font, V.gameState.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, Vector2.Distance(sprites[0].Position, sprites[1].Position).ToString(), new Vector2(0, 20), Color.White);


            //controllo start program
            //----------

            if (V.level <= C.MAXLEVEL)
            {
                H.DrawMap(spriteBatch, V.level);                

                foreach (var sprite in sprites)
                    sprite.Draw(spriteBatch);
            }
            else
            {
                //end program
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
