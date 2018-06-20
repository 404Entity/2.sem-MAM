using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    enum DIRECTION {  Right, Left, Up, Down };
    class Player : Component, IUpdate, ILoadable, IAnimateable
    {
        
        #region Fields
        private float speed;
        private Animator animator;
        private IStrategy strategy;
        private DIRECTION direction;
        private bool canMove;
        private MouseState previousMouseState;
        private float health;




        #endregion
        #region Property
        public float Health { get => health; set => health = value; }

        #endregion
        #region Constructor
        public Player(GameObject gameobject) : base(gameobject)
        {
            speed = 100;
            animator = (gameobject.GetComponent("Animator") as Animator);
            canMove = true;
        }
        #endregion
        #region Methods
        public void LoadContent(ContentManager Content)
        {
            CreateAnimation();
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();
            if (canMove)
            {
                if (keyState.IsKeyDown(Keys.F))
                {
                    if (GameWorld._Instance.PlayerGold > 50)
                    {
                        GameWorld._Instance.PlayerGold -= 50;
                        BuildTower();
                    }
                    
                }
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    Shoot();
                }
                if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.A))
                {
                    Vector2 translation = Vector2.Zero;

                   
                    if (keyState.IsKeyDown(Keys.D))

                    {
                        direction = DIRECTION.Right;
                        translation += new Vector2(2f, 0);
                    }

                    else if (keyState.IsKeyDown(Keys.A))
                    {
                        direction = DIRECTION.Left;
                        translation += new Vector2(-2f, 0);
                    }

                    else if (keyState.IsKeyDown(Keys.W))
                    {
                        //direction = DIRECTION.Up;
                        translation += new Vector2(0, -2f);
                    }

                    else if (keyState.IsKeyDown(Keys.S))
                    {
                        //direction = DIRECTION.Down;
                        translation += new Vector2(0, 2f);
                    }

                    if (!(strategy is Walk))
                    {
                        strategy = new Walk(animator, gameObject.Transform, speed);
                    }
                    gameObject.Transform.Translate(translation * GameWorld._Instance.deltaTime * speed);
                }
                else
                {
                    strategy = new Idle(animator);
                    gameObject.Transform.stop();
                }
                previousMouseState = mouseState;
                strategy.Execute(direction);
            }
        }
        public void CreateAnimation()
        {
            animator.CreateAnimation("WalkLeft", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 0,369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(1, 0, 0, 369, 398, 1, Vector2.Zero));
            animator.PlayAnimation("IdleLeft");
        }

        public void OnAnimationDone(string animationName)
        {
            /*
            if (animationName == null)
            {
                animationName = "Idle";
            }
            if (animationName.Contains("Attack"))
            {
                canMove = true;

            }
            if (animationName.Contains("Jump"))
            {
                strategy = null;
            }
            */
        }

        public void BuildTower()
        {
            Director dic = new Director(new TowerBuilder());
            dic.Builder.BuildGameObject(gameObject.Transform.Position, 1);
            GameWorld._Instance.AddGameObjects.Add(dic.Builder.GetResult());
        }
        public void destroyTower()
        {

        }

        public void Shoot()
        {
            Vector2 cursorPosition = new Vector2(Mouse.GetState().Position.X + ((GameWorld._Instance.camera.screenValue-1) * GameWorld._Instance.ScreenWidth),Mouse.GetState().Position.Y);
            Vector2 shootdirection = cursorPosition - gameObject.Transform.Position;
            Vector2 shootdirectonnormalized = Vector2.Normalize(shootdirection);
            Director director = new Director(new BulletBuilder());
            director.Construct(gameObject.Transform.Position, 1, shootdirectonnormalized, 3,AttackType.Light);
            GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
        }

        
        #endregion
    }
}
