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
        private Color penColor;

        public Vector2 Position { get => position; set => position = value; }
        public Color PenColor { get => penColor; set => penColor = value; }
        public string Text { get => text; set => text = value; }

        public EventHandler updateMe;

        public UILabel(SpriteFont font, string text)
        {
            this.font = font;
            this.Text = text;
            penColor = Color.White;
        }

        /// <summary>
        /// Draw the text if there is a text.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                spriteBatch.DrawString(font, Text, Position, PenColor);
            }
        }

        public override void Update()
       {
            //Not null operator(elvis Operator) checks if the label has any subscriptions
            updateMe?.Invoke(this,new EventArgs());
        }

    }
}
