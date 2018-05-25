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
    class UI : Component, IDrawable, ILoadable
    {
        Texture2D UIPicture;
        SpriteFont spriteFont;
        int playerGold;
        String text;

        public int PlayerGold { get => playerGold; set => playerGold = value; }

        public UI()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            //UIPicture = content.Load<Texture2D>("Picture Name");
            spriteFont = content.Load<SpriteFont>("GoldDraw");
            text = "Gold Amount:  " + PlayerGold.ToString();
        }

        public void Update(int playerGold)
        {
            this.playerGold = playerGold;
            text = "Gold Amount:  " + PlayerGold.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(spriteFont, text, new Vector2(500f, 100), Color.White);
        }
    }
}
