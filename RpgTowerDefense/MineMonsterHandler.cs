using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgTowerDefense
{
class MineMonsterHandler
    {
        List<GameObject> halfwayMine = new List<GameObject>();
        List<GameObject> disabledMine = new List<GameObject>();

        public void Update()
        {
            foreach(GameObject go in GameWorld._Instance.MineList)
            {
                if(go.Transform.Position.X <= 3600)
                {
                    disabledMine.Add(go);
                }
                else if(go.Transform.Position.X <= 4000)
                {
                    halfwayMine.Add(go);
                    disabledMine.Clear();
                }
                else
                {
                    disabledMine.Clear();
                    halfwayMine.Clear();
                }
            }


            if(disabledMine.Count != 0)
            {
                GameWorld._Instance.mineDisabled = true;
            }
            else if(halfwayMine.Count != 0)
            {
                GameWorld._Instance.mineHalfway = true;
            }
            else
            {
                GameWorld._Instance.mineDisabled = false;
                GameWorld._Instance.mineHalfway = false;
            }
        }
    }
}
