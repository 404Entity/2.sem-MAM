using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace T_L_O_B_O
{
    enum DIRECTION {  Right, Left };
    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionStay, IGravity, ICollisionExit, ICollisionEnter
    {
        
        #region Fields
        private float speed;
        private Animator animator;
        private IStrategy strategy;
        private DIRECTION direction;
        private bool canMove;
        private bool isgrounded;

        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
        #endregion
        #region Constructor
        public Player(GameObject gameobject) : base(gameobject)
        {
            speed = 100;
            animator = (gameobject.GetComponent("Animator") as Animator);
            canMove = true;
            isgrounded = false;
        }
        #endregion
        #region Methods
        public void LoadContent(ContentManager Content)
        {
            CreateAnimation();
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (canMove)
            {
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
                    if (!(strategy is Walk) && !(strategy is Jump))
                    {
                        strategy = new Walk(animator, gameObject.Transform, speed);
                    }
                    gameObject.Transform.Translate(translation * GameWorld.Instance.deltaTime * speed);
                }
                else if (!(strategy is Jump))
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
                else if (isgrounded == true)
                {
                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        //jump 
                        strategy = new Jump(animator, this);
                        
                    }
                }
                
                strategy.Execute(direction);
            }
            Fall(isgrounded);
            Jump();
            if (gameObject.Transform.Position.Y > 1000 && gameObject.Transform.Position.Y < 1010)
            {
                Form1 f = new Form1();
                f.Show();
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
        }

        public void OnCollisionStay(Collider other)
        {
            Collider collider = (Collider)gameObject.GetComponent("Collider");
            if ((Enemy)other.GameObject.GetComponent("Enemy") != null || (Scissor)other.GameObject.GetComponent("Scissor") != null) 
            {
                if (strategy is Attack)
                {
                    GameWorld.Instance.RemoveList.Add(other.GameObject);
                }
            }
            else if (collider.CollisionBox.Bottom >= other.CollisionBox.Top && collider.CollisionBox.Bottom - 20 <= other.CollisionBox.Top)
            {
                isgrounded = true;
                gameObject.Transform.Velocity += new Vector2(0, -gameObject.Transform.Velocity.Y);
                gameObject.Transform.CorrectMove(new Vector2(0, other.CollisionBox.Top - collider.CollisionBox.Bottom + 1));
            }
            else if (collider.CollisionBox.Top <= other.CollisionBox.Bottom && collider.CollisionBox.Top + 30 >= other.CollisionBox.Bottom)
            {
                gameObject.Transform.CorrectMove(new Vector2(collider.CollisionBox.Top - other.CollisionBox.Bottom -1, 0));
            }
            else if (collider.CollisionBox.Right >= other.CollisionBox.Left && collider.CollisionBox.Right - 10 <= other.CollisionBox.Left)
            {
                gameObject.Transform.CorrectMove(new Vector2(other.CollisionBox.Left - collider.CollisionBox.Right + 1, 0));
            }
            else if (collider.CollisionBox.Left <= other.CollisionBox.Right && collider.CollisionBox.Left + 10 >= other.CollisionBox.Right)
            {
 
                gameObject.Transform.CorrectMove(new Vector2(other.CollisionBox.Right - collider.CollisionBox.Left, 0));
            }
        }

        public void Fall(bool isgrounded)
        {
            if (!isgrounded)
            {
                GameObject.Transform.Translate2(new Vector2(0,0.5f));
            }
        }
        
        public void Jump()
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (isgrounded == true)
                {
                    GameObject.Transform.ForceTranslate(new Vector2(0, -8));

                    isgrounded = false;
                }
            }
        }
        
        public void OnCollisionExit(Collider other)
        {
            isgrounded = false;
        }

        public void OnCollisionEnter(Collider other)
        {
            if ((ChestOfAThousandGrogs)other.GameObject.GetComponent("ChestOfAThousandGrogs") != null)
            {

            }
        }
        #endregion
    }
}
