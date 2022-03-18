﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Tile
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("newtilemap.png"), 16, 16);
        public string Name { get; protected set; }
        public virtual GTexture Texture { get; protected set; }
        private int MvCostFoot = 1;
        private int MvCostHorse = 1;

        public Tile(string name, GTexture texture, int mvCostFoot, int mvCostHorse)
        {
            Name = name;
            Texture = texture;
            MvCostFoot = mvCostFoot;
            MvCostHorse = mvCostHorse;
        }

        public virtual void OnStartOfTurn(Unit unit) { }

        public virtual void SteppedOn(Unit unit) { }

        public virtual void SteppedOff(Unit unit) { }

        public int MovementCost(UnitClass.MovementTypes movementType)
        {
            switch (movementType)
            {
                case UnitClass.MovementTypes.Foot:
                    return MvCostFoot;
                case UnitClass.MovementTypes.Horse:
                    return MvCostHorse;
                default:
                    return MvCostFoot;
            }
        }

        public int MovementCost(UnitClass unitClass)
        {
            return MovementCost(unitClass.MovementType);
        }

        public int MovementCost(Unit unit)
        {
            return MovementCost(unit.Class.MovementType);
        }

        public void Draw(Vector2 pos)
        {
            Texture.Draw(pos);
        }

        public virtual void DrawCard(Vector2 pos)
        {
            Texture.Draw(pos);
            Drawing.Font.Draw(Name, pos + new Vector2(20, 0));
            Drawing.Font.Draw("MV Cost: " + MvCostFoot, pos + new Vector2(20, 10));
        }

        public static Tile Grass = new Tile("Grass", tilemap[1], 1, 1);
        public static Tile Forest = new Tile("Forest", tilemap[2], 2, 5);
        public static Tile Mountain = new Tile("Mountain", tilemap[3], 3, 10);
        public static Tile Water = new Tile("Water", tilemap[4], 99, 99);
    }
}
