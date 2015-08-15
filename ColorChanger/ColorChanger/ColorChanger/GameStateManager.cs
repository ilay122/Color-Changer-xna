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
    public class GameStateManager
    {
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private int currentState;
        private List<GameState> gamestates;
        private ContentManager content;
        private Vector2 defaultcampos;

        private Dictionary<int,object> publicsObjs;
        //0-map
        //1-player
        public GameStateManager(ContentManager content)
        {
            
            this.batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            gamestates = new List<GameState>();
            this.content = content;
            defaultcampos = Vector2.Zero;

            currentState = Consts.MENUSTATE;

            publicsObjs = new Dictionary<int, object>();
            
        }
        private void addState(GameState state)
        {
            gamestates.Add(state);
        }
        public void loadContent()
        {
            addState(new PlayState(this, content));
            addState(new PauseState(this, content));
            addState(new MenuState(this, content));
            addState(new LevelSelectState(this, content));

        }
        public void setState(int state)
        {
            this.currentState = state;
        }
        public void update(GameTime gametime)
        {
            gamestates[currentState].update(gametime);
            
        }
        public void draw()
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Game1.cam.getTransformation());
            gamestates[currentState].draw();
            batch.End();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }

        public void setLevel(int level)
        {
            Map map = (Map)publicsObjs[Consts.MAPOBJ];
            map.setLevel(level);
        }
        public void restartPlayer()
        {
            Player player = (Player)publicsObjs[Consts.PLAYEROBJ];
            player.restart();
        }
        public void addPublicObj(int num,Object obj)
        {
            publicsObjs.Add(num,obj);
        }
    }
}
