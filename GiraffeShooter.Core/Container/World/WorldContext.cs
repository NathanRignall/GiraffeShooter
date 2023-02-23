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
        InventoryBar _inventoryBar;
        Player _player;
        
        List<Entity.Entity> _pauseEntities = new List<Entity.Entity>();

        private const float _delay = 0.1f;
        private float _remainingDelay = _delay;

        public WorldContext()
        {
            // setup the entity collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();

            // register entities
            _collection.AddEntity(new MasterMap());
            _collection.AddEntity(_player = new Player());

            if (SupabaseManager.Client.Auth.CurrentUser != null)
                _collection.AddEntity(new TextDisplay(new Vector3(0, 5, 0), SupabaseManager.Client.Auth.CurrentUser.Id));

            // add 10 giraffes at random positions
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                _collection.AddEntity(new Giraffe(new Vector3(random.Next(-45, 45), random.Next(-45, 45), 0), new Vector3(random.Next(-5, 5), random.Next(-5, 5), 0)));
            }
            
            // add 50 ammunition at random positions
            for (int i = 0; i < 400; i++)
            {
                _collection.AddEntity(new Ammunition(new Vector3(random.Next(-45, 45), random.Next(-45, 45),0), Vector3.Zero));
            }
            
            // add 20 pistol at random positions
            for (int i = 0; i < 100; i++)
            {
                _collection.AddEntity(new Gun(new Vector3(random.Next(-45, 45), random.Next(-45, 45), 0), Vector3.Zero));
            }

            // add exit button to pause entities and collection then hide it
            var exitButton = new ScreenButton(new Vector2(0f, -1.25f),  AssetManager.ExitButtonTexture, () => { ContextManager.SetState(ContextManager.State.Menu); });
            _pauseEntities.Add(exitButton);
            _collection.AddEntity(exitButton);
            exitButton.GetComponent<Sprite>().Visible = false;
            
            // add resume button to pause entities and collection then hide it
            var resumeButton = new ScreenButton(new Vector2(0f, 1.25f),  AssetManager.ResumeButtonTexture, ContextManager.TogglePause);
            _pauseEntities.Add(resumeButton);
            _collection.AddEntity(resumeButton);
            resumeButton.GetComponent<Sprite>().Visible = false;
            
            // add paused cover to pause entities and collection then hide it
            var pausedCover = new PausedCover(Vector2.Zero);
            _pauseEntities.Add(pausedCover);
            _collection.AddEntity(pausedCover);
            pausedCover.GetComponent<Sprite>().Visible = false;

            // reset the camera
            Camera.Reset();
            Camera.CurrentState = Camera.State.Follow;
            Camera.FollowTarget = _player.GetPosition() * 1000f / 32f;
            Camera.Snap();
        }
        
        // handle broadcasted player positions
        // public void HandleBroadcastedEntity(List<DB.Entity> entities)
        // {
        //
        //     // loop through the entities
        //     foreach (DB.Entity entity in entities)
        //     {
        //         // if the entity is a player
        //         if (entity.Type == "Player")
        //         {
        //             // if the player is not the current player
        //             if (entity.Id != _player.Id.ToString())
        //             {
        //
        //                 var Guid = new Guid(entity.Id);
        //         
        //                 // get the player
        //                 Giraffe giraffe = _collection.GetEntity(Guid) as Giraffe;
        //
        //                 // if the player does not exist
        //                 if (giraffe == null)
        //                 {
        //                     // create the player
        //                     giraffe = new Giraffe(new Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z), 
        //                         new Vector3(entity.Velocity.X, entity.Velocity.Y, entity.Velocity.Z), Guid);
        //                     
        //                     _collection.AddEntity(giraffe);
        //
        //                     // cosnole log the player
        //                     Console.WriteLine("Player " + Guid + " joined the game ");
        //                 }
        //                 else
        //                 {
        //                     // console log the player
        //                     Console.WriteLine("Player " + Guid + " moved ");
        //                 }
        //
        //                 // set the player position
        //                 giraffe.SetPosition(new Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z));
        //                 giraffe.SetVelocity(new Vector3(entity.Velocity.X, entity.Velocity.Y, entity.Velocity.Z));
        //             }
        //         }
        //     }
        // }

        public override void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        switch (e.Key)
                        {
                            case Keys.Escape:
                                ContextManager.TogglePause();
                                break;
                        }
                        
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

            // update components
            ScreenSystem.Update(gameTime);
            PhysicsSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            ControlSystem.Update(gameTime);
            TiledSystem.Update(gameTime);
            AnimationSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
            TextSystem.Update(gameTime);
            TextInputSystem.Update(gameTime);
            InventorySystem.Update(gameTime);
            AimSystem.Update(gameTime);
            CleanerSystem.Update(gameTime);
            
            // update the entity collection
            _collection.Update(gameTime);
            
            // timer update
            var timer = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _remainingDelay -= timer;

            // only execute when ready
            if(_remainingDelay <= 0)
            {
                // reset the delay
                _remainingDelay = _delay;
            }
            
            // if paused then hide the pause entities
            if (ContextManager.Paused)
            {
                foreach (Entity.Entity entity in _pauseEntities)
                {
                    entity.GetComponent<Sprite>().Visible = true;
                }
            }
            else
            {
                foreach (Entity.Entity entity in _pauseEntities)
                {
                    entity.GetComponent<Sprite>().Visible = false;
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // draw entities
            TiledSystem.Draw(gameTime, spriteBatch);
            SpriteSystem.Draw(gameTime, spriteBatch);
            TextSystem.Draw(gameTime, spriteBatch);
            AimSystem.Draw(gameTime, spriteBatch);
        }
    }
}