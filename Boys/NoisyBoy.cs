using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GangplankEngine;

namespace Perlin
{
    class NoisyBoy : Entity
    {

        private int[,] colors;
        int dimensions = 256;



        public NoisyBoy(Scene scene) : base(scene)
        {
            colors = new int[dimensions, dimensions];
            Randomize();
        }

        public void Randomize()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    colors[i, j] = Calc.Next(0, 256);
                }
            }
        }

        public void Smooth()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    float a = colors[i, j] / 255f;

                    colors[i, j] = (int)Ease.Calculate(a, 1f, 0, 255, Ease.Fade);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.Pressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                Smooth();
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
                    //if (c >= 250)
                    //    b = Color.White;
                    //else if (c >= 240)
                    //    b = Color.Gray;
                    //else if (c >= 180)
                    //    b = Color.Green;
                    //else if (c >= 140)
                    //    b = Color.Tan;
                    //else
                    //    b = Color.Blue;



                    Drawing.DrawPoint(new Vector2(i, j), b);
                }
            }

        }





    }
}
