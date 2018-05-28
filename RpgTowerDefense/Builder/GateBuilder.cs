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
            mainGate.AddComponent(new SpriteRenderer(mainGate, "Gate", 1, 0.2f));
            //mainGate.AddComponent(new Animator(mainGate));
            mainGate.AddComponent(new MainGate(mainGate));
            mainGate.AddComponent(new Collider(mainGate, false, 0.2f));
            mainGate.LoadContent(GameWorld._Instance.Content);
            SpriteRenderer sp = mainGate.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.GetStaticRectangle();
            buildObject = mainGate;
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
