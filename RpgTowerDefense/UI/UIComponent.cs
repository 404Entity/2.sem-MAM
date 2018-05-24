using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    abstract class UIComponent : IUpdate, IDrawable
    {
        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract void Update();

        public abstract void Update(GameTime gameTime);

    }
}
