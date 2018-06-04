using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    class VersionControl
    {
        string text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion+"       Under Development";
        SpriteFont spriteFont;
        Vector2 vector2;

        public VersionControl(Vector2 vector2)
        {
            this.vector2 = vector2;
        }

        

        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("Gold");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, vector2, Color.White);
        }
    }
}
