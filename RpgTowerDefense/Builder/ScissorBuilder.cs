using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace T_L_O_B_O
{
    class ScissorBuilder : IBuilder
    {
        private GameObject buildObject;



        public void BuildGameObject(Vector2 position)
        {
            GameObject Scissor = new GameObject();
            Scissor.AddComponent(new Transform(Scissor, position));
            Scissor.AddComponent(new SpriteRenderer(Scissor, "Scissor_sheet", 1, 0.1f));
            Scissor.AddComponent(new Animator(Scissor));
            Scissor.AddComponent(new Scissor(Scissor));
            Scissor.LoadContent(GameWorld.Instance.Content);
            Scissor.AddComponent(new Collider(Scissor, false, 0.1f));
            buildObject = Scissor;
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
