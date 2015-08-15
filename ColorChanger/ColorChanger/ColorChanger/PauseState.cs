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
    class PauseState : GameState
    {
        String[] towrite;
        int hover;
        KeyboardState lastkeyb;
        public PauseState(GameStateManager gsm, ContentManager content)
            : base(gsm, content)
        {
            hover = 0;
            towrite = new String[3];
            towrite[0] = "Resume";
            towrite[1] = "Back to level select";
            towrite[2] = "Quit to dekstop";
            lastkeyb = Keyboard.GetState();
        }
        public override void draw()
        {
            for (int i = 0; i < towrite.Length; i++)
            {
                Vector2 pos = new Vector2(Game1.cam.GetPosition().X - Consts.WIDTH/4 , Game1.cam.GetPosition().Y + i *50);
                if (hover == i)
                {
                    batch.DrawString(Game1.boldfont, towrite[i], pos, Color.Black);
                }
                else
                {
                    batch.DrawString(Game1.font, towrite[i], pos, Color.Black);
                }
            }
            
        }
        public override void update(GameTime gametime)
        {
            KeyboardState keyb = Keyboard.GetState();

            if(keyb.IsKeyDown(Keys.Up) && !lastkeyb.IsKeyDown(Keys.Up)){
                hover--;
            }
            if (keyb.IsKeyDown(Keys.Down) && !lastkeyb.IsKeyDown(Keys.Down))
            {
                hover++;
            }
            if (hover < 0)
                hover = 2;
            if (hover > 2)
                hover = 0;


            if (keyb.IsKeyDown(Keys.Enter) && !lastkeyb.IsKeyDown(Keys.Enter))
            {
                if (hover == 0)
                {
                    gsm.setState(Consts.PLAYSTATE);
                }
                else if (hover == 1)
                {
                    gsm.setState(Consts.LEVELSELECTSTATE);
                    gsm.restartPlayer();
                }
                else if (hover == 2)
                {
                    gsm.Exit();
                }
            }
            lastkeyb = keyb;
        }
        
        
    }
}
