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
    class MenuState : GameState
    {
        private Random dice;
        private KeyboardState lastkeyb;
        private String[] towrite;
        private int hover;
        private Color bgcolor;
        float time;
        public MenuState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            lastkeyb = Keyboard.GetState();
            towrite = new String[2];

            towrite[0] = "Play";
            towrite[1] = "Exit to desktop";
            hover = 0;

            dice = new Random();

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
            KeyboardState keyb = Keyboard.GetState();
            if (keyb.IsKeyDown(Keys.Down) && !lastkeyb.IsKeyDown(Keys.Down))
            {
                hover++;
            }
            if (keyb.IsKeyDown(Keys.Up) && !lastkeyb.IsKeyDown(Keys.Up))
            {
                hover--;
            }
            if (hover < 0)
                hover = towrite.Length - 1;
            if (hover >= towrite.Length)
                hover = 0;

            if (keyb.IsKeyDown(Keys.Enter) && !lastkeyb.IsKeyDown(Keys.Enter))
            {
                if (hover == 0)
                {
                    gsm.setState(Consts.LEVELSELECTSTATE);
                }
                else if (hover == 1)
                {
                    gsm.Exit();
                }
            }

            

            Game1.cam.setPosition(Vector2.Zero);
            lastkeyb = keyb;
        }
        public override void draw()
        {
            graphics.GraphicsDevice.Clear(bgcolor);
            for (int i = 0; i < towrite.Length; i++)
            {
                Vector2 pos = new Vector2(-Consts.WIDTH/8, i * 50);
                if (hover == i)
                {
                    batch.DrawString(Game1.boldfont, towrite[i], pos, Color.White);
                }
                else
                {
                    batch.DrawString(Game1.font, towrite[i], pos, Color.White);
                }
            }
        }
    }
}
