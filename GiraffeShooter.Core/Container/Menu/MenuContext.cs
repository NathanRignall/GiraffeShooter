using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Entity;
using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class MenuContext
    {
        
        Collection _collection;
        TextInput _textInput;
        
        public MenuContext()
        {
            // create a new collection
            _collection = new Collection();
            
            // register entities
            _collection.AddEntity(new Button(new Vector3(0, 0, 0), "Login"));
            _collection.AddEntity(_textInput = new TextInput(new Vector3(0, 1f, 0)));
            _collection.AddEntity(new TextInput(new Vector3(0, 2f, 0)));
            
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

            // switch to the game context
            if (gameTime.TotalGameTime.TotalSeconds > 20)
            {
                GameContext.SetState(Game.GameContext.State.World);
            }

            // update camera location
            Camera.FollowTarget = new Vector2(0, 0);

            // update entities
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
        
    }
}