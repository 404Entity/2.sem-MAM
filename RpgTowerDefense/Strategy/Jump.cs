using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class Jump:IStrategy
    {
        Animator animator;
        Player player;
        public Jump(Animator animator,Player player)
        {
            this.animator = animator;
            this.player = player;
        }

        public void Execute(DIRECTION direction)
        {
            animator.PlayAnimation("Jump" + direction);
        }
    }
}
