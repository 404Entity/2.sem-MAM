using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    class UI
    {
        #region Fields
        PlayerGold playerGold = new PlayerGold(new Vector2(600f, 10));
        GateHealth gateHealth = new GateHealth(new Vector2(840, 10));
        VersionControl versionControl = new VersionControl(new Vector2(1, 880));
        HighScore highScore = new HighScore(new Vector2(100, 50));


        UIButton tower_01Button;
        UIButton tower_01ProxyButton;
        UIButton tower_02Button;
        UIButton tower_02ProxyButton;
        UIButton tower_03Button;
        UIButton tower_03ProxyButton;


        private List<UIComponent> uielements;
        private List<UIComponent> activeUielements;
        private List<UIComponent> addToActiveElements;
        private List<UIComponent> removeUIElements;

        internal List<UIComponent> RemoveUIElements { get => removeUIElements; set => removeUIElements = value; }
        internal List<UIComponent> ActiveUielements { get => activeUielements; set => activeUielements = value; }
        internal List<UIComponent> Uielements { get => uielements; set => uielements = value; }
        internal List<UIComponent> AddToActiveElements { get => addToActiveElements; set => addToActiveElements = value; }

        #endregion
        #region LoadContent
        public void LoadContent(ContentManager content)
        {
            playerGold.LoadContent(content);
            gateHealth.LoadContent(content);
            versionControl.LoadContent(content);
            highScore.LoadContent(content);


            var exitButton = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(0, 0),
                Text = "Exit",
                Scale = 0.2f,
                TextScale = 1f
            };

            tower_01Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1240, 780),
                Text = "Tower_01",
                Scale = 0.5f
            };
            var tower_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1360, 780),
                Text = "Tower_02",
                Scale = 0.5f
            };
            var tower_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1480, 780),
                Text = "Tower_03",
                Scale = 0.5f
            };
            tower_01ProxyButton = new UIButton(content.Load<Texture2D>("tower_01"), content.Load<SpriteFont>("Fonts/UiFont"), true)
            {
                Position = new Vector2(500, 500),
                Scale = 0.2f
            };


            Uielements = new List<UIComponent>()
            {
                exitButton,
                tower_01Button,
                tower_02Button,
                tower_03Button
            };
            ActiveUielements = new List<UIComponent>();
            removeUIElements = new List<UIComponent>();
            addToActiveElements = new List<UIComponent>();


            exitButton.Click += ExitButton_Click;

            tower_01Button.Click += Tower_01Button_Click;
            tower_02Button.Click += Tower_01Button_Click;
            tower_03Button.Click += Tower_01Button_Click;

            tower_01ProxyButton.Click += Tower_01ProxyButton_Click;

        }
        #endregion
        #region Update
        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
            highScore.Update();

            foreach (UIComponent component in Uielements)
            {
                component.Update();
            }
            foreach (UIComponent componet in addToActiveElements)
            {
                Uielements.Add(componet);
            }
            foreach (UIComponent componet in removeUIElements)
            {
                uielements.Remove(componet);
            }
            ClearTemplists();
        }
        #endregion
        #region Draw
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
        #endregion
        #region Buttons
        private void Tower_01Button_Click(object sender, System.EventArgs e)
        {
            addToActiveElements.Add(tower_01ProxyButton);
        }
        private void Tower_02Button_Click(object sender, System.EventArgs e)
        {
            addToActiveElements.Add(tower_02ProxyButton);
        }
        private void Tower_03Button_Click(object sender, System.EventArgs e)
        {
            addToActiveElements.Add(tower_03ProxyButton);
        }
        private void Tower_01ProxyButton_Click(object sender, System.EventArgs e)
        {
            if (GameWorld._Instance.PlayerGold >= 50)
            {
                Director dic = new Director(new TowerBuilder());
                dic.Builder.BuildGameObject(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1);
                GameWorld._Instance.AddGameObjects.Add(dic.Builder.GetResult());
                GameWorld._Instance.PlayerGold -= 50;
                removeUIElements.Add(tower_01ProxyButton);
            }
        }
  
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
           GameWorld._Instance.Exit();
        }
        private void ClearTemplists()
        {
            addToActiveElements.Clear();
            removeUIElements.Clear();
        }
        #endregion
    }
}
