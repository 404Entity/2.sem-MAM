using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace RpgTowerDefense
{
    public class GameWorldBuilder
    {
        GameWorld gw = GameWorld._Instance;
        
        //dictates ammount of tiles for generation
        public int xTiles = 32;
        public int yTiles = 18;
        //indicates the dimensions of the tiles
        public float xWidth;
        public float yHeight;
        //list of coordinates on grid, curently not used
        float[] coordinatesX;
        float[] coordinatesY;

        //data for map, needs to be texture for scalability
        public Texture2D yyMap;
        public Rectangle map1Rect;
        public Texture2D mineMap;
        public Rectangle map2Rect;

        //world 1 data

        //list of vectors to indicate what direction enemy will be facing 
        Vector2[] walkdirection1 = { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(1, 0) };


        public GameWorldBuilder()
        {
            yHeight = GameWorld._Instance.graphics.GraphicsDevice.Viewport.Height / yTiles;
            xWidth = GameWorld._Instance.graphics.GraphicsDevice.Viewport.Width / xTiles;
        }

        public void SetupData()
        {

            //coordinateContains = new float[xTiles, yTiles];
            //saves dimension of tiles as a result of the size of viewport, needed for scalability

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

            //sets coordinates for buildspots to be pixel corrdinates instead of tile coordinates
            for (int i = 0; i < gw.buildSpotLocation.Length;)
            {
                gw.buildSpotLocation[i] = new Vector2(gw.buildSpotLocation[i].X * xWidth, gw.buildSpotLocation[i].Y * yHeight);
                i++;
            }
            gw.buildSpotAvailable = new bool[gw.buildSpotLocation.Length];

            //sets monster waypoints to be pixel coordinates instead of tile coordinates
            for (int i = 0; i < gw.walkCoordinates.Length;)
            {
                gw.walkCoordinates[i] = new Vector2(gw.walkCoordinates[i].X * xWidth, gw.walkCoordinates[i].Y * yHeight);
                i++;
            }
        }

        public void AssignWorld(int index)
        {
            if (index == 0)
            {
                gw.currentMap = yyMap;
                gw.currentRect = map1Rect;
            }
        }
            //World 2, mine
            /*for (int i = 0; i < buildSpotLocation2.Length;)
            {
                buildSpotLocation2[i].X = buildSpotLocation2[i].X * xWidth;
                buildSpotLocation2[i].Y = buildSpotLocation2[i].Y * yHeight;
                i++;
            }
            buildSpotAvailable2 = new bool[buildSpotLocation2.Length];

            for (int i = 0; i < walkCoordinates1.Length;)
            {
                walkCoordinates2[i].X = walkCoordinates2[i].X * xWidth;
                walkCoordinates2[i].Y = walkCoordinates2[i].Y * yHeight;
                i++;
            }*/
            
    }
}
