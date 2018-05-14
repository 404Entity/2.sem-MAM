using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace T_L_O_B_O
{
    class Walk : IStrategy
    {
        private Animator animator;
        private Transform transform;
        private float speed;
        public Walk(Animator animator, Transform transform, float speed)
        {
            this.animator = animator;
            this.transform = transform;
            this.speed = speed;
        }
        public void Execute(DIRECTION ref_direction)
        {
          
            if (ref_direction == DIRECTION.Right)
            {
   
                animator.PlayAnimation("WalkRight");
            }
            if (ref_direction == DIRECTION.Left)
            {
        
                animator.PlayAnimation("WalkLeft");
            }
            }
       
        
    }
}
