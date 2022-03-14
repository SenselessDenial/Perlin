using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Unit
    {
        public static Tilemap tilemap = new Tilemap(new GTexture("units.png"), 16, 16);

        public Map Map;
        public Point Position;
        public string Name { get; private set; }
        public GTexture Texture { get; private set; }
        public WeaponStruct Weapon;
        public int Movement;


        

        public Unit(string name, GTexture texture, Weapon weapon)
        {
            Name = name;
            Texture = texture;
            Weapon = new WeaponStruct(weapon);
            Position = new Point(-1, -1);
            Movement = 3;
        }

        public List<Point> FindTiles()
        {



            if (Map == null || Position == new Point(-1, -1))
            {
                Logger.Log("no");
                return null;
            }

            List<Point> points = new List<Point>();
            points.Add(Position);


            Queue<MovePoint> queue = new Queue<MovePoint>();
            queue.Enqueue(new MovePoint(Position, Movement));

            while (queue.Count > 0)
            {
                MovePoint p = queue.Peek();
                Point point = p.Point;

                if (Map.units[point.X, point.Y] == null && !points.Contains(point))
                {
                    points.Add(point);
                }

                Point current = point + new Point(1, 0);
                int movement;

                if (current.X >= 0 && current.Y >= 0 && current.X < Map.Width && current.Y < Map.Height)
                {
                    movement = p.Movement - Map.tiles[current.X, current.Y].MovementCost;
                    if (movement >= 0)
                        queue.Enqueue(new MovePoint(current, movement));
                }

                current = point + new Point(-1, 0);
                if (current.X >= 0 && current.Y >= 0 && current.X < Map.Width && current.Y < Map.Height)
                {
                    movement = p.Movement - Map.tiles[current.X, current.Y].MovementCost;
                    if (movement >= 0)
                        queue.Enqueue(new MovePoint(current, movement));
                }

                current = point + new Point(0, 1);
                if (current.X >= 0 && current.Y >= 0 && current.X < Map.Width && current.Y < Map.Height)
                {
                    movement = p.Movement - Map.tiles[current.X, current.Y].MovementCost;
                    if (movement >= 0)
                        queue.Enqueue(new MovePoint(current, movement));
                }

                current = point + new Point(0, -1);
                if (current.X >= 0 && current.Y >= 0 && current.X < Map.Width && current.Y < Map.Height)
                {
                    movement = p.Movement - Map.tiles[current.X, current.Y].MovementCost;
                    if (movement >= 0)
                        queue.Enqueue(new MovePoint(current, movement));
                }

                queue.Dequeue();
            }

            return points;
        }

        private struct MovePoint
        {
            public Point Point;
            public int Movement;

            public MovePoint(Point point, int movement)
            {
                Point = point;
                Movement = movement;
            }
        }


    }
}
