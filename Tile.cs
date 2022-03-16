using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Tile
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("newtilemap.png"), 16, 16);
        public string Name { get; private set; }
        public GTexture Texture { get; private set; }
        public int MovementCost = 1;
        public int DodgeBonus = 0;

        public Tile(string name, GTexture texture, int movementCost, int dodgeBonus)
        {
            Name = name;
            Texture = texture;
            MovementCost = movementCost;
            DodgeBonus = dodgeBonus;
        }

        public static Tile Grass = new Tile("Grass", tilemap[1], 1, 0);
        public static Tile Forest = new Tile("Forest", tilemap[2], 2, 10);
        public static Tile Mountain = new Tile("Mountain", tilemap[3], 3, 20);
        public static Tile Water = new Tile("Water", tilemap[4], 99, 0);

        public void Draw(Vector2 pos)
        {
            Texture.Draw(pos);
        }

        public void DrawCard(Vector2 pos)
        {
            Texture.Draw(pos);
            Drawing.Font.Draw(Name, pos + new Vector2(20, 0));
            Drawing.Font.Draw("MV Cost: " + MovementCost, pos + new Vector2(20, 10));
            Drawing.Font.Draw("Dodge: " + DodgeBonus, pos + new Vector2(20, 20));
        }


    }
}
