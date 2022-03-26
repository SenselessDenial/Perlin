using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class ModiferList
    {
        public Unit Unit { get; private set; }

        int[] StatBonuses;
        List<Modifier> Modifiers;

        public int this[ModiferStats stat] => StatBonuses[(int)stat];

        public ModiferList(Unit unit)
        {
            Unit = unit;
            StatBonuses = new int[Enum.GetValues(typeof(ModiferStats)).Length];
            Modifiers = new List<Modifier>();
        }
        
        public void Reevaluate()
        {
            int maxhp = Unit.MaxHP;

            for (int i = 0; i < StatBonuses.Length; i++)
                StatBonuses[i] = 0;

            foreach (var item in Modifiers)
                StatBonuses[((int)item.Stat)] += item.Value;

            Unit.HP += Unit.MaxHP - maxhp;
        }

        public void AddModifier(Modifier modifier)
        {
            Modifiers.Add(modifier);
            Reevaluate();
        }

        public void RemoveModifier(Modifier modifier)
        {
            Modifiers.Remove(modifier);
            Reevaluate();
        }

        public void AddModifier(List<Modifier> modifiers)
        {
            if (modifiers == null)
                return;
            foreach (var item in modifiers)
            {
                Modifiers.Add(item);
            }
            Reevaluate();
        }

        public void RemoveModifier(List<Modifier> modifiers)
        {
            if (modifiers == null)
                return;
            foreach (var item in modifiers)
            {
                Modifiers.Remove(item);
            }
            Reevaluate();
        }



    }

    enum ModiferStats
    {
        MaxHP = 0,
        Strength,
        Magic,
        Defense,
        Resilience,
        Speed,
        Dexterity,
        Luck,
        DodgeBonus,
        AccuracyBonus,
        MovementBonus
    }

    struct Modifier
    {
        public ModiferStats Stat;
        public int Value;

        public Modifier(ModiferStats stat, int value)
        {
            Stat = stat;
            Value = value;
        }
    }

}
