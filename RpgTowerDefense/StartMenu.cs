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

        List<UIComponent> activeelements;

        public void LoadContent(ContentManager content)
        {
           

            var exitButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(0, 0),
                Text = "Exit",
                Scale = 0.2f,
                TextScale = 1f
            };
            var startGameButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(500, 500),
                Text = "Start",
                Scale = 0.2f,
                TextScale = 1f
            };
            texture = content.Load<SpriteFont>("MenuButtom");


            activeelements = new List<UIComponent>() {
                startGameButton,
                exitButton
            };

            startGameButton.Click += StartGameButton_Click;
            exitButton.Click += ExitButton_Click;
        }

        public void Update()
        {
            foreach (UIComponent uielement in activeelements)
            {
                uielement.Update();
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            HighScore(spriteBatch);
            //spriteBatch.Draw(texture, new Rectangle(500,500,texture.Width,texture.Height), Color.Green);
            foreach (UIComponent uielement in activeelements)
            {
                uielement.Draw(spriteBatch);
            }

        }

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
        private void StartGameButton_Click(object sender, System.EventArgs e)
        {
            GameWorld._Instance.GameState = false;
        }
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            GameWorld._Instance.Exit();
        }
    }
}
