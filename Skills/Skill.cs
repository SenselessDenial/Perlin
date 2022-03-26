using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Skill
    {
        public string Name { get; private set; }

        public Skill(string name)
        {
            Name = name;
        }

        public virtual void OnEquipped(Unit user)
        {

        }

        public virtual void OnDequipped(Unit user)
        {

        }


        public virtual void OnEnteringCombat(Unit user, Unit opponent)
        {

        }

        public virtual void OnLeavingCombat(Unit user, Unit opponent)
        {

        }


        public static PermBonus Str2 = new PermBonus("Strength +2", new Modifier(ModiferStats.Strength, 2));
        public static PermBonus HP5 = new PermBonus("HP +5", new Modifier(ModiferStats.MaxHP, 5));






    }
}
