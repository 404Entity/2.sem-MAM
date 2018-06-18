using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class StartMenu
    {
        SpriteFont texture;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<SpriteFont>("MenuButtom");
        }



        public void Update()
        {

        }
        


        public void Draw(SpriteBatch spriteBatch)
        {
            HighScore(spriteBatch);
            //spriteBatch.Draw(texture, new Rectangle(500,500,texture.Width,texture.Height), Color.Green);
           

        }




        #region HighScore Draw
        public void HighScore(SpriteBatch spriteBatch)
        {
            //HighScore Draw
            Vector2 vec = new Vector2(200, 200);
            foreach (string t in Database._Instance.ReadHighScore("select * from highscore ORDER BY score DESC limit 10"))
            {
                spriteBatch.DrawString(texture, t, vec, Color.Red);
                vec.Y += 20;
            }
        }
        #endregion
    }
}
