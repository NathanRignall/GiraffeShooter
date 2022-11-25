using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Input;

using GiraffeShooterClient.Container.Map;

namespace GiraffeShooterClient.Container.World
{

    public class WorldContext
    {
        MapContext _mapContext;

        Giraffe _giraffe;

        public WorldContext()
        {   

            // setup test giraffe
            _giraffe = new Giraffe();

            // setup the additional contexts
            _mapContext = new MapContext();
            
        }

        public void HandleEvents(List<Event> events)
        {
            
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            // update the additional context
            _mapContext.Update(gameTime);

            // update entities
            PhysicsSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            // draw the additional contexts
            _mapContext.Draw(gameTime, spriteBatch);

            // draw entities
            SpriteSystem.Draw(gameTime, spriteBatch);

        }
        
    }

}