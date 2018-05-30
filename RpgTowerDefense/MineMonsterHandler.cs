using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
class MineMonsterHandler
    {
        Random rnd;
        
        int pos;

        Director mobDic = new Director(new EnemyBuilder());

        public void SpawnMob(int ammount)
        {
            rnd = new Random();
            
            for (int i = 0; i < ammount;)
            {
                pos = rnd.Next(200, 651);
                GameWorld._Instance.SpawnMobMine(pos);
                i++;
            }
        }
    }
}
