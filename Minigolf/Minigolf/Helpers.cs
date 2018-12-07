using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Minigolf.Ostacoli;

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

        public static bool PerPixelCollision(Oggetto a, Oggetto b)
        {
            // Get Color data of each Texture
            Color[] bitsA = new Color[a.Texture.Width * a.Texture.Height];
            a.Texture.GetData(bitsA);
            Color[] bitsB = new Color[b.Texture.Width * b.Texture.Height];
            b.Texture.GetData(bitsB);

            // Calculate the intersecting rectangle
            int top = Math.Max(a.Bounds.Top, b.Bounds.Top);
            int bottom = Math.Min(a.Bounds.Bottom , b.Bounds.Bottom);

            int left = Math.Max(a.Bounds.Left, b.Bounds.Left);
            int right = Math.Min(a.Bounds.Right, b.Bounds.Right);

            // For each single pixel in the intersecting rectangle
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color from each texture
                    Color colorA = bitsA[(x - a.Bounds.Left) + (y - a.Bounds.Top) * a.Bounds.Width];
                    Color colorB = bitsB[(x - b.Bounds.Left) + (y - b.Bounds.Top) * b.Bounds.Width];

                    if (colorA.A != 0 && colorB.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }

        public static Rectangle RotateRectangle(Rectangle a)
        {
            return new Rectangle(a.X - (a.Height / 2 - a.Width / 2), a.Y + (a.Height / 2 - a.Width / 2), a.Height, a.Width);
        }
        #endregion

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
                                //V.endPositionRect[k] = new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT);
                                break;
                        }
                    }
                }
                V.gridMaps.Add(map);
            }
        }

        public static void CreateOstacoli(int level)
        {
            for (int i = 0; i < C.MAINGRID.Y - 1; i++)
            {
                for (int j = 0; j < C.MAINGRID.X - 1; j++)
                {
                    switch (V.gridMaps[level][i, j])
                    {
                        case '1':
                            V.ostacoli.Add(new Wall(new Point(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y)));
                            break;
                        case 's':
                            V.ostacoli.Add(new Sand(new Point(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                        case 'b':
                            V.ostacoli.Add(new Climb(0, new Point(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                        case 'l':
                            V.ostacoli.Add(new Climb(1, new Point(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                        case 't':
                            V.ostacoli.Add(new Climb(2, new Point(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                        case 'r':
                            V.ostacoli.Add(new Climb(3, new Point(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2)));
                            break;
                    }
                }
            }
        }

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

        //public static void DrawMap(SpriteBatch sprite, int level)
        //{
        //    //V.listRectWall.Clear();
        //    //V.listRectSand.Clear();
        //    //V.listClimbRight.Clear();
        //    //V.listClimbLeft.Clear();
        //    //V.listClimbTop.Clear();
        //    //V.listClimbBottom.Clear();

        //    for (int i = 0; i < C.MAINGRID.Y - 1; i++)
        //    {
        //        for (int j = 0; j < C.MAINGRID.X - 1; j++)
        //        {
        //            switch (V.gridMaps[level][i, j])
        //            {
        //                //case '1':
        //                //    V.listRectWall.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT));
        //                //    sprite.Draw(C.TEXTURESILVER, new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), C.TEXTURESILVER.Bounds, Color.White);
        //                //    break;
        //                //case 'E': //fine
        //                //    sprite.Draw(C.TEXTUREHOLE, new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), C.TEXTUREHOLE.Bounds, Color.White);
        //                //    break;
        //                //case 'O': //sabbia
        //                //    V.listRectSand.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXSAND));
        //                //    sprite.Draw(C.TEXTURESAND, V.listRectSand.Last(), C.TEXTURESAND.Bounds, Color.White);
        //                //    break;
        //                //case 'R': //salita verso destra
        //                //    V.listClimbRight.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, Invert(C.PIXELSXCLIMB)));
        //                //    sprite.Draw(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y), null, Color.White, -(float)Math.PI / 2, new Vector2(C.PIXELSXCLIMB.X, 0), 1, SpriteEffects.None, 0);
        //                //    break;
        //                //case 'L': //salita verso sinistra
        //                //    V.listClimbLeft.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, Invert(C.PIXELSXCLIMB)));
        //                //    sprite.Draw(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y), null, Color.White, (float)Math.PI / 2, new Vector2(0, C.PIXELSXCLIMB.Y), 1, SpriteEffects.None, 0);
        //                //    break;
        //                //case 'T': //salita verso alto
        //                //    V.listClimbTop.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXCLIMB));
        //                //    sprite.Draw(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y), null, Color.White, (float)Math.PI, new Vector2(C.PIXELSXCLIMB.X, C.PIXELSXCLIMB.Y), 1, SpriteEffects.None, 0);
        //                //    break;
        //                //case 'B': // salita verso basso
        //                //    V.listClimbBottom.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXCLIMB));
        //                //    sprite.Draw(C.TEXTURECLIMB, new Vector2(j * C.PIXELSXPOINT.X, i * C.PIXELSXPOINT.Y), null, Color.White, 0, new Vector2(), 1, SpriteEffects.None, 0);
        //                //    break;
        //            }
        //        }
        //    }
        //}
    }
}
