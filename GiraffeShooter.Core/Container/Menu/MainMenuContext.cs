using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class MainMenuContext : Context
    {

        private readonly Collection _collection;

        public MainMenuContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, -2.5f, 0), AssetManager.PlayButtonTexture, () => ContextManager.SetState(ContextManager.State.World)));
            _collection.AddEntity(new Button(new Vector3(0, 0, 0), AssetManager.LeaderboardButtonTexture, () => ContextManager.SetState(ContextManager.State.Leaderboard)));
            _collection.AddEntity(new Button(new Vector3(0, 2.5f, 0), AssetManager.SettingsButtonTexture, () => ContextManager.MenuContext.SetState(MenuContext.State.Login)));
            
            // if the user is logged in show userid
            if (SupabaseManager.Client.Auth.CurrentSession != null)
            {
                _collection.AddEntity(new TextDisplay(new Vector3(0, 7.5f, 0), "Logged in as: " + SupabaseManager.Client.Auth.CurrentUser.Id));
            }

            // reset the camera
            Camera.Reset(ScreenManager.GetScaleFactor());
            Camera.CurrentState = Camera.State.Frozen;

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
            TextInputSystem.Update(gameTime);
            
            // update the entity collection
            _collection.Update(gameTime);
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // draw entities
            SpriteSystem.Draw(gameTime, spriteBatch);
            TextSystem.Draw(gameTime, spriteBatch);
        }
    }
}