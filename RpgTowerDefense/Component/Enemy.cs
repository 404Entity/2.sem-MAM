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
    class Enemy : Component, ILoadable, IAnimateable, IUpdate, ICollideEnter, ICollideStay, ICollideExit
    {
        #region Fields
        GameWorldBuilder worldBuilder;

        private Animator animator;
        private IStrategy strategy;

        //dmg = Damage of enemy
        //PointGain = amount of points gained for killing an enemy
        //GoldGain = Amount of gold gained for killing an enemy
        //threadSleep = Speed of enemy
        bool threadStarted = false, enemyMovementBool = true;
        int dmg, pointGain, goldGainOnKill, threadSleep;

        float speedModifier;
        float baseSpeed = 10;

        //size of tiles, used to scale size of enemy
        int TileSize;
        //used to find and save destinations for pathing
        int walkIndex;
        Vector2 moveTarget;

        public int Health { get; internal set; }
        public int Dmg { get => dmg; set => dmg = value; }
        #endregion
        #region Constructor
        public Enemy(GameObject gameobject, int dmg, int threadSleep, int health, int pointGain, int goldGainOnKill) : base(gameobject)
        {
            worldBuilder = GameWorld._Instance.worldBuilder;

            animator = (gameobject.GetComponent("Animator") as Animator);

            //Sets pathing destination as the first saved coordinate in GameWorld
            moveTarget = GameWorld._Instance.walkCoordinates[0];
            //makes enemy spawn on edge of screen on same y coordinate as first pathing destination
            TileSize = (int)worldBuilder.xWidth;
            if (gameObject.Transform.Position.X == 0)
            {
                gameObject.Transform.Position = new Vector2(-TileSize, moveTarget.Y);
            }
            Health = GameWorld._Instance.waveManager.addedHealth;
            speedModifier = GameWorld._Instance.waveManager.speedMod;
            this.dmg = dmg;
            this.threadSleep = threadSleep;
            this.pointGain = pointGain;
            this.goldGainOnKill = goldGainOnKill;
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
            animator.CreateAnimation("WalkBack", new Animation(7, 25, 0, 550, 650, 7, Vector2.Zero));
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
            if (Health <= 0)
            {
                GameWorld._Instance.RemoveGameObjects.Add(gameObject);
                Collider collider = gameObject.GetComponent("Collider") as Collider;
                GameWorld._Instance.Colliders.Remove(collider);
                //Giver spilleren points når en enemy dør
                GameWorld._Instance.HighScore += pointGain;
                //Giver spilleren guld hver gang en enemy dør
                GameWorld._Instance.PlayerGold += goldGainOnKill;
                enemyMovementBool = false;
            }
            if (strategy is Walk)
            {

            }
            if (strategy is Idle)
            {

            }
            if (strategy is Attack)
            {

            }

            if (gameObject.Transform.Position == moveTarget)
            {
                walkIndex++;
                moveTarget = GameWorld._Instance.walkCoordinates[walkIndex];
            }

            //Enemy Movement Thread
            if (threadStarted == false)
            {
                ThreadPool.QueueUserWorkItem(EnemyMovement);
                threadStarted = true;
            }
        }

        #region Collision
        /// <summary>
        /// When a enemy is hit by a bullet lose health and remove the bullet
        /// </summary>
        /// <param name="other">any other collider in the collider list</param>
        public void OnCollisionEnter(Collider other)
        {
            if ((Projectile)other.GameObject.GetComponent("Projectile") != null)
            {

                Projectile dmgObject = (Projectile)other.GameObject.GetComponent("Projectile");
                if (dmgObject.AttackType == AttackType.heavy)
                {
                    SpriteRenderer sp = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
                    Director director = new Director(new BulletBuilder());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale) , gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(-1, 0), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale) , gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(1, 0), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(0, -1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(0, 1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(-1, 1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(1, -1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(-1, -1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    director.Construct(new Vector2(gameObject.Transform.Position.X + ((sp.Rectangle.Width / 2) * sp.Scale), gameObject.Transform.Position.Y + ((sp.Rectangle.Height / 2) * sp.Scale)), 2, new Vector2(1, 1), 2, AttackType.fragment);
                    GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
                    this.Health -= (int)dmgObject.Damage;
                    GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
                    GameWorld._Instance.Colliders.Remove(other);

                }
                else if (dmgObject.AttackType == AttackType.fragment)
                {
                    this.Health -= (int)dmgObject.Damage;
                }
                else if (dmgObject.AttackType == AttackType.Light)
                {
                    this.Health -= (int)dmgObject.Damage;
                    GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
                    GameWorld._Instance.Colliders.Remove(other);
                }
                else if (dmgObject.AttackType == AttackType.Tesla)
                {
                    GameObject bounceTarget = this.gameObject;
                    foreach (GameObject enemy in GameWorld._Instance.MobList)
                    {
                        if (enemy != this.gameObject)
                        {
                            if (Vector2.Distance(enemy.Transform.Position, this.gameObject.Transform.Position) < 50)
                            {
                                bounceTarget = enemy;
                                Vector2 shootdirection = bounceTarget.Transform.Position - gameObject.Transform.Position;
                                dmgObject.DirectionVector = Vector2.Normalize(shootdirection);
                                dmgObject.Bounces++;
                                break;
                            }
                        }
                    }
                }



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
            while (enemyMovementBool == true)
            {
                //calculates distance between enemy and destination
                Vector2 moveVector = moveTarget - gameObject.Transform.Position;
                if (moveVector.Length() >= 1f)
                {
                    //normalizes the move vector if the distance is longer than 1
                    moveVector = Vector2.Normalize(moveVector);
                }
                //moves based on moveVector
                gameObject.Transform.Translate(moveVector * speedModifier);
                animator.PlayAnimation("WalkBack");
                Thread.Sleep(threadSleep);
            }

        }
        #endregion
    }
}