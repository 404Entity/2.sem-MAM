using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Projectile : Component, IUpdate,ICollideEnter,ICollideStay,ICollideExit
    {
        private int damage;
       public Projectile(GameObject gameObject,int damage): base(gameObject)
        {
            this.damage = damage;
        }

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
            throw new NotImplementedException();
        }
    }
}
