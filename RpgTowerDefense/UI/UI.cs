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
        PlayerGold playerGold;
        GateHealth gateHealth;

        public void LoadContent(ContentManager content)
        {
            playerGold = new PlayerGold();
            gateHealth = new GateHealth();
            playerGold.LoadContent(content);
            gateHealth.LoadContent(content);
        }

        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerGold.Draw(spriteBatch);
            gateHealth.Draw(spriteBatch);
        }
    }
}
