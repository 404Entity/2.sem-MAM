using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Projectile : Component, IUpdate,ICollideEnter,ICollideStay,ICollideExit
    {
       private Vector2 directionVector;
       private int damage;
       private int health;

       public Projectile(GameObject gameObject,int damage,Vector2 directionVector): base(gameObject)
        {
            this.damage = damage;
            this.directionVector = directionVector;
        }
        public int Health { get => health; set => health = value; }

        public void OnCollisionEnter(Collider other)
        {
            if ( (Enemy)other.GameObject.GetComponent("Enemy") is null)
            {
                throw new NotImplementedException();
            }
            else
            {
                Enemy target = (Enemy)other.GameObject.GetComponent("Enemy");
                target.Health -= damage;
            }
         
        }

        public void OnCollisionExit(Collider other)
        {
            //removes the bullet on collinsion+
            GameWorld._Instance.RemoveGameObjects.Add(gameObject);
        }

        public void OnCollisionStay(Collider other)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            gameObject.Transform.Translate(directionVector * 2);
        }
    }
}
