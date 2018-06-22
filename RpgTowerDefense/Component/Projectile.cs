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
        private float damage;
        private int decay;
        private int lifetime;
        private int bounces;
        private AttackType attackType;
        public Projectile(GameObject gameObject, float damage,AttackType attackType, Vector2 directionVector, int lifetime) : base(gameObject)
        {
            this.Damage = damage;
            this.directionVector = directionVector;
            this.AttackType = attackType;
            this.lifetime = lifetime;
            decay = 0;
            Bounces = 0;
        }

        public float Damage { get => damage; set => damage = value; }
        public int Bounces { get => bounces; set => bounces = value; }
        public Vector2 DirectionVector { get => directionVector; set => directionVector = value; }
        internal AttackType AttackType { get => attackType; set => attackType = value; }


        //Remove this and its Interface ?
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
            if (bounces == 5)
            {
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
                Collider collider = gameObject.GetComponent("Collider") as Collider;
                GameWorld._Instance.Colliders.Remove(collider);
            }

            if (decay > lifetime)
            {
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
                Collider collider = gameObject.GetComponent("Collider") as Collider; 
                GameWorld._Instance.Colliders.Remove(collider);
            }

            if (attackType == AttackType.fragment)
            {
                gameObject.Transform.Translate(directionVector * 2);
            }
            else if (attackType == AttackType.Tesla)
            {
                gameObject.Transform.Translate(directionVector * 5);
            }
            else
            {
                gameObject.Transform.Translate(directionVector * 10);
            }
          
            decay++;
        }
    }
}
