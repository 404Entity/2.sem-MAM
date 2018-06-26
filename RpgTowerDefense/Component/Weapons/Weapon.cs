using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    abstract class Weapon : Item
    {
        protected int damage;
        protected int reloadSpeed;

        public Weapon(GameObject gameObject, int goldValue,int damage,int reloadSpeed) : base(gameObject, goldValue)
        {
            this.damage = damage;
            this.reloadSpeed = reloadSpeed;
        }

        abstract public void Shoot();
        abstract public void Reload();

    }
}
