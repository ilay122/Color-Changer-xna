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
    class PlayState : GameState
    {
        private Map map;
        private Player player;
        private KeyboardState keyb;
        public PlayState(GameStateManager gsm,ContentManager content):base(gsm,content)
        {
            map = new Map(content.Load<Texture2D>("data/blocks"));
            player = new Player(content.Load<Texture2D>("data/arr"));
            keyb = Keyboard.GetState();

            gsm.addPublicObj(Consts.MAPOBJ,map);
            gsm.addPublicObj(Consts.PLAYEROBJ,player);
        }
        public override void draw()
        {
            map.draw();
            player.draw();
            
        }
        public override void update(GameTime gametime)
        {
            keyb = Keyboard.GetState();
            Game1.cam.setZoom(1.5f);
            bool alive=player.update(map);
            if (!alive)
            {
                gsm.restartPlayer();
                gsm.setState(Consts.LEVELSELECTSTATE);
            }
            if (keyb.IsKeyDown(Keys.Escape))
            {
                gsm.setState(Consts.PAUSESTATE);
            }

        }
        public void setLevel(int level)
        {
            map.setLevel(level);
        }
        public void restart()
        {
            player.restart();
        }
    }
}
