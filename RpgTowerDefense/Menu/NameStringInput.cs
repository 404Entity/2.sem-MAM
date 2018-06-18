using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RpgTowerDefense
{
    class NameStringInput : MenuBase
    {
        #region singleton
        public static NameStringInput NameString_Instance;
        static public NameStringInput _Instance
        {
            get
            {
                if (NameString_Instance == null)
                {
                    NameString_Instance = new NameStringInput();
                }
                return NameString_Instance;
            }
        }
        #endregion


        private Keys[] lastPressedKeys = new Keys[5];

        private string myName = string.Empty;


        private SpriteFont sf;
        private Texture2D texture2D;
        Rectangle rec;
        Vector2 pos;
        int maxString = 50;
        bool stringToLong = false;








        public string MyName { get => myName; }



        public override void LoadContent(ContentManager content)
        {
            //Load in the sprite
            sf = content.Load<SpriteFont>("MenuButtom");

            texture2D = content.Load<Texture2D>("EnterName");
            rec = new Rectangle(500, 500, texture2D.Width, texture2D.Height);
            pos = new Vector2(500, 200);
        }

        public override void Update()
        {
            if (rec.Contains(new Point(Mouse.GetState().X,Mouse.GetState().Y))&& Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                
            }




            GetKeys();
            if (MyName.Count() >= maxString)
            {
                myName = string.Empty;
                stringToLong = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture2D,pos,Color.White);
            spriteBatch.DrawString(sf, MyName, pos, Color.Black);
            if (stringToLong == true)
            {
                
                spriteBatch.DrawString(sf, "Your name can only contain "+maxString+" letters!", new Vector2(500, 225), Color.Black);
            }
        }







        public void GetKeys()
        {
            KeyboardState kbstate = Keyboard.GetState();

            Keys[] pressedkeys = kbstate.GetPressedKeys();

            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedkeys.Contains(key))
                {
                    //Key is no longer pressed
                    OnKeyUp(key);
                }
            }
            foreach (Keys key in pressedkeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyDown(key);
                }
                else if (key == Keys.Back)
                {
                    myName = string.Empty;
                }
            }
            lastPressedKeys = pressedkeys;
        }

        public void OnKeyUp(Keys key)
        {
            
        }
        public void OnKeyDown(Keys key)
        {
            myName += key.ToString();
        }
    }
}
