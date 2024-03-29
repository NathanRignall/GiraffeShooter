using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;

using Supabase.Gotrue;

namespace GiraffeShooterClient.Container.Menu
{
    public class RegisterContext : Context
    {
        private readonly Collection _collection;
        private readonly TextInput _emailInput;
        private readonly TextInput _passwordInput;
        private readonly TextInput _usernameInput;

        private bool _loading = false;
        
        public RegisterContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();
            
            // register entities
            _collection.AddEntity(new GiraffeShooterClient.Entity.Button(new Vector2(0, 7.5f), AssetManager.BackButtonTexture, () => ContextManager.MenuContext.SetState(MenuContext.State.Settings)));
            _collection.AddEntity(_emailInput = new TextInput(new Vector2(0, 3), "Email", "Enter Email"));
            _collection.AddEntity(_passwordInput = new TextInput(new Vector2(0, 0), "Password", "Enter Password", true));
            _collection.AddEntity(_usernameInput = new TextInput(new Vector2(0, -3), "Username", "Enter Username"));
            _collection.AddEntity(new GiraffeShooterClient.Entity.Button(new Vector2(0, -7.5f), AssetManager.RegisterButtonTexture, () => { Register(); }));

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
            InputSystem.Update(gameTime);

            // update the entity collection
            _collection.Update(gameTime);
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // draw entities
            SpriteSystem.Draw(gameTime, spriteBatch);
            TextSystem.Draw(gameTime, spriteBatch);
        }
        
        private async Task Register()
        {
            _loading = true;

            try
            {
                // create options dictionary
                var options = new Dictionary<string, object>
                {
                    {"username", _usernameInput.GetString()},
                };

                // with test username
                await SupabaseManager.Client.Auth.SignUp(_emailInput.GetString(), _passwordInput.GetString(), new SignUpOptions { Data = options });

                // stop loading
                _loading = false;
                
                // set the state
                ContextManager.SetState(ContextManager.State.Menu);
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

                throw e;

            }
        }
        
    }
}