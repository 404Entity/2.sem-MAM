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

        //Properties
        public EventHandler Click;
        public Color PenColor { get => penColor; set => penColor = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)position.Y, Texture.Width, Texture.Height);
            }
        }
        public string Text { get => text; set => text = value; }
        public float Scale { get => scale; set => scale = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        #endregion
        #region Methods
        public UIButton(Texture2D texture, SpriteFont font)
        {
            this.Texture = texture;
            this.font = font;
            this.PenColor = Color.Black;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        // an overload to the regular IDrawable
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var color = Color.White;
            if (ishovering)
            {
                color = Color.Gray;
            }
            spriteBatch.Draw(Texture, Rectangle, color);

            // if the text field is not null draw the text
            if (!string.IsNullOrEmpty(text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2) - (font.MeasureString(text).X / 2));
                var y = (Rectangle.Y + (Rectangle.Height / 2) - (font.MeasureString(text).Y / 2));
                spriteBatch.DrawString(font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Mouse.GetState();
            
            var mouseRectangle = new Rectangle(currentState.X, currentState.Y, 1, 1);

            ishovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                ishovering = true;
                if (currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
                {
                    // an elvis(aka the not null operator) operator to check if the button has been clicked
                    Click?.Invoke(this, new EventArgs());
                }
            }

        }
    
        public override void Update()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
