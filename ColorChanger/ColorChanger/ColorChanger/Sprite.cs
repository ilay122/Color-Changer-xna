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
    class Sprite
    {
        private SpriteBatch batch;
        private GraphicsDeviceManager graphics;
        private Texture2D tex;
        private Rectangle collision;
        public Sprite(Texture2D tex)
        {
            this.batch = Game1.spriteBatch;
            this.graphics = Game1.graphics;
            this.tex = tex;
            collision = new Rectangle((int)(0), (int)(0), tex.Width, tex.Height);
        }
        
        public void draw()
        {
            batch.Draw(tex, collision, Color.White);
            
        }
        public void draw(Color col)
        {
            batch.Draw(tex, collision, col);
        }
        public void setX(int x)
        {
            collision.X = x;
        }
        public void moveX(int x)
        {
            collision.X += x;
        }
        public void moveY(int y)
        {
            collision.Y += y;
        }
        public void move(Vector2 mov)
        {
            moveX((int)mov.X);
            moveY((int)mov.Y);
        }
        public void setTexture(Texture2D tex){
            this.tex = tex;
            collision.Width = tex.Width;
            collision.Height = tex.Height;
        }
        public void setY(int y)
        {
            collision.Y = y;
        }
        public void setWidth(int width)
        {
            collision.Width = width;
        }
        public void setHeight(int height)
        {
            collision.Height = height;
        }
        public Rectangle getCollisionRectangle()
        {
            return collision;
        }
        public Vector2 getPosition()
        {
            return new Vector2(collision.X, collision.Y);
        }
        public Vector2 getCenterPos()
        {
            return new Vector2(collision.X+collision.Width/2, collision.Y+collision.Height/2);
        }
        public int getHeight()
        {
            return collision.Height;
        }
        public int getWidth()
        {
            return collision.Width;
        }

    }
}
