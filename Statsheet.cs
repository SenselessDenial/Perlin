using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perlin
{
    class Statsheet
    {
        public int HP
        {
            get => hp;
            set
            {
                if (value <= 0)
                    hp = 0;
                else if (value >= MaxHP)
                    hp = MaxHP;
                else
                    hp = value;
            }
        }

        private int hp;
        public int MaxHP;

        public int Strength;
        public int Magic;
        public int Defense;
        public int Resilience;
        public int Speed;
        public int Dexterity;
        public int Luck;

        public static Statsheet Demo => new Statsheet(20, 5, 5, 5, 5, 5, 5, 5);

        public Statsheet(int hP, int strength, int magic, int defense, int resilience, int speed, int dexterity, int luck)
        {
            MaxHP = hP;
            HP = hP;
            Strength = strength;
            Magic = magic;
            Defense = defense;
            Resilience = resilience;
            Speed = speed;
            Dexterity = dexterity;
            Luck = luck;
        }


    }
}
