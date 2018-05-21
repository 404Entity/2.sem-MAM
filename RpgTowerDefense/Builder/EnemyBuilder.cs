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
            enemy.AddComponent(new SpriteRenderer(enemy, "Enemy", 1, 0.5f));
            enemy.AddComponent(new Animator(enemy));
            enemy.AddComponent(new Enemy(enemy));
            enemy.LoadContent(GameWorld._Instance.Content);
            //slime.AddComponent(new Collider(slime, false, 0.5f));
            buildobject = enemy;
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return buildobject;
        }
    }
}
