using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgTowerDefense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        Director dic;
        Director dic2;

        static private GameWorld instance;
        //Singleton
        static public GameWorld _Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        //move to seperate class
        //dictates ammount of tiles for generation
        int xTiles = 32;
        float xWidth;
        int yTiles = 18;
        float yHeight;
        public float[,] coordinateContains;
        public Vector2[,] coordinatesTopLeft;

        List<Enemy> mobList;
        void UpdateMobList(Enemy mob, bool newMob)
        {
            ////index 0, mob is new spawn
            //if(newMob)
            //{
            //    mobList.Add(mob);
            //}
            ////index 1, mob is dead, remove from list
            //else
            //{
            //    mobList.Remove(mob);
            //}
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        GameObject gameObject = new GameObject(); //??

        List<GameObject> gameObjects;
        private List<Collider> colliders;
        internal List<Collider> Colliders
        {
            get { return colliders; }
        }
        public float deltaTime;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        private List<UIComponent> Uielements;



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            //coordinateContains = new float[xTiles, yTiles];
            //yHeight = graphics.GraphicsDevice.Viewport.Height / yTiles;
            //xWidth = graphics.GraphicsDevice.Viewport.Width / xTiles;

            //for(int x = 0; x < xTiles - 1;)
            //{
            //    for (int y = 0; x < yTiles - 1;)
            //    {
            //        coordinatesTopLeft[x, y] = new Vector2(x * xWidth, y * yHeight);
            //        y++;
            //    }
            //    x++;
            //}

            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();
            
            dic = new Director(new PlayerBuilder());
            dic2 = new Director(new EnemyBuilder());
            GameObject player = dic.Construct(new Vector2(1, 1));

            SpawnMob();

            gameObjects.Add(player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }

            var exitButton = new UIButton(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(0, 0),
                Text = "Exit"

            };
            var tower_01Button = new UIButton(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(0, 300),
                Text = "Tower_01",
                Scale = 0.5f
            };
            var tower_02Button = new UIButton(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(300, 300),
                Text = "Tower_02",
                Scale = 0.5f
            };
            var tower_03Button = new UIButton(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/UiFont"))
            {
                Position = new Vector2(600, 300),
                Text = "Tower_03",
                Scale = 0.5f
            };

            exitButton.Click += ExitButton_Click;

            tower_01Button.Click += tower_01Button_Click;
            tower_02Button.Click += tower_01Button_Click;
            tower_03Button.Click += tower_01Button_Click;
            // TODO: use this.Content to load your game content here

            Uielements = new List<UIComponent>()
            {
                exitButton,
                tower_01Button,
                tower_02Button,
                tower_03Button
            };



        }
        private void tower_01Button_Click(object sender, System.EventArgs e)
        {
            
        }
        private void tower_02Button_Click(object sender, System.EventArgs e)
        {

        }
        private void tower_03Button_Click(object sender, System.EventArgs e)
        {

        }
        private void  ExitButton_Click(object sender,System.EventArgs e)
        {
            Exit();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }
            foreach (UIComponent component in Uielements)
            {
                component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            foreach (UIComponent component in Uielements)
            {
                component.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SpawnMob()
        {
            Enemy mob = new Enemy(dic2.Construct(new Vector2(30, 30)));
            UpdateMobList(mob, true);

        }
    }
}