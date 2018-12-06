using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Point mousePosition;
        SpriteFont font;
        MouseState mouseState;
        BoardGolf board;

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
            board = new BoardGolf();

            H.ReadFile();

            C.TEXTURESILVER = H.CreateTexture(GraphicsDevice, C.PIXELSXPOINT.X, C.PIXELSXPOINT.Y, Color.Silver);
            C.TEXTURELINE = H.CreateTexture(GraphicsDevice, 1, 1, Color.Black);
            C.TEXTUREBALL = Content.Load<Texture2D>("0");
            C.TEXTUREHOLE = Content.Load<Texture2D>("end");
            C.TEXTURESAND = H.CreateTexture(GraphicsDevice, C.PIXELSXSAND.X, C.PIXELSXSAND.Y, Color.Beige);
            C.TEXTURECLIMB = Content.Load<Texture2D>("freccia");

            ball = new Ball(C.TEXTUREBALL, spriteBatch);
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

            ball.Update();
            mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;

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
            spriteBatch.DrawString(font, "Mouse: " + mousePosition.X + "x" + mousePosition.Y, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Speed: " + H.Norme(ball.Speed), new Vector2(0, 15), Color.White);

            //controllo start program
            //----------

            if (V.level <= C.MAXLEVEL)
            {
                H.DrawMap(spriteBatch, V.level);
                ball.Draw();
            }
            else
            {
                //end program
            }
            board.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
