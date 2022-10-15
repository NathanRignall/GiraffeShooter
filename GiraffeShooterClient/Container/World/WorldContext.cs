using GiraffeShooterClient.Entity.System;

namespace GiraffeShooterClient.Container.World
{

    public class WorldContext
    {
        public readonly WorldRender WorldRender;
        Giraffe _giraffe;

        public WorldContext()
        {
            WorldRender = new WorldRender();

            _giraffe = new Giraffe();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            WorldRender.Update(gameTime);
        }
    }

}