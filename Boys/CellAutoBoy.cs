using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GangplankEngine;

namespace Perlin
{
    class CellAutoBoy : Entity
    {

        bool[,] tiles;
        int dimensions = 64;

        Random random;
        int deathCount = 1;
        int birthCount = 5;
        float standard = 0.3f;

        Tilemap tilemap;

        public CellAutoBoy(Scene scene, int seed) : base(scene)
        {
            tiles = new bool[dimensions, dimensions];
            random = new Random(seed);
            Generate(standard);

            tilemap = new Tilemap(new GTexture("islandtilemap.png"), 16, 16);
        }

        public void Generate(float amount)
        {
            Clear();
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if ((float)random.NextDouble() < amount)
                    {
                        tiles[i, j] = true;
                    }
                }
            }
        }

        private void Clear()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    tiles[i, j] = false;
                }
            }
        }

        public void DoStep(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                DoStep();
            }
        }

        public void DoStep()
        {
            bool[,] newMap = new bool[dimensions, dimensions];

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    int neighbors = CountNeighbors(tiles, i, j);
                    newMap[i, j] = (tiles[i, j] == true) ? (neighbors > deathCount) : (neighbors >= birthCount);
                }
            }
            tiles = newMap;
        }

        private int Count(bool[,] map, bool condition)
        {
            int count = 0;
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (map[i, j] == condition)
                        count++;
                }
            }
            return count;
        }

        public void IslandGen(int nSteps)
        {
            deathCount = 1;
            birthCount = 5;

            DoStep(20);
            deathCount = 2;
            DoStep(20);
            birthCount = 4;
            DoStep(nSteps);
            birthCount = 5;
            DoStep(20);
            deathCount = 3;
            DoStep(20);
        }

        private int CountNeighbors(bool[,] map, int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;

                    if (i == 0 && j == 0)
                    {
                        // Nothing
                    }
                    else if (nx < 0 || ny < 0 || nx >= dimensions || ny >= dimensions)
                    {
                        //count++;
                    }
                    else if (map[nx, ny] == true)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool Borders(bool[,] map, int x, int y, bool borderCondition)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;

                    if (i == 0 && j == 0)
                    {
                        // Nothing
                    }
                    else if (nx < 0 || ny < 0 || nx >= dimensions || ny >= dimensions)
                    {
                        return true;
                    }
                    else if (map[nx, ny] == borderCondition)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Surroundings FindSurroundings(bool[,] map, int x, int y)
        {
            bool[] array = new bool[8];

            int index = 0;

            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    int nx = x + i;
                    int ny = y + j;

                    if (i == 0 && j == 0)
                    {
                        // Nothing
                    }
                    else if (nx < 0 || ny < 0 || nx >= dimensions || ny >= dimensions)
                    {
                        array[index] = false;
                        index++;
                    }
                    else
                    {
                        array[index] = map[nx, ny];
                        index++;
                    }
                }
            }

            return new Surroundings(array);
        }


        private struct Surroundings
        {
            public bool TopLeft;
            public bool TopCenter;
            public bool TopRight;
            public bool CenterLeft;
            public bool CenterRight;
            public bool BottomLeft;
            public bool BottomCenter;
            public bool BottomRight;

            public Surroundings(bool topLeft, bool topCenter, bool topRight, bool centerLeft, bool centerRight, bool bottomLeft, bool bottomCenter, bool bottomRight)
            {
                TopLeft = topLeft;
                TopCenter = topCenter;
                TopRight = topRight;
                CenterLeft = centerLeft;
                CenterRight = centerRight;
                BottomLeft = bottomLeft;
                BottomCenter = bottomCenter;
                BottomRight = bottomRight;
            }

            public Surroundings(bool[] boolArray) 
                : this(boolArray[0], boolArray[1], boolArray[2], boolArray[3], boolArray[4], boolArray[5], boolArray[6], boolArray[7]) { }
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(Keys.Right))
                DoStep();
            if (Input.Pressed(Keys.Left))
                Generate(standard);

            if (Input.Pressed(Keys.Up))
            {
                deathCount++;
                Logger.Log("Death Count: " + deathCount);
            }
            if (Input.Pressed(Keys.Down))
            {
                deathCount--;
                Logger.Log("Death Count: " + deathCount);
            }

            if (Input.Pressed(Keys.T))
            {
                birthCount++;
                Logger.Log("Birth Count: " + birthCount);
            }
            if (Input.Pressed(Keys.G))
            {
                birthCount--;
                Logger.Log("Birth Count: " + birthCount);
            }

            if (Input.Check(Keys.Enter))
                DoStep();

            if (Input.Pressed(Keys.E))
            {
                Generate(standard);
                IslandGen(5);
            }

        }


        public override void Render()
        {
            base.Render();

            RenderTilemap();
            
        }

        public void RenderTilemap()
        {
            int width = tilemap.TileWidth;
            int height = tilemap.TileHeight;



            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    GTexture temp;

                    if (tiles[i, j] == true)
                    {
                        if (Borders(tiles, i, j, false))
                        {
                            temp = tilemap[3];
                        }
                        else
                        {
                            temp = tilemap[1];
                        }
                    }
                    else
                    {
                        Surroundings surroundings = FindSurroundings(tiles, i, j);

                        if (surroundings.TopCenter == true)
                        {
                            temp = tilemap[4];
                        }
                        else
                        {
                            temp = tilemap[2];
                        }


                        
                    }

                    temp?.Draw(new Vector2(i * width, j * height));
                }
            }
        }

        public void RenderPixels()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (tiles[i, j] == true)
                    {
                        Color c = Color.Green;

                        if (Borders(tiles, i, j, false))
                        {
                            c = Color.Tan;
                        }

                        Drawing.DrawPoint(new Vector2(i, j), c);
                    }
                    else
                    {
                        if (Borders(tiles, i, j, true))
                        {
                            Drawing.DrawPoint(new Vector2(i, j), Color.Blue);
                        }
                    }

                }
            }
        }





    }
}
