using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perlin
{
    class PermBonus : Skill
    {
        public Modifier Modifier { get; private set; }

        public PermBonus(string name, Modifier modifier)
            : base(name) 
        {
            Modifier = modifier;
        }

        public override void OnEquipped(Unit user)
        {
            user.AddModifier(Modifier);
        }

        public override void OnDequipped(Unit user)
        {
            user.RemoveModifier(Modifier);
        }







    }
}
