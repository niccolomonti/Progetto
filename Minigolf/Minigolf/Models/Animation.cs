using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minigolf.Models
{
    public class Animation
    {
        public Texture2D Texture { get; private set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        private bool IsLooping { get; set; }

        public Vector2 Offset { get { return new Vector2(FrameWidth / 2, FrameHeight / 2); } } // il centro della texture

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = true;
            FrameSpeed = 0.05f;
        }
    }
}
