using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Threading;

namespace RpgTowerDefense
{
    class EnemyMine : Component, ILoadable, IAnimateable, IUpdate, ICollideEnter, ICollideStay, ICollideExit
    {
        #region Fields
        GameWorldBuilder worldBuilder;

        Player playerScript;

        private Animator animator;
        private IStrategy strategy;

        //dmg = Damage of enemy
        //PointGain = amount of points gained for killing an enemy
        //GoldGain = Amount of gold gained for killing an enemy
        //threadSleep = Speed of enemy
        bool mineThreadStarted = false;
        int dmg, pointGain, goldGainOnKill, health, threadSleep = 20;
        float attackCooldown = 0, attackSpeed = 0, attackRange = 15, speed, lookRange;
        GameObject player;
        Vector2 waitPos;

        //size of tiles, used to scale size of enemy
        int TileSize;
        //used to find and save destinations for pathing

        Vector2 moveTarget;

        public int Health { get; internal set; }
        public int Dmg { get => dmg; set => dmg = value; }
        #endregion
        #region Constructor
        public EnemyMine(GameObject gameobject, GameObject player) : base(gameobject)
        {
            worldBuilder = GameWorld._Instance.worldBuilder;
            this.player = player;

            animator = (gameobject.GetComponent("Animator") as Animator);

            waitPos = new Vector2(3950, (GameWorld._Instance.GraphicsDevice.Viewport.Height / 2) - (animator.SpriteRenderer.Rectangle.Height / 2));
            moveTarget = waitPos;
            attackCooldown = 1.5f;

            TileSize = (int)worldBuilder.xWidth;
            dmg = 1;
            pointGain = 5;
            goldGainOnKill = 5;

            health = 3;
            this.Health = health;
        }
        #endregion
        #region Methods
        public void LoadContent(ContentManager Content)
        {
            CreateAnimation();
        }
        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, TileSize * 2, TileSize * 2, 0, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 1, TileSize * 2, TileSize * 2, 0, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 2, TileSize * 2, TileSize * 2, 0, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 3, TileSize * 2, TileSize * 2, 0, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(4, 25, 0, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(4, 25, 4, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(4, 50, 0, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(4, 50, 4, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("DieBack", new Animation(4, 75, 0, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("DieFront", new Animation(4, 75, 4, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(4, 100, 0, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(4, 100, 4, TileSize * 2, TileSize * 2, 5, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }

        public void OnAnimationDone(string animationName)
        {

        }

        public void Update()
        {
            if (player == null)
            {

            }

            if (Vector2.Distance(player.Transform.Position, gameObject.Transform.Position) <= lookRange && player.Transform.Position.X >= 3200)
            {
                 moveTarget = player.Transform.Position;
            }
            else
            {
                 moveTarget = waitPos;
            }
            moveTarget = waitPos;

            if (Health <= 0)
            {
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
                //Giver spilleren points når en enemy dør
                //GameWorld._Instance.HighScore += pointGain;
                //Giver spilleren guld hver gang en enemy dør
                //GameWorld._Instance.PlayerGold += goldGainOnKill;
            }
            
            if (Vector2.Distance(player.Transform.Position, gameObject.Transform.Position) <= attackRange && attackCooldown <= 0)
            {
                Attack();
                attackCooldown = attackSpeed;
            }
            
            //Enemy Movement Thread
            if (mineThreadStarted == false)
            {
                ThreadPool.QueueUserWorkItem(EnemyMovement);
                mineThreadStarted = true;
            }
        }

        void Attack()
        {
        }

        #region Collision
        /// <summary>
        /// When a enemy is hit by a bullet lose health and remove the bullet
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {
            if ((Projectile)other.GameObject.GetComponent("Projectile") != null)
            {
                Projectile dmgObject = (Projectile)other.GameObject.GetComponent("Projectile");
                //this.Health -= dmgObject.Damage;
                GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
                GameWorld._Instance.Colliders.Remove(other);
            }
        }
        /// <summary>
        /// not implementet yet
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionExit(Collider other)
        {

        }
        /// <summary>
        /// not implementet
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {

        }
        #endregion


        //Enemy Movement Method
        public void EnemyMovement(Object stateInfo)
        {
            while (true)
            {
                //calculates distance between enemy and destination
                Vector2 moveVector = moveTarget - gameObject.Transform.Position;
                if (moveVector.Length() >= 1f)
                {
                    //normalizes the move vector if the distance is longer than 1
                    moveVector = Vector2.Normalize(moveVector);
                }
                //moves based on moveVector
                gameObject.Transform.Translate(moveVector);
                Thread.Sleep(threadSleep);
            }

        }
        #endregion
    }
}