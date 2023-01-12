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
        private readonly TextInput _emailInput;
        private readonly TextInput _passwordInput;

        private bool _loading = false;
        
        public LoginContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // clear the base
            Base.Clear();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, -2f, 0), AssetManager.OptionsButtonTexture, () => {}));
            _collection.AddEntity(_emailInput = new TextInput(new Vector3(0, 3f, 0)));
            _collection.AddEntity(_passwordInput = new TextInput(new Vector3(0, 4f, 0)));

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
            // send email
            if (InputManager.IsKeyDown(Keys.Enter) && !_loading)
            {
                Login();
            }

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
        
        private async Task<int> Login()
        {
            _loading = true;

            try
            {
                await SupabaseManager.Client.Auth.SignIn(_emailInput.String, _passwordInput.String);

                // stop loading
                _loading = false;
                
                // set the state
                ContextManager.SetState(ContextManager.State.World);
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

            return 1;
        }
        
    }
}