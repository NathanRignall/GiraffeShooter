using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class LoginContext : Context
    {
        private readonly Collection _collection;
        private readonly TextBaseInput _emailInput;
        private readonly TextBaseInput _passwordInput;

        private bool _loading = false;
        
        public LoginContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, -7.5f, 0), AssetManager.BackButtonTexture, () => ContextManager.MenuContext.SetState(MenuContext.State.MainMenu)));
            _collection.AddEntity(_emailInput = new TextEmailInput(new Vector3(0, -1.5f, 0)));
            _collection.AddEntity(_passwordInput = new TextPasswordInput(new Vector3(0, 1.5f, 0)));
            _collection.AddEntity(new Button(new Vector3(0, 7.5f, 0), AssetManager.LoginButtonTexture, () => { Login(); }));

            // reset the camera
            Camera.Reset(ScreenManager.GetScaleFactor());
            Camera.CurrentState = Camera.State.Frozen;

        }

        public override void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);

            foreach (var e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        
                        if (e.Key == Keys.Enter)
                        {
                            if (!_loading)
                            {
                                Login();
                            }
                        }

                        break;
                }
            }
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
        
        private async Task Login()
        {
            _loading = true;

            try
            {
                // create a new login request
                await SupabaseManager.Client.Auth.SignIn(_emailInput.GetString(), _passwordInput.GetString());

                // stop loading
                _loading = false;
                
                // set the state
                ContextManager.MenuContext.SetState(MenuContext.State.MainMenu);
            }
            catch (Exception e)
            {
                // log the error
                Console.WriteLine(e);
                        
                // reset the text input
                _emailInput.ResetString();
                _passwordInput.ResetString();
                        
                // stop loading
                _loading = false;

            }
            
        }
        
    }
}