using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangplankEngine;

namespace Perlin
{
    class Tile
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("newtilemap.png"), 16, 16);
        public GTexture Texture { get; private set; }

        public Tile(GTexture texture)
        {
            Texture = texture;
        }

        public static Tile Grass = new Tile(tilemap[1]);
        public static Tile Forest = new Tile(tilemap[2]);
        public static Tile Mountain = new Tile(tilemap[3]);
        public static Tile Water = new Tile(tilemap[4]);

    }
}
