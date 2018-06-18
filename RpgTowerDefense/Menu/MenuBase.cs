using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    abstract class MenuBase : ILoadable, IUpdate, IDrawable
    {
        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void LoadContent(ContentManager content);

        public abstract void Update();
     
    }
}
