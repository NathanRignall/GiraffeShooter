using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container.World
{

    public class WorldContext : Context
    {
        Collection _collection;
        Player _player;

        public WorldContext()
        {
            // setup the entity collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();

            // register entities
            _collection.AddEntity(new MasterMap());
            _collection.AddEntity(_player = new Player());
            //_collection.AddEntity(new Button(new Vector3(0, 5, 0), "Exit", new Action(() => { Base.SetContext(new MenuContext()); })));

            if (SupabaseManager.Client.Auth.CurrentUser != null)
                _collection.AddEntity(new TextDisplay(new Vector3(0, 5, 0), SupabaseManager.Client.Auth.CurrentUser.Id));

            // add 10 giraffes at random positions
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                _collection.AddEntity(new Giraffe(new Vector3(random.Next(-10, 10), random.Next(-5, 5), 0), new Vector3(random.Next(-3, 3), random.Next(-2, 2), 0)));
            }
            
            // add 10 ammunition at random positions
            for (int i = 0; i < 10; i++)
            {
                _collection.AddEntity(new Ammunition(new Vector3(random.Next(-10, 10), random.Next(-5, 5), 0)));
            }

            // reset the camera
            Camera.Reset();
            Camera.CurrentState = Camera.State.Follow;
            Camera.FollowTarget = _player.GetPosition() * 1000f / 32f;
            Camera.Snap();

        }

        public override void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);


            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyHold:

                        switch (e.Key)
                        {
                            case Keys.Up:
                                _player.Move(Control.Direction.up);
                                break;
                            case Keys.Down:
                                _player.Move(Control.Direction.down);
                                break;
                            case Keys.Left:
                                _player.Move(Control.Direction.left);
                                break;
                            case Keys.Right:
                                _player.Move(Control.Direction.right);
                                break;
                                
                        }
                        
                        break;
                    
                    case EventType.StickLeftMove:

                        // use delta to calculate rotation
                        Vector2 delta = e.Delta;
                        
                        // set rotation
                        var direction = (float)Math.Atan2(delta.Y, delta.X);
                        
                        _player.Move(direction, delta.Length());

                        break;
                }

            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

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
            AnimationSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
            TextSystem.Update(gameTime);
            
            // update the entity collection
            _collection.Update(gameTime);

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            // draw entities
            TiledSystem.Draw(gameTime, spriteBatch);
            SpriteSystem.Draw(gameTime, spriteBatch);
            TextSystem.Draw(gameTime, spriteBatch);

        }

    }

}