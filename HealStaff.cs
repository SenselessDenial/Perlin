﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class HealStaff : Weapon
    {

        public HealStaff()
            : base("Heal Staff", WeaponTextures[1, 2], WeaponTypes.Staff, 5, 1, 1, 100, true) 
        {
            TileColor = Color.Green;
        }

        public override int CalculateDodge(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return 0;
        }

        public override int CalculateRawAccuracy(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return 100;
        }

        public override int CalculateRawDamage(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return Damage + user.Magic / 2;
        }

        public override int CalculateReduction(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return 0;
        }

        public override bool CanBeCountered(Unit user, Unit defender, Weapon defenderWeapon)
        {
            return false;
        }

        public override bool IsValidTarget(Unit user, Unit target)
        {
            return user.Faction == target.Faction && target.HP < target.MaxHP;
        }

        public override void Attack(Unit user, Unit defender, Weapon defendWeapon)
        {
            int heal = CalculateRawDamage(user, defender, defendWeapon);

            defender.Heal(heal);

            Logger.Log(user.Name + " has healed " + defender.Name + " for " + heal + " HP.");
        }

    }
}
