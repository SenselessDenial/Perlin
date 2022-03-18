using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Swordkiller : Weapon
    {

        public Swordkiller()
            : base("Sword Killer", WeaponTextures[4], WeaponTypes.Lance, 5, 1, 1, 80, false)
        {

        }

        public override int CalculateRawAccuracy(Unit user, Unit defender, Weapon defenderWeapon)
        {
            int raw = base.CalculateRawAccuracy(user, defender, defenderWeapon);

            if (defenderWeapon.Type == WeaponTypes.Sword)
            {
                raw += 20;
            }

            return raw;
        }

        public override int CalculateRawDamage(Unit user, Unit defender, Weapon defenderWeapon)
        {
            int raw = base.CalculateRawDamage(user, defender, defenderWeapon);

            if (defenderWeapon.Type == WeaponTypes.Sword)
            {
                raw += 5;
            }
            return raw;
        }

        public override void OnEquipped(Unit unit)
        {
            Logger.Log("You feel your bones shiver...");
        }

    }
}
