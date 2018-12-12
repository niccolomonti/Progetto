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
    public static class H //Helpers
    {
        #region Helpers Costruttivi
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            texture.SetData(data);

            return texture;
        }
        public static float Norme(Vector2 v)
        {
            return (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
        }

        public static Point Invert(Point p)
        {
            return new Point(p.Y, p.X);
        }

        public static Vector2 Invert(Vector2 v)
        {
            return new Vector2(v.Y, v.X);
        }

        public static Rectangle RotateRectangle(Rectangle a)
        {
            return new Rectangle(a.X - (a.Height / 2 - a.Width / 2), a.Y + (a.Height / 2 - a.Width / 2), a.Height, a.Width);
        }

        public static bool Intersect(Point p, Rectangle rect)
        {
            Rectangle pRect = new Rectangle(p.X, p.Y, 1, 1);
            if (pRect.Intersects(rect))
                return true;
            return false;
        }
        #endregion

        #region ReadFile chiamato una sola volta
        public static void ReadFile()
        {
            // Ciclo for per ciascun file.txt (livello)
            for (int k = 0; k <= C.MAXLEVEL; k++)
            {
                string data = System.IO.File.ReadAllText(C.FILENAME[k]);
                string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                char[,] map = new char[lines.Length, lines[0].Length];

                // Ciclo for per ciascuna riga del file.txt
                for (int i = 0; i < lines.Length; i++)
                {
                    char[] line = lines[i].ToCharArray();
                    for (int j = 0; j < line.Length; j++)
                    {
                        map[i, j] = line[j];

                        switch(line[j])
                        {
                            case 'i':
                                V.startPosition[k] = new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2);
                                break;
                            case 'f':
                                V.endPosition[k] = new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2);
                                V.endPositionRect[k] = new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT);
                                break;
                            default:
                                break;
                        }
                    }
                }
                V.gridMaps.Add(map);
            }
        }
        #endregion

        #region Creazione muri e ostacoli chiamato ogni Update
        public static void CreateOstacoli(int level)
        {
            V.listSpriteLevel.Clear();

            for (int i = 0; i < C.MAINGRID.Y - 1; i++)
            {
                for (int j = 0; j < C.MAINGRID.X - 1; j++)
                {
                    switch (V.gridMaps[level][i, j])
                    {
                        case '1':
                            V.listSpriteLevel.Add(new Wall(C.TEXTUREWALL, new Vector2(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y)));
                            break;
                        case 's':
                            V.listSpriteLevel.Add(new Sand(C.TEXTURESAND, new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                        case 'b':
                            V.listSpriteLevel.Add(new Climb(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2), 0));
                            break;
                        case 'l':
                            V.listSpriteLevel.Add(new Climb(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2), 1));
                            break;
                        case 't':
                            V.listSpriteLevel.Add(new Climb(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2), 2));
                            break;
                        case 'r':
                            V.listSpriteLevel.Add(new Climb(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2), 3));
                            break;
                    }
                }
            }
        }
        #endregion

        #region DrawLine DrawArrow
        public static void DrawLine(SpriteBatch sprite, Texture2D texture, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            sprite.Draw(texture,
                new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 3),
                null,
                Color.White,
                angle, 
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }

        public static void DrawArrow()
        {

        }
        #endregion

        //public static void DrawMap(SpriteBatch sprite, int level)
        //{
        //    V.listRectWall.Clear();

        //    for (int i = 0; i < C.MAINGRID.Y - 1; i++)
        //    {
        //        for (int j = 0; j < C.MAINGRID.X - 1; j++)
        //        {
        //            switch (V.gridMaps[level][i, j])
        //            {
        //                case '1':
        //                    V.listRectWall.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT));
        //                    //spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(C.TEXTUREBALL.Width / 2, C.TEXTUREBALL.Height / 2), (2 * radius / C.TEXTUREBALL.Width), SpriteEffects.None, 1);
        //                    sprite.Draw(C.TEXTUREWALL, destinationRectangle: new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), sourceRectangle: C.TEXTUREWALL.Bounds,color: Color.White, layerDepth: 1);
        //                    break;
        //                case 'E':
        //                    sprite.Draw(C.TEXTUREHOLE, destinationRectangle: new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), sourceRectangle: C.TEXTUREHOLE.Bounds, color: Color.White, layerDepth: 1);
        //                    break;
        //            }
        //        }
        //    }
        //}

    }
}
