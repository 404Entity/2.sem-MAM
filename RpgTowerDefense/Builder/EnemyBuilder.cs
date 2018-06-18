using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class EnemyBuilder : IBuilder
    {
        private GameObject buildobject;
        

        public void BuildGameObject(Vector2 position)
        {
            GameObject enemy = new GameObject();
            enemy.AddComponent(new Transform(enemy, position));
            enemy.AddComponent(new SpriteRenderer(enemy, "Enemy01_SpriteSheetv.02", 1, 0.1f));
            enemy.AddComponent(new Animator(enemy));
            enemy.AddComponent(new Enemy(enemy, 10, 10, 20, 10, 7));
            enemy.LoadContent(GameWorld._Instance.Content);
            enemy.AddComponent(new Collider(enemy, true, 0.1f));
            SpriteRenderer sp = enemy.GetComponent("SpriteRenderer") as SpriteRenderer;

            buildobject = enemy;
        }


        public void BuildGameObject(Vector2 position, GameObject player)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int id, Vector2 direction, float damage, AttackType attackType)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return buildobject;
        }

    }
}
