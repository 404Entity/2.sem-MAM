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


        #endregion
        #region Property

        

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
                    BuildTower();
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
                if (keyState.IsKeyDown(Keys.E))
                {
                    //attack stuff
                    strategy = new Attack(animator);
                    canMove = false;
                }

                previousMouseState = mouseState;
                strategy.Execute(direction);
            }
        }
        public void CreateAnimation()
        {
            animator.CreateAnimation("WalkLeft", new Animation(8, 0, 0, 340, 436, 8, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(8, 450, 0, 340, 436, 8, Vector2.Zero));
            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 340, 436, 6, Vector2.Zero));
            animator.CreateAnimation("IdleBack", new Animation(1, 0, 4, 340, 436, 6, Vector2.Zero));
            animator.CreateAnimation("IdleLeft", new Animation(1, 0, 0, 340, 436, 1, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(1, 450, 0, 340, 436, 1, Vector2.Zero));
            animator.CreateAnimation("WalkFront", new Animation(1, 150, 0, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("WalkBack", new Animation(4, 150, 4, 90, 150, 6, Vector2.Zero));
            animator.CreateAnimation("AttackFront", new Animation(9, 1890, 0, 414, 460, 9, new Vector2(10, 0)));
            animator.CreateAnimation("AttackBack", new Animation(9, 1890, 0, 414, 460, 9, new Vector2(10, 0)));
            animator.CreateAnimation("AttackRight", new Animation(9, 2388, 0, 414, 460, 9, Vector2.Zero));
            animator.CreateAnimation("AttackLeft", new Animation(9, 1890, 0, 414, 460, 9, new Vector2(10, 0)));
            animator.CreateAnimation("JumpFront", new Animation(9, 930, 0, 363, 436, 9, Vector2.Zero));
            animator.CreateAnimation("JumpBack", new Animation(9, 930, 0, 363, 436, 9, Vector2.Zero));
            animator.CreateAnimation("JumpLeft", new Animation(9, 940, 0, 363, 436, 9, Vector2.Zero));
            animator.CreateAnimation("JumpRight", new Animation(9, 1400, 0, 363, 436, 9, Vector2.Zero));
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
            Vector2 cursorPosition = new Vector2(Mouse.GetState().Position.X,Mouse.GetState().Position.Y);
            Vector2 shootdirection = cursorPosition - gameObject.Transform.Position;
            Vector2 shootdirectonnormalized = Vector2.Normalize(shootdirection);
            Director director = new Director(new BulletBuilder());
            director.Construct(gameObject.Transform.Position, 1, shootdirectonnormalized);
            GameWorld._Instance.AddGameObjects.Add(director.Builder.GetResult());
        }

        
        #endregion
    }
}
