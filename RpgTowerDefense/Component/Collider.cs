using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace T_L_O_B_O
{
    class Collider : Component, IDrawable, ILoadable, IUpdateable
    {
        #region Fields and properties
        private SpriteRenderer spriteRender;
        private Tiles tiles;
        private Texture2D texture2D;
        private bool isCollideWith;
        private List<Collider> ohterColliders;
        private Animator animator;
        private Dictionary<string, Color[][]> pixels;
        private bool doCollisionChecks;
        private float scale;
        public bool DoCollisionChecks
        {
            set { doCollisionChecks = value; }
        }
        public Rectangle CollisionBox
        {
            get
            {

                if (spriteRender.Rectangle.Width < 2 && spriteRender.Rectangle.Height < 2)
                {
                    return new Rectangle
                    (
                    (int)(GameObject.Transform.Position.X),
                    (int)(gameObject.Transform.Position.Y),
                    (int)(spriteRender.Sprite.Width * scale),
                    (int)(spriteRender.Sprite.Height * scale));
                }
                else
                {
                    return new Rectangle
                    (
                    (int)(GameObject.Transform.Position.X + spriteRender.Offset.X),
                    (int)(gameObject.Transform.Position.Y + spriteRender.Offset.Y),

                    (int)(spriteRender.Rectangle.Width * scale),
                    (int)(spriteRender.Rectangle.Height * scale)
                );
                }
            }
        }


        #endregion
        #region Constructor
        public Collider(GameObject gameObject, bool CheckCollision, float scale) : base(gameObject)
        {
            isCollideWith = false;
            doCollisionChecks = CheckCollision;
            GameWorld.Instance.Colliders.Add(this);
            LoadContent(GameWorld.Instance.Content);
            ohterColliders = new List<Collider>();
            this.scale = scale;
        }
        #endregion

        #region Draw and LoadContent
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

#if DEBUG
            spriteBatch.Draw(texture2D, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture2D, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
#endif

        }

        public void LoadContent(ContentManager Content)
        {
            spriteRender = (SpriteRenderer)gameObject.GetComponent("SpriteRender");
            texture2D = Content.Load<Texture2D>("CollisionTexture");
        }
        public void Update()
        {
            CheckCollision();

        }
        #endregion

        #region Collision
        private void CheckCollision()
        {
            if (doCollisionChecks)
            {
                List<Collider> removelist = new List<Collider>();
                foreach (Collider collider in ohterColliders)
                {

                    if (!CollisionBox.Intersects(collider.CollisionBox))
                    {
                        gameObject.OnCollisionExit(collider);
                        removelist.Add(collider);
                    }
                    else
                    {
                        gameObject.OnCollisionStay(collider);
                    }
                }
                foreach (Collider item in removelist)
                {
                    ohterColliders.Remove(item);
                }
                //Genereate a optimized Temp list.  
                List<Collider> OptimList = new List<Collider>();
                OptimList.AddRange(GameWorld.Instance.Colliders);
                foreach (Collider collider in ohterColliders)
                {
                    OptimList.Remove(collider);
                }
                foreach (Collider collider in OptimList)
                {
                    if (collider != this)
                    {
                        if (CollisionBox.Intersects(collider.CollisionBox))
                        {
                            gameObject.OnCollisionEnter(collider);
                            ohterColliders.Add(collider);
                        }
                    }
                }
            }
        }


        private void CachePixels()
        {
            foreach (KeyValuePair<string, Animation> pair in animator.MyAnimations)
            {
                Animation animation = pair.Value;
                Color[][] colors = new Color[(int)animation.Fps][];

                for (int i = 0; i < (int)animation.Fps; i++)
                {
                    colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];
                    spriteRender.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0, animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                }
                pixels.Add(pair.Key, colors);
            }
        }
        #endregion
    }
}
