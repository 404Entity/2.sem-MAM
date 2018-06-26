using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    abstract class Item : Component
    {
        int goldValue;

        public Item(GameObject gameObject, int goldValue) : base(gameObject)
        {
            this.goldValue = goldValue;
        }

    }
}
