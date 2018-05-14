using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace T_L_O_B_O
{
    class PlayerBuilder : IBuilder
    {
        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject Player = new GameObject();            
            Player.AddComponent(new Transform(Player, position));
            Player.AddComponent(new SpriteRenderer(Player, "Dwarf_Sprite_Sheet", 1, 0.3f));
            Player.AddComponent(new Animator(Player));
            Player.AddComponent(new Player(Player));
            Player.AddComponent(new Collider(Player,true,0.3f));
            buildObject = Player;
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
