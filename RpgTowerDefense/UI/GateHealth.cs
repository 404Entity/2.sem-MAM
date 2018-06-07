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
    class GateHealth
    {
        #region fields
        string text;
        SpriteFont GateHealthFont;
        Vector2 vector2;
        string text2 = "YOU SHALL NOT PASS";

        #endregion

        #region Constructor
        public GateHealth(Vector2 vector2)
        {
            this.vector2 = vector2;
        }

        public GateHealth()
        {
        }

        #endregion
       

        #region Methods
        public void LoadContent(ContentManager content)
        {
            GateHealthFont = content.Load<SpriteFont>("Gold");
        }

        public void Update()
        {
            text = "Gate Health:  " + GameWorld._Instance.GateHealth.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GateHealthFont, text, vector2, Color.White);
            spriteBatch.DrawString(GateHealthFont, text2, new Vector2(1390,10), Color.Red);
        }
        #endregion





    }
}
