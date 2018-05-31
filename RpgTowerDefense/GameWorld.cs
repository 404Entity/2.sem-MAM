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
        Director dic3;

        //testing mobspawn
        float spawntime;
        float interval = 1.5f;

        public GameWorldBuilder worldBuilder;

        public Texture2D currentMap;
        public Rectangle currentRect;

        //list of locations on the grid where towers can be built
        public Vector2[] buildSpotLocation = { new Vector2(3, 12), new Vector2(6, 14), new Vector2(7, 3), new Vector2(12, 12), new Vector2(14, 3), new Vector2(16, 6), new Vector2(21, 12), new Vector2(24, 6), new Vector2(28, 1) };
        public bool[] buildSpotAvailable;
        //keeps track of coordinates for enemy pathing
        public Vector2[] walkCoordinates = { new Vector2(5, 14), new Vector2(5, 2), new Vector2(17, 2), new Vector2(17, 8), new Vector2(11, 8), new Vector2(11, 14), new Vector2(23, 14), new Vector2(23, 2), new Vector2(32, 2) };


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

        //used to keep track of enemies seperately from other objects
        List<GameObject> mobList = new List<GameObject>();

        //used to add to or remove from the seperated mob list
        void UpdateMobList(GameObject mob, bool newMob)
        {
            if(newMob)
            {
                MobList.Add(mob);
            }
            else
            {
                MobList.Remove(mob);
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BackGround backGround = new BackGround();
        UI ui;
        private int screenWidth;
        private int screenHeigth;
        private List<GameObject> gameObjects;
        private List<GameObject> addGameObjects;
        private List<GameObject> removeGameObjects;
        private List<Collider> colliders;
        internal List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        internal List<GameObject> AddGameObjects { get => addGameObjects; set => addGameObjects = value; }
        internal List<GameObject> RemoveGameObjects { get => removeGameObjects; set => removeGameObjects = value; }
        internal List<GameObject> MobList { get => mobList; set => mobList = value; }
        internal List<Collider> Colliders
        {
            get { return colliders; }
            set { colliders = value; }
        }

        public int ScreenWidth { get => screenWidth; set => screenWidth = value; }
        public int ScreenHeigth { get => screenHeigth; set => screenHeigth = value; }

        public float deltaTime;
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            worldBuilder = new GameWorldBuilder();

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            graphics.GraphicsDevice.Viewport = new Viewport(0, 0, 1600, 900);

            worldBuilder.yHeight = graphics.GraphicsDevice.Viewport.Height / worldBuilder.yTiles;
            worldBuilder.xWidth = graphics.GraphicsDevice.Viewport.Width / worldBuilder.xTiles;
            worldBuilder.map1Rect = new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            worldBuilder.map2Rect = new Rectangle(1600, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);


            // TODO: Add your initialization logic here
            GameObjects = new List<GameObject>();
            addGameObjects = new List<GameObject>();
            removeGameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            ui = new UI();
            dic = new Director(new PlayerBuilder());
            GameObject player = dic.Construct(new Vector2(1, 1));
            gameObjects.Add(player);
            dic2 = new Director(new EnemyBuilder());
            

            worldBuilder.SetupData();



            dic = new Director(new GateBuilder());
            GameObject cityGate = dic.Construct(new Vector2(1350, 0));
            gameObjects.Add(cityGate);
          

            //SpawnMob();

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
            foreach (GameObject go in GameObjects)
            {
                go.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here
            ui.LoadContent(Content);
            backGround.LoadContent(Content);
            //yyMap = Content.Load<Texture2D>("BackGround");
            worldBuilder.yyMap = Content.Load<Texture2D>("BackGroundWithGrid");
            worldBuilder.mineMap = Content.Load<Texture2D>("Mine");
            worldBuilder.AssignWorld(0);
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
            
            //test mob spawn
            spawntime += deltaTime;
            if(spawntime >= interval)
            {
                spawntime = 0;
                SpawnMob();
            }


            // TODO: Add your update logic here
            foreach (GameObject go in addGameObjects)
            {
                GameObjects.Add(go);
            }
            foreach (GameObject go in removeGameObjects)
            {
                gameObjects.Remove(go);
            }
            CleanTemptList();
            foreach (GameObject go in GameObjects)
            {
                go.Update(gameTime);
            }
            ui.Update();
            base.Update(gameTime);
        }
        private void CleanTemptList()
        {
            addGameObjects.Clear();
            removeGameObjects.Clear();
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //backGround.Draw(spriteBatch);
            
            spriteBatch.Draw(worldBuilder.yyMap, worldBuilder.map1Rect, Color.White);
            spriteBatch.Draw(worldBuilder.mineMap, worldBuilder.map2Rect, Color.White);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
            ui.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //spawns enemy and adds to both gameobjects and moblist
        public void SpawnMob()
        {
            GameObject mob = dic2.Construct(new Vector2(0, 270));
            UpdateMobList(mob, true);
            gameObjects.Add(mob);

        }
    }
}