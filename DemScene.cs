using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class DemScene : Scene
    {

        public Camera c;
        FullRenderer f;

        Map map;

        public DemScene() : base()
        {
            //Logger.Log("Right to perform step\nEnter to SUPER SPEED!\nUp to increase death count\nDown to decrease death count\nT to increase birth count\nG to decrease birth count\nE to spawn islands (my algorithm)");

            c = new Camera();
            f = new FullRenderer();
            f.Camera = c;
            c.Scale = new Vector2(4f, 4f);
            c.UpdateMatrix();
            Renderers.Add(f);

            FillColor = Color.Black;

            Logger.Log(NameGenerator.GenerateComboName());

            map = new Map(this, 10, 10);
            map.SetAllTiles(Tile.Grass);
            map.SetTile(2, 2, Tile.Forest);
            map.SetTile(3, 2, Tile.Mountain);
            map.SetTile(4, 2, Tile.Water);
            Unit s = new Unit(NameGenerator.GenerateComboName(), Unit.tilemap[1], Weapon.IronSword, Faction.BluTeam);
            Unit bro = new Unit(NameGenerator.GenerateComboName(), Unit.tilemap[4], Weapon.WoodenBow, Faction.RedTeam);
            map.PlaceUnit(0, 0, s);
            map.PlaceUnit(0, 1, bro);
        }

        public override void Update()
        {
            base.Update();

      
        }








    }
}
