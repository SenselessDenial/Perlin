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
        public static Tilemap WeaponTextures = new Tilemap(new GTexture("weapons.png"), 16, 16);

        public string Name { get; private set; }
        public GTexture Texture { get; private set; }
        public WeaponTypes Type { get; private set; }
        public int Damage { get; private set; }
        public int MinRange { get; private set; }
        public int MaxRange { get; private set; }
        public int Accuracy { get; private set; }
        public bool IsMagic { get; private set; }
        public bool IsRanged => MaxRange == 1;

        protected Weapon(string name, GTexture texture, WeaponTypes type, int damage, int minRange, int maxRange, int accuracy, bool isMagic)
        {
            Name = name;
            Texture = texture;
            Type = type;
            Damage = damage;
            MinRange = minRange;
            MaxRange = maxRange;
            Accuracy = accuracy;
            IsMagic = isMagic;
        }

        public virtual void OnEquipped(Unit unit)
        {

        }

        public virtual void OnDequipped(Unit unit)
        {

        }

        public virtual int CalculateRawDamage(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return IsMagic ? user.Stats.Magic + Damage : user.Stats.Strength + Damage;
        }

        public virtual int CalculateRawAccuracy(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return user.Stats.Dexterity + Accuracy;
        }

        public virtual int CalculateReduction(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return IsMagic ? defender.Stats.Resilience : defender.Stats.Defense;
        }

        public enum WeaponTypes
        {
            None,
            Sword,
            Lance,
            Axe,
            Bow,
            Spell,
            Staff
        }

        public static Weapon IronSword = new Weapon("Iron Sword", WeaponTextures[1], WeaponTypes.Sword, 6, 1, 1, 80, false);
        public static Weapon IronSpear = new Weapon("Iron Spear", WeaponTextures[3], WeaponTypes.Lance, 5, 1, 1, 85, false);
        public static Weapon WoodenBow = new Weapon("Wooden Bow", WeaponTextures[7], WeaponTypes.Bow, 6, 2, 2, 70, false);
        public static Weapon IronAxe = new Weapon("Iron Axe", WeaponTextures[5], WeaponTypes.Axe, 8, 1, 1, 60, false);

    }
}
