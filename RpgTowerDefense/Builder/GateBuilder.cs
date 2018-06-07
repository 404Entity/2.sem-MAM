using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class GateBuilder: IBuilder
    {

        private GameObject buildObject;

        public void BuildGameObject(Vector2 position)
        {
            GameObject mainGate = new GameObject();
            mainGate.AddComponent(new Transform(mainGate, position));
            mainGate.AddComponent(new SpriteRenderer(mainGate, "CityGate", 1, 0.5f));
            //mainGate.AddComponent(new Animator(mainGate));
            mainGate.AddComponent(new MainGate(mainGate));
            mainGate.LoadContent(GameWorld._Instance.Content);
            SpriteRenderer sp = mainGate.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.GetStaticRectangle();
            mainGate.AddComponent(new Collider(mainGate, true, 0.5f));
            buildObject = mainGate;
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
