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
    public static class H //Helpers
    {
        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(device, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            texture.SetData(data);

            return texture;
        }

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
                            case 'S':
                                V.startPosition[k] = new Vector2(j * C.PIXELSXPOINT.X + C.PIXELSXPOINT.X / 2, i * C.PIXELSXPOINT.Y + C.PIXELSXPOINT.Y / 2);
                                break;
                            case 'E':
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

        public static void DrawMap(SpriteBatch sprite, int level)
        {
            V.listRectWall.Clear();

            for (int i = 0; i < C.MAINGRID.Y - 1; i++)
            {
                for (int j = 0; j < C.MAINGRID.X - 1; j++)
                {
                    switch (V.gridMaps[level][i, j])
                    {
                        case '1':
                            V.listRectWall.Add(new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT));
                            sprite.Draw(C.TEXTURESILVER, new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), C.TEXTURESILVER.Bounds, Color.White);
                            break;
                        case 'E':
                            sprite.Draw(C.TEXTUREHOLE, new Rectangle(new Point(j, i) * C.PIXELSXPOINT, C.PIXELSXPOINT), C.TEXTUREHOLE.Bounds, Color.White);
                            break;
                    }
                }
            }
        }

        public static float Norme(Vector2 v)
        {
            return (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
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
    }
}
