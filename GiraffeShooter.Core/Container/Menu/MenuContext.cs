using System;
using System.Collections.Generic;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class MenuContext
    {
        
        public enum State
        {
            MainMenu,
            Login,
            Register,
            Game
        }
        
        public State CurrentState { get; set; }
        
        public RegisterContext RegisterContext { get; set; }

        public MenuContext()
        {
            CurrentState = State.Register;
            RegisterContext = new RegisterContext();
        }

        public void HandleEvents(List<Event> events)
        {
            switch (CurrentState)
            {
                case State.Register:
                    RegisterContext.HandleEvents(events);
                    break;
            }
        }
        
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            switch (CurrentState)
            {
                case State.Register:
                    RegisterContext.Update(gameTime);
                    break;
            }


            // switch to the game context for test
            if (gameTime.TotalGameTime.TotalSeconds > 60)
            {
                GameContext.SetState(Game.GameContext.State.World);
            }
        }
        
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            switch (CurrentState)
            {
                case State.Register:
                    RegisterContext.Draw(gameTime, spriteBatch);
                    break;
            }

        }

    }
}