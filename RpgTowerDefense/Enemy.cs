using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense.bin
{
    class Enemy : Component
    {
        #region Fields and Properties
        private float health;
        private float attackPower;
        private float movementSpeed;


        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float AttackPower { get => attackPower; set => attackPower = value; }
        public float Health1 { get => health; set => health = value; }
        #endregion
        public Enemy(float health, float attackPower, float movementSpeed)
        {
            this.health = health;
            this.attackPower = attackPower;
            this.movementSpeed = movementSpeed;
    }
    }
}
