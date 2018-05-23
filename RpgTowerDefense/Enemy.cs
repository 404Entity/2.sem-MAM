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
    class Enemy : Component, ILoadable, IAnimateable, IUpdate
    {
        #region Fields
        private Animator animator;
        private IStrategy strategy;
        private DIRECTION direction;
        //Decides move direction
        Vector2 OpVector = new Vector2(0, -1);
        Vector2 NedVector = new Vector2(0, 1);
        Vector2 HojreVector = new Vector2(1, 0);
        //Decides move direction
        //Speed of enemy
        int threadSleep = 20;
        //Speed of enemy
        bool threadStarted = false;
        #endregion
        #region Constructor
        public Enemy(GameObject gameobject) : base(gameobject)
        {
            animator = (gameobject.GetComponent("Animator")as Animator);
        }
        #endregion
        #region Methods
        public void LoadContent(ContentManager Content)
        {
            CreateAnimation();
        }
        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 1, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 2, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 3, 100, 100, 0, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(4, 100, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(4, 100, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkLeft", new Animation(4, 200, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(4, 200, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieBack", new Animation(4, 300, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieFront", new Animation(4, 300, 4, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(4, 400, 0, 100, 100, 5, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(4, 400, 4, 100, 100, 5, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }

        public void OnAnimationDone(string animationName)
        {
           
        }

        public void Update()
        {
            if (strategy is Walk)
            {

            }
            if (strategy is Idle)
            {

            }
            if (strategy is Attack)
            {

            }


            
            //Enemy Movement Thread
            if (threadStarted == false)
            {
                ThreadPool.QueueUserWorkItem(EnemyMovement);
                threadStarted = true;
            }
        }
        
        //Enemy Movement Method
        public void EnemyMovement(Object stateInfo)
        {
            while (true)
            {
                //Bevæger sig til hojre
                gameObject.Transform.Translate(HojreVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(115, 280))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig op
                gameObject.Transform.Translate(OpVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(115, 115))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig til hojre
                gameObject.Transform.Translate(HojreVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(282, 115))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig ned
                gameObject.Transform.Translate(NedVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(282, 340))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig til hojre
                gameObject.Transform.Translate(HojreVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(507, 340))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig Op 
                gameObject.Transform.Translate(OpVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(507, 226))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                //Bevæger sig til hojre
                gameObject.Transform.Translate(HojreVector);
                Thread.Sleep(threadSleep);
                if (gameObject.Transform.Position == new Vector2(750, 226))
                {
                    //Breaker hvis punktet er ramt
                    break;
                }
            }
            while (true)
            {
                gameObject.Transform.Translate(new Vector2(0, 0));
                Thread.Sleep(threadSleep);

            }

        }
        #endregion
    }
}