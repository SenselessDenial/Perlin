﻿using System;
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
        public Point Position { get; set; }

        public void LandOnTile()
        {
            if (Map != null && Map.Contains(Position))
            {
                CurrentTile?.SteppedOff(this);
                CurrentTile = Map.GetTile(Position);
                CurrentTile?.SteppedOn(this);
            }
        }

        public Tile CurrentTile { get; private set; }
        public string Name { get; private set; }
        public GTexture Texture { get; private set; }

        public UnitClass Class;
        private Statsheet Stats;
        public Weapon Weapon;
        public int Movement => Class.Movement;

        public Faction Faction;
        public bool IsFatigued;
        public bool IsActive;

        public ModiferList ModiferList { get; private set; }

        public bool IsDead => HP <= 0;
       
        public Unit(string name, GTexture texture, UnitClass unitClass, Weapon weapon, Faction faction)
        {
            Name = name;
            Texture = texture;
            ModiferList = new ModiferList();
            Equip(unitClass);
            Equip(weapon);
            Position = new Point(-1, -1);
            Stats = Statsheet.Demo;
            Faction = faction;
            IsFatigued = false;
            IsActive = false;

            HP = MaxHP;
        }

        public int HP
        {
            get => hp;
            set
            {
                if (value <= 0)
                    hp = 0;
                else if (value >= MaxHP)
                    hp = MaxHP;
                else
                    hp = value;
            }
        }
        private int hp;
        public int MaxHP => Stats.MaxHP + ModiferList[ModiferStats.MaxHP];
        public int Strength => Stats.Strength + ModiferList[ModiferStats.Strength];
        public int Magic => Stats.Magic + ModiferList[ModiferStats.Magic];
        public int Defense => Stats.Defense + ModiferList[ModiferStats.Defense];
        public int Resilience => Stats.Resilience + ModiferList[ModiferStats.Resilience];
        public int Speed => Stats.Speed + ModiferList[ModiferStats.Speed];
        public int Dexterity => Stats.Dexterity + ModiferList[ModiferStats.Dexterity];
        public int Luck => Stats.Luck + ModiferList[ModiferStats.Luck];
        public int Dodge => Speed + ModiferList[ModiferStats.DodgeBonus];


        public void Heal(int amount)
        {
            HP += amount;
        }

        public void Heal(float percent)
        {
            HP += (int)Math.Ceiling(percent * MaxHP);
        }




        public void Equip(Weapon weapon)
        {
            Weapon?.OnDequipped(this);
            Weapon = weapon;
            Weapon?.OnEquipped(this);
        }

        public void Equip(UnitClass unitClass)
        {
            Class?.OnDequipped(this);
            Class = unitClass;
            Class?.OnEquipped(this);
        }

        public int DistanceFromUnit(Unit unit)
        {
            Point diff = Position - unit.Position;
            return Math.Abs(diff.X) + Math.Abs(diff.Y);
        }

        public bool InRangeOf(Unit unit)
        {
            int distance = DistanceFromUnit(unit);
            return distance >= Weapon.MinRange && distance <= Weapon.MaxRange;
        }

        public bool CanCounter(Unit attacker, Weapon attackWeapon)
        {
            return InRangeOf(attacker);
        }

        public List<Point> FindMovementTiles()
        {
            if (Map == null || Position == new Point(-1, -1))
            {
                Logger.Log("no");
                return null;
            }

            List<Point> points = new List<Point>();
            points.Add(Position);
            List<Point> checkedPoints = new List<Point>();

            Queue<MovePoint> queue = new Queue<MovePoint>();
            queue.Enqueue(new MovePoint(Position, Movement));

            while (queue.Count > 0)
            {
                MovePoint p = queue.Peek();
                Point point = p.Point;

                if (Map.units[point.X, point.Y] == null && !points.Contains(point))
                    points.Add(point);

                checkedPoints.Add(point);

                int movement;
                Unit unit;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i*j == 0 && i != j)
                        {
                            Point current = point + new Point(i, j);

                            if (Map.Contains(current))
                            {
                                unit = Map.units[current.X, current.Y];
                                movement = p.Movement - Map.tiles[current.X, current.Y].MovementCost(this);
                                if (!checkedPoints.Contains(current) && movement >= 0 && (unit == null || unit.Faction == Faction))
                                    queue.Enqueue(new MovePoint(current, movement));
                            }

                        }
                    }
                }

                queue.Dequeue();
            }

            return points;
        }
        public List<Point> FindAttackTiles(Point position)
        {
            if (Map == null || position == new Point(-1, -1))
            {
                Logger.Log("no");
                return null;
            }

            List<Point> points = new List<Point>();
            List<Point> checkedPoints = new List<Point>();

            Queue<MovePoint> queue = new Queue<MovePoint>();
            int range = Weapon.MaxRange;
            queue.Enqueue(new MovePoint(position, range));

            while (queue.Count > 0)
            {
                MovePoint p = queue.Peek();
                Point point = p.Point;

                Unit unit = Map.GetUnit(point);

                if (!points.Contains(point) && p.Movement <= range - Weapon.MinRange && (unit == null || Weapon.IsValidTarget(this, unit)))
                    points.Add(point);
                checkedPoints.Add(point);

                int movement;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i * j == 0 && i != j)
                        {
                            Point current = point + new Point(i, j);

                            if (Map.Contains(current))
                            {
                                movement = p.Movement - 1;
                                if (!checkedPoints.Contains(current) && movement >= 0)
                                    queue.Enqueue(new MovePoint(current, movement));
                            }

                        }
                    }
                }

                queue.Dequeue();
            }

            return points;
        }

        public void FindMoveAndAttackTiles(out List<Point> movements, out List<Point> attacks)
        {
            movements = new List<Point>();
            attacks = new List<Point>();
        }


        public void Draw(Vector2 pos, Color color, DrawAlignment alignment)
        {
            if (IsFatigued)
            {
                Texture.Draw(pos, new Color(180, 180, 180), alignment);
            }
            else
            {
                Texture.Draw(pos, color, alignment);
            }
        }

        public void Draw(Vector2 pos, DrawAlignment alignment)
        {
            Draw(pos, Color.White, alignment);
        }

        public void Draw(Vector2 pos, Color color)
        {
            Draw(pos, color, DrawAlignment.TopLeft);
        }

        public void Draw(Vector2 pos)
        {
            Draw(pos, Color.White, DrawAlignment.TopLeft);
        }

        public void DrawCard(Vector2 pos)
        {
            Drawing.Font.Draw(Name, pos, Color.White);
            //Drawing.Font.Draw(Class.Name, pos + new Vector2(40, 0));
            Weapon.Texture.Draw(pos + new Vector2(0, 10));
            Drawing.Font.Draw(Weapon.Name, pos + new Vector2(20, 14));
            Drawing.Font.Draw("HP: " + HP + "/" + MaxHP, pos + new Vector2(0, 30));
            Drawing.Font.Draw("STR " + Strength, pos + new Vector2(0, 40));
            Drawing.Font.Draw("MAG " + Magic, pos + new Vector2(0, 50));
            Drawing.Font.Draw("DEF " + Defense, pos + new Vector2(0, 60));
            Drawing.Font.Draw("RES " + Resilience, pos + new Vector2(0, 70));
            Drawing.Font.Draw("SPD " + Speed, pos + new Vector2(40, 40));
            Drawing.Font.Draw("DEX " + Dexterity, pos + new Vector2(40, 50));
            Drawing.Font.Draw("LCK " + Luck, pos + new Vector2(40, 60));
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
