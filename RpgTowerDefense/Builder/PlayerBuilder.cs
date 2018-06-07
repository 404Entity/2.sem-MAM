using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class PlayerBuilder : IBuilder
    {
        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject Player = new GameObject();
            Player.AddComponent(new Transform(Player, position));
            Player.AddComponent(new SpriteRenderer(Player, "Player", 1, 0.2f));
            Player.AddComponent(new Animator(Player));
            Player.AddComponent(new Player(Player));
            Player.AddComponent(new Collider(Player, false, 0.2f));
            buildObject = Player;
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, GameObject player)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int id, Vector2 direction)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
