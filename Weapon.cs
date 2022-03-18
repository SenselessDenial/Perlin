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
        public Color TileColor { get; protected set; }

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
            TileColor = Color.Red;
        }

        public virtual void OnEquipped(Unit unit)
        {

        }

        public virtual void OnDequipped(Unit unit)
        {

        }

        public virtual int CalculateRawDamage(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return IsMagic ? user.Magic + Damage : user.Strength + Damage;
        }

        public virtual int CalculateRawAccuracy(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return user.Dexterity + Accuracy;
        }

        public virtual int CalculateReduction(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return IsMagic ? defender.Resilience : defender.Defense;
        }

        public virtual int CalculateDodge(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return defender.Dodge;
        }

        public virtual bool CanBeCountered(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return defender.InRangeOf(user);
        }

        public virtual bool IsValidTarget(Unit user, Unit target)
        {
            return target.Faction != user.Faction;
        }

        public virtual void Attack(Unit user, Unit defender, Weapon defendWeapon)
        {
            int damage = CalculateRawDamage(user, defender, defendWeapon);
            int accuracy = CalculateRawAccuracy(user, defender, defendWeapon);

            int reduction = CalculateReduction(user, defender, defendWeapon);
            int dodge = CalculateDodge(user, defender, defendWeapon);

            int chance = Calc.Next(0, 100);

            int trueDamage = (damage - reduction <= 0) ? 0 : damage - reduction;

            if (accuracy - dodge >= chance)
            {
                defender.HP -= trueDamage;
                Logger.Log(user.Name + " has attacked " + defender.Name + " for " + trueDamage + " damage.");
            }
            else
            {
                Logger.Log(user.Name + " has missed an attack on " + defender.Name + ".");
            }
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

        public static Swordkiller Swordkiller = new Swordkiller();
        public static HealStaff HealStaff = new HealStaff();

    }
}
