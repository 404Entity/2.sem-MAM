using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Player
    {
        #region Fields and Properties
        private int health;
        private List<object> Equipment;
        private float movementSpeed;
        private float attackPower;
        private float attackSpeed;

        public int Health { get => health; set => health = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public List<object> Equipment1 { get => Equipment; set => Equipment = value; }
        public float AttackPower { get => attackPower; set => attackPower = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        #endregion
    }
}
