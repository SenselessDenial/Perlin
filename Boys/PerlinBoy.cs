using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class PerlinBoy : Entity
    {

        private int[,] colors;
        private Vector2[,] vectors;

        int dimensions = 1024;
        int cellSize = 128;

        public PerlinBoy(Scene scene) : base(scene)
        {
            colors = new int[dimensions, dimensions];
            vectors = new Vector2[(dimensions / cellSize) + 1, (dimensions / cellSize) + 1];
            Randomize();
        }

        public void Randomize()
        {
            for (int i = 0; i < (dimensions / cellSize) + 1; i++)
            {
                for (int j = 0; j < (dimensions / cellSize) + 1; j++)
                {
                    vectors[i, j] = new Vector2(Calc.NextFloat(-1, 1), Calc.NextFloat(-1, 1));
                    vectors[i, j].Normalize();
                }
            }

            int min = 10000;
            int max = 0;

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    int x = i / cellSize;
                    int y = j / cellSize;

                    Vector2 tl = vectors[x, y];
                    Vector2 tr = vectors[x + 1, y];
                    Vector2 bl = vectors[x, y + 1];
                    Vector2 br = vectors[x + 1, y + 1];

                    float i2 = i;
                    float j2 = j;

                    Vector2 tlO = new Vector2(i2 - x * cellSize, j2 - y * cellSize);
                    Vector2 trO = new Vector2(i2 - (x + 1) * cellSize, j2 - y * cellSize);
                    Vector2 blO = new Vector2(i2 - x * cellSize, j2 - (y + 1) * cellSize);
                    Vector2 brO = new Vector2(i2 - (x + 1) * cellSize, j2 - (y + 1) * cellSize);

                    float tlD = Vector2.Dot(tl, tlO);
                    float trD = Vector2.Dot(tr, trO);
                    float blD = Vector2.Dot(bl, blO);
                    float brD = Vector2.Dot(br, brO);

                    float a = ((float)i2 - (x * cellSize)) / (((x + 1) * cellSize) - (x * cellSize));
                    a = Ease.Calculate(a, 1f, 0f, 1f, Ease.Fade);
                    float b = ((float)j2 - (y * cellSize)) / (((y + 1) * cellSize) - (y * cellSize));
                    b = Ease.Calculate(b, 1f, 0f, 1f, Ease.Fade);

                    Ease.Easing f = Ease.Linear;

                    float t1 = Ease.Calculate(a, 1f, tlD, trD, f);
                    float b1 = Ease.Calculate(a, 1f, blD, brD, f);
                    float c1 = Ease.Calculate(b, 1f, t1, b1, f);

                    //Logger.Log(c1);
                    int argh = (int)(c1 * 255);
                    colors[i, j] = argh;

                    if (argh > max)
                        max = argh;
                    if (argh < min)
                        min = argh;
                }
            }

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    float scale = (float)(colors[i,j] - min) / (max - min);
                    colors[i,j] = (int)(scale * 255);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Logger.Log("Randomizing...");
                Randomize();
            }
                
        }

        public override void Render()
        {
            base.Render();

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    int c = colors[i, j];
                    Color b = new Color(c, c, c);
                    //if (c >= 245)
                    //    b = Color.White;
                    //else if (c >= 230)
                    //    b = Color.Gray;
                    //else if (c >= 165)
                    //    b = Color.Green;
                    //else if (c >= 155)
                    //    b = Color.Tan;
                    //else
                    //    b = Color.Blue;



                    Drawing.DrawPoint(new Vector2(i, j), b);
                }
            }

        }



    }
}
