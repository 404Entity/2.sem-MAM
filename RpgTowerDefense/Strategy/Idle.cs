using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_L_O_B_O
{
    class Idle : IStrategy
    {
        private Animator animator;
        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(DIRECTION ref_direction)
        {
            animator.PlayAnimation("Idle" + ref_direction);
        }
    }
}
