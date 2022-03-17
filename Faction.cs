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
        public static Faction OrangeDoves = new Faction("Topaz Kingdom", new Color(232, 155, 0));
        public static Faction GreenWolves = new Faction("Emerald Nation", new Color(0, 232, 135));
        public static Faction PurpleDragons = new Faction("Amethyst Empire", new Color(193, 0, 232));
        public static Faction BlueFrogs = new Faction("Sapphire Republic", new Color(0, 104, 232));

    }
}
