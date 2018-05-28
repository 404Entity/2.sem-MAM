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
    class UI : Component, IDrawable, ILoadable, IUpdate
    {
        Texture2D UIPicture;
        SpriteFont spriteFont;
        int playerGold;
        String text;

        public int PlayerGold { get => playerGold; set => playerGold = value; }
        

        public void LoadContent(ContentManager content)
        {
            //UIPicture = content.Load<Texture2D>("Picture Name");
            spriteFont = content.Load<SpriteFont>("Gold");
        }

        public void Update()
        {
            text = "Gold Amount:  " + PlayerGold.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(spriteFont, text, new Vector2(600f, 10), Color.White);
        }
    }
}
