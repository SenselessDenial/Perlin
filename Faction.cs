using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Faction
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }

        public Faction(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public static Faction Neutral = new Faction("Neutral", Color.White);
        public static Faction OrangeDoves = new Faction("Orange Doves", Color.Orange);
        public static Faction GreenWolves = new Faction("Green Wolves", Color.Green);
        public static Faction PurpleDragons = new Faction("Purple Dragons", Color.Purple);
        public static Faction BlueFrogs = new Faction("Blue Frogs", Color.SkyBlue);

    }
}
