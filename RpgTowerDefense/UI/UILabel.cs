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

        public Vector2 Position { get => position; set => position = value; }


        public UILabel(SpriteFont font, string text)
        {
            this.font = font;
            this.text = text;
        
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

    }
}
