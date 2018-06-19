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
    class GameOver
    {
        SpriteFont texture;
        private Texture2D t2D;
        Vector2 vec = new Vector2(400, 100);

        

        public void LoadContent(ContentManager content)
        {
            t2D = content.Load<Texture2D>("GameOver");
            texture = content.Load<SpriteFont>("MenuButtom");
        }
        
        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t2D, vec, Color.White);
            HighScore(spriteBatch);
        }


        #region HighScore Draw
        public void HighScore(SpriteBatch spriteBatch)
        {
            //HighScore Draw
            Vector2 HighScoreText = new Vector2(100, 160);
            Vector2 vec = new Vector2(100, 200);
            spriteBatch.DrawString(texture, "HighScore", HighScoreText, Color.White);
            foreach (string t in Database._Instance.ReadHighScore("select * from highscore ORDER BY score DESC limit 10"))
            {
                spriteBatch.DrawString(texture, t, vec, Color.White);
                vec.Y += 20;
            }
        }
        #endregion
    }
}
