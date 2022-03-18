using GangplankEngine;

namespace Perlin
{
    static class CombatHandler
    {
        public static void Attack(Unit attacker, Weapon attackWeapon, Unit defender, Weapon defendWeapon)
        {
            int damage = attackWeapon.CalculateRawDamage(attacker, defender, defendWeapon);
            int accuracy = attackWeapon.CalculateRawAccuracy(attacker, defender, defendWeapon);

            int reduction = attackWeapon.CalculateReduction(attacker, defender, defendWeapon);
            int dodge = attackWeapon.CalculateDodge(attacker, defender, defendWeapon);

            int chance = Calc.Next(0, 100);

            int trueDamage = (damage - reduction <= 0) ? 0 : damage - reduction;

            if (accuracy - dodge >= chance)
            {
                defender.HP -= trueDamage;
                Logger.Log(attacker.Name + " has attacked " + defender.Name + " for " + trueDamage + " damage.");
            }
            else
            {
                Logger.Log(attacker.Name + " has missed an attack on " + defender.Name + ".");
            }
        }

        public static void FindNumbers(Unit attacker, Unit defender, out int a2dDmg, out int a2dAcc, out int d2aDmg, out int d2aAcc)
        {
            FindNumbers(attacker, defender, out a2dDmg, out a2dAcc);
            if (attacker.Weapon.CanBeCountered(attacker, defender, defender.Weapon))
                FindNumbers(defender, attacker, out d2aDmg, out d2aAcc);
            else
            {
                d2aDmg = 0;
                d2aAcc = 0;
            }
              
        }

        public static void FindNumbers(Unit attacker, Unit defender, out int a2dDmg, out int a2dAcc)
        {
            Weapon attackWeapon = attacker.Weapon;
            Weapon defendWeapon = defender.Weapon;

            int damage = attackWeapon.CalculateRawDamage(attacker, defender, defendWeapon);
            int accuracy = attackWeapon.CalculateRawAccuracy(attacker, defender, defendWeapon);

            int reduction = attackWeapon.CalculateReduction(attacker, defender, defendWeapon);
            int dodge = attackWeapon.CalculateDodge(attacker, defender, defendWeapon);

            int trueDamage = (damage - reduction <= 0) ? 0 : damage - reduction;
            int trueAccuracy = accuracy - dodge;
            if (trueAccuracy <= 0)
                trueAccuracy = 0;
            else if (trueAccuracy >= 100)
                trueAccuracy = 100;

            a2dDmg = trueDamage;
            a2dAcc = trueAccuracy;
        }


        public static void AttackPlus(Unit attacker, Weapon attackWeapon, Unit defender, Weapon defendWeapon)
        {
            attackWeapon.Attack(attacker, defender, defendWeapon);
            if (attackWeapon.CanBeCountered(attacker, defender, defendWeapon))
                defendWeapon.Attack(defender, attacker, attackWeapon);
        }















    }
}
