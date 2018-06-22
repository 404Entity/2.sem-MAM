using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RpgTowerDefense
{
    class UIButton : UIComponent
    {
        #region Fields and Properties
        //Fields
        private MouseState currentState;
        private MouseState previousState;
        private SpriteFont font;
        private float scale;
        private bool ishovering;
        private Texture2D texture;
        private Color penColor;
        private Vector2 position;
        private string text;
        private float textScale;
        private bool isProxy;

        //Properties
        public EventHandler Click;
        public EventHandler RightClick;
        public Color PenColor { get => penColor; set => penColor = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)position.Y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
            }
        }
        public string Text { get => text; set => text = value; }
        public float Scale { get => scale; set => scale = value; }

        public Texture2D Texture { get => texture; set => texture = value; }
        public float TextScale { get => textScale; set => textScale = value; }
        public bool IsProxy { get => isProxy; set => isProxy = value; }
        #endregion

        #region Methods
        public UIButton(Texture2D texture, SpriteFont font, bool isProxy)
        {
            this.texture = texture;
            this.font = font;
            this.PenColor = Color.Black;
            this.isProxy = isProxy;
            TextScale = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            if (ishovering)
            {
                if (IsProxy)
                {
                    color = Color.Red;
                }
                else
                {
                    color = Color.Gray;
                }

            }

            spriteBatch.Draw(Texture, Rectangle, color);
            //spriteBatch.Draw(Texture, Position, Rectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 1);

            // if the text field is not null draw the text
            if (!string.IsNullOrEmpty(text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2) - (font.MeasureString(text).X / 2));
                var y = (Rectangle.Y + (Rectangle.Height / 2) - (font.MeasureString(text).Y / 2));
                //spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
                spriteBatch.DrawString(font, text, new Vector2(x, y), penColor, 0, Vector2.Zero, TextScale, SpriteEffects.None, 1);
            }
        }
        public override void Update()
        {
            if (isProxy)
            {
                position = new Vector2(Mouse.GetState().X - ((texture.Width  * Scale)-20) / 2, Mouse.GetState().Y - (texture.Height * scale) / 2);
            }
            previousState = currentState;
            currentState = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentState.X, currentState.Y, 1, 1);

            ishovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                ishovering = true;
                if (currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
                if (currentState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed)
                {
                    RightClick?.Invoke(this, new EventArgs());
                }
            }
         
        }
        #endregion
    }
}
