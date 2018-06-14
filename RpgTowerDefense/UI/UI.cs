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

        //None Permenet Buttons some what dynamic buttons.
        private UIButton tower_01Button;
        private UIButton tower_01ProxyButton;
        private UIButton tower_02Button;
        private UIButton tower_02ProxyButton;
        private UIButton tower_03Button;
        private UIButton tower_03ProxyButton;
        private UIButton upgrade_01Button;
        private UIButton upgrade_02Button;
        private UIButton upgrade_03Button;

        //Labels
        private UILabel goldLabel;
        private UILabel scoreLabel;
        private UILabel gateHealthLabel;
        private UILabel attackPowerLabel;
        private UILabel attackSpeedLabel;
        private UILabel attackRangeLabel;

        GameObject previousGameObject;

        private List<UIComponent> uielements;
        private List<UIComponent> addUIElements;
        private List<UIComponent> removeUIElements;

        internal List<UIComponent> RemoveUIElements { get => removeUIElements; set => removeUIElements = value; }
        internal List<UIComponent> UIElements { get => uielements; set => uielements = value; }
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
                Position = new Vector2(1374, 824),
                Text = "Light Tower",
                Scale = 0.3f,
                TextScale = 0.8F
            };

            tower_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1449, 824),
                Text = "Heavy Tower",
                Scale = 0.3f,
                TextScale = 0.8F
            };

            tower_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1524, 824),
                Text = "Tesla Tower",
                Scale = 0.3f,
                TextScale = 0.8f
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

            upgrade_01Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1374, 824),
                Text = "AttackDamage",
                Scale = 0.3f,
                TextScale = 0.8f
            };

            upgrade_02Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1449, 824),
                Text = "AttackRange",
                Scale = 0.3f,
                TextScale = 0.8f
            };

            upgrade_03Button = new UIButton(content.Load<Texture2D>("Controls/Button"), content.Load<SpriteFont>("Fonts/UiFont"), false)
            {
                Position = new Vector2(1524, 824),
                Text = "AttackSpeed",
                Scale = 0.3f,
                TextScale = 0.8f
            };

            goldLabel = new UILabel(content.Load<SpriteFont>("Fonts/UiFont"), "0")
            {
                Position = new Vector2(1280, 18),
                PenColor = Color.Gold
            };
            gateHealthLabel = new UILabel(content.Load<SpriteFont>("Fonts/UiFont"), "0")
            {
                Position = new Vector2(1470, 18),
                PenColor = Color.Red
            };
            scoreLabel = new UILabel(content.Load<SpriteFont>("Fonts/UiFont"), "0")
            {
                Position = new Vector2(180, 18),
                PenColor = Color.White
            };

            UIElements = new List<UIComponent>()
            {
                gateHealthLabel,
                scoreLabel,
                goldLabel,
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

            upgrade_01Button.Click += UpgradeButton01_Click;
            upgrade_02Button.Click += UpgradeButton02_Click;
            upgrade_03Button.Click += UpgradeButton03_Click;

            exitButton.Click += ExitButton_Click;
            goldLabel.updateMe += UpdateGoldAmount;
            scoreLabel.updateMe += UpdateScore;

        }
        #endregion
        #region Update
        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
            highScore.Update();

            if (GameWorld._Instance.SelectedGameObject != null && GameWorld._Instance.SelectedGameObject != previousGameObject)
            {
                ChangeColor(GameWorld._Instance.SelectedGameObject, Color.Cyan);
                if (previousGameObject != null)
                {
                    ChangeColor(previousGameObject, Color.White);
                }

                if (GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") != null)
                {

                    bool containsUpgrade = false;
                    foreach (UIComponent item in UIElements)
                    {

                        if (item == upgrade_01Button || item == upgrade_02Button || item == upgrade_03Button)
                        {
                            containsUpgrade = true;
                            break;
                        }

                    }

                    if (!containsUpgrade)
                    {
                        RemoveUIElements.Add(tower_01Button);
                        RemoveUIElements.Add(tower_02Button);
                        RemoveUIElements.Add(tower_03Button);

                        addUIElements.Add(upgrade_01Button);
                        addUIElements.Add(upgrade_02Button);
                        addUIElements.Add(upgrade_03Button);

                        Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;
                        if (tower.APCapped == true)
                        {
                            upgrade_01Button.Text = "Fully Upgraded";
                        }
                        else if (tower.APUpgradeLvl == 1)
                        {
                            upgrade_01Button.Text = string.Format("{0}", CalculateUpgradecost(tower.APUpgradeLvl));
                        }
                        else
                        {
                            upgrade_01Button.Text = string.Format("{0}", CalculateUpgradecost(tower.APUpgradeLvl + 1));
                        }

                        if (tower.ASCapped == true)
                        {
                            upgrade_02Button.Text = "Fully Upgraded";
                        }
                        else if (tower.ASUpgradeLvl == 1)
                        {
                            upgrade_02Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ASUpgradeLvl));
                        }
                        else
                        {
                            upgrade_02Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ASUpgradeLvl));
                        }

                        if (tower.ARCapped == true)
                        {
                            upgrade_03Button.Text = "Fully Upgraded";
                        }
                        else if (tower.ARUpgradeLvl == 1)
                        {
                            upgrade_03Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ARUpgradeLvl));
                        }
                        else
                        {
                            upgrade_03Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ARUpgradeLvl));
                        }


                    }
                }
                else
                {
                    bool containsUpgrade = false;
                    foreach (UIComponent item in UIElements)
                    {

                        if (item == tower_01Button || item == tower_02Button || item == tower_03Button)
                        {
                            containsUpgrade = true;
                            break;
                        }

                    }
                    if (!containsUpgrade)
                    {
                        RemoveUIElements.Add(upgrade_01Button);
                        RemoveUIElements.Add(upgrade_02Button);
                        RemoveUIElements.Add(upgrade_03Button);

                        addUIElements.Add(tower_01Button);
                        addUIElements.Add(tower_02Button);
                        addUIElements.Add(tower_03Button);
                    }
                }
            }
            foreach (UIComponent component in UIElements)
            {
                component.Update();
            }
            foreach (UIComponent componet in addUIElements)
            {
                UIElements.Add(componet);
            }
            foreach (UIComponent componet in removeUIElements)
            {
                uielements.Remove(componet);
            }
            ClearTemplists();
            previousGameObject = GameWorld._Instance.SelectedGameObject;
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


            foreach (UIComponent component in UIElements)
            {
                component.Draw(spriteBatch);
            }
        }
        #endregion
        #region Events

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

        /// <summary>
        /// When clicked try to Upgrade the towers AttackPower if gold avalible and the upgrade is not capped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpgradeButton01_Click(object sender, System.EventArgs e)
        {

            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;

            float upgradeCost = CalculateUpgradecost(tower.APUpgradeLvl);

            if (tower.APCapped != true)
            {
                upgrade_01Button.Text = string.Format("{0}", CalculateUpgradecost(tower.APUpgradeLvl + 1));
            }
            else
            {
                upgrade_01Button.Text = "Fully Upgraded";
            }
        

            if (GameWorld._Instance.PlayerGold > upgradeCost && tower.APCapped == false)
            {
                tower.UpgradeTower(1);
                GameWorld._Instance.PlayerGold -= (int)Math.Floor(upgradeCost);
            }
        }

        /// <summary>
        ///  When clicked try to Upgrade the towers AttackSpeed if gold avalible and the upgrade is not capped 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpgradeButton02_Click(object sender, System.EventArgs e)
        {

            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;

            float upgradeCost = CalculateUpgradecost(tower.ASUpgradeLvl);

            if (tower.ASCapped != true)
            {
                upgrade_02Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ASUpgradeLvl + 1));
            }
            else
            {
                upgrade_02Button.Text = "Fully Upgraded";
            }
            

            if (GameWorld._Instance.PlayerGold > upgradeCost && tower.ASCapped == false)
            {
                tower.UpgradeTower(2);
                GameWorld._Instance.PlayerGold -= (int)Math.Floor(upgradeCost);
            }

        }

        /// <summary>
        /// When clicked try to Upgrade the towers AttackRange if gold avalible and the upgrade is not capped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpgradeButton03_Click(object sender, System.EventArgs e)
        {
            Towerobj tower = GameWorld._Instance.SelectedGameObject.GetComponent("Towerobj") as Towerobj;

            float upgradeCost = CalculateUpgradecost(tower.ARUpgradeLvl);
            if (tower.ARCapped != true)
            {
                upgrade_03Button.Text = string.Format("{0}", CalculateUpgradecost(tower.ARUpgradeLvl + 1));
            }
            else
            {
                upgrade_03Button.Text = "Fully Upgraded";
            }
          

            if (GameWorld._Instance.PlayerGold > upgradeCost && tower.ARCapped == false)
            {
                tower.UpgradeTower(3);
                GameWorld._Instance.PlayerGold -= (int)Math.Floor(upgradeCost);

            }

        }

        /// <summary>
        /// Calculate the cost of the Upgrade for the Upgradebutton.
        /// </summary>
        /// <param name="upgradelvl"></param>
        /// <returns></returns>
        private float CalculateUpgradecost(int upgradelvl)
        {
            float upgradeCost = 10 * upgradelvl;
            return upgradeCost;
        }

        private void UpdateGoldAmount(object sender, System.EventArgs e)
        {
            goldLabel.Text = GameWorld._Instance.PlayerGold.ToString();
        }

        private void UpdateScore(object sender, System.EventArgs e)
        {
            scoreLabel.Text = GameWorld._Instance.HighScore.ToString();
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

        private void ChangeColor(GameObject gameObject, Color color)
        {
            SpriteRenderer sp = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.Color = color;
        }
    }
}
