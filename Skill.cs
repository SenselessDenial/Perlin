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

        public virtual bool IsTrue(Unit user)
        {
            return true;
        }

        public virtual void OnTrue(Unit user)
        {

        }





    }
}
