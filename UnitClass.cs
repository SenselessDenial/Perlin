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
        public int Movement { get; private set; }
        public MovementTypes MovementType { get; private set; }
        public Weapon.WeaponTypes WeaponType { get; private set; }

        public List<Modifier> Modifiers { get; private set; }

        public UnitClass(string name, int movement, MovementTypes movementType, Weapon.WeaponTypes weaponType, List<Modifier> modifiers)
        {
            Name = name;
            Movement = movement;
            MovementType = movementType;
            WeaponType = weaponType;
            Modifiers = modifiers;
        }

        public UnitClass(string name) 
            : this(name, 3, MovementTypes.Foot, Weapon.WeaponTypes.None, null) { }

        public virtual void OnEquipped(Unit unit)
        {
            unit.ModiferList.AddModifier(Modifiers);
        }

        public virtual void OnDequipped(Unit unit)
        {
            unit.ModiferList.RemoveModifier(Modifiers);
        }



        public static UnitClass Villager = new UnitClass("Villager");
        public static UnitClass Swordsman = new UnitClass("Swordsman", 3, MovementTypes.Foot, Weapon.WeaponTypes.Sword, new List<Modifier>() 
        { 
            new Modifier(ModiferStats.Strength, 1),
            new Modifier(ModiferStats.Speed, 1),
            new Modifier(ModiferStats.MaxHP, 1),
            new Modifier(ModiferStats.Magic, -1)

        });
        public static UnitClass Pikeman = new UnitClass("Pikeman", 4, MovementTypes.Foot, Weapon.WeaponTypes.Lance, new List<Modifier>()
        {
            new Modifier(ModiferStats.Strength, 1),
            new Modifier(ModiferStats.Speed, 1),
            new Modifier(ModiferStats.Dexterity, 1),
            new Modifier(ModiferStats.Magic, -1)
        });
        public static UnitClass Axeman = new UnitClass("Axeman", 3, MovementTypes.Foot, Weapon.WeaponTypes.Axe, new List<Modifier>()
        {
            new Modifier(ModiferStats.Strength, 2),
            new Modifier(ModiferStats.MaxHP, 3),
            new Modifier(ModiferStats.Magic, -1),
            new Modifier(ModiferStats.Resilience, -2)

        });
        public static UnitClass Archer = new UnitClass("Archer", 3, MovementTypes.Foot, Weapon.WeaponTypes.Bow, new List<Modifier>()
        {
            new Modifier(ModiferStats.Speed, 2),
            new Modifier(ModiferStats.Dexterity, 2),
            new Modifier(ModiferStats.Magic, -1),
            new Modifier(ModiferStats.Luck, -1)

        });
        public static UnitClass Cavalry = new UnitClass("Cavalry", 7, MovementTypes.Horse, Weapon.WeaponTypes.Lance, new List<Modifier>()
        {
            new Modifier(ModiferStats.Strength, 1),
            new Modifier(ModiferStats.Defense, 2),
            new Modifier(ModiferStats.Dexterity, 1),
            new Modifier(ModiferStats.Speed, 3),
            new Modifier(ModiferStats.Magic, -2),
            new Modifier(ModiferStats.Resilience, -2),
            new Modifier(ModiferStats.Luck, -2)
        });

        public static UnitClass Wizard = new UnitClass("Wizard", 3, MovementTypes.Foot, Weapon.WeaponTypes.Staff, new List<Modifier>()
        {
            new Modifier(ModiferStats.Magic, 4),
            new Modifier(ModiferStats.Resilience, 2),
            new Modifier(ModiferStats.Strength, -3),
            new Modifier(ModiferStats.Defense, -2)
        });

        public enum MovementTypes
        {
            Foot,
            Horse
        }

       
    }
}
