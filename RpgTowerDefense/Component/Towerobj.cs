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
    enum AttackType { heavy, Light, Tesla, fragment}
    #endregion
    class Towerobj : Component, ILoadable, IUpdate, IAnimateable
    {
        #region Fields And Properties
        private float attackPower; // how much damage a projectile fired by the tower will inflict.
        private float attackSpeed; // how fast the tower will fire a projectile.
        private float attackRadius; // determins the search range of the tower.
        private AttackType attackType; // field that determins the type of attack the tower will use.
        private GameObject target; // the enemy the tower will shoot after when cooldown is 0.

        //fields for 
        private byte aPUpgradeLvl;
        private byte aSUpgradeLvl;
        private byte aRUpgradeLvl;

        private byte aPUpgradeCap; //AttackPower UpgradeCap
        private byte aSUpgradeCap; //AttackSpeed UPgradeCap
        private byte aRUpgradeCap; //AttackRange UpgradeCap
        private bool aPCapped;
        private bool aSCapped;
        private bool aRCapped;


        public float AttackPower { get => attackPower;}
        public float AttackSpeed { get => attackSpeed;}
        public float AttackRadius { get => attackRadius;}
        internal AttackType AttackType { get => attackType; set => attackType = value; }
        internal GameObject Target { get => target; set => target = value; }
        public byte APUpgradeLvl { get => aPUpgradeLvl;}
        public byte ASUpgradeLvl { get => aSUpgradeLvl;}
        public byte ARUpgradeLvl { get => aRUpgradeLvl;}
        public bool APCapped { get => aPCapped;}
        public bool ASCapped { get => aSCapped;}
        public bool ARCapped { get => aRCapped;}
        

        private float coolDown;
        #endregion
        #region Constructor
        public Towerobj(GameObject gameObject, float attackPower, float attackSpeed, AttackType attackType, float attackRadius) : base(gameObject)
        {
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
            this.attackType = attackType;
            this.attackRadius = attackRadius;
            coolDown = 0;
            DatabaseData._Instance.TotalTowersPlaced += 1;

            // limt upgrades
            // sets base lvl to one for all upgrades
            aPUpgradeLvl = 1; 
            aSUpgradeLvl = 1;
            aRUpgradeLvl = 1;
            aPCapped = false;
            aSCapped = false;
            aRCapped = false;
            aPUpgradeCap = 10;
            aSUpgradeCap = 10;
            aRUpgradeCap = 10;
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


        /// <summary>
        /// when called will upgrade one of the towers atributes to a certain lvl
        /// </summary>
        /// <param name="upgradetype"></param>
        public void UpgradeTower(int upgradetype)
        {
            if (upgradetype == 1)// upgrade Tower AttackPower with 1 damage
            {
                attackPower++;
                aPUpgradeLvl++;
                if (APUpgradeLvl >= aPUpgradeCap)
                {
                    aPCapped = true;
                }
            }
            else if (upgradetype == 2) // upgrade Tower AttackSpeed with 1
            {
                attackSpeed++;
                aSUpgradeLvl++;
                if (ASUpgradeLvl >= aSUpgradeCap)
                {
                    aSCapped = true;
                }
            }
            else if (upgradetype == 3) // upgrade Tower Range with 50 px
            {
                attackRadius += 50;
                aRUpgradeLvl++;
                if (ARUpgradeLvl >= aRUpgradeCap)
                {
                    aRCapped = true;
                }
            }
            else // pass nothing
            {

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
        //create tower animation
        public void SellTower()
        {
            int sellValue = (40 * aPUpgradeLvl * ASUpgradeLvl * ARUpgradeLvl) / 3;

            GameWorld._Instance.PlayerGold += sellValue;
            GameWorld._Instance.RemoveGameObjects.Add(this.gameObject);

        }
        private void CreateAnimation()
        {

        }
        public void OnAnimationDone(string animationName)
        {
            
        }
        #endregion
    }

}
