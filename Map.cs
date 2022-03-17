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
    class Map : Entity
    {
        public int Width;
        public int Height;
        private int TileWidth = 16;

        public Unit[,] units;
        public Tile[,] tiles;

        public Point Cursor;
        private GTexture cursorTexture;

        private enum CursorStates
        {
            Unselected,
            Selected,
            AfterMove
        }

        private CursorStates cursorState = CursorStates.Unselected;


        private Unit SelectedUnit
        {
            get => selectedUnit;
            set
            {
                if (value == null)
                    Logger.Log("Unselecting unit");
                else
                    Logger.Log("Selecting unit: " + value.Name);
                selectedUnit = value;
            }
        }
        private Unit selectedUnit;

        private Unit HoverUnit;
        private Tile HoverTile;

        private List<Point> highlightedTiles;
        private List<Point> attackTiles;
        private Point storedPosition = new Point(-1, -1);

        private List<Faction> Factions;
        private List<Unit> ListOfUnits;

        private Faction currentFaction => Factions[currentFactionNum];
        private int currentFactionNum = 0;

        private bool DisplayAttacking = false;
        private int A2dDamage;
        private int A2dAccuracy;
        private int D2aDamage;
        private int D2aAccuracy;

        public Map(Scene scene, int width, int height) : base(scene)
        {
            this.Width = width;
            this.Height = height;

            units = new Unit[width, height];
            tiles = new Tile[width, height];
            Cursor = Point.Zero;

            cursorTexture = new GTexture("cursor.png");
            highlightedTiles = null;
            attackTiles = null;
            Factions = new List<Faction>();
            ListOfUnits = new List<Unit>();
        }

        public void Start()
        {
            StartOfTurn();
        }

        private void StartOfTurn()
        {
            Logger.Log("Current faction: " + currentFaction.Name);
        }

        private void EndofTurn()
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (units[i,j] != null && units[i, j].Faction == currentFaction)
                        units[i, j].IsFatigued = false;
                }
            }

            if (currentFactionNum == Factions.Count - 1)
                currentFactionNum = 0;
            else
                currentFactionNum++;

            StartOfTurn();
        }

        public void EndTurn()
        {
            EndofTurn();
        }

        private bool AllUnitsFatigued(Faction faction)
        {
            foreach (var item in ListOfUnits)
            {
                if (item.Faction == faction && !item.IsFatigued)
                    return false;
            }
            return true;
        }


        public void UpdateCursorState(bool aButtonPress, bool bButtonPress)
        {
            Unit unit = units[Cursor.X, Cursor.Y];
            DisplayAttacking = false;

            switch (cursorState)
            {
                case CursorStates.Unselected:
                    if (aButtonPress && unit != null && !unit.IsFatigued)
                    {
                        selectedUnit = units[Cursor.X, Cursor.Y];
                        storedPosition = selectedUnit.Position;
                        highlightedTiles = SelectedUnit?.FindMovementTiles();
                        cursorState = CursorStates.Selected;
                    }
                    break;
                case CursorStates.Selected:
                    if (bButtonPress)
                    {
                        selectedUnit = null;
                        highlightedTiles = null;
                        attackTiles = null;
                        storedPosition = new Point(-1, -1);
                        cursorState = CursorStates.Unselected;
                    }
                    else if (aButtonPress && highlightedTiles != null && highlightedTiles.Contains(Cursor) && selectedUnit.Faction == currentFaction)
                    {
                        PlaceUnit(Cursor, selectedUnit);
                        highlightedTiles = null;
                        attackTiles = selectedUnit?.FindAttackTiles();
                        cursorState = CursorStates.AfterMove;
                    }
                    break;
                case CursorStates.AfterMove:
                    if (bButtonPress)
                    {
                        PlaceUnit(storedPosition, selectedUnit);
                        attackTiles = null;
                        highlightedTiles = SelectedUnit?.FindMovementTiles();
                        cursorState = CursorStates.Selected;

                    }
                    if (aButtonPress && attackTiles.Contains(Cursor) && unit != null && unit.Faction != selectedUnit.Faction)
                    {
                        CombatHandler.AttackPlus(selectedUnit, selectedUnit.Weapon, units[Cursor.X, Cursor.Y], units[Cursor.X, Cursor.Y].Weapon);
                        selectedUnit.IsFatigued = true;
                        selectedUnit = null;
                        highlightedTiles = null;
                        attackTiles = null;
                        storedPosition = new Point(-1, -1);
                        cursorState = CursorStates.Unselected;
                        if (AllUnitsFatigued(currentFaction))
                            EndofTurn();
                    }
                    else if (aButtonPress && Cursor == selectedUnit.Position)
                    {
                        selectedUnit.IsFatigued = true;
                        selectedUnit = null;
                        highlightedTiles = null;
                        attackTiles = null;
                        storedPosition = new Point(-1, -1);
                        cursorState = CursorStates.Unselected;
                        if (AllUnitsFatigued(currentFaction))
                            EndofTurn();
                    }
                    if (attackTiles != null && attackTiles.Contains(Cursor) && unit != null && unit.Faction != selectedUnit.Faction)
                    {
                        DisplayAttacking = true;
                        CombatHandler.FindNumbers(selectedUnit, unit, out A2dDamage, out A2dAccuracy, out D2aDamage, out D2aAccuracy);
                        cursorState = CursorStates.AfterMove;
                    }
                    break;
                default:
                    break;
            }
        }


        public void SetAllTiles(Tile tile)
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    tiles[i, j] = tile;
                }
            }

            HoverTile = tiles[Cursor.X, Cursor.Y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
                tiles[x, y] = tile;

            HoverTile = tiles[Cursor.X, Cursor.Y];
        }

        public void PlaceUnit(int x, int y, Unit unit)
        {
            if (units[x, y] != null && units[x, y] != unit)
            {
                Logger.Log("A unit already occupies this space.");
                return;
            }

            if (ContainsUnit(unit))
            {
                Point p = unit.Position;
                units[p.X, p.Y] = null;
            }

            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                units[x, y] = unit;
                unit.Position = new Point(x, y);
                unit.Map = this;
            }

            if (!ListOfUnits.Contains(unit))
            {
                ListOfUnits.Add(unit);
            }

            if (!Factions.Contains(unit.Faction))
            {
                Factions.Add(unit.Faction);
            }

            HoverUnit = units[Cursor.X, Cursor.Y];
        }

        public void PlaceUnit(Point p, Unit unit)
        {
            PlaceUnit(p.X, p.Y, unit);
        }

        public bool Contains(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height;
        }

        private bool ContainsUnit(Unit unit)
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (units[i, j] == unit)
                        return true;
                }
            }
            return false;
        }

        private void UpdateCursor()
        {
            bool changed = false;
            if (Input.Pressed(Keys.Left))
            {
                if (Cursor.X - 1 < 0)
                {

                }
                else
                {
                    Cursor.X -= 1;
                    changed = true;
                }
            }
            else if (Input.Pressed(Keys.Right))
            {
                if (Cursor.X + 1 >= Width)
                {

                }
                else
                {
                    Cursor.X += 1;
                    changed = true;
                }
            }

            if (Input.Pressed(Keys.Up))
            {
                if (Cursor.Y - 1 < 0)
                {

                }
                else
                {
                    Cursor.Y -= 1;
                    changed = true;
                }
            }
            else if (Input.Pressed(Keys.Down))
            {
                if (Cursor.Y + 1 >= Height)
                {

                }
                else
                {
                    Cursor.Y += 1;
                    changed = true;
                }
            }

            if (changed)
            {
                HoverUnit = units[Cursor.X, Cursor.Y];
                HoverTile = tiles[Cursor.X, Cursor.Y];
            }
                
        }

        public override void Update()
        {
            base.Update();

            UpdateCursor();

            UpdateCursorState(Input.Pressed(Keys.C), Input.Pressed(Keys.X));
        }

        public override void Render()
        {
            base.Render();

            int tileWidth = TileWidth;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    tiles[i, j]?.Draw(new Vector2(i * tileWidth, j * tileWidth));
                }
            }

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    units[i, j]?.Draw(new Vector2(i * tileWidth, (j+1) * tileWidth), DrawAlignment.BottomLeft);
                }
            }

            if (highlightedTiles != null)
            {
                foreach (var item in highlightedTiles)
                {
                    Drawing.DrawBox(new Rectangle(item.X * tileWidth, item.Y * tileWidth, tileWidth-1, tileWidth-1), new Color(0, 0, 255, 125));
                }
            }

            if (attackTiles != null)
            {
                foreach (var item in attackTiles)
                {
                    Drawing.DrawBox(new Rectangle(item.X * tileWidth, item.Y * tileWidth, tileWidth-1, tileWidth-1), new Color(255, 0, 0, 125));
                }
            }



            cursorTexture.Draw(Cursor.ToVector2() * tileWidth, currentFaction.Color);

            if (HoverUnit != null)
            {
                HoverUnit.DrawCard(new Vector2(10, 170));
            }

            if (HoverTile != null)
            {
                HoverTile.DrawCard(new Vector2(120, 170));
            }

            if (currentFaction != null)
                Drawing.Font.Draw(currentFaction.Name, new Vector2(165, 10), currentFaction.Color);

            if (DisplayAttacking)
            {
                Drawing.Font.Draw(A2dDamage.ToString(), new Vector2(165, 20));
                Drawing.Font.Draw(A2dAccuracy.ToString(), new Vector2(165, 30));
                Drawing.Font.Draw(D2aDamage.ToString(), new Vector2(200, 20));
                Drawing.Font.Draw(D2aAccuracy.ToString(), new Vector2(200, 30));
            }



        }










    }
}
