using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    class GameObject : Component, IAnimateable, ICollideStay, ICollideEnter, ICollideExit
    {
        //Fields
        private List<Component> components;
        private bool isLoaded;
        private Transform transform;
        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        //Constructor
        public GameObject()
        {
            components = new List<Component>();
            isLoaded = false;
        }

        //Methods
        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public Component GetComponent(string component)
        {
            Component returnComponent = null;
            foreach (Component comp in components)
            {
                if (comp.GetType().ToString().ToLower().Contains(component.ToLower()))
                {
                    returnComponent = comp;
                    break;
                }
            }
            return returnComponent;
        }
        //Loading in content on load
        public void LoadContent(ContentManager content)
        {
            if (!isLoaded)
            {
                foreach (Component component in components)
                {
                    if (component is ILoadable)
                    {
                        (component as ILoadable).LoadContent(content);
                    }
                }
                isLoaded = true;
            }
            
        }
        //Running updates on the loaded in content
        public void Update(GameTime gameTime)
        {
            foreach (Component component in components)
            {
                if (component is IUpdate)
                {
                    (component as IUpdate).Update();
                }
            }
        }
        //Draws the content
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch);
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable)
                {
                    (component as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }
        public void OnCollisionStay(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollideStay)
                {
                    (component as ICollideStay).OnCollisionStay(other);
                }
            }
        }

        public void OnCollisionExit(Collider other)
        {
            foreach(Component component in components)
            {
                if (component is ICollideExit)
                {
                    (component as ICollideExit).OnCollisionExit(other);
                }
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollideEnter)
                {
                    (component as ICollideEnter).OnCollisionEnter(other);
                }
            }
        }
    }
}
