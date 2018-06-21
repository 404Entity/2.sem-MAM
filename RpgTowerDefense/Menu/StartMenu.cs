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

        private List<UIComponent> menuElements;


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<SpriteFont>("MenuButtom");
            NameStringInput._Instance.LoadContent(content);
            var exitButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(800,600),
                Text = "Exit",
                Scale = 0.2f,
                TextScale = 1f
            };
            var startGameButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(800, 500),
                Text = "StartGame",
                Scale = 0.2f,
                TextScale = 1f
            };

            var resetHighScore = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(100, 800),
                Text = "ResetHighScore",
                Scale = 0.2f,
                TextScale = 1f
            };

            menuElements = new List<UIComponent>()
            {
                exitButton,
                startGameButton,
                resetHighScore
            };

            exitButton.Click += ExitButton_Click;
            startGameButton.Click += StartButton_Click;
            resetHighScore.Click += ResetHighScore_Click;
        }

        public void Update()
        {
            NameStringInput._Instance.Update();
            foreach (UIComponent component in menuElements)
            {
                component.Update();
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Rectangle(500,500,texture.Width,texture.Height), Color.Green);
            NameStringInput._Instance.Draw(spriteBatch);

            foreach (UIComponent component in menuElements)
            {
                component.Draw(spriteBatch);
            }

        }


        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            GameWorld._Instance.Exit();
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            if (NameStringInput._Instance.MyName != string.Empty)
            {
                GameWorld._Instance.GameState = false;
            }
        }

        private void ResetHighScore_Click(object sender, System.EventArgs e)
        {
            //Drops Highscore tabel
            Database._Instance.DropTable("HighScore");

            //Creates database again
            Database._Instance.CreateTables();
        }
    }
}
