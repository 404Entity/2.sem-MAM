using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    class MainGate : Component, IUpdate, IDrawable, ILoadable
    {
        #region Fields
        int health = 100;

        SpriteFont GateHealth;
        String text;
        #endregion


        #region Propertys
        public int Health { get => health; set => health -= value; }

        #endregion


        #region Constructer
        public MainGate(GameObject gameObject): base(gameObject)
        {
        }



        #endregion


        #region Methods

        public void LoadContent(ContentManager content)
        {
            GateHealth = content.Load<SpriteFont>("GateHealth");
        }

        public void Update()
        {
            text = "Stronghold Health:  " + health.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GateHealth, text, new Vector2(830f, 10), Color.White);
        }

        


        #endregion
    }
}
