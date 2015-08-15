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
    public class Camera2d
    {
        private Viewport view;
        private Vector2 center;
        private Vector3 zoom;
        public Camera2d(Viewport view)
        {
            this.view = view;
            zoom = new Vector3(1, 1, 0);
        }
        public void setZoom(float zom)
        {
            zoom.X = zom;
            zoom.Y = zom;
        }
        public void setPosition(Vector2 pos)
        {
            this.center = pos;
        }
        public Vector2 GetPosition()
        {
            return center;
        }
        public Rectangle getCameraView()
        {
            int x = (int)center.X - view.Width / 2;
            int y = (int)center.Y - view.Height / 2;
            return new Rectangle(x, y, view.Width, view.Height);
        }
        public float getZoom()
        {
            return zoom.X;
        }
        public Matrix getTransformation()
        {
            return Matrix.CreateScale(zoom) * Matrix.CreateTranslation(new Vector3(-center.X * zoom.X, -center.Y * zoom.Y, 0)) * Matrix.CreateTranslation(new Vector3(view.Width/2,view.Height/2,0));
        }
    }
}
