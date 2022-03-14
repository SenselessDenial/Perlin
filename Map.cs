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
        private int tileWidth = 16;

        public Unit[,] units;
        public Tile[,] tiles;

        public Point Cursor;
        private GTexture cursorTexture;

        private Unit selectedUnit;


        private List<Point> highlightedTiles;



        public Map(Scene scene, int width, int height) : base(scene)
        {
            this.Width = width;
            this.Height = height;

            units = new Unit[width, height];
            tiles = new Tile[width, height];
            Cursor = Point.Zero;

            cursorTexture = new GTexture("cursor.png");
            highlightedTiles = null;
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
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
                tiles[x, y] = tile;
        }

        public void PlaceUnit(int x, int y, Unit unit)
        {
            if (units[x, y] != null)
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
                
        }

        public void PlaceUnit(Point p, Unit unit)
        {
            PlaceUnit(p.X, p.Y, unit);
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
            if (Input.Pressed(Keys.Left))
            {
                if (Cursor.X - 1 < 0)
                {

                }
                else
                {
                    Cursor.X -= 1;
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
                }
            }
        }

        public override void Update()
        {
            base.Update();

            UpdateCursor();

            if (Input.Pressed(Keys.C))
            {
                if (selectedUnit == null)
                {
                    selectedUnit = units[Cursor.X, Cursor.Y];
                    highlightedTiles = selectedUnit?.FindTiles();
                }
                    
                else
                {
                    if (highlightedTiles.Contains(Cursor))
                    {
                        PlaceUnit(Cursor, selectedUnit);
                        selectedUnit = null;
                        highlightedTiles = null;
                    }
                    
                }
                   
            }

        }

        public override void Render()
        {
            base.Render();

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    tiles[i, j]?.Texture.Draw(new Vector2(i*tileWidth, j*tileWidth));
                }
            }

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    units[i, j]?.Texture.Draw(new Vector2(i * tileWidth, j * tileWidth));
                }
            }

            if (highlightedTiles != null)
            {
                foreach (var item in highlightedTiles)
                {
                    Drawing.DrawBox(new Rectangle(item.X * tileWidth, item.Y * tileWidth, tileWidth, tileWidth), new Color(0, 0, 255, 125));
                }
            }

            


            cursorTexture.Draw(Cursor.ToVector2() * 16);


        }










    }
}
