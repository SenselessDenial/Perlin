using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Perlin
{
    class LandMap
    {

        private Point[,] map;
        private int Width;
        private int Height;

        public LandMap(int width, int height)
        {
            Width = width;
            Height = height;
            map = new Point[Width, Height];
        }


        public void UpdateMap()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {




                }
            }
        }






    }
}
