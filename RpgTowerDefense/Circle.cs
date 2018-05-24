using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    struct Circle
    {
        private int x;
        private int y;
        private float radius;

        public float Radius { get => radius; set => radius = value; }
        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }

        public Circle(int x, int y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public bool ContainsPoint(Point point)
        {
            var vector2 = new Vector2(point.X - X, point.Y - Y);
            return vector2.Length() <= radius;
        }

        public bool Intersects(Rectangle rectangle)
        {
            var corners = new[]
            {
                new Point(rectangle.Top, rectangle.Left),
                new Point(rectangle.Top, rectangle.Right),
                new Point(rectangle.Bottom, rectangle.Left),
                new Point(rectangle.Bottom, rectangle.Right)
            };

            foreach (var corner in corners)
            {
                if (ContainsPoint(corner))
                {
                    return true;
                }
            }

            if (X - radius > rectangle.Right || X + radius < rectangle.Left)
            {
                return false;
            }
            if (Y - radius > rectangle.Bottom || Y + radius < rectangle.Top)
            {
                return false;
            }

            
            return true;
        }

        public bool Intersect(Circle circle)
        {
            var center0 = new Vector2(circle.x, circle.y);
            var center1 = new Vector2(X, Y);

            return Vector2.Distance(center0, center1) < Radius + circle.radius;
        }
    }
}
