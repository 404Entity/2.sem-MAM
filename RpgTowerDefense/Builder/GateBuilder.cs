using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class GateBuilder
    {

        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {

        }

        public void BuildGameObject(Vector2 position, int id)
        {
            GameObject tower = new GameObject();
            tower.AddComponent(new Transform(tower, position));
            tower.AddComponent(new SpriteRenderer(tower, "Tower", 1, 0.5f));
            tower.AddComponent(new Animator(tower));
            if (id == 1)
            {
                tower.AddComponent(new Tower(tower, 5, 4, AttackType.Light, 10));
            }
            else
            {
                tower.AddComponent(new Tower(tower, 10, 2, AttackType.heavy, 10));
            }
            tower.LoadContent(GameWorld._Instance.Content);
            //slime.AddComponent(new Collider(slime, false, 0.5f));
            buildObject = tower;
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
