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


        public UnitClass(string name, int movement, MovementTypes movementType, Weapon.WeaponTypes weaponType)
        {
            Name = name;
            Movement = movement;
            MovementType = movementType;
            WeaponType = weaponType;
        }

        public UnitClass(string name) 
            : this(name, 3, MovementTypes.Foot, Weapon.WeaponTypes.None) { }

        public static UnitClass Villager = new UnitClass("Villager");
        public static UnitClass Swordsman = new UnitClass("Swordsman", 3, MovementTypes.Foot, Weapon.WeaponTypes.Sword);
        public static UnitClass Pikeman = new UnitClass("Pikeman", 4, MovementTypes.Foot, Weapon.WeaponTypes.Lance);
        public static UnitClass Axeman = new UnitClass("Axeman", 3, MovementTypes.Foot, Weapon.WeaponTypes.Axe);
        public static UnitClass Archer = new UnitClass("Archer", 3, MovementTypes.Foot, Weapon.WeaponTypes.Bow);
        public static UnitClass Cavalry = new UnitClass("Cavalry", 7, MovementTypes.Horse, Weapon.WeaponTypes.Lance);

        public enum MovementTypes
        {
            Foot,
            Horse
        }

       
    }
}
