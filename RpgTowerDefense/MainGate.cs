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
        int health;

        SpriteFont TextGateHealth;
        String text;
        #endregion


        #region Properties
        public int Health { get => health;}

        public void TakeDmg(int health)
        {
            this.health -= health;
        }

        #endregion


        #region Constructer
        public MainGate(GameObject gameObject): base(gameObject)
        {
            health = 100;
        }



        #endregion


        #region Methods

        public void LoadContent(ContentManager content)
        {
            TextGateHealth = content.Load<SpriteFont>("GateHealth");
        }

        public void Update()
        {
            text = "Stronghold Health:  " + health.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextGateHealth, text, new Vector2(800f, 100), Color.White);
        }

        


        #endregion
    }
}
