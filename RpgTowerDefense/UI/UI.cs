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

        //None Permenet Buttons
        UIButton tower_01Button;
        UIButton tower_01ProxyButton;
        UIButton tower_02Button;
        UIButton tower_02ProxyButton;
        UIButton tower_03Button;
        UIButton tower_03ProxyButton;
        UIButton Upgrade_01Button;
        UIButton Upgrade_02Button;
        UIButton Upgrade_03Button;


        private List<UIComponent> uielements;

        private List<UIComponent> addUIElements;
        private List<UIComponent> removeUIElements;

        internal List<UIComponent> RemoveUIElements { get => removeUIElements; set => removeUIElements = value; }
        internal List<UIComponent> Uielements { get => uielements; set => uielements = value; }
        internal List<UIComponent> AddUIElements { get => addUIElements; set => addUIElements = value; }

        #endregion
        #region LoadContent

        /// <summary>
        /// Loads ui Content into the game and instantiate it 
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            playerGold.LoadContent(content);
            gateHealth.LoadContent(content);
            versionControl.LoadContent(content);
            highScore.LoadContent(content);

            // Buttons
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
                Text = "Light Tower",
                Scale = 0.5f
            };

             tower_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1360, 780),
                Text = "Heavy Tower",
                Scale = 0.5f
            };

             tower_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1480, 780),
                Text = "Tesla Tower",
                Scale = 0.5f
            };

            tower_01ProxyButton = new UIButton(content.Load<Texture2D>("tower_01"), content.Load<SpriteFont>("Fonts/UiFont"), true)
            {
                Position = new Vector2(500, 500),
                Scale = 0.2f
            };

            tower_02ProxyButton = new UIButton(content.Load<Texture2D>("tower_01"), content.Load<SpriteFont>("Fonts/UiFont"), true)
            {
                Position = new Vector2(500, 500),
                Scale = 0.2f
            };

            tower_03ProxyButton = new UIButton(content.Load<Texture2D>("tower_01"), content.Load<SpriteFont>("Fonts/UiFont"), true)
            {
                Position = new Vector2(500, 500),
                Scale = 0.2f
            };






            Upgrade_01Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1240, 780),
                Text = "AttackDamage",
                Scale = 0.5f,
                TextScale = 0.8f
            };

            Upgrade_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1360, 780),
                Text = "AttackRange",
                Scale = 0.5f,
                TextScale = 0.8f
            };

            Upgrade_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1480, 780),
                Text = "AttackSpeed",
                Scale = 0.5f,
                TextScale = 0.8f
            };


            Uielements = new List<UIComponent>()
            {
                exitButton,
                tower_01Button,
                tower_02Button,
                tower_03Button
            };

            removeUIElements = new List<UIComponent>();
            addUIElements = new List<UIComponent>();

            //subscribe to events
     
            tower_01Button.Click += Tower_01Button_Click;
            tower_02Button.Click += Tower_02Button_Click;
            tower_03Button.Click += Tower_03Button_Click;

            tower_01ProxyButton.Click += Tower_01ProxyButton_Click;
            tower_02ProxyButton.Click += Tower_02ProxyButton_Click;
            tower_03ProxyButton.Click += Tower_03ProxyButton_Click;

            Upgrade_01Button.Click += UpgradeButton01_Click;
            Upgrade_02Button.Click += UpgradeButton02_Click;
            Upgrade_03Button.Click += UpgradeButton03_Click;

            exitButton.Click += ExitButton_Click;



        }
        #endregion
        #region Update
        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
            highScore.Update();

            if (GameWorld._Instance.SelectedGameObject != null)
            {
                if(GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") != null)
                {
                    //Make this to a Method of its own
                    bool containsUpgrade = false;
                    foreach (UIComponent item in Uielements)
                    {
                        
                        if (item == Upgrade_01Button || item == Upgrade_02Button || item == Upgrade_03Button)
                        {
                            containsUpgrade = true;
                            break;
                        }

                        if (!containsUpgrade)
                        {
                            RemoveUIElements.Add(tower_01Button);
                            RemoveUIElements.Add(tower_02Button);
                            RemoveUIElements.Add(tower_03Button);

                            addUIElements.Add(Upgrade_01Button);
                            addUIElements.Add(Upgrade_02Button);
                            addUIElements.Add(Upgrade_03Button);
                        }
                    }
                }
                else
                {
                    bool containsUpgrade = false;
                    foreach (UIComponent item in Uielements)
                    {

                        if (item == tower_01Button|| item == tower_02Button || item == tower_03Button)
                        {
                            containsUpgrade = true;
                            break;
                        }

                        if (!containsUpgrade)
                        {
                            RemoveUIElements.Add(Upgrade_01Button);
                            RemoveUIElements.Add(Upgrade_02Button);
                            RemoveUIElements.Add(Upgrade_03Button);

                            addUIElements.Add(tower_01Button);
                            addUIElements.Add(tower_02Button);
                            addUIElements.Add(tower_03Button);
                        }
                    }
                }
            }
            foreach (UIComponent component in Uielements)
            {
                component.Update();
            }
            foreach (UIComponent componet in addUIElements)
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

        /// <summary>
        /// Draws the Ui
        /// </summary>
        /// <param name="spriteBatch"></param>
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

        /// <summary>
        ///  Add Proxy Button of tower_01 to active ui-elememts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tower_01Button_Click(object sender, System.EventArgs e)
        {
            addUIElements.Add(tower_01ProxyButton);
        }

        /// <summary>
        /// Add Proxy Button of tower_02 to active ui-elememts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tower_02Button_Click(object sender, System.EventArgs e)
        {
            addUIElements.Add(tower_02ProxyButton);
        }

        /// <summary>
        /// Add Proxy Button of tower_01 to active ui-elememts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tower_03Button_Click(object sender, System.EventArgs e)
        {
            addUIElements.Add(tower_03ProxyButton);
        }

        /// <summary>
        /// Proxy button at players mouse location when clicked add a tower whit tower_01 params and remove the button from the active ui-elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Proxy button at players mouse location when clicked add a tower whit tower_02 params and remove the button from the active ui-elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tower_02ProxyButton_Click(object sender, System.EventArgs e)
        {
            if (GameWorld._Instance.PlayerGold >= 100 && Mouse.GetState().X < 1600)
            {
                Director dic = new Director(new TowerBuilder());
                dic.Builder.BuildGameObject(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 2);
                GameWorld._Instance.AddGameObjects.Add(dic.Builder.GetResult());
                GameWorld._Instance.PlayerGold -= 100;
                removeUIElements.Add(tower_02ProxyButton);
            }
        }

        /// <summary>
        /// Proxy button at players mouse location when clicked add a tower whit tower_03 params and remove the button from the active ui-elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tower_03ProxyButton_Click(object sender, System.EventArgs e)
        {
            if (GameWorld._Instance.PlayerGold >= 150)
            {
                Director dic = new Director(new TowerBuilder());
                dic.Builder.BuildGameObject(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 3);
                GameWorld._Instance.AddGameObjects.Add(dic.Builder.GetResult());
                GameWorld._Instance.PlayerGold -= 150;
                removeUIElements.Add(tower_03ProxyButton);
            }
        }

        private void UpgradeButton01_Click(object sender, System.EventArgs e)
        {

            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;
            tower.AttackPower++;
        }

        private void UpgradeButton02_Click(object sender, System.EventArgs e)
        {
            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;
            tower.AttackRadius += 50;
        }

        private void UpgradeButton03_Click(object sender, System.EventArgs e)
        {
            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;
            tower.AttackSpeed ++;
        }



        /// <summary>
        /// when button is clicked exit the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, System.EventArgs e)
        {
           GameWorld._Instance.Exit();
        }


        #endregion
        /// <summary>
        /// clear the add and remove list for next update iteration
        /// </summary>
        private void ClearTemplists()
        {
            addUIElements.Clear();
            removeUIElements.Clear();
        }
    }
}
