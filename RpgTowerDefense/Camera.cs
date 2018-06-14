    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    /// <summary>
    /// The Camera class locks to a specified target(Gameobject) and allows us to transform the render area, using the offset.
    /// </summary>
    class Camera
    {
    
        private Matrix transform;
        private Matrix offset;
        private int screenValue;
        public Matrix Transform { get { return transform; } private set { transform = value; } }


        public Matrix Offset { get => offset; set => offset = value; }
        public int Screenvalue { get => screenValue; set => screenValue = value; }



        /// <summary>
        ///Calculate Transform using target parameters.
        /// </summary>
        /// <param name="target"></param>
        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);

            var offset = Matrix.CreateTranslation(0, 0, 0);
            if (screenValue == 1)
            {
                position = Matrix.CreateTranslation(0, 0, 0);
            }
            else if (screenValue == 2)
            {
                position = Matrix.CreateTranslation(-GameWorld._Instance.ScreenWidth, 0, 0);
                
            }
            else
            {
                position = Matrix.CreateTranslation(-GameWorld._Instance.ScreenWidth * 2, 0, 0);
            }
            Transform = position * offset;

        }

    }
}

