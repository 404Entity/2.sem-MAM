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
    class Transform : Component, IUpdate
    {
        private Vector2 position;
        private Vector2 velocity;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public Transform(GameObject gameobject, Vector2 position) :base(gameobject)
        {
            this.position = position;
            gameobject.Transform = this;
        
        }
        public void  Translate(Vector2 translation)
        {
            Position += translation;
        }
        public void ForceTranslate(Vector2 translation)
        {
            velocity = translation;
            position += velocity;
        }
        public void stop()
        {
            velocity.X = 0;
        }
        public void CorrectMove(Vector2 Correction)
        {
            position += Correction;
        }
        public void Update()
        {
            position += velocity;
        }
    }
}
