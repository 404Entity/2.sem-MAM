using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class TowerBuilder : IBuilder
    {
        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {

        }

        public void BuildGameObject(Vector2 position, int id)
        {
            GameObject tower = new GameObject();
            tower.AddComponent(new Transform(tower, position));
            tower.AddComponent(new SpriteRenderer(tower, "tower_01", 1, 0.2f));
            //tower.AddComponent(new Animator(tower));
            if (id == 1)
            {
                tower.AddComponent(new Tower(tower,5,4,AttackType.Light,10));
            }
            else if (id == 2)
            {
                tower.AddComponent(new Tower(tower,10,2,AttackType.heavy,10));
            }
            else if (id == 3)
            {
                tower.AddComponent(new Tower(tower, 3, 5, AttackType.Tesla, 10));
            }
            else
            {
                tower.AddComponent(new Tower(tower, 1, 1, AttackType.Light, 1));
            }
            tower.LoadContent(GameWorld._Instance.Content);

            buildObject = tower;
            SpriteRenderer sp = tower.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.Rectangle = new Rectangle((int)position.X,(int)position.Y,300,300);
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
