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
        int yTiles = 18;
        //indicates the dimensions of the tiles
        public float xWidth;
        public float yHeight;
        //list of locations on the grid where towers can be built
        Vector2[] buildSpotLocation = { new Vector2(3,12),new Vector2(6,14),new Vector2(7,3),new Vector2(12,12),new Vector2(14,3),new Vector2(16,6),new Vector2(21,12),new Vector2(24,6),new Vector2(28,1) };
        bool[] buildSpotAvailable;
        //list of coordinates on gird, curently not used
        float[] coordinatesX;
        float[] coordinatesY;
        //used to keep track of enemies seperately from other objects
        List<GameObject> mobList = new List<GameObject>();

        //used to add to or remove from the seperated mob list
        void UpdateMobList(GameObject mob, bool newMob)
        {
            if(newMob)
            {
                mobList.Add(mob);
            }
            else
            {
                mobList.Remove(mob);
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameObject gameObject = new GameObject();
        BackGround backGround = new BackGround();
        UI ui;

        //data for map, needs to be texture for scalability
        Texture2D yyMap;
        Rectangle mapRect;
        //keeps track of coordinates for enemy pathing
        public Vector2[] walkCoordinates = { new Vector2(5, 14), new Vector2(5, 2), new Vector2(17, 2), new Vector2(17, 8), new Vector2(11, 8), new Vector2(11, 14), new Vector2(23, 14), new Vector2(23, 2), new Vector2(32, 2) };
        //list of vectors to indicate what direction enemy will be facing 
        Vector2[] walkdirection = { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(1, 0) };

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


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();

            graphics.GraphicsDevice.Viewport = new Viewport(0, 0, 1600, 900);

            //coordinateContains = new float[xTiles, yTiles];
            //saves dimension of tiles as a result of the size of viewport, needed for scalability
            yHeight = graphics.GraphicsDevice.Viewport.Height / yTiles;
            xWidth = graphics.GraphicsDevice.Viewport.Width / xTiles;
            coordinatesX = new float[xTiles];
            coordinatesY = new float[yTiles];

            //saves worldspace coordinates for the grid
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
            
            //saves worldspace coordinates for buildspots
            for (int i = 0; i < buildSpotLocation.Length;)
            {
                buildSpotLocation[i].X = buildSpotLocation[i].X * xWidth;
                buildSpotLocation[i].Y = buildSpotLocation[i].Y * yHeight;
                i++;
            }
            buildSpotAvailable = new bool[buildSpotLocation.Length];

            //saves worldspace coordinates for pathing 
            for (int i = 0; i < walkCoordinates.Length;)
            {
                walkCoordinates[i].X = walkCoordinates[i].X * xWidth;
                walkCoordinates[i].Y = walkCoordinates[i].Y * yHeight;
                i++;
            }

            mapRect = new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();

            ui = new UI();
            dic = new Director(new PlayerBuilder());
            dic2 = new Director(new EnemyBuilder());
            dic3 = new Director(new GateBuilder());
            GameObject player = dic.Construct(new Vector2(1,1));
            GameObject enemy = dic2.Construct(new Vector2(0, 280));
            GameObject cityGate = dic3.Construct(new Vector2(1350, 15));
            gameObjects.Add(player);
            gameObjects.Add(enemy);
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
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here
            ui.LoadContent(Content);
            backGround.LoadContent(Content);
            //yyMap = Content.Load<Texture2D>("BackGround");
            yyMap = Content.Load<Texture2D>("BackGroundWithGrid");
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
            
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }
            ui.Update();
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

            //backGround.Draw(spriteBatch);
            
            spriteBatch.Draw(yyMap, mapRect, Color.White);
            
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
            GameObject mob = dic2.Construct(new Vector2(0, 280));
            UpdateMobList(mob, true);
            gameObjects.Add(mob);

        }
    }
}