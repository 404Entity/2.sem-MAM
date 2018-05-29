using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    class Camera
    {
        /// <summary>
        /// The Camera class locks to a specified targer(Gameobject) and allows us to transform the render area, using the offset.
        /// </summary>
        private Matrix transform;
        public Matrix Transform { get { return transform; } private set { transform = value; } }


        public void CameraLock()
        {

        }
        public void Follow(GameObject target, Collider targetColisionbox)
        {
            ///<summary>
            ///Calculate Transform using target parameters.
            /// </summary>
            var offset = Matrix.CreateTranslation(GameWorld.ScreenWidth / 2, GameWorld.ScreenHeight / 2, 0);
            var position = Matrix.CreateTranslation(
                -target.Transform.Position.X - (targetColisionbox.CollisionBox.Width / 2),
                -target.Transform.Position.Y - (targetColisionbox.CollisionBox.Height / 2),
                0);

            Transform = position * offset;

        }
    }
}
}
