using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorChanger
{
    class Consts
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 600;
        public const int BLOCKSIZE = 32;

        public const int PLAYSTATE = 0;
        public const int PAUSESTATE = 1;
        public const int MENUSTATE = 2;
        public const int LEVELSELECTSTATE = 3;

        public const int LEVELCOUNT = 7;

        public const float GRAVITY = 5.5f;
        public const float XMOVMENT = 2f;
        public const float JUMPHEIGHT = 20f;

        public const int PLAYEROBJ = 0;
        public const int MAPOBJ= 1;

        public const int EVERY = 1000;


        public const String TITLE = "Color Changer";
    }
}
