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
        float spawnTimer;

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
        List<Vector2> buildSpots;
        public float[] coordinatesX;
        public float[] coordinatesY;
        Vector2[] walkCoordinates = { new Vector2(2, 7), new Vector2(2, 1), new Vector2(8, 1), new Vector2(8, 4), new Vector2(5, 4), new Vector2(5, 7), new Vector2(11, 7), new Vector2(11, 1) };
        Vector2[] walkdirection = { new Vector2(0,-1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(1, 0) };

        Texture2D mapTexture;
        Rectangle mapRect;

        internal List<GameObject> mobList = new List<GameObject>();
        void UpdateMobList(GameObject mob, bool newMob)
        {
            //index 0, mob is new spawn
            if(newMob)
            {
                mobList.Add(mob);
            }
            //index 1, mob is dead, remove from list
            else
            {
                mobList.Remove(mob);
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameObject gameObject = new GameObject();

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
            //coordinateContains = new float[xTiles, yTiles];
            yHeight = graphics.GraphicsDevice.Viewport.Height / yTiles;
            xWidth = graphics.GraphicsDevice.Viewport.Width / xTiles;
            coordinatesX = new float[xTiles];
            coordinatesY = new float[yTiles];

            for (int x = 0; x < xTiles;)
            {
                for (int y = 0; y < yTiles;)
                {
                    coordinatesX[x] = x * xWidth;
                    coordinatesY[y] = y * yHeight; 
                    y++;
                }
                x++;
            }
            for (int i = 0; i < walkCoordinates.Length;)
            {
                walkCoordinates[i].X = walkCoordinates[i].X * xWidth;
                walkCoordinates[i].Y = walkCoordinates[i].Y * yHeight;
                i++;
            }

            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();

            dic = new Director(new PlayerBuilder());
            dic2 = new Director(new EnemyBuilder());
            GameObject player = dic.Construct(new Vector2(1,1));

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
            // TODO: use this.Content to load your game content here
            mapTexture = Content.Load<Texture2D>("map_yingyang");
            mapRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);


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

            spawnTimer += deltaTime;

            // TODO: Add your update logic here
            
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }
            if (spawnTimer >= 1.5f) { SpawnMob(); spawnTimer = 0; }
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

            spriteBatch.Draw(mapTexture, mapRect, Color.White);
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SpawnMob()
        {
            GameObject mob = dic2.Construct(new Vector2(coordinatesX[0], coordinatesY[14]));
            gameObjects.Add(mob);
            UpdateMobList(mob, true);
            //mob.GetComponent<Enemy>().
        }
    }
}