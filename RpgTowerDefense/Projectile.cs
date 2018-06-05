using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Projectile : Component, IUpdate, ICollideEnter
    {
        private Vector2 directionVector;
        private int damage;
        private int decay;
        private int lifetime;
        private AttackType attackType;
        public Projectile(GameObject gameObject, int damage, Vector2 directionVector) : base(gameObject)
        {
            this.Damage = damage;
            this.directionVector = directionVector;
            lifetime = 200;
            decay = 0;
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
            if (decay > lifetime)
            {
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
                Collider collider = gameObject.GetComponent("Collider") as Collider; 
                GameWorld._Instance.Colliders.Remove(collider);
            }
            gameObject.Transform.Translate(directionVector * 5);
            decay++;
        }
    }
}
