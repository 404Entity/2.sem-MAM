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
    class HighScore
    {
        SpriteFont spriteFont;
        String text;
        Vector2 vector2;




        public HighScore(Vector2 vector2)
        {
            this.vector2 = vector2;
        }

        public void LoadContent(ContentManager content)
        {
            //UIPicture = content.Load<Texture2D>("Picture Name");
            spriteFont = content.Load<SpriteFont>("GateHealth");
        }

        public void Update()
        {
            text = "Score:  " + GameWorld._Instance.HighScore.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, vector2, Color.White);
        }
    }
}
