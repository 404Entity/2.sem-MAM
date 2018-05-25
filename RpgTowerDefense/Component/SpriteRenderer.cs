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
        private float rotation;
        private string spriteName;
        private float layerDepth;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public float Rotation { get => rotation; set => rotation = value; }

        public SpriteRenderer(GameObject gameobject, string spriteName, float layerDepth, float scale) : base(gameobject)
        {
            this.layerDepth = layerDepth;
            this.spriteName = spriteName;
            this.scale = scale;
            rotation = 0;
        }
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.Transform.Position + offset, rectangle, Color.White, rotation, new Vector2((gameObject.Transform.Position.X + (sprite.Width / 2)) * scale, (gameObject.Transform.Position.Y + (sprite.Height / 2)) * scale +110), scale, SpriteEffects.None, layerDepth);
        }

        public void GetStaticRectangle()
        {
            rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }
    }
}
