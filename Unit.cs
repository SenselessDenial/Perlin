using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Unit
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("units.png"), 16, 16);
        public string Name { get; private set; }
        public GTexture Texture { get; private set; }
        public WeaponStruct Weapon;

        public Unit(string name, GTexture texture, Weapon weapon)
        {
            Name = name;
            Texture = texture;
            Weapon = new WeaponStruct(weapon);
        }


    }
}
