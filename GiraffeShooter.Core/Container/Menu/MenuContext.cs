using System;
using System.Collections.Generic;

using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Container.Menu
{
    public class MenuContext : Context
    {
        
        public enum State
        {
            MainMenu,
            Login,
            Register,
            Game
        }

        private State _currentState;
        private Context _currentContext;

        public MenuContext()
        {
            _currentState = State.MainMenu;
            _currentContext = new MainMenuContext();
        }

        public override void HandleEvents(List<Event> events)
        {
            _currentContext.HandleEvents(events);
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _currentContext.Update(gameTime);
        }
        
        public void SetState(State state)
        {
            _currentState = state;
            switch (_currentState)
            {
                case State.MainMenu:
                    _currentContext = new MainMenuContext();
                    break;
                case State.Login:
                    _currentContext = new LoginContext();
                    break;
                case State.Register:
                    _currentContext = new RegisterContext();
                    break;
            }
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _currentContext.Draw(gameTime, spriteBatch);
        }

    }
}