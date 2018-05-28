using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class BulletBuilder : IBuilder
    {
        private GameObject buildObject;
        public void BuildGameObject(Vector2 position)
        {
            GameObject bullet = new GameObject();
            bullet.AddComponent(new Transform(bullet, position));
            bullet.AddComponent(new SpriteRenderer(bullet, "Bullet", 1, 0.2f));
            //bullet.AddComponent(new Collider(bullet, true, 0.5f));
            bullet.AddComponent(new Projectile(bullet, 10,Vector2.Zero));
            bullet.LoadContent(GameWorld._Instance.Content);
            buildObject = bullet;
            SpriteRenderer sp = bullet.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.GetStaticRectangle();
        }

        public void BuildGameObject(Vector2 position, int id)
        {
            throw new NotImplementedException();
        }
        public void BuildGameObject(Vector2 position, int id, Vector2 directionVector)
        {
            GameObject bullet = new GameObject();
            bullet.AddComponent(new Transform(bullet, position));
            bullet.AddComponent(new SpriteRenderer(bullet, "Bullet", 1, 0.2f));
            //bullet.AddComponent(new Collider(bullet, true, 0.5f));
            bullet.AddComponent(new Projectile(bullet, 10, directionVector));
            bullet.LoadContent(GameWorld._Instance.Content);
            buildObject = bullet;
            SpriteRenderer sp = bullet.GetComponent("SpriteRenderer") as SpriteRenderer;
            sp.GetStaticRectangle();
        }

        public GameObject GetResult()
        {
            return buildObject;
        }
    }
}
