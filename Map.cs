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
        private int width;
        private int height;
        private int tileWidth = 16;

        public Unit[,] units;
        public Tile[,] tiles;

        public Point Cursor;
        private GTexture cursorTexture;


        public Map(Scene scene, int width, int height) : base(scene)
        {
            this.width = width;
            this.height = height;

            units = new Unit[width, height];
            tiles = new Tile[width, height];
            Cursor = Point.Zero;

            cursorTexture = new GTexture("cursor.png");
        }

        public void SetAllTiles(Tile tile)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    tiles[i, j] = tile;
                }
            }
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
                tiles[x, y] = tile;
        }

        public void PlaceUnit(int x, int y, Unit unit)
        {
            if (ContainsUnit(unit))
            {
                Logger.Log("Unit " + unit.Name + " is already on the map.");
                return;
            }

            if (x >= 0 && y >= 0 && x < width && y < height)
                units[x, y] = unit;
        }

        private bool ContainsUnit(Unit unit)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
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
                if (Cursor.X + 1 >= width)
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
                if (Cursor.Y + 1 >= height)
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
        }

        public override void Render()
        {
            base.Render();

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    tiles[i, j]?.Texture.Draw(new Vector2(i*tileWidth, j*tileWidth));
                }
            }

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    units[i, j]?.Texture.Draw(new Vector2(i * tileWidth, j * tileWidth));
                }
            }

            cursorTexture.Draw(Cursor.ToVector2() * 16);


        }










    }
}
