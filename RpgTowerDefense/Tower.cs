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

        private int coolDown;
        #endregion
        #region Constructor
        public Tower(GameObject gameObject, float attackpower, float attackspeed, AttackType attackType, float attackRadius) : base(gameObject)
        {
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
            AttackType = attackType;
            AttackRadius = attackRadius;
            coolDown = 0;
        }
        #endregion
        public void FindTarget()
        {

            foreach (GameObject enemy in GameWorld._Instance.MobList)
            {
                if (Vector2.Distance(enemy.Transform.Position, this.gameObject.Transform.Position) < AttackRadius)
                {
                    target = enemy;
                    break;
                }
            }
        }
        public void checkTarget()
        {

            foreach (GameObject enemy in GameWorld._Instance.MobList)
            {
                if (enemy == target)
                {
                 break;
                }
                else
                {
                    target = null;
                }
            }
        }
        public void TowerAttack()
        {
            if (target != null)
            {
                Vector2 shootdirection = target.Transform.Position - gameObject.Transform.Position;
                Vector2 shootdirectonnormalized = Vector2.Normalize(shootdirection);
                Director director = new Director(new BulletBuilder());
                director.Construct(gameObject.Transform.Position, 1, shootdirectonnormalized);
                GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                coolDown += 100;
            }

        }
        public void LookAttarget()
        {
            if (target != null)
            {
                SpriteRenderer sp = gameObject.GetComponent("spriteRenderer") as SpriteRenderer;
                Vector2 cannonPosition = new Vector2(sp.Sprite.Width - 20, sp.Sprite.Height / 2);
                sp.Rotation += (float)GetAngle(cannonPosition, target.Transform.Position);
            }
        }
        private double GetAngle(Vector2 a, Vector2 b)
        {

            return Math.Atan2(b.Y - a.Y, b.X - a.X);
        }
        public void LoadContent(ContentManager content)
        {

        }
        /// <summary>
        ///  runs various checks in order to find a shoot targets
        /// </summary>
        public void Update()
        {
            if (target == null)
            {
                FindTarget();
            }
            else
            {
                checkTarget();
            }
            if (coolDown != 0)
            {
                coolDown -= 1;
            }
            else
            {
                LookAttarget();
                TowerAttack();
            }
        }
        public void spin()
        {
            SpriteRenderer sp = gameObject.GetComponent("spriteRenderer") as SpriteRenderer;
            sp.Rotation += 0.05f;
        }
        public void Upgrade(int param)
        {
            //psudo kode
            if (param == 1)
            {
                attackPower += 1;
            }
            else if (param == 2)
            {
                attackSpeed += 1;
            }
            else if (param == 3)
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
