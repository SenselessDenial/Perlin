using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangplankEngine;

namespace Perlin
{
    static class CombatHandler
    {
        public static void Attack(Unit attacker, Weapon attackWeapon, Unit defender, Weapon defendweapon)
        {
            int damage = attackWeapon.CalculateRawDamage(attacker, defender, defendweapon);
            int accuracy = attackWeapon.CalculateRawAccuracy(attacker, defender, defendweapon);

            int reduction = attackWeapon.CalculateReduction(attacker, defender, defendweapon);
            int dodge = defender.CalculateDodge();

            int chance = Calc.Next(0, 100);

            int trueDamage = (damage - reduction <= 0) ? 0 : damage - reduction;

            if (accuracy - dodge >= chance)
            {
                defender.Stats.HP -= trueDamage;
                Logger.Log(attacker.Name + " has attacked " + defender.Name + " for " + trueDamage + " damage.");
            }
            else
            {
                Logger.Log(attacker.Name + " has missed an attack on " + defender.Name + ".");
            }
        }

        public static void AttackPlus(Unit attacker, Weapon attackWeapon, Unit defender, Weapon defendweapon)
        {
            Attack(attacker, attackWeapon, defender, defendweapon);
            if (defender.CanCounter(attacker, attackWeapon))
                Attack(defender, defendweapon, attacker, attackWeapon);
        }















    }
}
