using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container.World
{

    class WorldContext
    {
        Collection _collection;
        Player _player;

        public WorldContext()
        {
            // setup the entity collection
            _collection = new Collection();

            // register entities
            _collection.AddEntity(new MasterMap());
            _collection.AddEntity(_player = new Player());

            // add 10 giraffes at random positions
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                _collection.AddEntity(new Giraffe(new Vector3(random.Next(-10, 10), random.Next(-10, 10), 0), new Vector3(0, 0, 0)));
            }

            // reset the camera
            Camera.Reset();
            Camera.CurrentState = Camera.State.Follow;

        }

        public void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            // use keyboard input to move the player
            if (InputManager.IsKeyDown(Keys.Up))
            {
                _player.Move(Control.Direction.up);
            }
            if (InputManager.IsKeyDown(Keys.Down))
            {
                _player.Move(Control.Direction.down);
            }
            if (InputManager.IsKeyDown(Keys.Left))
            {
                _player.Move(Control.Direction.left);
            }
            if (InputManager.IsKeyDown(Keys.Right))
            {
                _player.Move(Control.Direction.right);
            }

            // update the player position (convert from tile to pixel coordinates)
            Camera.FollowTarget = _player.GetPosition() * 1000f / 32f;

            // remove all giraffes
            if (InputManager.IsKeyDown(Keys.Space))
            {
                // for all entities
                foreach (Entity.Entity entity in _collection.GetEntities())
                {
                    // if the entity is a giraffe
                    if (entity is Giraffe)
                    {
                        entity.Delete();
                    }
                }
            }

            // update entities
            PhysicsSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            ControlSystem.Update(gameTime);
            TiledSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
            
            // update the entity collection
            _collection.Update(gameTime);

            // clean up entities
            _collection.CleanUp();

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            // draw entities
            TiledSystem.Draw(gameTime, spriteBatch);
            SpriteSystem.Draw(gameTime, spriteBatch);

        }

    }

}