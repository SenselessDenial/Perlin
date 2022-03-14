using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GangplankEngine;

namespace Perlin
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Engine
    {

        public Game1() : base(1024, 1024)
        {

        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene = new DemScene();

        }

        protected override void LoadContent()
        {
            
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);


        }
    }
}
