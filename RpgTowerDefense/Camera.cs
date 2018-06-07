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
    class Camera
    {
        /// <summary>
        /// The Camera class locks to a specified target(Gameobject) and allows us to transform the render area, using the offset.
        /// </summary>
        private Matrix transform;
        private Matrix offset;
        private int screenvalue;
        public Matrix Transform { get { return transform; } private set { transform = value; } }

<<<<<<< HEAD

        public void CameraLock()
        {
=======
        public Matrix Offset { get => offset; set => offset = value; }
        public int Screenvalue { get => screenvalue; set => screenvalue = value; }
>>>>>>> 0bf90f3b50c4c6ab001f29dfe28f11b26ad53cb3

        public void Follow(Vector2 target)
        {
            ///<summary>
            ///Calculate Transform using target parameters.
            /// </summary>
            
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);

            var offset = Matrix.CreateTranslation(0, 0, 0);
            if (screenvalue == 1)
            {
                position = Matrix.CreateTranslation(0, 0, 0);
            }
            else if (screenvalue == 2)
            {
                position = Matrix.CreateTranslation(-GameWorld._Instance.ScreenWidth, 0, 0);
                
            }
            else
            {
                position = Matrix.CreateTranslation(-GameWorld._Instance.ScreenWidth * 2, 0, 0);
            }
            Transform = position * offset;

        }
       public void GetMousePosition()
        {
           //Mouse.WindowHandle = this.transform.
        }
    }
}

