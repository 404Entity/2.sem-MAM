using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class EnemyMineBuilder : IBuilder
    {
        GameObject player;
        GameObject enemyMine;

        private GameObject buildobject;
        public void BuildGameObject(Vector2 position, GameObject player)
        {
            enemyMine = new GameObject();
            enemyMine.AddComponent(new Transform(enemyMine, position));
            enemyMine.AddComponent(new SpriteRenderer(enemyMine, "Enemy", 1, 0.5f));
            enemyMine.AddComponent(new Animator(enemyMine));
            enemyMine.AddComponent(new EnemyMine(enemyMine, player));
            enemyMine.LoadContent(GameWorld._Instance.Content);
            enemyMine.AddComponent(new Collider(enemyMine, true, 0.5f));
            buildobject = enemyMine;

        }

        public void BuildGameObject(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public void BuildGameObject(Vector2 position, int id, Vector2 direction)
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
