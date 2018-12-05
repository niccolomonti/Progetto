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
    public class BoardGolf
    {
        Rectangle dimBoard;
        int[,] level = new int[2, 10];

        public BoardGolf()
        {
            dimBoard = new Rectangle(0, 0, (level.GetUpperBound(1) + 1) * 40, (level.GetUpperBound(0) + 1) * 40);
            dimBoard.Location = new Point((C.MAINWINDOW.X - dimBoard.Width) / 2, (C.MAINWINDOW.Y - dimBoard.Height) / 2);
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(C.TEXTURESTART, dimBoard, Color.White);
        }
    }
}
