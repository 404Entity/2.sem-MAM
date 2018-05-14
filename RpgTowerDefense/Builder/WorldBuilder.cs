using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace T_L_O_B_O
{
    class WorldBuilder : IBuilder
    {
        private GameObject buildobject;
        public void BuildGameObject(Vector2 position, int map_Id)
        {
            GameObject worldBuilder = new GameObject();
            worldBuilder.AddComponent(new Transform(worldBuilder, position));
            worldBuilder.AddComponent(new SpriteRenderer(worldBuilder, "Map"+ map_Id.ToString(), 1, 1));
            //SpriteRenderer test = (SpriteRenderer)worldBuilder.GetComponent("SpriteRenderer");
            worldBuilder.LoadContent(GameWorld.Instance.Content);
            worldBuilder.AddComponent(new Collider(worldBuilder, false, 1));
            buildobject = worldBuilder;
        }

        public void BuildGameObject(Vector2 position)
        {
           
        }

        public GameObject GetResult()
        {
            return buildobject;
        }
    }
}
