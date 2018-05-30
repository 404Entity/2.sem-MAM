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
        private Matrix offset;
        public Matrix Transform { get { return transform; } private set { transform = value; } }

        public Matrix Offset { get => offset; set => offset = value; }

        public void Follow(Vector2 target, int val)
        {
            ///<summary>
            ///Calculate Transform using target parameters.
            /// </summary>
            
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);

            var offset = Matrix.CreateTranslation(0, 0, 0);
            if (val == 0)
            {
                offset = Matrix.CreateTranslation(0, 0, 0);
            }
            else if (val == 1)
            {
                offset = Matrix.CreateTranslation(GameWorld._Instance.ScreenWidth, 0, 0);
            }
            else
            {
                offset = Matrix.CreateTranslation(GameWorld._Instance.ScreenWidth * 2, 0, 0);
            }
            Transform = position * offset;

        }
    }
}

