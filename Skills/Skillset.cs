using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangplankEngine;

namespace Perlin
{
    class Skillset
    {
        public Unit Unit { get; private set; }

        private List<Skill> skills;


        public Skillset(Unit unit)
        {
            Unit = unit;
            skills = new List<Skill>();
        }
        
        public void Add(Skill skill)
        {
            if (!skills.Contains(skill))
            {
                skills.Add(skill);
                skill.OnEquipped(Unit);
            }
        }

        public void Remove(Skill skill)
        {
            if (skills.Contains(skill))
            {
                skills.Remove(skill);
                skill.OnDequipped(Unit);
            }
        }

        public void OnEnteringCombat(Unit opponent)
        {
            foreach (var item in skills)
                item.OnEnteringCombat(Unit, opponent);
        }

        public void OnLeavingCombat(Unit opponent)
        {
            foreach (var item in skills)
                item.OnLeavingCombat(Unit, opponent);
        }










    }
}
