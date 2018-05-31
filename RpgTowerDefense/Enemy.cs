﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Threading;

namespace RpgTowerDefense
{
    class Enemy : Component, ILoadable, IAnimateable, IUpdate, ICollideEnter
    {
        #region Fields
        GameWorldBuilder worldBuilder;

        private Animator animator;
        private IStrategy strategy;
        private DIRECTION direction;
        //Decides move direction
        Vector2 OpVector = new Vector2(0, -1);
        Vector2 NedVector = new Vector2(0, 1);
        Vector2 HojreVector = new Vector2(1, 0);
        UI uI = new UI();
        //Decides move direction
        //Speed of enemy
        int threadSleep = 20;
        //Speed of enemy
        bool threadStarted = false;

        //size of tiles, used to scale size of enemy
        int TileSize;
        //used to find and save destinations for pathing
        int walkIndex;
        Vector2 moveTarget;

        public int Health { get; internal set; }
        #endregion
        #region Constructor
        public Enemy(GameObject gameobject) : base(gameobject)
        {
            worldBuilder = GameWorld._Instance.worldBuilder;

            animator = (gameobject.GetComponent("Animator")as Animator);

            //Sets pathing destination as the first saved coordinate in GameWorld
            moveTarget = GameWorld._Instance.walkCoordinates[0];
            //makes enemy spawn on edge of screen on same y coordinate as first pathing destination
            gameObject.Transform.Position = new Vector2 (-TileSize,moveTarget.Y);

            TileSize = (int)worldBuilder.xWidth;
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
            if (strategy is Walk)
            {

            }
            if (strategy is Idle)
            {

            }
            if (strategy is Attack)
            {

            }

            if(gameObject.Transform.Position == moveTarget)
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

        public void OnCollisionEnter(Collider other)
        {
            Collider collider = (Collider)gameObject.GetComponent("Collider");
            if ((Enemy)other.GameObject.GetComponent("Enemy") != null || (Enemy)other.GameObject.GetComponent("Scissor") != null)
            {
                GameWorld._Instance.RemoveGameObjects.Add(other.GameObject);
            }



        }
        #endregion
    }
}