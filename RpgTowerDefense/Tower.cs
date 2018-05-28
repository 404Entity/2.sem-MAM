using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
        private GameObject target;

        public float AttackPower { get => attackPower; set => attackPower = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRadius { get => attackRadius; set => attackRadius = value; }
        internal AttackType AttackType { get => attackType; set => attackType = value; }
        internal GameObject Target { get => target; set => target = value; }
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

                foreach (GameObject enemy in GameWorld._Instance.MobList)
                {
                    if (Vector2.Distance(enemy.GameObject.Transform.Position,GameObject.Transform.Position) < AttackRadius)
                    {
                        target = enemy;
                        break;
                    }
                }

            }
        }
        public void TowerAttack()
        {
            if (target != null)
            {
                Vector2 shootdirection = target.GameObject.Transform.Position - gameObject.Transform.Position;
                Vector2 shootdirectonnormalized = Vector2.Normalize(shootdirection);
                Director director = new Director(new BulletBuilder());
                director.Construct(gameObject.Transform.Position, 1, shootdirectonnormalized);
                GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
            }
      
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {
            spin();
            string varstring = "hello";
            TowerAttack();
        }
        public void spin()
        {
            SpriteRenderer sp = gameObject.GetComponent("spriteRenderer") as SpriteRenderer;
            sp.Rotation += 0.05f;
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
