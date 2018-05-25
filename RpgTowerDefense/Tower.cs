using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace RpgTowerDefense
{
    enum AttackType { heavy, Light, Tesla }
    class Tower : Component, ILoadable, IUpdate, IAnimateable
    {
        #region Fields And Properties
        private float attackPower;
        private float attackSpeed;
        private float attackRadius;
        private AttackType attackType;
        private Enemy target;

        public float AttackPower { get => attackPower; set => attackPower = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRadius { get => attackRadius; set => attackRadius = value; }
        internal AttackType AttackType { get => attackType; set => attackType = value; }
        internal Enemy Target { get => target; set => target = value; }
        #endregion
        #region Constructor
        public Tower(GameObject gameObject, float attackpower, float attackspeed, AttackType attackType, float attackRadius): base(gameObject)
        {
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
            AttackType = attackType;

        }
        #endregion
        public void FindTarget()
        {

            if (target == null)
            {

            }
        }
        public void TowerAttack(Enemy target)
        {
            Director director = new Director(new BulletBuilder());
            director.Construct(gameObject.Transform.Position);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {
            string varstring = "hello";

        }

        public void Upgrade(int param)
        {
            //psudo kode
            if (param ==1)
            {
                attackPower += 1;
            }else if (param == 2)
            {
                attackSpeed += 1;
            }else if (param == 3)
            {
                attackRadius += 1;
            }
        }
        private void CreateAnimation()
        {

        }
        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }
    }
}
