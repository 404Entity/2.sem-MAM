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
    class MainGate : Component, IUpdate, IDrawable, ILoadable, ICollideEnter
    {
        #region Fields
        int health;

        SpriteFont GateHealth;
        String text;
        #endregion


        #region Propertys
        public int Health { get => health; set => health = value; }

        #endregion


        #region Constructer
        public MainGate(GameObject gameObject): base(gameObject)
        {
            GateHealth gateHealth = new GateHealth();










             

        }



        #endregion


        #region Methods

        public void LoadContent(ContentManager content)
        {
            GateHealth = content.Load<SpriteFont>("GateHealth");
        }

        public void Update()
        {
            if (GameWorld._Instance.GateHealth <= 1)
            {
                GameWorld._Instance.GameState = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //(spriteBatch.DrawString(GateHealth, text, new Vector2(800f, 100), Color.White);
        }

        public void OnCollisionEnter(Collider other)
        {
            if ((Enemy)other.GameObject.GetComponent("Enemy") != null)
            {
                Enemy dmgObject = (Enemy)other.GameObject.GetComponent("Enemy");
                GameWorld._Instance.GateHealth -= dmgObject.Dmg;
                GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
            }
        }




        #endregion
    }
}
