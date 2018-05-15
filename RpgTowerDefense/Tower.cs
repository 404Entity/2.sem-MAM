using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    enum AttackType {heavy,Light,Tesla}
    class Tower
    {
        #region Fields And Properties
        private float attackPower;
        private float attackSpeed;
        private AttackType attackType;
        private Enemy target;

        public float AttackPower { get => attackPower; set => attackPower = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        internal AttackType AttackType { get => attackType; set => attackType = value; }
        internal Enemy Target { get => target; set => target = value; }
        #endregion
        public Tower(float attackpower,float attackspeed,AttackType attackType)
        {

        }
    }
}
