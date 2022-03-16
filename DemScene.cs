using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

            map = new Map(this, 10, 10);
            map.SetAllTiles(Tile.Grass);
            map.SetTile(2, 2, Tile.Forest);
            map.SetTile(3, 2, Tile.Mountain);
            map.SetTile(4, 2, Tile.Water);
            Unit s = new Unit(NameGenerator.GenerateComboName(), Unit.tilemap[1, 0], UnitClass.Swordsman, Weapon.IronSword, Faction.OrangeDoves);
            Unit bro = new Unit(NameGenerator.GenerateComboName(), Unit.tilemap[2, 3], UnitClass.Archer, Weapon.WoodenBow, Faction.GreenWolves);
            Unit karl = new Unit("Karl", Unit.tilemap[1, 2], UnitClass.Axeman, Weapon.IronAxe, Faction.OrangeDoves);
            Unit tharzin = new Unit("Tharzin", Unit.tilemap[3, 1], UnitClass.Pikeman, Weapon.IronSpear, Faction.PurpleDragons);
            Unit henneson = new Unit("Henneson", new GTexture("cavalry.png"), UnitClass.Cavalry, Weapon.IronSpear, Faction.BlueFrogs);
            map.PlaceUnit(0, 0, s);
            map.PlaceUnit(0, 1, bro);
            map.PlaceUnit(5, 6, karl);
            map.PlaceUnit(2, 3, tharzin);
            map.PlaceUnit(4, 7, henneson);

            map.Start();
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(Keys.R))
                map.EndTurn();

      
        }








    }
}
