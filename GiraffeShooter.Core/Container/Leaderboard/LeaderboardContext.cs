using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Container.Leaderboard
{
    public class LeaderboardContext : Context
    {
        
        private enum State
        {
            Loading,
            Main,
        }
        
        private State _currentState;
        private readonly Collection _collection;
        
        private Button _loadingButton;

        public LeaderboardContext()
        {
            // set the current state to loading
            _currentState = State.Loading;
            
            // create a new collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, -7.5f, 0), AssetManager.BackButtonTexture, () => ContextManager.SetState(ContextManager.State.Menu)));
            _collection.AddEntity(_loadingButton = new Button(new Vector3(0, 0, 0), AssetManager.BackButtonTexture, () => { }));
            
            // reset the camera
            Camera.Reset(ScreenManager.GetScaleFactor());
            Camera.CurrentState = Camera.State.Frozen;
            
            // load the leaderboard
            GetLeaderboard();
        }
        
        public override void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // update entities
            PhysicsSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
            TextSystem.Update(gameTime);
            
            // update the entity collection
            _collection.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // draw entities
            SpriteSystem.Draw(gameTime, spriteBatch);
            TextSystem.Draw(gameTime, spriteBatch);
        }

        public async Task GetLeaderboard()
        {
            try
            {
                var data = await SupabaseManager.Client.Postgrest.Table<DB.Leaderboard>().Get();
                
                // log the data
                Console.WriteLine(data);
                
                // delete the loading button
                _loadingButton.Delete();
                
                // create a set of text entities for each leaderboard entry
                for (var i = 0; i < data.Models.Count; i++)
                {
                    var entry = data.Models[i];
                    _collection.AddEntity(new TextDisplay(new Vector3(0, -5.5f + (i * 1.5f), 0), $"{entry.UserId} - {entry.Wins}"));
                }
                
                // set the state to main
                _currentState = State.Main;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
