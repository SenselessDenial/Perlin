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

        NoisyBoy nb;

        PerlinBoy pb;
        CellAutoBoy cb;

        Foogoo fab;

        Cube cube;

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
            //nb = new NoisyBoy(this);
            //pb = new PerlinBoy(this);
            //cb = new CellAutoBoy(this, 200);
            //cb.IslandGen(5);

            //fab = new Foogoo(10, 10, this);
            //fab.Set(0, 0, 5, 5, 0, 2);

            //cube = new Cube(this);

            map = new Map(this, 10, 10);
            map.SetAllTiles(Tile.Grass);
            map.SetTile(2, 2, Tile.Forest);
            Unit s = new Unit("Dude", Unit.tilemap[1], Weapon.Sword);
            map.PlaceUnit(0, 0, s);

        }

        public override void Update()
        {
            base.Update();

      
        }








    }
}
