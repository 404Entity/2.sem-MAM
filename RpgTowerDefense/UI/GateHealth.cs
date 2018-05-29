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
    class GateHealth : UI
    {
        int health = 100;
        string text;
        SpriteFont GateHealthFont;

        public int Health { get => health; set => health -= value; }
        
        public void LoadContent(ContentManager content)
        {
            GateHealthFont = content.Load<SpriteFont>("GateHealth");
        }

        public void Update()
        {
            text = "Gate Health:  " + Health.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(GateHealthFont, text, new Vector2(840, 10), Color.White);
        }






    }
}
