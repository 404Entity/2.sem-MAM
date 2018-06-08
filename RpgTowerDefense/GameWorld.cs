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
        Director dic4;

        int playerGold, GoldGainEachRound = 10, highScore, gateHealth = 100;
        //Bestemmer om menu er på eller spillet kører
        bool gameState = false;

        //testing mobspawn
        float spawntime;
        float interval = 1.5f;

        float mineSpawntime;
        float mineInterval = 15;
        
        MineMonsterHandler mine;
        
        private Camera camera;
        private GameObject selectedGameObject;
        private MouseState previouseMouseState;
        private StartMenu startMenu;


        public GameWorldBuilder worldBuilder;
        GameObject player;
        public Texture2D currentMap;
        public Rectangle currentRect;

        Texture2D UITex;
        Rectangle UIRect;

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
        public int PlayerGold { get => playerGold; set => playerGold = value; }
        public int HighScore { get => highScore; set => highScore = value; }
        public int GateHealth { get => gateHealth; set => gateHealth = value; }
        public bool GameState { get => gameState; set => gameState = value; }

        internal GameObject SelectedGameObject { get => selectedGameObject; set => selectedGameObject = value; }
        public MouseState PreviouseMouseState { get => previouseMouseState; set => previouseMouseState = value; }

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

            mine = new MineMonsterHandler();
            worldBuilder = new GameWorldBuilder();
            
            startMenu = new StartMenu();
            //intialize camera
            camera = new Camera();
            camera.Screenvalue = 1;

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.GraphicsDevice.Viewport = new Viewport(0, 0, 1600, 900);
            graphics.ApplyChanges();

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeigth = graphics.PreferredBackBufferHeight;

            worldBuilder.yHeight = graphics.GraphicsDevice.Viewport.Height / worldBuilder.yTiles;
            worldBuilder.xWidth = graphics.GraphicsDevice.Viewport.Width / worldBuilder.xTiles;
            worldBuilder.map1Rect = new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            worldBuilder.map2Rect = new Rectangle(3200, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);


            // TODO: Add your initialization logic here
            GameObjects = new List<GameObject>();
            addGameObjects = new List<GameObject>();
            removeGameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            ui = new UI();
            dic = new Director(new PlayerBuilder());
            player = dic.Construct(new Vector2(1, 1));
            gameObjects.Add(player);
            dic2 = new Director(new EnemyBuilder());
            dic4 = new Director(new EnemyMineBuilder());
            

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

            UITex = Content.Load<Texture2D>("UI_01");
            UIRect = graphics.GraphicsDevice.Viewport.Bounds;



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
            MouseState mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 50, 50);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Set the selected object to the object clicked on
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && previouseMouseState.LeftButton == ButtonState.Released)
            {
                foreach (GameObject gameObject in GameObjects)
                {
                    //SpriteRenderer sr = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
                    if (mouseRectangle.Contains(gameObject.Transform.Position))
                    {
                        selectedGameObject = gameObject;
                        break;
                    }

                }
            }
            //test mob spawn
            spawntime += deltaTime;
            if(spawntime >= interval)
            {
                spawntime = 0;
                SpawnMob();
                //Giver spilleren 10+ guld hvert enemy spawn
                PlayerGold += GoldGainEachRound;
            }
            if (GameState == true)
            {
                startMenu.Update();
            }
            else
            {
                deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D1))
                {

                    camera.Screenvalue = 1;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    Mouse.SetPosition(screenWidth, 0);
                    camera.Screenvalue = 2;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    camera.Screenvalue = 3;
                }

                //test mob spawn
                spawntime += deltaTime;
                if (spawntime >= interval)
                {
                    spawntime = 0;
                    SpawnMob();
                    //Giver spilleren 10+ guld hvert enemy spawn
                    PlayerGold += GoldGainEachRound;
                }
                mineSpawntime += deltaTime;
                if (mineSpawntime >= mineInterval)
                {
                    mineSpawntime = 0;
                    SpawnMobMine();
                }


                // TODO: Add your update logic here
                foreach (GameObject go in addGameObjects)
                {
                    GameObjects.Add(go);
                }
                foreach (GameObject go in removeGameObjects)
                {
                    gameObjects.Remove(go);
                    mobList.Remove(go);
                }
                CleanTemptList();
                foreach (GameObject go in GameObjects)
                {
                    go.Update(gameTime);
                }

                ui.Update();
                camera.Follow(new Vector2(0, 0));
                previouseMouseState = mouseState;
                base.Update(gameTime);
            }
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
            if (GameState == true)
            {
                GraphicsDevice.Clear(Color.Blue);
                spriteBatch.Begin();
                startMenu.Draw(spriteBatch);
                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin(transformMatrix: camera.Transform);

                //backGround.Draw(spriteBatch);

                spriteBatch.Draw(worldBuilder.yyMap, worldBuilder.map1Rect, Color.White);
                spriteBatch.Draw(worldBuilder.mineMap, worldBuilder.map2Rect, Color.White);

                foreach (GameObject go in gameObjects)
                {
                    go.Draw(spriteBatch);
                }


                spriteBatch.End();

                //draw UI

                spriteBatch.Begin();
                spriteBatch.Draw(UITex, UIRect, Color.White);
                ui.Draw(spriteBatch);
                spriteBatch.End();

                base.Draw(gameTime);
            }
        }

        //spawns enemy and adds to both gameobjects and moblist
        public void SpawnMob()
        {
            if (mobList.Count < 120)
            {
                dic2.Construct(new Vector2(0, 270));
                GameObject mob = dic2.Builder.GetResult();
                UpdateMobList(mob, true);
                gameObjects.Add(mob);
            }
        }

        public void SpawnMobMine()
        {
            if (mobList.Count < 80)
            {
                GameObject mob = dic4.Construct(new Vector2(graphics.GraphicsDevice.Viewport.Width * 3, 425), player);
                UpdateMobList(mob, true);
                gameObjects.Add(mob);
            }
        }
    }
}