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
    class UI
    {
        PlayerGold playerGold = new PlayerGold(new Vector2(600f, 10));
        GateHealth gateHealth = new GateHealth(new Vector2(840, 10));
        VersionControl versionControl = new VersionControl(new Vector2(1, 880));
        HighScore highScore = new HighScore(new Vector2(100, 50));

        private List<UIComponent> Uielements;

        public void LoadContent(ContentManager content)
        {
            playerGold.LoadContent(content);
            gateHealth.LoadContent(content);
            versionControl.LoadContent(content);
            highScore.LoadContent(content);


            var exitButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(0, 0),
                Text = "Exit",
                Scale = 0.2f,
                TextScale = 1f
            };
            var tower_01Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(1240, 780),
                Text = "Tower_01",
                Scale = 0.5f
            };
            var tower_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(1360, 780),
                Text = "Tower_02",
                Scale = 0.5f
            };
            var tower_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(1480, 780),
                Text = "Tower_03",
                Scale = 0.5f
            };


            Uielements = new List<UIComponent>()
            {
                exitButton,
                tower_01Button,
                tower_02Button,
                tower_03Button
            };

            exitButton.Click += ExitButton_Click;

            tower_01Button.Click += tower_01Button_Click;
            tower_02Button.Click += tower_01Button_Click;
            tower_03Button.Click += tower_01Button_Click;

        }

        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
            highScore.Update();

            foreach (UIComponent component in Uielements)
            {
                component.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerGold.Draw(spriteBatch);
            gateHealth.Draw(spriteBatch);
            versionControl.Draw(spriteBatch);
            highScore.Draw(spriteBatch);


            foreach (UIComponent component in Uielements)
            {
                component.Draw(spriteBatch);
            }
        }

        private void tower_01Button_Click(object sender, System.EventArgs e)
        {

        }
        private void tower_02Button_Click(object sender, System.EventArgs e)
        {

        }
        private void tower_03Button_Click(object sender, System.EventArgs e)
        {

        }
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
           GameWorld._Instance.Exit();
        }
    }
}
