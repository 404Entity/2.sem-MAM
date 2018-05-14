using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace T_L_O_B_O
{
    class SpriteRenderer: Component, ILoadable, IDrawable
    {
        float scale;
        private Rectangle rectangle;
        private Texture2D sprite;
        private Vector2 offset;
        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        private string spriteName;
        private float layerDepth;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public SpriteRenderer(GameObject gameobject, string spriteName, float layerDepth, float scale) : base(gameobject)
        {
            this.layerDepth = layerDepth;
            this.spriteName = spriteName;
            this.scale = scale;
        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
         
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Der var fejl i rectangle, jeg har lavet cheat off da jeg har ingen ide om hvad der lige sker :D
            string ChestImage = "ChestImage";
            if (ChestImage == sprite.ToString())
            {
                rectangle.Height = sprite.Height;
                rectangle.Width = sprite.Width;
            }
            spriteBatch.Draw(sprite, GameObject.Transform.Position + offset,rectangle, Color.White,0,Vector2.Zero,scale,SpriteEffects.None,layerDepth);
        }
    }
}
