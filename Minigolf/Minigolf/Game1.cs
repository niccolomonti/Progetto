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
        Ball ball;

        private List<Sprite> sprites;

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

            H.ReadFile();

            C.TEXTURESILVER = H.CreateTexture(GraphicsDevice, C.PIXELSXPOINT.X, C.PIXELSXPOINT.Y, Color.Silver);
            C.TEXTURELINE = H.CreateTexture(GraphicsDevice, 1, 1, Color.Black);
            C.TEXTUREBALL = Content.Load<Texture2D>("0");
            C.TEXTUREHOLE = Content.Load<Texture2D>("end");            

            ball = new Ball(C.TEXTUREBALL, spriteBatch);

            var animations = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 5) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 5) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 5) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 5) }
            };

            sprites = new List<Sprite>() // non so se ci servirà una lista...
            {
                new Sprite(animations)
                {
                    Position = V.startPosition[V.level],
                    input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                        Left = Keys.Left,
                        Right = Keys.Right
                    }
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

            ball.Update();

            foreach (var sprite in sprites)
                sprite.Update(gameTime);

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

            //controllo start program
            //----------

            if (V.level <= C.MAXLEVEL)
            {
                H.DrawMap(spriteBatch, V.level);
                ball.Draw();

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
