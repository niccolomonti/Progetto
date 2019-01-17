using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf
{
    public class Button
    {
        protected Texture2D[] arrayTexture;
        protected Texture2D usingTexture;
        protected Rectangle rectangle;
        protected MouseState oldMouseState;
        protected GamePadState oldGamePadState;
        protected KeyboardState oldKeyboardState;

        public Button(Texture2D[] arrayTexture, int LocationX, int LocationY)
        {
            this.arrayTexture = arrayTexture;
            rectangle = new Rectangle(Point.Zero, C.DIMBUTTON);            
            rectangle.Location = new Point(LocationX - rectangle.Width / 2, LocationY);
            usingTexture = arrayTexture[0];
        }

        public bool Update()
        {
            usingTexture = arrayTexture[0];

            MouseState newMouseState = Mouse.GetState();
            Rectangle posMouse = new Rectangle(newMouseState.X, newMouseState.Y, 1, 1);

            GamePadState newGamePadState = GamePad.GetState(PlayerIndex.One);

            KeyboardState newKeyboardState = Keyboard.GetState();

            if (posMouse.Intersects(rectangle))
            {
                if ((newMouseState.LeftButton == ButtonState.Pressed) && (oldMouseState.LeftButton == ButtonState.Released))
                    usingTexture = arrayTexture[2];
                if (newMouseState.LeftButton == ButtonState.Pressed)
                    usingTexture = arrayTexture[2];
                if (newMouseState.LeftButton == ButtonState.Released)
                    usingTexture = arrayTexture[1];
                if ((newMouseState.LeftButton == ButtonState.Released) && (oldMouseState.LeftButton == ButtonState.Pressed))
                {
                    usingTexture = arrayTexture[1];
                    oldMouseState = newMouseState;
                    C.buttonSound.Play();
                    return true;
                }
                oldMouseState = newMouseState;
            }
            else
            {
                if (newGamePadState.IsButtonDown(Buttons.A))                
                    usingTexture = arrayTexture[2];                
                if(newGamePadState.IsButtonUp(Buttons.A) && oldGamePadState.IsButtonDown(Buttons.A))
                {
                    usingTexture = arrayTexture[1];
                    oldGamePadState = newGamePadState;
                    C.buttonSound.Play();
                    return true;
                }

                oldGamePadState = newGamePadState;
            }

            if (newKeyboardState.IsKeyDown(Keys.Enter))
                usingTexture = arrayTexture[2];
            if(newKeyboardState.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                usingTexture = arrayTexture[1];
                oldKeyboardState = newKeyboardState;
                C.buttonSound.Play();
                return true;
            }

            oldKeyboardState = newKeyboardState;

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(usingTexture, rectangle, Color.White);
        }        
    }
}
