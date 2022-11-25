using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Input;

using GiraffeShooterClient.Container.Map;
using GiraffeShooterClient.Container.Camera;

namespace GiraffeShooterClient.Container.World
{

    public class WorldContext
    {
        MapContext _mapContext;
        Player _player;
        Giraffe _giraffe;
        
        public WorldContext()
        {   

            _player = new Player();
            
            // _giraffe = new Giraffe();

            // setup the additional contexts
            _mapContext = new MapContext();
            
        }

        public void HandleEvents(List<Event> events)
        {
            
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            // use keyboard input to move the player
            if (InputManager.IsKeyDown(Keys.Up))
            {
                _player.Move(Player.direction.up);
            }
            if (InputManager.IsKeyDown(Keys.Down))
            {
                _player.Move(Player.direction.down);
            }
            if (InputManager.IsKeyDown(Keys.Left))
            {
                _player.Move(Player.direction.left);
            }
            if (InputManager.IsKeyDown(Keys.Right))
            {
                _player.Move(Player.direction.right);
            }

            // update the additional context
            _mapContext.Update(gameTime);

            // update entities
            PhysicsSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);

            // external contexts
            CameraContext.FollowTarget = _player.GetPosition();

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