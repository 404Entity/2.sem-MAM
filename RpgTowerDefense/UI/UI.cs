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

        public void LoadContent(ContentManager content)
        {
            playerGold.LoadContent(content);
            gateHealth.LoadContent(content);
            versionControl.LoadContent(content);
            highScore.LoadContent(content);
        }

        public void Update()
        {
            playerGold.Update();
            gateHealth.Update();
            highScore.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerGold.Draw(spriteBatch);
            gateHealth.Draw(spriteBatch);
            versionControl.Draw(spriteBatch);
            highScore.Draw(spriteBatch);
        }
    }
}
