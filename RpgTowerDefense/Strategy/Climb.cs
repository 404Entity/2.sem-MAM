using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace T_L_O_B_O
{
    class Climb:IStrategy
    {
        Animator animator;

        public Climb(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(DIRECTION direction)
        {
            animator.PlayAnimation("Climb" + direction);
        }
    }
}
