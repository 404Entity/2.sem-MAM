using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;


namespace T_L_O_B_O
{
    class Animator : Component, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer
        {
            get { return spriteRenderer; }
        }
        private int currentIndex;
        public int CurrentIndex
        {
            get { return currentIndex; }
        }
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        public Rectangle[] Rectangles
        {
            get { return rectangles; }
            set { rectangles = value; }
        }
        private string animationName;
        private Dictionary<string, Animation> animations;
        public Dictionary<string,Animation> MyAnimations
        {
            get { return animations; }
        }
        public Animator(GameObject gameObject) : base(gameObject)
        {
                fps = 4;
            this.spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            animations = new Dictionary<string, Animation>();
        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.deltaTime;
            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                gameObject.OnAnimationDone(animationName);
                timeElapsed = 0;
                currentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[currentIndex];

        }
        public void CreateAnimation(string name, Animation animation)
        {
            //adds a animation to the Animators animations Dictionary
            animations.Add(name, animation);
        }
        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                this.rectangles = animations[animationName].Rectangles;

                this.spriteRenderer.Rectangle = rectangles[0];

                this.spriteRenderer.Offset = animations[animationName].Offset;

                this.animationName = animationName;

                this.fps = animations[animationName].Fps;

                timeElapsed = 0;

                currentIndex = 0;
            }   
        }
    }
}
