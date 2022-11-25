using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;

using GiraffeShooterClient.Container.Map;

namespace GiraffeShooterClient.Container.World
{

    public class WorldContext
    {
        MapContext _mapContext;
        Player _player;
        
        public WorldContext()
        {   

            _player = new Player();

            // setup the additional contexts
            _mapContext = new MapContext();

            // reset the camera
            Camera.Reset();

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

            // update the player position
            Camera.FollowTarget = _player.GetPosition();

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