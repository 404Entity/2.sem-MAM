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
            GameObject player = new GameObject();
            player.AddComponent(new Transform(player, position));
            player.AddComponent(new SpriteRenderer(player, "Player", 1, 0.2f));
            player.AddComponent(new Animator(player));
            player.AddComponent(new Player(player));
            player.AddComponent(new Collider(player, false, 0.2f));
            player.LoadContent(GameWorld._Instance.Content);
            SpriteRenderer sp = player.GetComponent("SpriteRenderer") as SpriteRenderer;
            buildObject = player;
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, GameObject player)
        {
            throw new NotImplementedException();
        }

        //public void BuildGameObject(Vector2 position, int id, Vector2 direction)
        public void BuildGameObject(Vector2 position, int id, Vector2 direction, float damage, AttackType attackType)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
