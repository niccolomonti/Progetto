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
        protected MouseState oldState;

        public Button(Texture2D[] arrayTexture)
        {
            this.arrayTexture = arrayTexture;
            rectangle = new Rectangle(Point.Zero, C.DIMBUTTON);
            rectangle.Location = new Point(C.MAINWINDOW.X / 2 - rectangle.Width / 2, 4 * C.MAINWINDOW.Y / 5);
            usingTexture = arrayTexture[0];
        }

        public bool Update()
        {
            usingTexture = arrayTexture[0];

            MouseState newState = Mouse.GetState();
            Rectangle posMouse = new Rectangle(newState.X, newState.Y, 1, 1);

            if (posMouse.Intersects(rectangle))
            {
                if ((newState.LeftButton == ButtonState.Pressed) && (oldState.LeftButton == ButtonState.Released))
                    usingTexture = arrayTexture[2];
                if (newState.LeftButton == ButtonState.Pressed)
                    usingTexture = arrayTexture[2];
                if (newState.LeftButton == ButtonState.Released)
                    usingTexture = arrayTexture[1];
                if ((newState.LeftButton == ButtonState.Released) && (oldState.LeftButton == ButtonState.Pressed))
                {
                    usingTexture = arrayTexture[1];
                    oldState = newState;
                    return true;
                }
                oldState = newState;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(usingTexture, rectangle, Color.White);
        }
    }
}
