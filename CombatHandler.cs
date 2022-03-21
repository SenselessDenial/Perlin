using GangplankEngine;

namespace Perlin
{
    static class CombatHandler
    {
        public static void FindNumbers(Unit attacker, Unit defender, out int a2dDmg, out int a2dAcc, out int d2aDmg, out int d2aAcc)
        {
            FindNumbers(attacker, defender, out a2dDmg, out a2dAcc);
            if (attacker.Weapon.CanBeCountered(attacker, defender) && defender.Weapon.CanCounter(defender, attacker))
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

            int damage = attackWeapon.CalculateRawDamage(attacker, defender);
            int accuracy = attackWeapon.CalculateRawAccuracy(attacker, defender);

            int reduction = attackWeapon.CalculateReduction(attacker, defender);
            int dodge = attackWeapon.CalculateDodge(attacker, defender);

            int trueDamage = (damage - reduction <= 0) ? 0 : damage - reduction;
            int trueAccuracy = accuracy - dodge;
            if (trueAccuracy <= 0)
                trueAccuracy = 0;
            else if (trueAccuracy >= 100)
                trueAccuracy = 100;

            a2dDmg = trueDamage;
            a2dAcc = trueAccuracy;
        }

        public static void Attack(Unit attacker, Weapon attackWeapon, Unit defender, Weapon defendWeapon)
        {
            bool a2dHit;
            attackWeapon.Attack(attacker, defender, out a2dHit);
            if (defender.IsDead)
                defender.OnDeath(attacker);
            else if (attackWeapon.CanBeCountered(attacker, defender) && defendWeapon.CanCounter(defender, attacker))
                defendWeapon.Attack(defender, attacker);

            if (attacker.IsDead)
                attacker.OnDeath(defender);
            else if (a2dHit)
                attacker.AfterCombat(defender);
        }















    }
}
