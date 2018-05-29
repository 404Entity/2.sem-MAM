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
    class SpriteRenderer : Component, ILoadable, IDrawable
    {
        private float scale;
        private Rectangle rectangle;
        private Texture2D sprite;
        private Vector2 offset;
        private Vector2 origin;
        private float rotation;
 
        private string spriteName;
        private float layerDepth;
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
        public float Rotation { get => rotation; set => rotation = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public float Scale { get => scale; set => scale = value; }

        public SpriteRenderer(GameObject gameobject, string spriteName, float layerDepth, float scale) : base(gameobject)
        {
            this.layerDepth = layerDepth;
            this.spriteName = spriteName;
            this.Scale = scale;
            rotation = 0;
            origin = Vector2.Zero;
        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.Transform.Position + offset, rectangle, Color.White, rotation, origin, Scale, SpriteEffects.None, layerDepth);
        }

        public void GetStaticRectangle()
        {
            rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }
    }
}
