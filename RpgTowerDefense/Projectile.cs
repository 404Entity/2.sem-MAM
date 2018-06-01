using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Projectile : Component, IUpdate,ICollideEnter
    {
       private Vector2 directionVector;
       private int damage;
   
       public Projectile(GameObject gameObject,int damage,Vector2 directionVector): base(gameObject)
        {
            this.Damage = damage;
            this.directionVector = directionVector;
        }

        public int Damage { get => damage; set => damage = value; }

        public void OnCollisionEnter(Collider other)
        {
            if ((Enemy)other.GameObject.GetComponent("Enemy") != null)
            {
                GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
            }
            else
            {
                Collider collider = (Collider)gameObject.GetComponent("Collider");

                
            }
         
        }

        public void Update()
        {
            gameObject.Transform.Translate(directionVector * 5);
            
        }
    }
}
