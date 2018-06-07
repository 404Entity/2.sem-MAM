using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    interface IBuilder
    {
        GameObject GetResult();
        void BuildGameObject(Vector2 position);
        void BuildGameObject(Vector2 position, GameObject player);
        void BuildGameObject(Vector2 position, int id);
        void BuildGameObject(Vector2 position, int id, Vector2 direction, float damage, AttackType attackType);
    }
}
