using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace RpgTowerDefense
{
    public class GameWorldBuilder
    {
        
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
        {   }

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

            //World 1, maze
            for (int i = 0; i < GameWorld._Instance.buildSpotLocation.Length;)
            {
                GameWorld._Instance.buildSpotLocation[i].X = GameWorld._Instance.buildSpotLocation[i].X * xWidth;
                GameWorld._Instance.buildSpotLocation[i].Y = GameWorld._Instance.buildSpotLocation[i].Y * yHeight;
                i++;
            }
            GameWorld._Instance.buildSpotAvailable = new bool[GameWorld._Instance.buildSpotLocation.Length];

            for (int i = 0; i < GameWorld._Instance.walkCoordinates.Length;)
            {
                GameWorld._Instance.walkCoordinates[i].X = GameWorld._Instance.walkCoordinates[i].X * xWidth;
                GameWorld._Instance.walkCoordinates[i].Y = GameWorld._Instance.walkCoordinates[i].Y * yHeight;
                i++;
            }
        }

        public void AssignWorld(int index)
        {
            if (index == 0)
            {
                GameWorld._Instance.currentMap = yyMap;
                GameWorld._Instance.currentRect = map1Rect;
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
