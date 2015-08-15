using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ColorChanger
{
    class Map
    {
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private int clevel;
        public List<string> map;
        private Texture2D tiles;
        private Rectangle colhere;
        private Rectangle sourcerec;
        public Map(Texture2D tex)
        {
            this.batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            map= new List<string>();
            colhere = new Rectangle(0, 0, 32, 32);
            sourcerec = new Rectangle(0, 0, 32, 32);
            this.tiles = tex;
            
        }
        public void setLevel(int level)
        {
            map.Clear();
            clevel = level;
            string strlevel = @"Content/data/maps/level" + level + ".txt";
            using (StreamReader r = new StreamReader(strlevel))
            {
                string line=string.Empty;
                while ((line = r.ReadLine()) != null)
                {
                    map.Add(line);
                }
            }

            int max = 0;
            for (int i = 1; i < map.Count; i++)
            {
                if (map[i].Length > map[max].Length)
                {
                    max = i;
                }
                if (map[i].Trim() == "")
                {
                    map[i] = "";
                }
            }
            
            for (int i = 0; i < map.Count; i++)
            {
                while (map[i].Length < map[max].Length)
                {
                    map[i] += " ";
                }
                map[i] += "f";
            }
            
            for (int i = 0; i < map.Count; i++)
            {
                Console.WriteLine(map[i]);
            }

        }
        public void nextLevel()
        {
            if (clevel + 1 <= Consts.LEVELCOUNT)
            {
                setLevel(clevel + 1);
            }
            else
            {
                Game1.gsm.setState(Consts.LEVELSELECTSTATE);
            }
        }
        public void draw()
        {
            batch.DrawString(Game1.font, "level : " + clevel, new Vector2(Game1.cam.GetPosition().X + 80, Game1.cam.GetPosition().Y - 200), Color.White);
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    char here = map[i][j];
                    switch (here)
                    {
                        case 'b':
                            {
                                sourcerec.X=64;
                                break;
                            }
                        case 'g':
                            {
                                sourcerec.X=32;
                                break;
                            }
                        case 'r':
                            {
                                sourcerec.X=0;
                                break;
                            }
                        default:
                            {
                                continue;
                            }
                      }
                    colhere.X = j * 32;
                    colhere.Y = i * 32;
                    if(Game1.cam.getCameraView().Intersects(colhere))
                        batch.Draw(tiles, colhere, sourcerec, Color.White);

                }
            }
        }
    }
}
