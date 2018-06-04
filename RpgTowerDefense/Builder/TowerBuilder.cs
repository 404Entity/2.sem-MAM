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
                tower.AddComponent(new Tower(tower, 1, 4, AttackType.Light, 1000));
            }
            else if (id == 2)
            {
                tower.AddComponent(new Tower(tower, 2, 2, AttackType.heavy, 100));
            }
            else if (id == 3)
            {
                tower.AddComponent(new Tower(tower, 3, 5, AttackType.Tesla, 100));
            }
            else
            {
                tower.AddComponent(new Tower(tower, 1, 1, AttackType.Light, 1));
            }
            tower.LoadContent(GameWorld._Instance.Content);

            buildObject = tower;
            SpriteRenderer sp = tower.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.GetStaticRectangle();
            //sp.Origin = new Vector2((tower.Transform.Position.X + (sp.Sprite.Width * sp.Scale)) , (tower.Transform.Position.Y + (sp.Sprite.Height * sp.Scale)));
            sp.Origin = new Vector2(95, sp.Sprite.Height / 2);
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
