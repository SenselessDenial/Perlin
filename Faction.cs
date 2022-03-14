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
        public static Faction BluTeam = new Faction("Blu Team", Color.Blue);
        public static Faction RedTeam = new Faction("Red Team", Color.Red);

    }
}
