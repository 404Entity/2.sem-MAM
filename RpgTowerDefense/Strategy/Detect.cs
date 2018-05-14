using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_L_O_B_O
{
    class Detect:IStrategy
    {
        Animator animator;

        public Detect()
        {
            this.animator = animator;
        }

        public void Execute(DIRECTION direction)
        {
            animator.PlayAnimation("Detect" + direction);
        }
    }
}
