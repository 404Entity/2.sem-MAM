using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace T_L_O_B_O
{
    class ChestBuilder : IBuilder
    {
        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject Chest = new GameObject();
            Chest.AddComponent(new Transform(Chest, position));
            Chest.AddComponent(new SpriteRenderer(Chest, "ChestImage", 1, 0.5f));
            Chest.AddComponent(new ChestOfAThousandGrogs(Chest));
            Chest.LoadContent(GameWorld.Instance.Content);
            Chest.AddComponent(new Collider(Chest, true, 0.5f));
            GameWorld.Instance.AddList.Add(Chest);
            buildObject = Chest;
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
