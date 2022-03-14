using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    public class Tilemap
    {
        public GTexture Parent;

        public GTexture[,] Textures;

        public int TileWidth;
        public int TileHeight;

        public int NumX;
        public int NumY;

        public Tilemap(GTexture parent, int tileWidth, int tileHeight)
        {
            Parent = parent;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            NumX = Parent.Width / TileWidth;
            NumY = Parent.Height / TileHeight;

            GenerateTextures();
        }

        private void GenerateTextures()
        {
            Textures = new GTexture[NumX, NumY];

            for (int i = 0; i < NumX; i++)
            {
                for (int j = 0; j < NumY; j++)
                {
                    Textures[i, j] = new GTexture(Parent, new Rectangle(i * TileWidth, j * TileHeight, TileWidth, TileHeight));
                }
            }
        }

        public GTexture this[int x, int y] => Textures[x, y];
        public GTexture this[int index] => Textures[index % NumX, index / NumX];








    }
}
