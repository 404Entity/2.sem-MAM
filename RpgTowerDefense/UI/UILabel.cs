using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTowerDefense
{
    class UILabel : UIComponent
    {
        private string text;
        private Vector2 position;
        private SpriteFont font;
        private Color color;

        public Vector2 Position { get => position; set => position = value; }
        public Color Color { get => color; set => color = value; }

        public UILabel(SpriteFont font, string text)
        {
            this.font = font;
            this.text = text;
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
        }

        public override void Update()
        {

        }

    }
}
