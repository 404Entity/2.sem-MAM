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
    class BackGround: Component, IDrawable, ILoadable
    {
        Texture2D sprite;
        Rectangle rectangle;
        

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("BackGround");
            rectangle.Height = sprite.Height;
            rectangle.Width = sprite.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Vector2.Zero, rectangle, Color.White, 0, Vector2.Zero, 1.4f, SpriteEffects.None, 1);
        }
    }
}
