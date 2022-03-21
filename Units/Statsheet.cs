using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangplankEngine;

namespace Perlin
{
    class Statsheet
    {
        public Unit Unit { get; private set; }

        public int Level { get; private set; }

        public int XP
        {
            get => xp;
            set
            {
                if (value < 0)
                    XP = 0;
                else if (value >= 100)
                    LevelUp();
                else
                    xp = value;
            }
        }

        private int xp;

        private void LevelUp()
        {
            Level += 1;
            XP -= 100;

            for (int i = 0; i < NumOfStats; i++)
            {
                int growthChance = growths[i] + Unit.Class.Growths[i];
                int chance = Calc.Next(0, 100);

                int growth = 0;

                while (growthChance > 100)
                {
                    growth++;
                    growthChance -= 100;
                }

                if (growthChance > chance)
                    growth++;

                stats[i] += growth;
            }
        }

        public int TotalStats
        {
            get
            {
                int total = 0;
                for (int i = 0; i < NumOfStats; i++)
                {
                    total += stats[i];
                }
                return total;
            }
        }


        public static readonly int NumOfStats = Enum.GetValues(typeof(Stats)).Length;

        private int[] stats;
        private int[] growths;

        public int GetStat(Stats stat)
        {
            return stats[(int)stat];
        }

        public int GetGrowth(Stats stat)
        {
            return growths[(int)stat];
        }

        public static Statsheet Demo(Unit unit)
        {
            return new Statsheet(unit, 20, 5, 5, 5, 5, 5, 5, 5);
        }

        public Statsheet(Unit unit, int hP, int strength, int magic, int defense, int resilience, int speed, int dexterity, int luck, 
                         int hpG, int strG, int magG, int defG, int resG, int spdG, int dexG, int lckG)
        {
            Unit = unit;
            stats = new int[NumOfStats];
            growths = new int[NumOfStats];
            Level = 1;
            XP = 0;

            stats[(int)Stats.MaxHP] = hP;
            stats[(int)Stats.Strength] = strength;
            stats[(int)Stats.Magic] = magic;
            stats[(int)Stats.Defense] = defense;
            stats[(int)Stats.Resilience] = resilience;
            stats[(int)Stats.Speed] = speed;
            stats[(int)Stats.Dexterity] = dexterity;
            stats[(int)Stats.Luck] = luck;

            growths[(int)Stats.MaxHP] = hpG;
            growths[(int)Stats.Strength] = strG;
            growths[(int)Stats.Magic] = magG;
            growths[(int)Stats.Defense] = defG;
            growths[(int)Stats.Resilience] = resG;
            growths[(int)Stats.Speed] = spdG;
            growths[(int)Stats.Dexterity] = dexG;
            growths[(int)Stats.Luck] = lckG;

        }

        public Statsheet(Unit unit, int hP, int strength, int magic, int defense, int resilience, int speed, int dexterity, int luck)
            : this(unit, hP, strength, magic, defense, resilience, speed, dexterity, luck, 0, 0, 0, 0, 0, 0, 0, 0) { }

    }

    public enum Stats
    {
        MaxHP = 0,
        Strength,
        Magic,
        Defense,
        Resilience,
        Speed,
        Dexterity,
        Luck
    }

}
