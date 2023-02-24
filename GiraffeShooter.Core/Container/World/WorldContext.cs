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
        public Collection EntityCollection { get; private set; }

        InventoryBar _inventoryBar;
        Player _player;
        
        List<Entity.Entity> _pauseEntities = new List<Entity.Entity>();

        private const float _delay = 0.1f;
        private float _remainingDelay = _delay;

        public WorldContext()
        {
            // setup the entity collection
            EntityCollection = new Collection();
            
            // clear the base
            Base.Clear();

            // register entities
            EntityCollection.AddEntity(new MasterMap());
            EntityCollection.AddEntity(_player = new Player());

            if (SupabaseManager.Client.Auth.CurrentUser != null)
                EntityCollection.AddEntity(new TextDisplay(new Vector2(0, 0), SupabaseManager.Client.Auth.CurrentUser.Id));

            // add 10 giraffes at random positions
            Random random = new Random();
            for (int i = 0; i < 1; i++)
            {
                EntityCollection.AddEntity(new Giraffe(new Vector3(random.Next(-5, 5), random.Next(-5, 5), 0), new Vector3(random.Next(-5, 5), random.Next(-5, 5), 0)));
            }
            
            // add 50 ammunition at random positions
            for (int i = 0; i < 400; i++)
            {
                EntityCollection.AddEntity(new Ammunition(new Vector3(random.Next(-45, -20), random.Next(-45, -20),0), Vector3.Zero));
                EntityCollection.AddEntity(new Ammunition(new Vector3(random.Next(20, 45), random.Next(20, 45),0), Vector3.Zero));
            }
            
            // add 30 pistol at random positions
            for (int i = 0; i < 30; i++)
            {
                EntityCollection.AddEntity(new Gun(new Vector3(random.Next(-45, -30), random.Next(-45, -30), 0), Vector3.Zero));
            }
            
            // // add 30 machine gun at random positions
            // for (int i = 0; i < 30; i++)
            // {
            //     EntityCollection.AddEntity(new MachineGun(new Vector3(random.Next(-45, 45), random.Next(-45, 45), 0), Vector3.Zero));
            // }

            // add exit button to pause entities and collection then hide it
            var exitButton = new Button(new Vector2(0f, -1.25f),  AssetManager.ExitButtonTexture, () => { ContextManager.SetState(ContextManager.State.Menu); });
            _pauseEntities.Add(exitButton);
            EntityCollection.AddEntity(exitButton);
            exitButton.GetComponent<Sprite>().Visible = false;
            
            // add resume button to pause entities and collection then hide it
            var resumeButton = new Button(new Vector2(0f, 1.25f),  AssetManager.ResumeButtonTexture, ContextManager.TogglePause);
            _pauseEntities.Add(resumeButton);
            EntityCollection.AddEntity(resumeButton);
            resumeButton.GetComponent<Sprite>().Visible = false;
            
            // add paused cover to pause entities and collection then hide it
            var pausedCover = new PausedCover(Vector2.Zero);
            _pauseEntities.Add(pausedCover);
            EntityCollection.AddEntity(pausedCover);
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
        //                 Giraffe giraffe = EntityCollection.GetEntity(Guid) as Giraffe;
        //
        //                 // if the player does not exist
        //                 if (giraffe == null)
        //                 {
        //                     // create the player
        //                     giraffe = new Giraffe(new Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z), 
        //                         new Vector3(entity.Velocity.X, entity.Velocity.Y, entity.Velocity.Z), Guid);
        //                     
        //                     EntityCollection.AddEntity(giraffe);
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
            if (Camera.CurrentState == Camera.State.Follow)
                EntityCollection.HandleEvents(events);
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        switch (e.Key)
                        {
                            case Keys.C:
                                if (Camera.CurrentState == Camera.State.Follow && !ContextManager.Paused)
                                    Camera.CurrentState = Camera.State.Free;
                                else
                                    Camera.CurrentState = Camera.State.Follow;
                                break;
                            
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
            // -- update game logic --
            
            // check if there are any giraffes left
            if (EntityCollection.GetEntities<Giraffe>().Count == 0)
            {
                // if there are no giraffes left then the player has won
                ContextManager.SetState(ContextManager.State.Win);
            }
            
            // check if player is deleted
            if (_player == null || _player.IsDeleted)
            {
                // if the player is deleted then the player has lost
                ContextManager.SetState(ContextManager.State.Lose);
            }

            // update the player position (convert from tile to pixel coordinates)
            Camera.FollowTarget = _player.GetPosition() * 1000f / 32f;
            
            
            // -- update systems --

            // update components
            ScreenSystem.Update(gameTime);
            PhysicsSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            ControlSystem.Update(gameTime);
            TiledSystem.Update(gameTime);
            AnimationSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
            TextSystem.Update(gameTime);
            InputSystem.Update(gameTime);
            InventorySystem.Update(gameTime);
            AimSystem.Update(gameTime);
            HealthSystem.Update(gameTime);
            CleanerSystem.Update(gameTime);
            BotSystem.Update(gameTime);
            
            // update the entity collection
            EntityCollection.Update(gameTime);
            
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