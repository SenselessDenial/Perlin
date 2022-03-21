using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GangplankEngine;
using Microsoft.Xna.Framework;

namespace Perlin
{
    class FarmTile : Tile
    {
        Animation animation;
        public override GTexture Texture => animation.CurrentTexture;

        private int State
        {
            get => state;
            set
            {
                if (value < 0)
                    state = 0;
                else if (value > 3)
                    state = 3;
                else
                    state = value;

                animation.CurrentIndex = state;
            }
        }
        private int state = 0;


        public FarmTile()
            : base("Farm", null, 1, 1)
        {
            animation = new Animation();
            animation.AddFrames(tilemap[6], tilemap[7], tilemap[8], tilemap[9]);
            Texture = animation[0];
        }

        public override void OnStartOfTurn(Unit unit)
        {
            if (unit == null)
                State++;
        }

        public override void SteppedOn(Unit unit)
        {
            base.SteppedOn(unit);

            if (unit != null)
            {
                    unit.Heal(0.2f * State);
                    State = 0;
            }

        }



    }
}
