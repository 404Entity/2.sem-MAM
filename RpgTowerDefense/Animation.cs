using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;
        public float Fps
        {
            get { return fps; }
            set { fps = value; }
        }
        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public Rectangle[] Rectangles
        {
            get { return rectangles; }
            set { rectangles = value; }
        }
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, int fps, Vector2 offset)
        {
            Rectangles = new Rectangle[frames];
            Offset = offset;
            this.Fps = fps;
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
        }
    }
}
