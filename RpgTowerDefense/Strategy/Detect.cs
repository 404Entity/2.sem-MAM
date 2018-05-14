using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
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
