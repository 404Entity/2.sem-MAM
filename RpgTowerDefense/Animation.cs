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
        float fps;
        Vector2 offset;
        Rectangle[] rectangles;

        public Animation(int frames, int yPos, int xPos, int xStratFrame, int width, int height, float fps, Vector2 offset)
        {

        }
        public float GetFPS()
        {
            return fps;
        }
        public float SetFPS(float FPS)
        {
            fps = FPS;
            return fps;
        }
        public Vector2 GetOffset()
        {
            return offset;
        }
        public Vector2 SetOffset(Vector2 offset)
        {
            this.offset = offset;
            return this.offset;
        }
    }
}
