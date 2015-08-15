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
    
    class Player
    {
        private KeyboardState lastkeyb;
        private char charcolor;
        private Color color;
        private bool isjumping;
        private int jumpingspeed;
        private Sprite shape;
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private Rectangle checkrec;

        private bool moveCam;
        private float more;
        public Player(Texture2D tex)
        {
            shape = new Sprite(tex);
            isjumping = false;

            batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            lastkeyb = Keyboard.GetState();

            color = Color.Red;
            charcolor = 'r';

            moveCam = true;
            checkrec = new Rectangle(0, 0, Consts.BLOCKSIZE, Consts.BLOCKSIZE);
            more = Consts.BLOCKSIZE * 4 / Consts.XMOVMENT;
        }
        public bool update(Map map)
        {
            KeyboardState keyb = Keyboard.GetState();
            shape.moveX((int)Consts.XMOVMENT);
            shape.moveY((int)Consts.GRAVITY);
            if (isjumping)
            {
                shape.moveY(jumpingspeed);
                jumpingspeed++;
            }
            else
            {
                if (keyb.IsKeyDown(Keys.Space) && !lastkeyb.IsKeyDown(Keys.Space))
                {
                    isjumping = true;
                    jumpingspeed = -(int)Consts.JUMPHEIGHT;
                    shape.moveY(-10);
                }
            }
            if (keyb.IsKeyDown(Keys.Q))
            {
                shape.moveY((int)-Consts.GRAVITY -5);
            }
            if (keyb.IsKeyDown(Keys.Z) && !lastkeyb.IsKeyDown(Keys.Z))
            {
                if (charcolor == 'r')
                {
                    charcolor = 'b';
                    color = Color.Blue;
                }
                else if (charcolor == 'b')
                {
                    charcolor = 'g';
                    color = new Color(0, 255, 0);
                }
                else {
                    charcolor = 'r';
                    color = Color.Red;
                }
            }
            
            for (int i = 0; i < map.map.Count;i++) {
                for(int j=0;j<map.map[i].Length;j++){
                    if (map.map[i][j] != ' ')
                    {
                        checkrec.X = j * Consts.BLOCKSIZE;
                        checkrec.Y = i * Consts.BLOCKSIZE;
                        if (map.map[i][j] == charcolor)
                        {
                            if (checkrec.Intersects(shape.getCollisionRectangle()))
                            {
                                float player_bottom = shape.getPosition().Y + shape.getHeight();
                                float tiles_bottom = checkrec.Y + checkrec.Height;
                                float player_right = shape.getPosition().X + shape.getWidth();
                                float tiles_right = checkrec.X + checkrec.Width;

                                float b_collision = tiles_bottom - shape.getPosition().Y;
                                float t_collision = player_bottom - checkrec.Y;
                                float l_collision = player_right - checkrec.X;
                                float r_collision = tiles_right - shape.getPosition().X;

                                if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
                                {
                                    isjumping = false;
                                    shape.setY(checkrec.Y - shape.getHeight());
                                    //Top collision
                                }
                                if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
                                {
                                    shape.setY(checkrec.Y + checkrec.Height);
                                    //bottom collision
                                }
                                if (l_collision < r_collision && l_collision < t_collision && l_collision < b_collision)
                                {
                                    shape.setX(checkrec.X - shape.getWidth());
                                    //Left collision
                                }
                                if (r_collision < l_collision && r_collision < t_collision && r_collision < b_collision)
                                {

                                    //Right collision
                                }
                            }
                        }
                        if (map.map[i][j] == 'f')
                        {
                            if (Game1.cam.getCameraView().Intersects(checkrec) && moveCam)
                            {
                                moveCam = false;
                            }
                            if (shape.getCollisionRectangle().Intersects(checkrec))
                            {
                                restart();
                                map.nextLevel();
                            }
                        }
                    }
                }
            }

            if (moveCam)
            {
                Game1.cam.setPosition(new Vector2(Game1.cam.GetPosition().X + Consts.XMOVMENT, 30));
            }
            else
            {
                if (more >= 0)
                {
                    Game1.cam.setPosition(new Vector2(Game1.cam.GetPosition().X + Consts.XMOVMENT, 30));
                    more--;
                }
            }
            lastkeyb = keyb;
            return shape.getCollisionRectangle().Intersects(Game1.cam.getCameraView());
        }
        public void draw()
        {
            shape.draw(color);
        }
        private void jump()
        {
            jumpingspeed = (int)(-Consts.JUMPHEIGHT);
            isjumping = true;
        }
        public void restart()
        {
            Game1.cam.setPosition(Vector2.Zero);

            shape.setX(0);
            shape.setY(0);
            isjumping = false;

            color = Color.Red;
            charcolor = 'r';
            moveCam = true;
            more = Consts.BLOCKSIZE * 4 / Consts.XMOVMENT;
        }
        public Rectangle getCollisionRectangle()
        {
            return shape.getCollisionRectangle();
        }
    }
}
