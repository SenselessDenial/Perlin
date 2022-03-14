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
    public class Cube : Entity
    {
        private Image image;
        private float speed = 2f;
        private float gravity = 0.2f;

        private Vector2 velocity = Vector2.Zero;
        private Vector2 accel = Vector2.Zero;

        public Cube(Scene scene) : base(scene)
        {
            image = new Image(this, new GTexture("glom.png"));
            velocity.Y = -10;
        }


        public override void Update()
        {
            base.Update();

            velocity.X = Input.HorizontalAxisCheck() * speed;

            accel.Y = gravity;





            velocity += accel;
            Position += velocity;
        }



        public override void Render()
        {
            base.Render();
        }




    }
}
