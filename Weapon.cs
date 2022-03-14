using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Weapon
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("weapons.png"), 16, 16);
        public string Name { get; private set; }
        public GTexture Texture { get; private set; }

        public Weapon(string name, GTexture texture)
        {
            Name = name;
            Texture = texture;
        }



        public static Weapon Sword = new Weapon("Sword", tilemap[1]);
        
    }

    struct WeaponStruct
    {
        public Weapon Weapon;

        public WeaponStruct(Weapon weapon)
        {
            Weapon = weapon;
        }
    }


}
