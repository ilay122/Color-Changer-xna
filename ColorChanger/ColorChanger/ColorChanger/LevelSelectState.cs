using System;
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
    class LevelSelectState : GameState
    {
        private int hover;
        private KeyboardState lastkeyb;
        private Texture2D aroundrect;
        private String back;
        private Random dice;
        private Color bgcolor;
        float time;
        public LevelSelectState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            hover = 0;
            lastkeyb = Keyboard.GetState();
            aroundrect = new Texture2D(graphics.GraphicsDevice, 55, 50);

            Color[] arr = new Color[aroundrect.Width * aroundrect.Height];
            for (int i = 0; i < aroundrect.Height; i++)
            {
                for (int j = 0; j < aroundrect.Width; j++)
                {
                    if (j == 0 || j == aroundrect.Width - 1 || i == 0 || i == aroundrect.Height - 1)
                    {
                        arr[j + i * aroundrect.Width] = Color.White;
                    }
                }
            }
            aroundrect.SetData<Color>(arr);
            dice = new Random();

            back = "Back to menu";
            time = 0;

            bgcolor = Color.Blue;
        }
        public override void update(GameTime gametime)
        {
            Game1.cam.setZoom(1);
            time += gametime.ElapsedGameTime.Milliseconds;
            if (time >= Consts.EVERY)
            {
                if (bgcolor == Color.Blue)
                {
                    bgcolor = Color.Red;
                }
                else if (bgcolor == Color.Red)
                {
                    bgcolor = Color.Green;
                }
                else if (bgcolor == Color.Green)
                {
                    bgcolor = Color.Blue;
                }

                time -= Consts.EVERY;
            }
            Game1.cam.setPosition(Vector2.Zero);
            KeyboardState keyb = Keyboard.GetState();
            if (keyb.IsKeyDown(Keys.Down) && !lastkeyb.IsKeyDown(Keys.Down))
            {
                hover++;
            }

            if (keyb.IsKeyDown(Keys.Up) && !lastkeyb.IsKeyDown(Keys.Up))
            {
                hover--;
            }
            if (keyb.IsKeyDown(Keys.Right) && !lastkeyb.IsKeyDown(Keys.Right) && hover != 0)
            {
                hover += 5;
            }
            if (keyb.IsKeyDown(Keys.Left) && !lastkeyb.IsKeyDown(Keys.Left) && hover != 0)
            {
                hover -= 5;
            }

            if (hover < 0)
                hover = 0;
            if (hover > Consts.LEVELCOUNT)
                hover = Consts.LEVELCOUNT;

            if (keyb.IsKeyDown(Keys.Enter) && !lastkeyb.IsKeyDown(Keys.Enter))
            {
                if (hover == 0)
                {
                    gsm.setState(Consts.MENUSTATE);
                }
                else
                {
                    gsm.setLevel(hover);
                    gsm.setState(Consts.PLAYSTATE);
                }
            }
            if (keyb.IsKeyDown(Keys.Escape) && !lastkeyb.IsKeyDown(Keys.Escape))
            {
                gsm.setState(Consts.MENUSTATE);
            }
            lastkeyb = keyb;
        }
        public override void draw()
        {
            graphics.GraphicsDevice.Clear(bgcolor);
            Vector2 pos = new Vector2(-Consts.WIDTH / 6, 0);
            for (int i = 0; i < Consts.LEVELCOUNT; i++)
            {
                if (i % 5 ==0 && i!=0)
                {
                    pos.X += 55;
                    pos.Y = 0;
                }
                else
                {
                    if(i !=0)
                        pos.Y += 50;
                }
                if (hover == i + 1)
                {
                    batch.DrawString(Game1.boldfont, Convert.ToString(i + 1), new Vector2(pos.X + 10, pos.Y), Color.White);
                }
                else
                {
                    batch.DrawString(Game1.font, Convert.ToString(i + 1), new Vector2(pos.X + 10, pos.Y), Color.White);
                }
                batch.Draw(aroundrect, pos, Color.White);
            }
            if (hover == 0)
            {
                batch.DrawString(Game1.boldfont, back, new Vector2(Game1.cam.GetPosition().X - 200, Game1.cam.GetPosition().Y - 200), Color.White);
            }
            else
            {
                batch.DrawString(Game1.font, back, new Vector2(Game1.cam.GetPosition().X - 200, Game1.cam.GetPosition().Y - 200), Color.White);
            }
        }
    }
}
