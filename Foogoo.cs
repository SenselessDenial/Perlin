using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class Foogoo : Entity
    {

        int[,,] map;
        int depth = 2;
        int width;
        int height;
        Tilemap tilemap;


        public Foogoo(int width, int height, Scene scene) : base(scene)
        {
            map = new int[width, height, depth];
            this.width = width;
            this.height = height;
            tilemap = new Tilemap(new GTexture("islandtilemap.png"), 16, 16);
        }

        public void Set(int x, int y, int z, int num)
        {
            map[x, y, z] = num;
        }

        public void Set(int x, int y, int width, int height, int z, int num)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Set(x+i, y+j, z, num);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Rectangle rect = new Rectangle(i * 16, j * 16, width, height);
                    if (rect.Contains(Input.MousePos))
                    {
                        if (Input.LeftMouseDown())
                        {
                            map[i, j, 0] = 1;
                        }





                        return;
                    }
                }
            }
        }


        public override void Render()
        {
            base.Render();

            for (int k = 0; k < depth; k++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        int num = map[i, j, k];
                        if (num == 0)
                            break;

                        tilemap[num].Draw(new Vector2(16 * i, 16 * j));
                    }
                }
            }


        }





    }
}
