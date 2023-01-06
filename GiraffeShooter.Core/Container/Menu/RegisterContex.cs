using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class RegisterContext
    {

        private readonly Collection _collection;
        private TextInput _emailInput;
        private TextInput _passwordInput;

        private bool _loading = false;
        
        public RegisterContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, 0, 0), "Login"));
            _collection.AddEntity(_emailInput = new TextInput(new Vector3(0, 1f, 0)));
            _collection.AddEntity(_passwordInput = new TextInput(new Vector3(0, 2f, 0)));

            // reset the camera
            Camera.CurrentState = Camera.State.Frozen;
            Camera.Reset(1f);

        }

        public void HandleEvents(List<Event> events)
        {
            _collection.HandleEvents(events);
        }
        
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
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

            // clean up entities
            _collection.CleanUp();
        }
        
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
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

                // set the text input to uid
                _emailInput.String = SupabaseManager.Client.Auth.CurrentUser.Id;
                        
                // stop loading
                _loading = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                        
                // set the text input to empty
                _emailInput.String = "";
                        
                // stop loading
                _loading = false;

            }

            return 1;
        }
        
    }
}