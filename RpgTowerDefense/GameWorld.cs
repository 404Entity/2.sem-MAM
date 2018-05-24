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

        List<Enemy> mobList = new List<Enemy>();
        void UpdateMobList(Enemy mob, bool newMob)
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
        BackGround backGround = new BackGround();

        Texture2D yyMap;
        Rectangle mapRect;
        public Vector2[] walkCoordinates = { new Vector2(5, 15), new Vector2(5, 2), new Vector2(17, 2), new Vector2(17, 8), new Vector2(11, 8), new Vector2(11, 15), new Vector2(23, 15), new Vector2(23, 2), new Vector2(32, 2) };
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

            for(int i = 0; i < walkCoordinates.Length;)
            {
                walkCoordinates[i].X = walkCoordinates[i].X * xWidth;
                walkCoordinates[i].Y = walkCoordinates[i].Y * yHeight;
                i++;
            }

            mapRect = new Rectangle (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();

            dic = new Director(new PlayerBuilder());
            dic2 = new Director(new EnemyBuilder());
            GameObject player = dic.Construct(new Vector2(1,1));
            GameObject enemy = dic2.Construct(new Vector2(0, 280));
            gameObjects.Add(player);
            gameObjects.Add(enemy);

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
            
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
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