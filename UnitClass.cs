using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class UnitClass
    {
        public string Name { get; private set; }



        public UnitClass(string name)
        {
            Name = name;
        }




        public static UnitClass Villager = new UnitClass("Villager");

       
    }
}
