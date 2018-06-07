using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RpgTowerDefense
{
    #region Attacktype Enum
    enum AttackType { heavy, Light, Tesla }
    #endregion
    class Towerobj : Component, ILoadable, IUpdate, IAnimateable
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

        private float coolDown;
        #endregion
        #region Constructor
        public Towerobj(GameObject gameObject, float attackPower, float attackSpeed, AttackType attackType, float attackRadius) : base(gameObject)
        {
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
            this.attackType = attackType;
            AttackRadius = attackRadius;
            coolDown = 0;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Find the first target in the list within tower range
        /// </summary>
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
        /// <summary>
        /// Check if the target still is alive.
        /// </summary>
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

        /// <summary>
        /// shoot bullet at target enemy
        /// </summary>
        public void TowerAttack()
        {
            if (target != null)
            {
                Vector2 shootdirection = target.Transform.Position - gameObject.Transform.Position;
                Vector2 shootdirectonnormalized = Vector2.Normalize(shootdirection);
                Director director = new Director(new BulletBuilder());
                director.Construct(gameObject.Transform.Position, 1, shootdirectonnormalized,AttackPower,AttackType);
                GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                coolDown += 2.5f/attackSpeed;
            }

        }

        /// <summary>
        /// rotate the tower so it look at the enemy.
        /// </summary>
        public void LookAttarget()
        {
            if (target != null)
            {
                SpriteRenderer sp = gameObject.GetComponent("spriteRenderer") as SpriteRenderer;
                Vector2 cannonPosition = new Vector2(sp.Sprite.Width - 20, sp.Sprite.Height / 2);
                sp.Rotation = (float)GetAngle(gameObject.Transform.Position, target.Transform.Position);
            }
        }
        /// <summary>
        /// Claculate the angle between the point a and point b
        /// </summary>
        /// <param name="a">Position Vector of the Tower</param>
        /// <param name="b">Position Vector of the Enemy</param>
        /// <returns>this a returns an decimail value containing the angle between the two vectors</returns>
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
            if (coolDown >= 0)
            {
                coolDown -= GameWorld._Instance.deltaTime;
            }
            else
            {
                LookAttarget();
                TowerAttack();
            }
        }
        //test Metod
        /// <summary>
        /// a basic test metod for testing rotation of a tower to find the best Origin point.
        /// </summary>
        public void spin()
        {
            SpriteRenderer sp = gameObject.GetComponent("spriteRenderer") as SpriteRenderer;
            sp.Rotation += 0.05f;
        }
        private void CreateAnimation()
        {

        }
        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
