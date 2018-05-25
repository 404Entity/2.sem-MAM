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

        //dictates ammount of tiles for generation
        int xTiles = 32;
        float xWidth;
        int yTiles = 18;
        float yHeight;
        Vector2[] buildSpotLocation;
        bool[] buildSpotAvailable;
        public float[] coordinatesX;
        public float[] coordinatesY;

        private List<Enemy> mobList = new List<Enemy>();
        void UpdateMobList(Enemy mob, bool newMob)
        {
            //index 0, mob is new spawn
            if(newMob)
            {
                MobList.Add(mob);
            }
            //index 1, mob is dead, remove from list
            else
            {
                MobList.Remove(mob);
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BackGround backGround = new BackGround();
        Texture2D yyMap;
        Rectangle mapRect;

        private List<GameObject> gameObjects;
        private List<GameObject> addGameObjects;
        private List<GameObject> removeGameObjects;
        private List<Collider> colliders;
        internal List<Collider> Colliders
        {
            get { return colliders; }
        }

        internal List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        internal List<GameObject> AddGameObjects { get => addGameObjects; set => addGameObjects = value; }
        internal List<GameObject> RemoveGameObjects { get => removeGameObjects; set => removeGameObjects = value; }
        internal List<Enemy> MobList { get => mobList; set => mobList = value; }

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
            //coordinateContains = new float[xTiles, yTiles];
            yHeight = graphics.GraphicsDevice.Viewport.Height / yTiles;
            xWidth = graphics.GraphicsDevice.Viewport.Width / xTiles;
            coordinatesX = new float[xTiles];
            coordinatesY = new float[yTiles];

            for (int x = 0; x < xTiles - 1;)
            {
                for (int y = 0; y < yTiles - 1;)
                {
                    coordinatesX[x] = x * xWidth;
                    coordinatesY[y] = y * yHeight; 
                    y++;
                }
                x++;
            }

            mapRect = new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            // TODO: Add your initialization logic here
            GameObjects = new List<GameObject>();
            addGameObjects = new List<GameObject>();
            removeGameObjects = new List<GameObject>();

            dic = new Director(new PlayerBuilder());
            dic2 = new Director(new EnemyBuilder());
            Director dic3 = new Director(new TowerBuilder());
            GameObject player = dic.Construct(new Vector2(1,1));
            GameObject enemy = dic2.Construct(new Vector2(0, 280));
            dic3.Construct(new Vector2(300, 200),1);
            GameObject tower = dic3.Builder.GetResult();
            GameObjects.Add(player);
            GameObjects.Add(enemy);
            GameObjects.Add(tower);

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

            backGround.LoadContent(Content);
            yyMap = Content.Load<Texture2D>("BackGround");

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
            spriteBatch.Draw(yyMap, mapRect, Color.White);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SpawnMob()
        {
            Enemy mob = new Enemy(dic2.Construct(new Vector2(coordinatesX[3], coordinatesY[1])));
            UpdateMobList(mob, true);
            
        }
    }
}