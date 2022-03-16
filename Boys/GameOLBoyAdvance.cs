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
    class GameOLBoyAdvance : Entity
    {

        bool[,] tiles;
        int width = 256;
        int height = 256;
        Random random;
        int deathCount = 1;
        int birthCount = 5;
        float startingGrowth = 0.3f;




        public GameOLBoyAdvance(Scene scene, int seed) : base(scene)
        {
            tiles = new bool[width, height];
            random = new Random(seed);
            Generate(startingGrowth);
        }

        public void Generate(float amount)
        {
            Clear();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
            bool[,] newMap = new bool[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
                    else if (nx < 0 || ny < 0 || nx >= width || ny >= height)
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
                    else if (nx < 0 || ny < 0 || nx >= width || ny >= height)
                    {

                    }
                    else if (map[nx, ny] == borderCondition)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void FindIslands()
        {
            bool[,] claimed = new bool[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {







                }
            }
        }
    }
}
