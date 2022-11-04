using System.Collections.Generic;

using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Input;

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

        public void HandleEvents(List<Event> events)
        {
            // Print all events to console
            foreach (Event e in events)
            {
                System.Console.WriteLine(e.ToString());
            }

        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            WorldRender.Update(gameTime);
        }
    }

}